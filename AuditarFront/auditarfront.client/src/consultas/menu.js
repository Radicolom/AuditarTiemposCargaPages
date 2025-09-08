const apiUrl = import.meta.env.VITE_API_URL;

export async function obtenerMenus() {
  try {
    const response = await fetch(`${apiUrl}Configuracion/MenuObtener`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ estado: true }),
    });
    if (!response.ok) throw new Error('Error en la consulta');
    return await response.json();
  } catch (error) {
    console.error('Error al obtener menus:', error);
    return null;
  }
  
}




