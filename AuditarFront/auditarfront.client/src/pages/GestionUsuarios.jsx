import React, { useState, useEffect } from 'react';
import { Plus, Edit, Trash2 } from 'lucide-react';
import UserForm from './UserForm'; 
import ModalVacia from '../generico/ModalVacia';
import { obtenerUsuarios } from '../consultas/users';

const GestionUsuarios = () => {
    const [users, setUsers] = useState([]);
    const [isFormVisible, setIsFormVisible] = useState(false);

    useEffect(() => {
        const fetchUsers = async () => {
            const data = await obtenerUsuarios();
            setUsers(data || []);
        };
        fetchUsers();
    }, []);


    const roles = { 1: 'Admin', 2: 'Supervisor', 3: 'Operador' };

    const handleToggleForm = () => {
        setIsFormVisible(!isFormVisible);
    };

    const handleSaveUser = (newUser) => {
        setUsers([...users, { ...newUser, id: Date.now(), rolNombre: roles[newUser.rolId] }]);
        setIsFormVisible(false);
    };
    

    return (
        <div className="card p-4 border-0 flex-grow-1" style={{ background: '#f9f9f9' }}>
            {/* Cabecera de la pagina */}
            <div className="rounded-4 mb-4 p-4 d-flex flex-column flex-md-row align-items-center justify-content-between gap-3"
                style={{
                    background: 'linear-gradient(90deg, #2563eb 0%, #1e40af 100%)',
                    boxShadow: '0 4px 24px 0 rgba(30,64,175,0.10)',
                }}>
                <div className="d-flex flex-column flex-md-row align-items-center gap-3">
                    <div className="d-flex align-items-center justify-content-center rounded-circle bg-white bg-opacity-25" style={{width: 56, height: 56}}>
                        <Plus size={32} color="#fff" />
                    </div>
                    <div>
                        <h1 className="mb-1 fw-bold text-white" style={{fontSize: '2.2rem', letterSpacing: '-1px'}}>
                            Gestión de Usuarios
                        </h1>
                        <p className="mb-0 text-white-50" style={{fontWeight: 400}}>
                            Administra los usuarios del sistema, crea, edita o elimina cuentas según sea necesario.
                        </p>
                    </div>
                </div>
                <button
                    type="button"
                    className="btn btn-light d-flex align-items-center px-4 py-2 fw-bold shadow-sm"
                    style={{fontSize: '1.1rem'}}
                    onClick={handleToggleForm}
                >
                    <Plus size={20} className="me-2" />
                    Nuevo Usuario
                </button>
            </div>

            <div className="bg-white shadow-xl rounded-4 overflow-x-auto border border-gray-100">
                <table className="table table-hover align-middle table-bordered mb-0 rounded-4 overflow-hidden">
                    <thead className="tabla-app-thead">
                        <tr>
                            <th>Nombre Completo</th>
                            <th>Documento</th>
                            <th>Correo</th>
                            <th className="text-center">Rol</th>
                            <th className="text-center">Acciones</th>
                        </tr>
                    </thead>
                    <tbody>
                        {users.map(user => (
                            <tr key={user.id}>
                                <td>{user.nombre} {user.apellido}</td>
                                <td>{user.documento}</td>
                                <td>{user.correo}</td>
                                <td className="text-center">
                                    <span className={`badge ${user.rolId === 1 ? 'bg-danger' : 'bg-success'}`}>
                                        {user.rolNombre}
                                    </span>
                                </td>
                                <td className="text-center">
                                    <button className="btn btn-sm btn-outline-primary me-2"><Edit size={18} /></button>
                                    <button className="btn btn-sm btn-outline-danger"><Trash2 size={18} /></button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>

            {isFormVisible && (
                <ModalVacia
                    show={isFormVisible}
                    onClose={() => setIsFormVisible(false)}
                    style={{
                        width: '100%',
                        maxWidth: '500px', 
                        height: 'auto',
                        maxHeight: '90vh',
                        overflowY: 'auto'
                    }}
                >
                    <UserForm
                        onSave={handleSaveUser}
                        onCancel={() => setIsFormVisible(false)}
                        modo="crear"
                    />
                </ModalVacia>
            )}

        </div>
    );
};

export default GestionUsuarios;

