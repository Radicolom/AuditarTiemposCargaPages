import React from 'react';
import { UserCircle, Bell } from 'lucide-react';

const Header = ({ modo = 'Inicio', toggleMenu, menuVisible }) => {

    return (
        <header className="flex items-center justify-between px-6 py-4 bg-white border-b-2 border-gray-200 shadow-sm header-color">
            <div
                className="row"
                style={{
                    display: "flex",
                    alignItems: "center", // Centra verticalmente
                    width: "100%",
                    gap: 0,
                }}
            >
                <div className="col-md-3"
                    style={{
                        display: menuVisible ? "block" : "none",
                    }}
                >
                    {/* Logo o Título de la App y botón de ocultar */}
                    <div className="flex items-center justify-between h-20 border-b border-gray-700 px-4">
                        <button onClick={toggleMenu} className="btn btn-sm" title="Ocultar menú">
                            <h3 className="text-2xl font-bold">
                                <span className="material-icons">menu</span>
                                Inspectia
                            </h3>
                        </button>
                    </div>
                </div>
                <div className="col-md-7 d-flex">
                    {/* Modo centered */}
                    {/* <div className="flex-1 flex justify-center items-center h-20 border-b border-gray-700 px-4">
                        <h3 className="text-3xl font-extrabold text-gray-800 tracking-tight mb-1 flex items-center gap-2">
                            <span className="inline-block bg-blue-100 text-blue-700 px-3 py-1 rounded-lg text-lg font-semibold">{modo}</span>
                        </h3>
                    </div> */}
                </div>
                <div className="col-md-2 d-flex justify-content-end align-items-center"
                    style={{
                        color: "white",
                    }}
                >
                    {/* User info */}
                    <div className="flex items-center">
                        <span className="text-gray-700 font-medium mr-2">Admin Principal</span>
                        <UserCircle size={28} className="text-gray-600" />
                    </div>
                </div>
            </div>
        </header>
    );
};

export default Header;

