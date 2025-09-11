import React, { useEffect, useState } from "react";

/**
 * SelectorBootstrap
 * @param {function} fetchOptions - función async que retorna un array de objetos
 * @param {string} valueKey - nombre de la propiedad para el value
 * @param {string} labelKey - nombre de la propiedad para mostrar
 * @param {object} props - cualquier otra prop para el <select>
 */
export function Selectores({ fetchOptions, valueKey = "id", labelKey = "nombre", ...props }) {
  const [options, setOptions] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    let mounted = true;
    setLoading(true);
    fetchOptions().then(data => {
      if (mounted && data) setOptions(data);
      setLoading(false);
    });
    return () => { mounted = false; };
  }, [fetchOptions]);

  return (
    <select
      className="form-select form-select-lg"
      disabled={loading}
      {...props}
    >
      <option value="" disabled>
        {loading ? "Cargando opciones..." : "Seleccione una opción"}
      </option>
      {options.map(opt => (
        <option key={opt[valueKey]} value={opt[valueKey]}>
          {opt[labelKey]}
        </option>
      ))}
    </select>
  );
}