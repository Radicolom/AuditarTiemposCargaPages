import React from 'react';
import Swal from 'sweetalert2';
import { Selectores } from '../generico/Selectores';
import { ESTADOS } from '../../utils/genericos';  
import { insertarPaguesAuditar, modificarPaguesAuditar } from '../../llamadasApi/PagesAuditar';  

//onSave => recargar tabla
//onCancel => cerrar modal
//modo => crear o editar
const PageAuditarForm = ({ onSave, onCancel, initialData }) => {

    const handleSave = async (formData) => {
        try {
            let result = null;
            if (initialData) {
                result = await modificarPaguesAuditar(formData);
            } else {
                result = await insertarPaguesAuditar(formData);
            }
            if (result.length > 0) {
                onSave(result[0]);
            }
        } catch (e) {
            Swal.fire({
                icon: 'error',
                title: 'Error al crear pagina',
                text: 'Ha ocurrido un error al registrar la pagina: ' + (e.message || '')
            });
        }
    };

    const [formData, setFormData] = React.useState(
        initialData || {
            nombre: '',
            url: '',
            estado: '',
        }
    );


    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({
            ...formData,
            [name]: name === "estado" ? value === "true" : value
        });
    }

    const handleSubmit = async (e) => {
        e.preventDefault();
        await handleSave(formData);
    };

    return (
        <form onSubmit={handleSubmit} className="card shadow p-4 border-0 h-100 w-100" style={{ maxWidth: 420 }}>
            <h2 className="h5 mb-4 fw-bold text-primary">
                {initialData ? "Editar Página" : "Crear Página"}
            </h2>
            <div className="mb-3">
                <input
                    type="text"
                    name="nombre"
                    value={formData.nombre}
                    onChange={handleChange}
                    className="form-control form-control-lg mb-2"
                    placeholder="Nombre"
                    required
                />
                <input
                    type="text"
                    name="url"
                    value={formData.url}
                    onChange={handleChange}
                    className="form-control form-control-lg mb-2"
                    placeholder="Url"
                    required
                />
                <Selectores
                    fetchOptions={async () => ESTADOS}
                    valueKey="value"
                    labelKey="label"
                    name="estado"
                    onChange={handleChange}
                    value={formData.estado}
                    required
                />
            </div>
            <div className="d-flex justify-content-end gap-2">
                <button type="button" className="btn btn-secondary" onClick={onCancel}>Cancelar</button>
                <button type="submit" className="btn btn-primary">Guardar</button>
            </div>
        </form>
    );
};

export default PageAuditarForm;
