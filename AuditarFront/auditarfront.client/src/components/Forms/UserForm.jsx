import React from 'react';
import { Selectores } from '../generico/Selectores';
import { obtenerRoles } from '../../llamadasApi/roles';
import { insertarUsuario, modificarUsuario } from '../../llamadasApi/users';
import Swal from 'sweetalert2';

<Selectores fetchOptions={obtenerRoles} valueKey="id" labelKey="nombre" />

const UserForm = ({ onSave, onCancel, initialData }) => {

    const handleSave = async (formData) => {
        try {
            let result = null;
            if (initialData) {
                result = await insertarUsuario(formData);
            } else {
                result = await modificarUsuario(formData);
            }
            if (result) {
                onSave(result[0]);
            }
        } catch (e) {
            Swal.fire({
                icon: 'error',
                title: 'Error al crear usuario',
                text: 'Ha ocurrido un error al registrar el usuario'
            });
        }
    };

    const [formData, setFormData] = React.useState(
        initialData || {
            nombre: '',
            apellido: '',
            documento: '',
            correo: '',
            telefono: '',
            rolId: ''
        }
    );

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        await handleSave(formData);
    };
    

    return (
        <form onSubmit={handleSubmit} className="card shadow p-4 border-0 h-100 w-100" style={{ maxWidth: 420 }}>
            <h2 className="h5 mb-4 fw-bold text-primary">
                {initialData ? "Editar Usuario" : "Crear Usuario"}
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
                    name="apellido"
                    value={formData.apellido}
                    onChange={handleChange}
                    className="form-control form-control-lg mb-2"
                    placeholder="Apellido"
                    required
                />
                <input
                    type="text"
                    name="documento"
                    value={formData.documento}
                    onChange={handleChange}
                    className="form-control form-control-lg mb-2"
                    placeholder="Documento"
                    required
                />
                <input
                    type="email"
                    name="correo"
                    value={formData.correo}
                    onChange={handleChange}
                    className="form-control form-control-lg mb-2"
                    placeholder="Correo"
                    required
                />
                <input
                    type="tel"
                    name="telefono"
                    value={formData.telefono}
                    onChange={handleChange}
                    className="form-control form-control-lg mb-2"
                    placeholder="Teléfono"
                    required
                />
                <Selectores
                    fetchOptions={obtenerRoles}
                    valueKey="id"
                    labelKey="nombre"
                    name="rolId"
                    onChange={handleChange}
                    value={formData.rolId}
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

export default UserForm;
