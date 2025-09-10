import React, { useEffect } from 'react';
import '../../css/ModalVacia.css';

/**
 * ModalVacia: Modal reutilizable con animación y callbacks de apertura/cierre.
 * Props:
 * - show: boolean (mostrar/ocultar)
 * - onClose: función para cerrar el modal
 * - ModalOpen: función opcional a ejecutar al abrir
 * - ModalClose: función opcional a ejecutar al cerrar
 * - children: contenido del modal
 */
export default function ModalVacia({ show, onClose, ModalOpen, ModalClose, children, style }) {
  useEffect(() => {
    if (show && ModalOpen) ModalOpen();
    if (!show && ModalClose) ModalClose();
    // eslint-disable-next-line
  }, [show]);

  if (!show) return null;

  return (
    <div className="modalvacia-backdrop" onClick={onClose}>
      <div      
        className="modalvacia-content animate-modal-in"
        style={style}
        onClick={e => e.stopPropagation()}
      >
        <button className="modalvacia-close" onClick={onClose}>&times;</button>
        {children}
      </div>
    </div>
  );
}
