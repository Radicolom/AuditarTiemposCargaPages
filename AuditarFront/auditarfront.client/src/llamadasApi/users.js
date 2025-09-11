import { apiLogin, apiPost, apiPut } from "../api";
import { esOperacionExitosa } from "../utils/genericos";

export async function insertarUsuario(data) {
  const response = await apiPost("Seguridad/UsuarioInsertar", data);
  if (esOperacionExitosa(response)) {
    return response.vista;
  }
  return [];
}

export async function modificarUsuario(data) {
  const response = await apiPut("Seguridad/UsuarioEditar", data);
  if (esOperacionExitosa(response)) {
    return response.vista;
  }
  return [];
}

export async function obtenerUsuarios(data = {}) {
  const response = await apiPost("Seguridad/UsuarioObtener", data);
  if (esOperacionExitosa(response)) {
    return response.vista;
  }
  return [];
}

export async function Login(data = {}) {
  let response = await apiLogin("Auth/login", data);
  if (response.ok) {
    let result = await response.json();
    if (esOperacionExitosa(result)){
      localStorage.setItem("usuario", JSON.stringify(result.vista[0]));
      localStorage.setItem("token", result.vista[0].token);
      return true;
    }
    return result.message;
  } else if (!response.ok) {
    response = await response.json();
  }
  return response.message;
}
