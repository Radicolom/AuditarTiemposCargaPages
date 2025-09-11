import React from 'react';

const Inicio = () => (
  <div className="container py-5">
    <div className="bg-white shadow rounded-4 p-4 mb-4">
      <h1 className="fw-bold text-primary mb-3">Bienvenido a Auditar INSPECTIA</h1>
      <p className="lead mb-4">
        <strong>Auditar INSPECTIA</strong> es una herramienta de monitoreo de rendimiento web diseñada para medir y registrar los tiempos de carga de diferentes páginas, permitiendo analizar su eficiencia y detectar posibles cuellos de botella.
      </p>
      <h4 className="fw-semibold mt-4 mb-2">¿Para qué funciona?</h4>
      <ul>
        <li>
          <strong>Optimización y experiencia de usuario (UX):</strong> Permite hacer seguimiento histórico del rendimiento de tus sitios web para mejorar la experiencia de tus visitantes.
        </li>
        <li>
          <strong>SEO:</strong> Ayuda a identificar problemas que pueden afectar el posicionamiento en buscadores.
        </li>
      </ul>
      <h4 className="fw-semibold mt-4 mb-2">Funcionalidades Clave</h4>
      <ul>
        <li>
          <strong>Gestión de Usuarios y Roles:</strong>
          <ul>
            <li>Autenticación segura mediante login.</li>
            <li>
              <strong>Administrador:</strong> Control total sobre usuarios y páginas monitoreadas.
            </li>
            <li>
              <strong>Usuario:</strong> Acceso de solo lectura a reportes y datos de rendimiento.
            </li>
          </ul>
        </li>
        <li>
          <strong>Auditoría de Páginas Web:</strong>
          <ul>
            <li>Registro de URLs a monitorear.</li>
            <li>Recolección automática de métricas clave como Tiempo Total de Carga, TTFB, procesamiento del DOM, etc.</li>
          </ul>
        </li>
        <li>
          <strong>Almacenamiento y Visualización de Datos:</strong>
          <ul>
            <li>Historial de mediciones en tablas (y próximamente gráficos).</li>
            <li>Panel de inspecciones para analizar tendencias.</li>
          </ul>
        </li>
      </ul>
      <h4 className="fw-semibold mt-4 mb-2">Stack Tecnológico</h4>
      <ul>
        <li><strong>Backend:</strong> C# con .NET API</li>
        <li><strong>Autenticación:</strong> JWT (JSON Web Tokens)</li>
        <li><strong>Base de Datos:</strong> SQL Server</li>
        <li><strong>ORM:</strong> Entity Framework Core</li>
        <li><strong>Frontend:</strong> JavaScript (APIs nativas del navegador para métricas)</li>
      </ul>
      <div className="alert alert-info mt-4">
        <strong>En resumen:</strong> Auditar INSPECTIA es una solución integral para analizar y mejorar el rendimiento de tus páginas web, con un sistema de roles seguro y una arquitectura moderna basada en .NET y SQL Server.
      </div>
    </div>
  </div>
);

export default Inicio;