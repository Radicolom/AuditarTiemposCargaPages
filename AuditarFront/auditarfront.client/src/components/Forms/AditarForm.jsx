import React, { useState, useEffect } from 'react';
import { obtenerPaguesAuditar, analyzePaguesAuditar } from '../../llamadasApi/PagesAuditar'; 
import { Selectores } from '../generico/Selectores';
import Swal from 'sweetalert2';


const AuditarForm = ({ onSubmit, onCancel, pagesAuditar }) => {
    const [isLoading, setIsLoading] = useState(false);
    
    const [formData, setFormData] = React.useState({
        url: null,
        id: null,
    });

    useEffect(() => {
        const fetchData = async () => {
            setIsLoading(true);
            const data = await obtenerPaguesAuditar(
                {
                    estado: true
                }
            );
            setIsLoading(false);
        };
        fetchData();
    }, []);
    

    const handleChange = (e) => {
        const { name, value } = e.target;
        if (!isNaN(parseInt(value, 10))) {
            setFormData(prev => ({
                ...prev,
                id: parseInt(value, 10),
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
        if (formData.id) { 
            setIsLoading(true);
            const result = await analyzePaguesAuditar(formData);
            if (result.length > 0) {
                Swal.fire('Éxito', 'Página auditada correctamente', 'success');
                onSubmit();
            } else {
                Swal.fire('Error', 'Hubo un problema al auditar la página', 'error');
            }
            setIsLoading(false);
        }
    }
    
    // Si solo hay un registro, seleccionarlo automáticamente
    useEffect(() => {
        if (pagesAuditar.length > 0 && formData.id === null) {
            setFormData({
                ...formData,
                id: pagesAuditar[0].id,
                url: pagesAuditar[0].url,
            });
        }
    }, [pagesAuditar]);


    return (
        <form className="card shadow p-4 border-0 h-100 w-100" style={{ maxWidth: 420 }} onSubmit={AuditarPagina}>
            <h2 className="h5 mb-4 fw-bold text-primary">Selecciona la página a auditar</h2>
            <div className="mb-3">
                <Selectores
                    fetchOptions={async () => pagesAuditar.map(page => ({ value: page.id, label: page.nombre }))}
                    valueKey="value"
                    labelKey="label"
                    name="id"
                    onChange={handleChange}
                    value={formData.id}
                    required
                />
            </div>
            <div className="d-flex justify-content-end gap-2">
                <button type="submit" className="btn btn-primary"
                    disabled={isLoading} 
                >
                    {isLoading ? 'Auditando...' : 'Auditar'}
                </button>
            </div>
        </form>
    );

};

export default AuditarForm;
