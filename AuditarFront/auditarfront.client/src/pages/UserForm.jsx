import React from 'react';
import { SelectorGenerico } from '../generico/Selectores';
import { obtenerRoles } from '../consultas/roles';

<SelectorGenerico fetchOptions={obtenerRoles} valueKey="id" labelKey="nombre" />


const UserForm = ({ onSave, onCancel, modo = "crear" }) => {
    const [formData, setFormData] = React.useState({
        nombre: '',
        apellido: '',
        documento: '',
        correo: '',
        rolId: ''
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        onSave(formData);
    };
    

    return (
        <form onSubmit={handleSubmit} className="card shadow p-4 border-0 h-100 w-100" style={{ maxWidth: 420 }}>
            <h2 className="h5 mb-4 fw-bold text-primary">
                {modo === "editar" ? "Editar Usuario" : "Crear Usuario"}
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
                <SelectorGenerico
                    fetchOptions={obtenerRoles}
                    valueKey="id"
                    labelKey="nombre"
                    name="rolId"
                    onChange={handleChange}
                    value={formData.rolId}
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
