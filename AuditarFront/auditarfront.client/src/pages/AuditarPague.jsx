import React, { useState, useEffect } from 'react';
import { Plus, Edit, Trash2 } from 'lucide-react';
import { obtenerPaguesAuditar, obtenerAuditarLog } from '../llamadasApi/PagesAuditar';
import { Selectores } from '../components/generico/Selectores';
import { buscarEstado,formatearFecha } from '../utils/genericos';  
import Title from '../components/layout/Title';

const AuditarPague = () => {
    const [pagesAuditar, setPagesAuditar] = useState([]);
    const [isLoading, setIsLoading] = useState(false);
    const [pages, setPages] = useState([]);

    const [formData, setFormData] = useState({
        AuditarPaginaId: null,
        EstadoAuditarPagina: true,
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        formData.AuditarPaginaId = value;
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

    const AuditarPagina = async (e) => {
        e.preventDefault();
        if (formData.AuditarPaginaId) { 
            setIsLoading(true);
            const result = await obtenerAuditarLog(formData);
            setPages(result);
            setIsLoading(false);
        }
    };

    // Si solo hay un registro, seleccionarlo automáticamente
    useEffect(() => {
        if (pagesAuditar.length === 1) {
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
                {/* Cabecera de la pagina */}
                <Title 
                    icon={<Edit size={32} color="#fff" />} 
                    title="Auditar Páginas"
                    subtitle="En esta sección puedes auditar las páginas."
                    actions ={[]}
                />

                <div className="mt-4">
                    <Selectores
                        fetchOptions={async () => pagesAuditar.map(page => ({ value: page.id, label: page.url }))}
                        valueKey="value"
                        labelKey="label"
                        name="id"
                        onChange={handleChange}
                        value={formData.id}
                        required
                    />
                    <button type="submit" className="btn btn-primary w-100 mt-3" disabled={isLoading}>
                        {isLoading ? 'Consultando...' : 'Consultar Página'}
                    </button>
                </div>
            </form>
            <table className="table table-hover align-middle table-bordered mb-0 rounded-4 overflow-hidden">
                <thead className="tabla-app-thead">
                    <tr>
                        <th>Nombre pagina</th>
                        <th>Url</th>
                        <th>Fecha de Auditoria</th>
                        {/* <th>Fecha de modificación</th> */}
                        <th>Performance Score</th>
                        <th>Time To First Byte (ms)</th>
                        <th>Dom Processing Time (ms)</th>
                        <th>Page Load Time (ms)</th>
                        <th>FCP Value (ms)</th>
                        <th>LCP Value (ms)</th>
                        <th>CLS Value</th>
                        <th>Speed Index Value (ms)</th>
                        <th>Usuario</th>
                        
                    </tr>
                </thead>
                <tbody>
                    {pages.map(page => (
                        <tr key={page.id}>
                            <td>{page.nombre}</td>
                            <td>{page.url}</td>
                            {/* <td>{buscarEstado(page.EstadoAuditarPagina)}</td> */}
                            <td>{formatearFecha(page.fechaCreacion)}</td>
                            {/* <td>{formatearFecha(page.fechaModificacion)}</td> */}
                            <td>{page.performanceScore}</td>
                            <td>{page.timeToFirstByteMs}</td>
                            <td>{page.domProcessingTimeMs}</td>
                            <td>{page.pageLoadTimeMs}</td>
                            <td>{page.fcpValue}</td>
                            <td>{page.lcpValue}</td>
                            <td>{page.clsValue}</td>
                            <td>{page.speedIndexValue}</td>
                            <td>{page.usuarioCreacion}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
            
        </div>
    );
};

export default AuditarPague;

