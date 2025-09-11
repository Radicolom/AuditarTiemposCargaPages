import React, { useState, useEffect } from 'react';
import { Plus, Edit, Trash2 } from 'lucide-react';
import { obtenerPaguesAuditar } from '../llamadasApi/PagesAuditar';
import { buscarEstado,formatearFecha } from '../utils/genericos';  
import { modificarPaguesAuditar } from '../llamadasApi/PagesAuditar';
import TablaGenerica from '../components/generico/TablaGenerica';
import ModalVacia from '../components/generico/ModalVacia';
import Title from '../components/layout/Title';
import PageAuditarForm from '../components/Forms/PageAuditarForm';
import Swal from 'sweetalert2';

const GestionPaguesAuditar = () => {
    
    const [isFormVisible, setIsFormVisible] = useState(false);
    const [selectedPage, setSelectedPage] = useState(null);
    const [pages, setPages] = useState([]);


    const columnasGrilla = 
    [
        { key: 'nombre', header: 'Nombre pagina' },
        { key: 'url', header: 'Url' },
        { key: 'estado', header: 'Estado', render: (row) => buscarEstado(row.estado) },
        { key: 'fechaCreacion', header: 'Fecha de creación', render: (row) => formatearFecha(row.fechaCreacion) },
        // { key: 'fechaModificacion', header: 'Fecha de modificación', render: (row) => formatearFecha(row.fechaModificacion) },
        { key: 'usuarioNombre', header: 'Usuario' },
    ]

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

    useEffect(() => {
        const fetchPages = async () => {
            const data = await obtenerPaguesAuditar();
            setPages(data);
        };
        fetchPages();
    }, []);

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
                <TablaGenerica
                    columns={columnasGrilla}
                    data={pages}
                    acciones={(page) => (
                        <>
                            <button
                                className="btn btn-sm btn-outline-primary me-2"
                                onClick={() => handleToggleForm("editar", page)}
                            >   
                                <Edit size={18} />
                            </button>
                            <button className="btn btn-sm btn-outline-danger" onClick={
                                () => handleToggleForm("eliminar", page)
                            }><Trash2 size={18} /></button>
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

