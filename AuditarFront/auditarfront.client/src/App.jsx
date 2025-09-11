import React, { useEffect, useState } from 'react';
import { Routes, Route } from 'react-router-dom';
import { obtenerMenus } from './llamadasApi/menu';
import { PrivateRoute } from './components/PrivateRoute';
import LoginForm from './components/auth/LoginForm';
import Layout from './components/layout/Layout';
import GestionUsuarios from './pages/GestionUsuarios';
import GestionPaguesAuditar from './pages/GestionPaguesAuditar';
import AuditarPague from './pages/AuditarPague';
import Inicio from './pages/Inicio';
import Fund from './components/layout/fund';


function App() {
    const [menus, setMenus] = useState([]);

    useEffect(() => {
        const storedMenus = sessionStorage.getItem("menus");
        if (storedMenus) {
            setMenus(JSON.parse(storedMenus));
        }
    }, []);


    async function fetchMenus() {
        if (menus.length > 0) return;
        const data = await obtenerMenus();
        setMenus(data || []);
    }

    // Mapeo de nombre de menú a componente
    const componentMap = {
        1: Inicio,
        2: GestionPaguesAuditar,
        3: GestionUsuarios,
        4: AuditarPague
    };

    return (
        <div className="min-h-screen bg-gradient-to-br from-blue-200 via-blue-100 to-blue-300">
            <Fund />
            <Routes>
                <Route path="/login" element={
                    <div style={{ minHeight: '100vh', display: 'flex', alignItems: 'center', justifyContent: 'center', background: 'linear-gradient(135deg, #e0e7ff 0%, #f8fafc 100%)' }}>
                        <LoginForm />
                    </div>
                } />
                <Route element={<Layout
                    onLogin={fetchMenus}
                />}>
                    <Route element={<PrivateRoute />}>
                        <Route path="/" element={<Inicio />} />
                        {menus.map(menu => {
                            const Comp = componentMap[menu.id];
                            if (!Comp) return null;
                            return (
                                <Route key={menu.id} path={menu.url} element={<Comp />} />
                            );
                        })}
                    </Route>
                </Route>
            </Routes>
        </div>
    );
}

export default App;

