import Swal from "sweetalert2";
import 'bootstrap/dist/js/bootstrap.bundle.min.js';

//------------------------------
// Constantes Genericas
//------------------------------

//## Validacion de resultados de operaciones
export function esOperacionExitosa(response) {
    if (!response) {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'No se recibió respuesta del servidor.'
        });
        return;
    }
    if (response.operacionExitosa && !response.validacionesNegocio ) {
        return true;
    } else if (response.operacionExitosa && response.validacionesNegocio) {
        Swal.fire({
            icon: 'warning',
            title: 'Advertencia',
            text: response.Mensaje
        });
    } else {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Se produjo un error en la operación: ' + (response.Mensaje || '')
        });
    }
    return false;
}


//## Constantes de Estados
export const ESTADOS = [
    { value: true, label: "Activo" },
    { value: false, label: "Inactivo" }
];

export function buscarEstado(valor) {
    const estado = ESTADOS.find(e => e.value === valor);
    return estado ? estado.label : 'Desconocido';
}


//## Formateo de fechas
export function formatearFecha(fecha) {
    if (!fecha) return '';
    const date = new Date(fecha);
    if (isNaN(date)) return '';
    const opciones = { year: 'numeric', month: '2-digit', day: '2-digit', hour: '2-digit', minute: '2-digit' };
    return date.toLocaleDateString('es-ES', opciones).replace(',', '');
}