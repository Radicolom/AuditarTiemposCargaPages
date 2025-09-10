import React, { useState, useEffect } from 'react';
import { Plus, Edit, Trash2 } from 'lucide-react';
import { obtenerPaguesAuditars } from '../llamadasApi/PagesAuditar';
import { buscarEstado,formatearFecha } from '../utils/genericos';  
import { modificarPaguesAuditar } from '../llamadasApi/PagesAuditar';
import ModalVacia from '../components/generico/ModalVacia';
import Title from '../components/layout/Title';
import PageAuditarForm from '../components/Forms/PageAuditarForm';
import Swal from 'sweetalert2';

const GestionPaguesAuditar = () => {
    // Estado para controlar la visibilidad del formulario
    const [isFormVisible, setIsFormVisible] = useState(false);
    const [selectedPage, setSelectedPage] = useState(null);

    // Alternar la visibilidad del formulario y el tipo de acción
    const handleToggleForm = (tipo, page = null) => {
        switch (tipo) {
            case "editar":
                setIsFormVisible(!isFormVisible);  
                setSelectedPage(page);
                break;
            case "eliminar":
                const existingPage = pages.find(p => p.id === page.id);
                if (!existingPage) {
                    Swal.fire({
                        title: 'Error',
                        text: 'Página no encontrada.',
                        icon: 'error'
                    });
                    return;
                }
                if (existingPage.estado === false) {
                    Swal.fire({
                        title: 'Error',
                        text: 'La página ya está inactiva.',
                        icon: 'error'
                    });
                    return;
                }
                Swal.fire({
                    title: '¿Estás seguro?',
                    text: "No se podrá Auditar.",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Sí, eliminar',
                    cancelButtonText: 'Cancelar'
                }).then((result) => {
                    if (result.isConfirmed) {
                        modificarPaguesAuditar({...page, estado: false});
                        setPages(pages.map(p => p.id === page.id ? {...p, estado: false} : p));
                        Swal.fire(
                            'Eliminado',
                            'La página ha sido eliminada.',
                            'success'
                        )
                    }
                })
                break;
            default:
                setIsFormVisible(!isFormVisible);  
                setSelectedPage(null);
        }
    };

    // Estado para almacenar las páginas a auditar
    const [pages, setPages] = useState([]);

    useEffect(() => {
        const fetchPages = async () => {
            const data = await obtenerPaguesAuditars();
            setPages(data);
        };
        fetchPages();
    }, []);

    const handleSavePage = (newPage) => {
        let NuevaPagina;
        if (newPage.id) {
            NuevaPagina = pages.some(p => p.id === newPage.id);
        }
        if (NuevaPagina) {
            setPages(pages.map(p => p.id === newPage.id ? newPage : p));
        } else {
            setPages([...pages, { ...newPage }]);
        }
        setIsFormVisible(false);
    }
    
    return (
        <div className="card p-4 border-0 flex-grow-1" style={{ background: '#f9f9f9' }}>
            {/* Cabecera de la pagina */}
            <Title 
                icon={<Edit size={32} color="#fff" />} 
                title="Gestión páginas Auditar"
                subtitle="Administra las páginas a auditar, crea, edita o elimina según sea necesario."
                actions={[
                    {
                        onClick: () => handleToggleForm("crear"),
                        text: "Nueva Página", 
                        icon: <Plus size={20} className="me-2" /> 
                    },
                ]}
            />
            <div className="bg-white shadow-xl rounded-4 overflow-x-auto border border-gray-100">
                <table className="table table-hover align-middle table-bordered mb-0 rounded-4 overflow-hidden">
                    <thead className="tabla-app-thead">
                        <tr>
                            <th>Nombre pagina</th>
                            <th>Url</th>
                            <th>Estado</th>
                            <th>Fecha de creación</th>
                            {/* <th>Fecha de modificación</th> */}
                            <th>Usuario</th>
                            <th className="text-center">Acciones</th>
                        </tr>
                    </thead>
                    <tbody>
                        {pages.map(page => (
                            <tr key={page.id}>
                                <td>{page.nombre}</td>
                                <td>{page.url}</td>
                                <td>{buscarEstado(page.estado)}</td>
                                <td>{formatearFecha(page.fechaCreacion)}</td>
                                {/* <td>{formatearFecha(page.fechaModificacion)}</td> */}
                                <td>{page.usuarioNombre}</td>
                                <td className="text-center">
                                    <button
                                        className="btn btn-sm btn-outline-primary me-2"
                                        onClick={() => handleToggleForm("editar", page)}
                                    >
                                        <Edit size={18} />
                                    </button>
                                    <button className="btn btn-sm btn-outline-danger" onClick={
                                        () => handleToggleForm("eliminar", page)
                                    }><Trash2 size={18} /></button>
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
                    <PageAuditarForm
                        onSave={handleSavePage}
                        onCancel={() => setIsFormVisible(false)}
                        initialData={selectedPage}
                    />
                </ModalVacia>
            )}
            

        </div>
    );
};

export default GestionPaguesAuditar;

