import React, { useState, useEffect } from 'react';
import { Plus, Edit, Trash2 } from 'lucide-react';
import { obtenerUsuarios } from '../llamadasApi/users';
import UserForm from '../components/Forms/UserForm'; 
import ModalVacia from '../components/generico/ModalVacia';
import Title from '../components/layout/Title';
import TablaGenerica from '../components/generico/TablaGenerica';

const GestionUsuarios = () => {
    const [users, setUsers] = useState([]);
    const [isFormVisible, setIsFormVisible] = useState(false);
    const [selectedUser, setSelectedUser] = useState(null);

    const columnasGrilla =
    [
        { key: 'nombreCompleto', header: 'Nombre Completo', render: (user) => `${user.nombre} ${user.apellido}` },
        { key: 'documento', header: 'Documento' },
        { key: 'correo', header: 'Correo' },
        { key: 'rolNombre', header: 'Rol', style: { textAlign: 'center' }, render: (user) => (
            <span className={`badge ${user.rolId === 1 ? 'bg-danger' : 'bg-success'}`}>
                {user.rolNombre}
            </span>
        ) },
    ]

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
    
    useEffect(() => {
        const fetchUsers = async () => {
            const data = await obtenerUsuarios();
            setUsers(data || []);
        };
        fetchUsers();
    }, []);

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
                <TablaGenerica
                    columns={columnasGrilla}
                    data={users}
                    acciones={(user) => (
                        <>
                            <button className="btn btn-sm btn-outline-primary me-2" onClick={() => handleToggleForm("editar", user)}>
                                <Edit size={18} />
                            </button>
                            <button className="btn btn-sm btn-outline-danger">
                                <Trash2 size={18} />
                            </button>
                        </>
                    )}
                    pageSize={10}
                />
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
