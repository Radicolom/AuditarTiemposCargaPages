import { apiPost, apiPut } from "../api";
import { esOperacionExitosa } from "../utils/genericos";


export async function obtenerRoles() {
  const response = await apiPost("Seguridad/RolObtener", { estado: true });
  if (esOperacionExitosa(response)) {
      return response.vista;
  }
  return [];
}

export async function insertarRol(data) {
  const response = await apiPost("Seguridad/RolInsertar", data);
  if (esOperacionExitosa(response)) {
      return response.vista;
  }
  return [];
}

export async function modificarRol(data) {
  const response = await apiPut("Seguridad/RolEditar", data);
  if (esOperacionExitosa(response)) {
      return response.vista;
  }
  return [];
}



