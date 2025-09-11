const apiUrl = import.meta.env.VITE_API_URL;

export async function apiGet(endpoint) {
  const response = await fetch(`${apiUrl}${endpoint}`);
  if (!response.ok) throw new Error("Error en la petición GET");
  return response.json();
}

export async function apiPost(endpoint, data) {
    try { 
        const token = localStorage.getItem("token");
        const headers = {
            "Content-Type": "application/json",
            ...(token ? { "Authorization": `Bearer ${token}` } : {})
        };
        const response = await fetch(`${apiUrl}${endpoint}`, {
            method: "POST",
            headers,
            body: JSON.stringify(data),
        });
        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(errorText || "Error en la petición");
        }
        return await response.json();
    } catch (e) {
        console.error(e);
    }
}

export async function apiPut(endpoint, data) {
  const token = localStorage.getItem("token");
  const headers = {
    "Content-Type": "application/json",
    ...(token ? { "Authorization": `Bearer ${token}` } : {})
  };
  const response = await fetch(`${apiUrl}${endpoint}`, {
    method: "PUT",
    headers,
    body: JSON.stringify(data),
  });
  if (!response.ok) throw new Error("Error en la petición PUT");
  return response.json();
}

export async function apiLogin(endpoint, data) {
  try {
    const response = await fetch(`${apiUrl}${endpoint}`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(data),
    });
    return await response;
  } catch (e) {
    console.error(e);
  }
}
