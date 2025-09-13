## Enlace al Proyecto

Accede al frontend desplegado en el siguiente enlace:  
[https://frontapi.somee.com/](http://frontapi.somee.com/)

**Credenciales de acceso de prueba:**  
- **Usuario:** `Administrador@admin.com`  
- **Contraseña:** `Clave123+.`

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
- Abilitar PageSpeed Insights API (Incluido un manual más abajo)

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
3. **Configuración de la API PageSpeed Insights**
    - Accede a la [biblioteca de APIs de Google Cloud](https://console.cloud.google.com/apis/library/pagespeedonline.googleapis.com) y selecciona la API de PageSpeed Insights.
    - Verifica que tienes seleccionado el proyecto correcto en la parte superior (debe ser el mismo donde generaste tu clave de API).
    - En la página de la API, localiza el botón azul principal:
        - Si aparece "HABILITAR", haz clic para activar la API. Espera a que el proceso finalice; el botón cambiará a "GESTIONAR" cuando la API esté habilitada correctamente.
    
4. **Uso de la API en Producción**  
  - Si no deseas ejecutar la API localmente, puedes configurar el frontend para consumir la API desplegada en producción.  
  - Para ello, edita el archivo `.env` ubicado en:  
    ```
    AuditarTiemposCargaPages/
    └── AuditarFront/
      └── auditarfront.client/
          └── .env
    ```
  - Cambia la línea:
    ```
    VITE_API_URL=https://localhost:7169/api/
    ```
    por:
    ```
    VITE_API_URL=https://Radicolom2402.somee.com/api/
    ```
  - Así, el frontend utilizará directamente la API publicada en el entorno de producción.


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
│   ├── node-v22.19.0-x64.msi  # Instalador Node.js
│   ├── .vsconfig              # Configuración (Visual Studio 2022)
│   └── ScripsSQL              # Ejecutables (Mapeo tablas BD)
└── README.md                  # Este archivo
```

## Tecnologías Utilizadas

- **Backend**: .NET Core, Entity Framework
- **Frontend**: Node.js, HTML/CSS/JavaScript
- **Base de Datos**: SqlServer

## Contribución

1. Crear una rama desde `AlejoFront`
2. Realizar cambios
3. Crear Pull Request

## Notas Adicionales

- Asegúrate de tener instalada la versión correcta de Node.js incluida en la carpeta `Necesarios/`
- Verifica que la configuración de Visual Studio 2022 esté aplicada correctamente

## Scripts de Inserción Inicial

A continuación se incluyen scripts SQL para poblar las tablas principales de seguridad con datos iniciales. La contraseña actual es (Clave123+.):

```sql
-- Insertar Rol Admin
INSERT INTO Seguridad.Rol (NombreRol, EstadoRol) VALUES ('Administrador', 1);
-- Insertar Usuario Admin
INSERT INTO Seguridad.Usuario (NombreUsuario, ApellidoUsuario, DocumentoUsuario, CorreoUsuario, PasswordUsuario, RolId, EmailConfirmed)
VALUES ('Admin', 'Principal', '123456789', 'Administrador@admin.com', '$2a$11$.7tOwnQLkkT9FNocj2ZEhem49xa5XUI042l.nOvqfwjFrZxfQmG1S', 1, 1);

-- Insertar Men�s
INSERT INTO Configuracion.Menu (NombreMenu, UrlMenu, IconoMenu) VALUES ('Dashboard', '/Inicio', 'home');
INSERT INTO Configuracion.Menu (NombreMenu, UrlMenu, IconoMenu) VALUES ('Paginas Auditadas', '/auditar', 'description');
INSERT INTO Configuracion.Menu (NombreMenu, UrlMenu, IconoMenu) VALUES ('Gestion de Usuarios', '/usuarios', 'group');
INSERT INTO Configuracion.Menu (NombreMenu, UrlMenu, IconoMenu) VALUES ('Auditar Paginas', '/auditar', 'search');

```

> **Nota:** La contraseña está encriptada. Modifica los valores según tus necesidades antes de ejecutar los scripts.

