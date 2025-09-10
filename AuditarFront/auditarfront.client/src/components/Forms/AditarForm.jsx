import React, { useState, useEffect } from 'react';
import { Plus, Edit, Trash2 } from 'lucide-react';
import { obtenerPaguesAuditar, analyzePaguesAuditar } from '../llamadasApi/PagesAuditar'; 
import { Selectores } from '../components/generico/Selectores';
import ModalVacia from '../components/generico/ModalVacia';
import Title from '../components/layout/Title';
import PageAuditarForm from '../components/Forms/PageAuditarForm';
import Swal from 'sweetalert2';


const AuditarForm = ({ onSubmit, onCancel, formData, handleChange, pagesAuditar, isLoading }) => {
const [pagesAuditar, setPagesAuditar] = useState([]);
    const [isLoading, setIsLoading] = useState(false);

    const [formData, setFormData] = React.useState({
        url: null,
        id: null,
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    useEffect(() => {
        const fetchData = async () => {
            const data = await obtenerPaguesAuditar();
            setPagesAuditar(data);
        };
        fetchData();
    }, []);

    const AuditarPagina = async (e) => {
        e.preventDefault();
        if (formData.id) { 
            setIsLoading(true);
            const result = await analyzePaguesAuditar(formData);
            if (result.success) {
                Swal.fire('Éxito', 'Página auditada correctamente', 'success');
                onCancel();
                const data = await obtenerPaguesAuditar();
                setPages(data);
            } else {
                Swal.fire('Error', 'Hubo un problema al auditar la página', 'error');
            }
            setIsLoading(false);
        }
    }
    
    // Si solo hay un registro, seleccionarlo automáticamente
    useEffect(() => {
        if (pagesAuditar.length === 1) {
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
                    fetchOptions={async () => pagesAuditar.map(page => ({ value: page.id, label: page.url }))}
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

