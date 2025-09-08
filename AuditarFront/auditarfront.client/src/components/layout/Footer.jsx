import React from 'react';

const Footer = () => {
    return (
        <footer className="text-center py-4 bg-white border-t border-gray-200 text-sm text-gray-500">
            &copy; {new Date().getFullYear()} Inspectia. Todos los derechos reservados.
        </footer>
    );
};

export default Footer;

