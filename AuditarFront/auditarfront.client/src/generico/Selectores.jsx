import React, { useEffect, useState } from 'react';

// Componente genérico para combos/selectores
// Recibe:
// - fetchOptions: función async que retorna un array de objetos
// - valueKey: nombre de la propiedad para el value
// - labelKey: nombre de la propiedad para mostrar
// - ...props: cualquier otra prop para el <select>
export function SelectorGenerico({ fetchOptions, valueKey = 'id', labelKey = 'nombre', ...props }) {
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
    <div className="mb-3 position-relative" >
      <select
        disabled={loading}
        className="form-select form-select-lg border-2 hadow bg-white text-dark"
        style={{
          minWidth: '200px',
          background: 'linear-gradient(90deg, #ffffffff 0%, #f8f8f8ff 100%)',
          borderRadius: '0.75rem',
          fontWeight: 500,
          boxShadow: '0 2px 8px 0 rgba(0,0,0,0.08)',
          paddingRight: '2.5rem',
          appearance: 'none',
          cursor: loading ? 'not-allowed' : 'pointer',
          transition: 'box-shadow 0.2s, border-color 0.2s',
        }}
        onFocus={e => e.target.style.boxShadow = '0 0 0 0.2rem #a7f3d0'}
        onBlur={e => e.target.style.boxShadow = '0 2px 8px 0 rgba(0,0,0,0.08)'}
        {...props}
      >
        <option value="" disabled selected>
          {loading ? 'Cargando opciones...' : 'Seleccione una opción'}
        </option>
        {options.map(opt => (
          <option key={opt[valueKey]} value={opt[valueKey]}>
            {opt[labelKey]}
          </option>
        ))}
      </select>
      {/* Flecha personalizada tipo Bootstrap */}
      <span style={{
        position: 'absolute',
        top: '50%',
        right: '1.2rem',
        pointerEvents: 'none',
        transform: 'translateY(-50%)',
        color: '#0d6efd',
        fontSize: '1.3rem',
      }}>
        ▼
      </span>
    </div>
  );
}
