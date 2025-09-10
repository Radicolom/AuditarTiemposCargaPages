import { apiPost, apiPut } from "../api";
import { esOperacionExitosa } from "../utils/genericos";

export async function insertarPaguesAuditar(data) {
    const response = await apiPost("Pages/AuditarPaginaInsertar", data);
    if (esOperacionExitosa(response)) {
      return response.vista;
    }
    return [];
}

export async function modificarPaguesAuditar(data) {
  const response = await apiPut("Pages/AuditarPaginaEditar", data);
  if (esOperacionExitosa(response)) {
      return response.vista;
  }
  return [];
}

export async function obtenerPaguesAuditar(data = {}) {
  const response = await apiPost("Pages/AuditarPaginaObtener", data);
  if (esOperacionExitosa(response)) {
      return response.vista;
  }
  return [];
}

export async function analyzePaguesAuditar(data) {
    const response = await apiPost("PageSpeed/analyze", data);
    if (esOperacionExitosa(response)) {
        return response.vista;
    }
    return [];
}