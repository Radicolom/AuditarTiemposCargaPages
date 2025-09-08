import React, { useEffect, useState } from 'react';
import { Routes, Route } from 'react-router-dom';
import Layout from './components/layout/Layout';
import GestionUsuarios from './pages/GestionUsuarios';
import UserForm from './pages/UserForm';
import Inicio from './pages/Inicio';
import { obtenerMenus } from './consultas/menu';


function App() {
    const [menus, setMenus] = useState([]);
    useEffect(() => {
        const fetchMenus = async () => {
            const data = await obtenerMenus();
            setMenus(data || []);
        };
        fetchMenus();
    }, []);

    // Mapeo de nombre de menú a componente
    const componentMap = {
        1: Inicio,
        2: UserForm,
        3: GestionUsuarios,
    };

    return (
        <div className="min-h-screen bg-gradient-to-br from-blue-200 via-blue-100 to-blue-300">
            <Layout>
                <Routes>
                    {/* Ruta por defecto */}
                    <Route path="/" element={<Inicio />} />
                    {/* Rutas dinámicas desde la base de datos */}
                    {menus.map(menu => {
                        const Comp = componentMap[menu.id];
                        if (!Comp) return null;
                        return (
                            <Route key={menu.id} path={menu.url} element={<Comp />} />
                        );
                    })}
                </Routes>
            </Layout>
        </div>
    );
}

export default App;

