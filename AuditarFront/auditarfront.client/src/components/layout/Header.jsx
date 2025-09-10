import React from 'react';
import { UserCircle } from 'lucide-react';

const Header = ({ modo = 'Inicio', toggleMenu, menuVisible }) => {
    // Si menuVisible es true, solo redondea la derecha, si no, redondea todo
    const borderRadiusClass = !menuVisible
        ? 'rounded-end-4'
        : 'rounded-4';

    return (
        <header
            className={`${borderRadiusClass} mb-4 px-4 py-3 d-flex align-items-center justify-content-between shadow-sm`}
            style={{
                background: 'linear-gradient(90deg, #2563eb 0%, #1e40af 100%)',
                boxShadow: '0 4px 24px 0 rgba(30,64,175,0.10)',
                minHeight: 72,
                marginBottom: "0px !important"
            }}
        >
            {menuVisible ? (
                <div className="d-flex align-items-center gap-3">
                    <button onClick={toggleMenu} className="btn btn-light btn-sm d-flex align-items-center px-3 py-2 fw-bold shadow-sm me-3" title="Mostrar/Ocultar menú">
                        <span className="material-icons" style={{fontSize: 24, color: '#2563eb'}}>menu</span>
                    </button>
                    <h3 className="mb-0 fw-bold text-white" style={{fontSize: '1.7rem', letterSpacing: '-1px'}}>
                        Inspectia
                    </h3>
                </div>
            ) : (
                <div style={{ width: 180 }} /> 
            )}
            <div className="d-flex align-items-center gap-3">
                <div className="dropdown">
                    <button className="btn btn-link text-white fw-medium dropdown-toggle d-flex align-items-center p-0" type="button" id="userDropdown" data-bs-toggle="dropdown" aria-expanded="false">
                        Admin Principal <UserCircle size={32} color="#fff" className="ms-2" />
                    </button>
                    <ul className="dropdown-menu dropdown-menu-end" aria-labelledby="userDropdown">
                        <li>
                            <button
                                className="dropdown-item"
                                onClick={() => {
                                    localStorage.removeItem("token");
                                    localStorage.removeItem("usuario");
                                    window.location.href = "/login";
                                }}
                            >
                                Cerrar sesión
                            </button>
                        </li>
                    </ul>
                </div>
            </div>
        </header>
    );
};

export default Header;

