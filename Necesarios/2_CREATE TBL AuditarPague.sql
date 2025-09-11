USE AuditarPague;

	BEGIN TRANSACTION; 
		BEGIN TRY

			 -- Creaci�n de esquemas
			-- Se usa EXEC para ejecutar cada CREATE SCHEMA en su propio lote y se especifica AUTHORIZATION dbo para evitar conflictos.
			IF SCHEMA_ID('Seguridad') IS NULL EXEC('CREATE SCHEMA Seguridad AUTHORIZATION dbo');
			IF SCHEMA_ID('Configuracion') IS NULL EXEC('CREATE SCHEMA Configuracion AUTHORIZATION dbo');

			CREATE TABLE Seguridad.Rol (
				RolId INT IDENTITY(1,1) PRIMARY KEY,
				NombreRol VARCHAR(100) NOT NULL,
				EstadoRol BIT DEFAULT 'true'
			);
			

			CREATE TABLE Seguridad.Usuario(
				UsuarioId INT IDENTITY(1,1) PRIMARY KEY,
				NombreUsuario VARCHAR(150) NOT NULL,
				ApellidoUsuario VARCHAR(150) NOT NULL,
				DocumentoUsuario VARCHAR(20) UNIQUE NOT NULL,
				CorreoUsuario VARCHAR(255) UNIQUE NOT NULL,
				EmailConfirmed BIT DEFAULT 0,
				PasswordUsuario VARCHAR(MAX) NOT NULL,
				TelefonoUsuario VARCHAR(12),
				TelefonoConfirmadoUsuario BIT DEFAULT 0,
				AutenticacionDobleFactor BIT DEFAULT 0,
				AutenticacionIntentos INT,
				EstadoUsuario BIT DEFAULT 'true',
				RolId INT
			);
			

			ALTER TABLE Seguridad.Usuario�
			WITH CHECK ADD CONSTRAINT FK_Usuario_Rol FOREIGN KEY (RolId)
			REFERENCES Seguridad.Rol (RolId);
			

			CREATE TABLE Configuracion.Menu(
				MenuId INT IDENTITY(1,1) PRIMARY KEY,
				NombreMenu VARCHAR(150) NOT NULL,
				UrlMenu VARCHAR(MAX),
				IconoMenu VARCHAR(500),
				EstadoMenu BIT DEFAULT 'True'
			);
			

			CREATE TABLE Configuracion.MenuRol(
				MenuRolId INT IDENTITY(1,1) PRIMARY KEY,
				MenuId INT,
				RolId INT
			);
			

			ALTER TABLE Configuracion.MenuRol
			WITH CHECK ADD CONSTRAINT FK_MenuRol_Rol FOREIGN KEY (RolId)
			REFERENCES Seguridad.Rol (RolId);
			

			ALTER TABLE Configuracion.MenuRol
			WITH CHECK ADD CONSTRAINT FK_MenuRol_Menu FOREIGN KEY (MenuId)
			REFERENCES Configuracion.Menu (MenuId);
			

			-- Tabla para definir acciones/operaciones (Ej: Crear, Leer, Actualizar, Borrar)
			CREATE TABLE Configuracion.Accion(
				AccionId INT IDENTITY(1,1) PRIMARY KEY,
				NombreAccion VARCHAR(50) NOT NULL
			);
			

			-- Tabla para definir servicios/m�dulos del sistema (Ej: Usuarios, Productos)
			CREATE TABLE Configuracion.Servicio (
				ServicioId INT IDENTITY(1,1) PRIMARY KEY,
				NombreServicio VARCHAR(100) NOT NULL
			);
			

			-- Tabla para asignar permisos detallados (qu� Rol puede hacer qu� Accion sobre qu� Servicio)
			CREATE TABLE Seguridad.RolOperacionAccion (
				RolOperacionAccionId INT IDENTITY(1,1) PRIMARY KEY,
				RolId INT,
				ServicioId INT,
				AccionId INT
			);
			

			ALTER TABLE Seguridad.RolOperacionAccion
			WITH CHECK ADD CONSTRAINT FK_RolOperacionAccion_Rol FOREIGN KEY (RolId)
			REFERENCES Seguridad.Rol (RolId);
			

			ALTER TABLE Seguridad.RolOperacionAccion
			WITH CHECK ADD CONSTRAINT FK_RolOperacionAccion_Servicio FOREIGN KEY (ServicioId)
			REFERENCES Configuracion.Servicio (ServicioId);
			

			ALTER TABLE Seguridad.RolOperacionAccion
			WITH CHECK ADD CONSTRAINT FK_RolOperacionAccion_Accion FOREIGN KEY (AccionId)
			REFERENCES Configuracion.Accion (AccionId);
			

			-- =========== NUEVOS ESQUEMAS PARA LA EMPRESA DE ROPA ===========
			
			IF SCHEMA_ID('Pages') IS NULL EXEC('CREATE SCHEMA Pages AUTHORIZATION dbo');

			-- =====================================================================
			-- TABLAS DEL NECIO PRINCIPAL (AUDITOR�A DE P�GINAS)
			-- =====================================================================

			-- Tabla para registrar las p�ginas web que se van a auditar
			CREATE TABLE Pages.AuditarPagina (
				AuditarPaginaId INT IDENTITY(1,1) PRIMARY KEY,
				UrlAuditarPagina VARCHAR(1700) NOT NULL UNIQUE,
				NombreAuditarPagina VARCHAR(255) NOT NULL, -- Un nombre amigable para la p�gina
				EstadoAuditarPagina BIT DEFAULT 'true',
				FechaCreacionAuditarPagina DATETIME DEFAULT GETDATE(),
				UsuarioId INT
			);
			

			ALTER TABLE Pages.AuditarPagina
			WITH CHECK ADD CONSTRAINT FK_AuditarPagina_Usuario FOREIGN KEY (UsuarioId)
			REFERENCES Seguridad.Usuario(UsuarioId);
			


			-- Tabla para almacenar cada registro de auditor�a con sus m�tricas de rendimiento
			CREATE TABLE Pages.AuditarLog (
				-- Columnas de Identificaci�n y Control
				AuditarLogId INT IDENTITY(1,1) PRIMARY KEY,
				AuditarPaginaId INT NOT NULL, -- FK a la tabla de p�ginas que auditas
				FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
				EstadoAuditarPagina BIT NOT NULL,
    
				-- M�trica Principal de Google
				PerformanceScore INT NULL, -- Puntuaci�n general de rendimiento (0-100)
    
				-- M�tricas de Tiempo Clave (las que solicitaste)
				TimeToFirstByteMs INT NULL,     -- TTFB (ms), representa la respuesta del servidor
				DomProcessingTimeMs INT NULL,   -- Tiempo de procesamiento del DOM (ms)
				PageLoadTimeMs INT NULL,        -- Tiempo hasta que la p�gina es interactiva (TTI)
    
				-- M�tricas Modernas (Core Web Vitals y otras)
				FcpValue VARCHAR(50) NULL,      -- Valor de First Contentful Paint (e.g., "1.2 s")
				LcpValue VARCHAR(50) NULL,      -- Valor de Largest Contentful Paint (e.g., "2.5 s")
				ClsValue VARCHAR(50) NULL,      -- Valor de Cumulative Layout Shift (e.g., "0.01")
				SpeedIndexValue VARCHAR(50) NULL -- Valor de Speed Index (e.g., "3.4 s")
    
				-- Podr�as a�adir tambi�n las puntuaciones de cada m�trica si lo necesitas
				-- FcpScore INT NULL,
				-- LcpScore INT NULL,
				-- ClsScore INT NULL
			);

			ALTER TABLE Pages.AuditarLog
			WITH CHECK ADD CONSTRAINT FK_AuditarLog_AuditarPagina FOREIGN KEY (AuditarPaginaId)
			REFERENCES Pages.AuditarPagina(AuditarPaginaId);



	COMMIT TRANSACTION;
		END TRY
		BEGIN CATCH
			IF @@TRANCOUNT > 0
				ROLLBACK TRANSACTION;

			DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
			DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
			DECLARE @ErrorState INT = ERROR_STATE();
			RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
			USE master
		END CATCH;
		
