import React from 'react';
import Header from './Header';
import Menu from './Menu';
import Footer from './Footer';

const Layout = ({ children }) => {
    const [menuVisible, setMenuVisible] = React.useState(true);
    const toggleMenu = () => setMenuVisible((open) => !open);

    const modo = "Inicio"; 
    

    return (
        <div className="d-flex flex-column min-vh-100">
            <div className="row h-100 flex-grow-1">
                {/* Sidebar siempre a la izquierda */}
                <div className={
                    menuVisible ? "col-md-2" : "d-none"
                }>
                    {menuVisible && <Menu toggleMenu={toggleMenu} />}
                </div>

                <div className={
                    (menuVisible ? "col-md-10" : "col-md-12") + " d-flex flex-column h-100"
                } style={{ minHeight: '100vh' }}>
                    {/* Encabezado superior */}
                    <Header modo={modo} toggleMenu={toggleMenu} menuVisible={!menuVisible} />
                    <div className="flex-grow-1 d-flex flex-column">
                        {/* Contenido principal de la página */}
                        {children}
                    </div>
                    {/* Pie de página al final visible */}
                    <Footer />
                </div>
            </div>

        </div>
    );
};

export default Layout;

