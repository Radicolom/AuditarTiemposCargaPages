import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { Users, LayoutDashboard, Settings } from 'lucide-react';
import { obtenerMenus } from '../../consultas/menu';


const Menu = ({modo, toggleMenu}) => {
    const [menus, setMenus] = useState([]);

    useEffect(() => {       
        const fetchMenus = async () => {
            const data = await obtenerMenus();
            setMenus(data || []);
        };
        fetchMenus();
    }, []);

    return (
        <aside
            className="h-100 d-flex flex-column menu-color">
            <div className="d-flex align-items-center justify-content-center border-bottom border-secondary" style={{ height: '5rem' }}>
               <button onClick={toggleMenu} className="btn btn-sm" title="Ocultar menú">
                    <h1 className="fs-2 fw-bold m-0" 
                        style={
                            {
                                color: 'white',
                            }
                        }
                    >
                        <span className="material-icons">menu</span>
                        Inspectia
                    </h1>
               </button>
            </div>
            <nav className="mt-4">
                {menus.map(menu => (
                    <Link key={menu.id} to={menu.url} className="d-flex align-items-center py-3 px-4 text-white hover-bg-secondary rounded mb-2">
                        {/* Si icono es el nombre de un ícono de Material Icons */}
                        <span className={"material-icons"}>{menu.icono}</span>
                        <span className="ms-3 fw-medium">{menu.nombre}</span>
                    </Link>
                ))}
            </nav>
        </aside>
    );
};

export default Menu;

