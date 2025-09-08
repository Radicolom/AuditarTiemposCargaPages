import React from 'react';
import { Users, LayoutDashboard, Settings } from 'lucide-react';

const Sidebar = () => {
    return (
        // Contenedor principal de la barra lateral.
        // w-64: ancho fijo.
        // bg-gray-800: fondo gris oscuro.
        // text-white: color de texto blanco.
        // flex-shrink-0: evita que se encoja si el contenido principal es muy grande.
        <aside className="position-fixed h-100 d-flex flex-column">
            {/* Menú de Navegación */}
            <nav className="mt-6">
                {/* Enlace Activo */}
                <a href="#" className="flex items-center py-3 px-6 text-gray-300 bg-gray-700">
                    <Users size={20} />
                    <span className="mx-4 font-medium">Gestión de Usuarios</span>
                </a>

                {/* Otros Enlaces */}
                <a href="#" className="flex items-center py-3 px-6 text-gray-300 hover:bg-gray-700 hover:text-white transition-colors">
                    <LayoutDashboard size={20} />
                    <span className="mx-4 font-medium">Dashboard</span>
                </a>
                <a href="#" className="flex items-center py-3 px-6 text-gray-300 hover:bg-gray-700 hover:text-white transition-colors">
                    <Settings size={20} />
                    <span className="mx-4 font-medium">Configuraci�n</span>
                </a>
            </nav>

        </aside>
    );
};

export default Sidebar;

