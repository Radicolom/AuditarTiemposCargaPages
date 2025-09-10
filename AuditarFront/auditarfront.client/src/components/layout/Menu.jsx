import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { Users, LayoutDashboard, Settings } from 'lucide-react';
import { obtenerMenus } from '../../llamadasApi/menu';


const Menu = ({ toggleMenu}) => {
    const [menus, setMenus] = useState([]);

    useEffect(() => {       
        const fetchMenus = async () => {
            const data = await obtenerMenus();
            setMenus(data);
            sessionStorage.setItem("menus", JSON.stringify(data));
        };
        fetchMenus();
    }, []);

    return (
        <aside
            className="h-100 d-flex flex-column shadow-lg"
            style={{
                background: 'linear-gradient(180deg, #2563eb 0%, #1e40af 100%)',
                minWidth: 0,
                minHeight: 0,
                borderTopLeftRadius: '1.5rem',
                borderTopRightRadius: 0,
                borderBottomLeftRadius: '1.5rem',
                borderBottomRightRadius: '1.5rem',
            }}
        >
            <div className="d-flex align-items-center justify-content-center border-bottom border-0" style={{ height: '4.5rem' }}>
                <button onClick={toggleMenu} className="btn btn-light btn-sm d-flex align-items-center px-3 py-2 fw-bold shadow-sm" title="Ocultar menú">
                    <span className="material-icons" style={{fontSize: 24, color: '#2563eb'}}>menu</span>
                </button>
                <h1 className="fs-3 fw-bold m-0 ms-2 text-white" style={{letterSpacing: '-1px'}}>
                    Inspectia
                </h1>
            </div>
            <nav className="mt-4 px-2 flex-grow-1">
                {menus.map(menu => (
                    <Link
                        key={menu.id}
                        to={menu.url}
                        className="d-flex align-items-center py-3 px-3 text-white rounded-3 mb-2 sidebar-link position-relative"
                        style={{
                            transition: 'background 0.2s, box-shadow 0.2s',
                        }}
                    >
                        <span className="material-icons" style={{fontSize: 22, opacity: 0.85}}>{menu.icono}</span>
                        <span className="ms-3 fw-medium" style={{fontSize: '1.08rem'}}>{menu.nombre}</span>
                    </Link>
                ))}
            </nav>
        </aside>
    );
};

export default Menu;

