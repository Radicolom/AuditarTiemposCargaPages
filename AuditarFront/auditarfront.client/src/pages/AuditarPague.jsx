import React, { useState, useEffect } from 'react';
import { Plus, Edit, Trash2 } from 'lucide-react';
import { obtenerPaguesAuditar, analyzePaguesAuditar } from '../llamadasApi/PagesAuditar'; 
import ModalVacia from '../components/generico/ModalVacia';
import Title from '../components/layout/Title';
import PageAuditarForm from '../components/Forms/PageAuditarForm';
import Swal from 'sweetalert2';

const AuditarPague = () => {

    const handleSubmit = async (e) => {
        e.preventDefault();
        const result = await analyzePaguesAuditar(formData);
        if (result.success) {
            Swal.fire('Éxito', 'Página auditada correctamente', 'success');
            onCancel();
            const data = await obtenerPaguesAuditar();
            setPages(data);
        } else {
            Swal.fire('Error', 'Hubo un problema al auditar la página', 'error');
        }
    }
    
    return (
        <div className="card p-4 border-0 flex-grow-1" style={{ background: '#f9f9f9' }}>
            {/* Cabecera de la pagina */}
            <Title 
                icon={<Edit size={32} color="#fff" />} 
                title="Auditar Páginas"
                subtitle="En esta sección puedes auditar las páginas."
            />

            <form onSubmit={handleSubmit} className="card shadow p-4 border-0 h-100 w-100" style={{ maxWidth: 420 }}>
                <h2 className="h5 mb-4 fw-bold text-primary">Selecciona la página a auditar</h2>
                <div className="mb-3">
                    <Selectores
                        fetchOptions={async () => obtenerPaguesAuditar({
                            estado: true,
                        })}
                        valueKey="value"
                        labelKey="label"
                        name="estado"
                        onChange={handleChange}
                        value={formData.estado}
                        required
                    />
                </div>
                <div className="d-flex justify-content-end gap-2">
                    <button type="submit" className="btn btn-primary">Auditar</button>
                </div>
            </form>

        </div>
    );
};

export default AuditarPague;

