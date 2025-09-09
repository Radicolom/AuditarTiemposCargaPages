const apiUrl = import.meta.env.VITE_API_URL;

export async function insertarUsuario(data) {
  const response = await fetch(`${apiUrl}Seguridad/UsuarioInsertar`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(data)
  });
  if (!response.ok) throw new Error('Error al crear usuario');
  return await response.json();
}

export async function obtenerUsuarios(data = {}) {
  const response = await fetch(`${apiUrl}Seguridad/UsuarioObtener`, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(data)
  });
  if (!response.ok) throw new Error('Error al obtener usuarios');  
  return await response.json();
}