import React, { useState, useEffect } from 'react';
import { Plus, Edit, Trash2 } from 'lucide-react';
import { obtenerPaguesAuditar, obtenerAuditarLog } from '../llamadasApi/PagesAuditar';
import { Selectores } from '../components/generico/Selectores';
import { formatearFecha } from '../utils/genericos'; 
import AuditarForm from '../components/Forms/AditarForm';
import Title from '../components/layout/Title';
import ModalVacia from '../components/generico/ModalVacia';
import Swal from 'sweetalert2';
import TablaGenerica from '../components/generico/TablaGenerica';
import GraficaAuditarForm from '../components/Forms/GraficaAuditarForm';

const AuditarPague = () => {
    const [pagesAuditar, setPagesAuditar] = useState([]);
    const [isLoading, setIsLoading] = useState(false);
    const [pages, setPages] = useState([]);
    const [isFormVisibleAuditar, setIsFormVisibleAuditar] = useState(false);
    const [isFormVisibleGrafica, setIsFormVisibleGrafica] = useState(false);
    const [formData, setFormData] = useState({
        AuditarPaginaId: null,
        EstadoAuditarPagina: true,
    });

    const columnasGrilla = 
    [
        { header: 'Nombre pagina', key: 'nombre' },
        { header: 'Url', key: 'url' },
        { header: 'Fecha de Auditoria', key: 'fechaCreacion', render: (row) => formatearFecha(row.fechaCreacion) },
        { header: 'Performance Score', key: 'performanceScore' },
        { header: 'Time To First Byte (ms)', key: 'timeToFirstByteMs' },
        { header: 'Dom Processing Time (ms)', key: 'domProcessingTimeMs' },
        { header: 'Page Load Time (ms)', key: 'pageLoadTimeMs' },
        { header: 'FCP Value (ms)', key: 'fcpValue' },
        { header: 'LCP Value (ms)', key: 'lcpValue' },
        { header: 'CLS Value', key: 'clsValue' },
        { header: 'Speed Index Value (ms)', key: 'speedIndexValue' },
        { header: 'Usuario', key: 'usuarioCreacion' },
    ]

    const handleChange = (e) => {
        const { name, value } = e.target;

        if (!isNaN(parseInt(value, 10))) {
            setFormData(prev => ({
                ...prev,
                AuditarPaginaId: parseInt(value, 10),
            }));
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'El valor seleccionado no es válido.',
            });
        }
    };
    
    const AuditarPagina = async (e) => {
        if (e && e.preventDefault) {
            e.preventDefault();
        }
        if (formData.AuditarPaginaId) { 
            setIsLoading(true);
            const result = await obtenerAuditarLog(formData);
            setPages(result);
            setIsLoading(false);
        }
    };

    useEffect(() => {
        const fetchData = async () => {
            setIsLoading(true);
            const data = await obtenerPaguesAuditar(
                {
                    estado: true
                }
            );
            setPagesAuditar(data);
            setIsLoading(false);
        };
        fetchData();
    }, []);

    useEffect(() => {
        if (pagesAuditar.length > 0 && formData.AuditarPaginaId === null) {
            setFormData({
                ...formData,
                AuditarPaginaId: pagesAuditar[0].id,
                EstadoAuditarPagina: true,
            });
        }
    }, [pagesAuditar]);

    return (
        <div className="card p-4 border-0 flex-grow-1" style={{ background: '#f9f9f9' }}>
            <form onSubmit={AuditarPagina}>
                <Title 
                    icon={<Edit size={32} color="#fff" />} 
                    title="Auditar Páginas"
                    subtitle="En esta sección puedes auditar las páginas."
                    actions={[
                        {
                            onClick: () => setIsFormVisibleAuditar(true),
                            text: "Nueva Auditoría",
                            icon: <Plus size={20} className="me-2" />,
                            type: "button"
                        },{
                            onClick: () => setIsFormVisibleGrafica(true),
                            text: "Ver Graficas",
                            icon: <svg width="20" height="20" className="me-2" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round"><path d="M3 3v18h18"/><rect x="7" y="12" width="3" height="6"/><rect x="12" y="8" width="3" height="10"/><rect x="17" y="4" width="3" height="14"/></svg>,
                            type: "button"
                        }
                    ]}
                />

                <div className="row g-3 align-items-end mt-3">
                    <div className="col-12 col-md-12">
                        <div className="input-group input-group-lg">
                            <Selectores
                                fetchOptions={async () => pagesAuditar.map(page => ({ value: page.id, label: page.nombre }))}
                                valueKey="value"
                                labelKey="label"
                                name="AuditarPaginaId"
                                onChange={handleChange}
                                value={formData.AuditarPaginaId}
                                required
                                className="form-select"
                                style={{ borderTopRightRadius: 0, borderBottomRightRadius: 0 }}
                            />
                            <button
                                type="submit"
                                className="btn btn-primary"
                                disabled={isLoading || !formData.AuditarPaginaId}
                                style={{ borderTopLeftRadius: 0, borderBottomLeftRadius: 0 }}
                            >
                                {isLoading ? 'Consultando...' : 'Consultar Página'}
                            </button>
                        </div>
                    </div>
                </div>
            </form>
            <div className="bg-white shadow-xl rounded-4 overflow-x-auto border border-gray-100 mt-4">
                <TablaGenerica 
                    columns={columnasGrilla}
                    data={pages}
                    isLoading={isLoading}
                    noDataMessage="No hay datos para mostrar."
                    pageSize={10}
                />
            </div>

            {(isFormVisibleAuditar || isFormVisibleGrafica) && (
                <ModalVacia
                    show={isFormVisibleAuditar || isFormVisibleGrafica}
                    onClose={() => {
                        setIsFormVisibleAuditar(false);
                        setIsFormVisibleGrafica(false);
                    }}
                    style={ !isFormVisibleGrafica ? { 
                        width: '100%',
                        maxWidth: '500px', 
                        height: 'auto',
                        maxHeight: '90vh',
                        overflowY: 'auto'
                    } : {
                        width: '100%',
                        maxWidth: '830px', 
                        height: 'auto',
                        maxHeight: '90vh',
                        overflowY: 'visible'
                    }}
                >
                    {isFormVisibleGrafica ? (
                        <GraficaAuditarForm 
                            pagesAuditar={pagesAuditar}
                        />
                    ) : (
                        <AuditarForm
                            onSubmit={AuditarPagina}
                            onCancel={() => setIsFormVisibleAuditar(false)}
                            pagesAuditar={pagesAuditar}
                        />
                    )}
                </ModalVacia>
            )}
        </div>
    );
};

export default AuditarPague;

