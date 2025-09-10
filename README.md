# AuditarTiemposCargaPages

Proyecto para auditoría de tiempos de carga de páginas web.

## Estructura del Proyecto

- **AuditarApi/**: API backend desarrollada en .NET
- **AuditarFront/**: Frontend desarrollado con tecnologías web modernas
- **Necesarios/**: Archivos de configuración y herramientas requeridas

## Requisitos Previos

- Visual Studio 2022 (configuración incluida en carpeta `Necesarios/`)
- Node.js (instalador incluido en carpeta `Necesarios/`)
- .NET SDK

## Configuración de Visual Studio 2022

1. **Configuración de Múltiples Proyectos de Inicio:**
   - Click derecho en la solución
   - Seleccionar "Propiedades"
   - En "Proyectos de inicio múltiples", configurar:
     - `auditarfront.client` - Acción: **Iniciar**
     - `AuditarApi` - Acción: **Iniciar**

2. **Archivos de Configuración:**
   - En la carpeta `Necesarios/` se incluyen:
     - Configuración exportada de Visual Studio 2022
     - Instalador de Node.js (node-v22.19.0-x64.msi)

## Ejecución del Proyecto

### Desde Visual Studio 2022
1. Abrir la solución principal
2. Configurar múltiples proyectos de inicio (ver sección anterior)
3. Presionar F5 o "Iniciar"

### Desde Visual Studio Code

#### Opción 1: Comandos Paso a Paso
```bash
# Navegar al cliente e instalar dependencias
cd "d:\ruta\AuditarTiemposCargaPages\AuditarFront\auditarfront.client"
npm install

# En otra terminal, ejecutar el servidor API
cd "d:\ruta\AuditarTiemposCargaPages\AuditarApi"
dotnet run

# En otra terminal, ejecutar el servidor frontend
cd "d:\ruta\AuditarTiemposCargaPages\AuditarFront\AuditarFront.Server"
dotnet run
```

#### Opción 2: Comando Combinado (PowerShell)
```powershell
cd "d:\ruta\AuditarTiemposCargaPages\AuditarFront\auditarfront.client" ; npm install ; cd "d:\ruta\AuditarTiemposCargaPages\AuditarFront\AuditarFront.Server" ; dotnet run
```

## Estructura de Carpetas

```
AuditarTiemposCargaPages/
├── AuditarApi/                 # API Backend (.NET)
│   ├── Application/            # Capa de aplicación
│   ├── AuditarApi/            # Proyecto principal API
│   └── Dominio/               # Capa de dominio
├── AuditarFront/              # Frontend
│   ├── auditarfront.client/   # Cliente web
│   └── AuditarFront.Server/   # Servidor frontend
├── Necesarios/                # Herramientas y configuraciones
│   └── node-v22.19.0-x64.msi # Instalador Node.js
└── README.md                  # Este archivo
```

## Tecnologías Utilizadas

- **Backend**: .NET Core, Entity Framework
- **Frontend**: Node.js, HTML/CSS/JavaScript
- **Base de Datos**: (Especificar según configuración)

## Contribución

1. Crear una rama desde `AlejoFront`
2. Realizar cambios
3. Crear Pull Request

## Notas Adicionales

- Asegúrate de tener instalada la versión correcta de Node.js incluida en la carpeta `Necesarios/`
- Verifica que la configuración de Visual Studio 2022 esté aplicada correctamente
- El proyecto utiliza múltiples puertos para API y frontend
