const apiUrl = import.meta.env.VITE_API_URL;

export async function obtenerRoles() {
  try {
    const response = await fetch(`${apiUrl}Seguridad/RolObtener`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ estado: true }),
    });
    if (!response.ok) throw new Error('Error en la consulta');
    return await response.json();
  } catch (error) {
    console.error('Error al obtener roles:', error);
    return null;
  }
  
}




