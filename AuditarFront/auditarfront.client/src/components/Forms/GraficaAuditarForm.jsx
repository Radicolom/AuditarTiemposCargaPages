import React, { useState, useEffect } from 'react';
import { Pie, Bar } from 'react-chartjs-2';
import { Chart, ArcElement, Tooltip, Legend, Title, BarElement, CategoryScale, LinearScale } from 'chart.js';
import { obtenerAuditarLog } from '../../llamadasApi/PagesAuditar';
import { Selectores } from '../generico/Selectores';

Chart.register(ArcElement, Tooltip, Legend, Title, BarElement, CategoryScale, LinearScale);

const metricas = [
    { key: "performanceScore", label: "Performance Score" },
    { key: "timeToFirstByteMs", label: "TTFB (ms)" },
    { key: "domProcessingTimeMs", label: "DOM Processing (ms)" },
    { key: "pageLoadTimeMs", label: "Page Load (ms)" },
    { key: "fcpValue", label: "FCP (s)" },
    { key: "lcpValue", label: "LCP (s)" },
    { key: "clsValue", label: "CLS" },
    { key: "speedIndexValue", label: "Speed Index (s)" }
];

function parseMetricValue(key, value) {
    if (typeof value === "string" && value.endsWith("s")) {
        return parseFloat(value.replace("s", "").replace(",", "."));
    }
    if (typeof value === "string") {
        return parseFloat(value.replace(",", "."));
    }
    return value;
}

const GraficaAuditarForm = ({ pagesAuditar = [] }) => {
    const [graficaData, setGraficaData] = useState([]);
    const [selectedId, setSelectedId] = useState('');
    const [formData, setFormData] = useState({ AuditarPaginaId: '' });

    useEffect(() => {
        const fetchData = async () => {
        const data = await obtenerAuditarLog();
            setGraficaData(data || []);
        };
        fetchData();
    }, []);

    // Manejar cambio del selector
    const handleChange = (e) => {
        const value = e.target.value;
        setSelectedId(value);
        setFormData({ AuditarPaginaId: value });
    };

    // --- Pie Chart Data (por página) ---
    const conteoPorNombre = graficaData.reduce((acc, item) => {
        acc[item.nombre] = (acc[item.nombre] || 0) + 1;
        return acc;
    }, {});
    const pieLabels = Object.keys(conteoPorNombre);
    const pieValues = Object.values(conteoPorNombre);
    const pieColors = pieLabels.map((_, idx) => `hsl(${(idx * 60) % 360}, 70%, 60%)`);
    const pieData = {
        labels: pieLabels,
        datasets: [
            {
                label: 'Cantidad de auditorías',
                data: pieValues,
                backgroundColor: pieColors,
                borderColor: '#fff',
                borderWidth: 2,
            },
        ],
    };
    const pieOptions = {
        responsive: true,
        plugins: {
            legend: { position: 'top' },
            title: { display: true, text: 'Cantidad de Auditorías por Página' },
            tooltip: {
                callbacks: {
                    label: function(context) {
                        const realValue = rawData[context.dataIndex];
                        return `${context.dataset.label}: ${realValue}`;
                    }
                }
            }
        },
    };

  // --- Bar Chart Data (por registro seleccionado) ---
    const dataFiltrada = selectedId
        ? graficaData.filter(item => String(item.auditarPaginaId) === String(selectedId))
        : [];

    const ultimoRegistro = dataFiltrada.length > 0 ? dataFiltrada[dataFiltrada.length - 1] : null;

    const rawData = metricas.map(m => parseMetricValue(m.key, ultimoRegistro ? ultimoRegistro[m.key] : 0));
    const max = Math.max(...rawData, 1);
    const normalizedData = rawData.map(v => (v / max) * 100);

  const barData = {
    labels: metricas.map(m => m.label),
    datasets: [
      {
        label: ultimoRegistro ? ultimoRegistro.nombre : "Sin datos",
        data: metricas.map(m => parseMetricValue(m.key, ultimoRegistro ? ultimoRegistro[m.key] : 0)),
        backgroundColor: 'rgba(37, 99, 235, 0.7)',
        borderColor: 'rgba(37, 99, 235, 1)',
        borderWidth: 1,
      }
    ]
  };
  const barOptions = {
    responsive: true,
    plugins: {
        legend: { display: false },
        title: { display: true, text: 'Métricas de la Página Seleccionada' },
        tooltip: {
                callbacks: {
                    label: function(context) {
                        return `${context.dataset.label}: ${context.parsed.y}`;
                    }
                }
        }
    },
    scales: {
        y: { beginAtZero: true }
    }
  };

  return (
    <div>
        <h2 className="mb-4">Gráfica de Auditoría</h2>
            <div className="mb-3" style={{ maxWidth: '100%' }}>
                <Selectores
                fetchOptions={async () => pagesAuditar.map(page => ({ value: page.id, label: page.nombre }))}
                valueKey="value"
                labelKey="label"
                name="AuditarPaginaId"
                onChange={handleChange}
                value={formData.AuditarPaginaId}
                className="form-select"
                />
            </div>
            <div style={{ height: '390px', width: '100%', display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
            {!selectedId ? 
                (
                    pieValues.length === 0 ? (
                    <p>No hay datos para mostrar.</p>
                    ) : (
                        <Pie data={pieData} options={pieOptions} height={80} />
                    )
                ) : !ultimoRegistro ? (
                    <p>No hay datos para mostrar.</p>
                ) : (
                    <Bar data={barData} options={barOptions} height={80} />
                )}
            </div>
    </div>
  );
};

export default GraficaAuditarForm;