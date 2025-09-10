import React, { useState, useEffect } from 'react';
import { Plus, Edit, Trash2 } from 'lucide-react';
import UserForm from '../components/Forms/UserForm'; 
import ModalVacia from '../components/generico/ModalVacia';
import { obtenerUsuarios } from '../llamadasApi/users';
import Title from '../components/layout/Title';

const GestionUsuarios = () => {
    const [users, setUsers] = useState([]);
    const [isFormVisible, setIsFormVisible] = useState(false);
    const [selectedUser, setSelectedUser] = useState(null);

    useEffect(() => {
        const fetchUsers = async () => {
            const data = await obtenerUsuarios();
            setUsers(data || []);
        };
        fetchUsers();
    }, []);

    const handleToggleForm = (tipo, user = null) => {
        switch (tipo) {
            case "editar":
                setIsFormVisible(!isFormVisible);  
                setSelectedUser(user);
                break;
            case "crear":
                setSelectedUser(null);
                break;
            default:
                setIsFormVisible(!isFormVisible);  
                setSelectedUser(null);
                break;
        }
    };

    const handleSaveUser = (newUser) => {
        let userExists = false;
        if (newUser.id) {
            userExists = users.some(u => u.id === newUser.id);
        }
        if (userExists) {
            setUsers(users.map(u => (u.id === newUser.id ? newUser : u)));
        } else {
            setUsers([...users, { ...newUser, id: Date.now() }]);
        }
        setIsFormVisible(false);
    };  
    

    return (
        <div className="card p-4 border-0 flex-grow-1" style={{ background: '#f9f9f9' }}>
            {/* Cabecera de la pagina */}
            <Title 
                icon={<Plus size={32} color="#fff" />}
                title="Gestión de Usuarios"
                subtitle="Administra los usuarios del sistema, crea, edita o elimina cuentas según sea necesario."
                actions={[
                    {
                    onClick: handleToggleForm,
                    text: "Nuevo Usuario",
                    icon: <Plus size={20} className="me-2" />
                    },
                    // Puedes agregar más botones aquí
                ]}
            />
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
                        initialData={selectedUser}
                    />
                </ModalVacia>
            )}

        </div>
    );
};

export default GestionUsuarios;
