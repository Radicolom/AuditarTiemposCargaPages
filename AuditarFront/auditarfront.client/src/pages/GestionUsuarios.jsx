import React, { useState, useEffect } from 'react';
import { Plus, Edit, Trash2 } from 'lucide-react';
import UserForm from './UserForm'; 
import ModalVacia from '../generico/ModalVacia';

const GestionUsuarios = () => {
    // --- ESTADOS ---
    const [users, setUsers] = useState([
        {
            id: 1,
            nombre: 'Admin',
            apellido: 'Principal',
            documento: '123456789',
            correo: 'admin@inspectia.com',
            rolId: 1,
            rolNombre: 'Admin'
        }
    ]);
    const [isFormVisible, setIsFormVisible] = useState(false);

    // TODO: Cargar usuarios desde la API con useEffect

    const roles = { 1: 'Admin', 2: 'Supervisor', 3: 'Operador' };

    const handleToggleForm = () => {
        setIsFormVisible(!isFormVisible);
    };

    const handleSaveUser = (newUser) => {
        setUsers([...users, { ...newUser, id: Date.now(), rolNombre: roles[newUser.rolId] }]);
        setIsFormVisible(false);
    };
    

    return (
        <div className="card container p-4 border-0 flex-grow-1" style={{ background: '#f9f9f9' }}>
            {/* Cabecera de la pagina */}
            <div className="flex flex-col sm:flex-row justify-between items-center mb-8 gap-4">
                <div>
                    <h1 className="text-3xl font-extrabold text-gray-800 tracking-tight mb-1 flex items-center gap-2">
                        <span className="inline-block bg-blue-100 text-blue-700 px-3 py-1 rounded-lg text-lg font-semibold">Gestión de Usuarios</span>
                    </h1>
                    <p className="text-gray-500 text-sm mt-1">
                        Administra los usuarios del sistema, crea, edita o elimina cuentas según sea necesario.
                    </p>
                </div>
                {/* Botón Bootstrap */}
                <button
                    type="button"
                    className="btn btn-primary d-flex align-items-center"
                    onClick={handleToggleForm}
                >
                    <Plus size={20} className="me-2" />
                    Nuevo Usuario
                </button>
            </div>

            <div className="bg-white shadow-xl rounded-2xl overflow-x-auto border border-gray-100">
                <table className="min-w-full divide-y divide-gray-200">
                    <thead className="bg-gradient-to-r from-gray-100 to-gray-200">
                        <tr>
                            <th className="py-4 px-6 text-left text-xs font-bold text-gray-600 uppercase tracking-wider">Nombre Completo</th>
                            <th className="py-4 px-6 text-left text-xs font-bold text-gray-600 uppercase tracking-wider">Documento</th>
                            <th className="py-4 px-6 text-left text-xs font-bold text-gray-600 uppercase tracking-wider">Correo</th>
                            <th className="py-4 px-6 text-center text-xs font-bold text-gray-600 uppercase tracking-wider">Rol</th>
                            <th className="py-4 px-6 text-center text-xs font-bold text-gray-600 uppercase tracking-wider">Acciones</th>
                        </tr>
                    </thead>
                    <tbody className="divide-y divide-gray-100">
                         {users.map(user => (
                            <tr key={user.id} className="border-b border-gray-200 hover:bg-gray-100">
                                <td className="py-3 px-6 text-left whitespace-nowrap">{user.nombre} {user.apellido}</td>
                                <td className="py-3 px-6 text-left">{user.documento}</td>
                                <td className="py-3 px-6 text-left">{user.correo}</td>
                                <td className="py-3 px-6 text-center">
                                    <span className={`px-2 py-1 inline-flex text-xs leading-5 font-semibold rounded-full ${user.rolId === 1 ? 'bg-red-100 text-red-800' : 'bg-green-100 text-green-800'}`}>
                                        {user.rolNombre}
                                    </span>
                                </td>
                                <td className="py-3 px-6 text-center">
                                    <div className="flex item-center justify-center space-x-4">
                                        <button className="w-6 h-6 text-blue-600 hover:text-blue-900"><Edit size={18} /></button>
                                        <button className="w-6 h-6 text-red-600 hover:text-red-900"><Trash2 size={18} /></button>
                                    </div>
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

