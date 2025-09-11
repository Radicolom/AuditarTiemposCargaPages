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

3. **Configuración PageSpeed Insights API**
  1. Ve a la biblioteca de APIs de Google Cloud y busca o ve directamente a la página de la API de PageSpeed Insights:
https://console.cloud.google.com/apis/library/pagespeedonline.googleapis.com
 2. Asegúrate de tener seleccionado el proyecto correcto en la parte superior de la página (el mismo proyecto donde creaste tu clave de API).

 3. En la página de la API, verás un botón azul grande.
    - Si el botón dice "HABILITAR", ¡esa es la causa del problema! Haz clic en él y espera a que se active. Una vez que termine, la página se recargará y el botón dirá "GESTIONAR".

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
│   ├── node-v22.19.0-x64.msi # Instalador Node.js
│   └── 
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

A continuación se incluyen scripts SQL para poblar las tablas principales de seguridad con datos iniciales:

```sql
-- Insertar roles predeterminados
INSERT INTO Seguridad.Rol (NombreRol, EstadoRol) VALUES ('Administrador', 1);
INSERT INTO Seguridad.Rol (NombreRol, EstadoRol) VALUES ('Usuario', 1);

-- Insertar usuario administrador por defecto
INSERT INTO Seguridad.Usuario (NombreUsuario,ApellidoUsuario,DocumentoUsuario,CorreoUsuario,EmailConfirmed,
  PasswordUsuario,
  TelefonoUsuario,TelefonoConfirmadoUsuario,AutenticacionDobleFactor,AutenticacionIntentos,RolId
) VALUES (
  'Admin','Admin','1010101010','Admin@Admin.com',1,
  '$2a$11$NlM8Zv23PYYOwgAZJ7IvGOPv1S4gcSUt04x.Q0nLaQXBjnJCcYIDG',
  '3005551234',1,0,0,1);
-- Insertar los menús para las 4 páginas principales
INSERT INTO configuracion.Menu (NombreMenu, UrlMenu, IconoMenu, EstadoMenu) VALUES 
('Inicio', '/inicio', 'home', 1),
('Gestión Páginas Auditar', '/GestionPaguesAuditar', 'edit', 1),
('Gestión de Usuarios', '/GestionUsuarios', 'users', 1),
('Auditar Página', '/AuditarPague', 'search', 1);

```

> **Nota:** La contraseña está encriptada. Modifica los valores según tus necesidades antes de ejecutar los scripts.

