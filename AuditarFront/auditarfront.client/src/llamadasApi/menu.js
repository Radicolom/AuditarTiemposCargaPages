import { apiPost } from "../api";
import { esOperacionExitosa } from "../utils/genericos";

export async function obtenerMenus() {
  const response = await apiPost("Configuracion/MenuObtener", { estado: true });
  if (esOperacionExitosa(response)) {
    return response.vista;
  }
  return [];
}



