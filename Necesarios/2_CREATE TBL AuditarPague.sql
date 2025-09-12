USE AuditarPague;

	BEGIN TRANSACTION; 
		BEGIN TRY

			 -- Creaci?n de esquemas
			-- Se usa EXEC para ejecutar cada CREATE SCHEMA en su propio lote y se especifica AUTHORIZATION dbo para evitar conflictos.
			IF SCHEMA_ID('Seguridad') IS NULL EXEC('CREATE SCHEMA Seguridad AUTHORIZATION dbo');
			IF SCHEMA_ID('Configuracion') IS NULL EXEC('CREATE SCHEMA Configuracion AUTHORIZATION dbo');

			--drop table Seguridad.Rol
			--drop table Seguridad.Usuario
			--drop table Configuracion.Menu
			--drop table Configuracion.MenuRol
			--drop table Configuracion.Accion
			--drop table Configuracion.Servicio
			--drop table Seguridad.RolOperacionAccion
			--drop table Pages.AuditarPagina
			--drop table Pages.AuditarLog

			IF SCHEMA_ID('Seguridad') IS NULL EXEC('CREATE SCHEMA Seguridad AUTHORIZATION dbo');

IF SCHEMA_ID('Configuracion') IS NULL EXEC('CREATE SCHEMA Configuracion AUTHORIZATION dbo');

IF SCHEMA_ID('Pages') IS NULL EXEC('CREATE SCHEMA Pages AUTHORIZATION dbo');

 
CREATE TABLE [Configuracion].[Accion] (

    [AccionId] INT IDENTITY(1,1) NOT NULL,
    [NombreAccion] VARCHAR(50) NOT NULL
    CONSTRAINT [PK__Accion__A60CAFC7F29FA1AC] PRIMARY KEY ([AccionId])
);


CREATE TABLE [Configuracion].[Menu] (

    [MenuId] INT IDENTITY(1,1) NOT NULL,
    [NombreMenu] VARCHAR(150) NOT NULL,
    [UrlMenu] VARCHAR(MAX) NULL,
    [IconoMenu] VARCHAR(500) NULL,
    [EstadoMenu] BIT NULL
    CONSTRAINT [PK__Menu__C99ED230FF7C249D] PRIMARY KEY ([MenuId])
);


CREATE TABLE [Configuracion].[MenuRol] (

    [MenuRolId] INT IDENTITY(1,1) NOT NULL,
    [MenuId] INT NULL,
    [RolId] INT NULL
    CONSTRAINT [PK__MenuRol__6640AD0C9286098E] PRIMARY KEY ([MenuRolId])
);


CREATE TABLE [Configuracion].[Servicio] (

    [ServicioId] INT IDENTITY(1,1) NOT NULL,
    [NombreServicio] VARCHAR(100) NOT NULL
    CONSTRAINT [PK__Servicio__D5AEECC214E29B7B] PRIMARY KEY ([ServicioId])
);

CREATE TABLE [Seguridad].[Rol] (

    [RolId] INT IDENTITY(1,1) NOT NULL,
    [NombreRol] VARCHAR(100) NOT NULL,
    [EstadoRol] BIT NULL
    CONSTRAINT [PK__Rol__F92302F135AE38BB] PRIMARY KEY ([RolId])
);


CREATE TABLE [Seguridad].[RolOperacionAccion] (

    [RolOperacionAccionId] INT IDENTITY(1,1) NOT NULL,
    [RolId] INT NULL,
    [ServicioId] INT NULL,
    [AccionId] INT NULL
    CONSTRAINT [PK__RolOpera__D2B227DECE877EBC] PRIMARY KEY ([RolOperacionAccionId])
);


CREATE TABLE [Seguridad].[Usuario] (

    [UsuarioId] INT IDENTITY(1,1) NOT NULL,
    [NombreUsuario] VARCHAR(150) NOT NULL,
    [ApellidoUsuario] VARCHAR(150) NOT NULL,
    [DocumentoUsuario] VARCHAR(20) NOT NULL,
    [CorreoUsuario] VARCHAR(255) NOT NULL,
    [EmailConfirmed] BIT NULL,
    [PasswordUsuario] VARCHAR(MAX) NOT NULL,
    [TelefonoUsuario] VARCHAR(12) NULL,
    [TelefonoConfirmadoUsuario] BIT NULL,
    [AutenticacionDobleFactor] BIT NULL,
    [AutenticacionIntentos] INT NULL,
    [EstadoUsuario] BIT NULL,
    [RolId] INT NULL
    CONSTRAINT [PK__Usuario__2B3DE7B81C0B815A] PRIMARY KEY ([UsuarioId])
);


CREATE TABLE [Pages].[AuditarPagina] (

    [AuditarPaginaId] INT IDENTITY(1,1) NOT NULL,
    [UrlAuditarPagina] VARCHAR(1700) NOT NULL,
    [NombreAuditarPagina] VARCHAR(255) NOT NULL,
    [FechaCreacionAuditarPagina]  DATETIME NULL DEFAULT GETDATE(),
    [EstadoAuditarPagina] BIT DEFAULT 'True',
    [UsuarioId] INT NULL,
    CONSTRAINT [PK__AuditarP__22CAB7527470E753] PRIMARY KEY ([AuditarPaginaId])
);


CREATE TABLE [Pages].[AuditarLog] (

    [AuditarLogId] INT IDENTITY(1,1) NOT NULL,
    [AuditarPaginaId] INT NOT NULL,
    [FechaCreacion]  DATETIME NOT NULL DEFAULT GETDATE(),
    [EstadoAuditarPagina] BIT NOT NULL,
    [PerformanceScore] INT NULL,
    [TimeToFirstByteMs] INT NULL,
    [DomProcessingTimeMs] INT NULL,
    [PageLoadTimeMs] INT NULL,
    [FcpValue] VARCHAR(50) NULL,
    [LcpValue] VARCHAR(50) NULL,
    [ClsValue] VARCHAR(50) NULL,
    [SpeedIndexValue] VARCHAR(50) NULL
    CONSTRAINT [PK__AuditarL__B5AFA16C40361FC4] PRIMARY KEY ([AuditarLogId])
);




ALTER TABLE [Pages].[AuditarPagina] ADD CONSTRAINT [UQ__AuditarP__3E2798B2A7EDF1C0] UNIQUE ([UrlAuditarPagina]);

ALTER TABLE [Seguridad].[Usuario] ADD CONSTRAINT [UQ__Usuario__365498782462CAFA] UNIQUE ([CorreoUsuario]);

ALTER TABLE [Seguridad].[Usuario] ADD CONSTRAINT [UQ__Usuario__ACE86E55C9CCE580] UNIQUE ([DocumentoUsuario]);

 
ALTER TABLE [Seguridad].[RolOperacionAccion] WITH CHECK ADD CONSTRAINT [FK_RolOperacionAccion_Rol] FOREIGN KEY([RolId]) REFERENCES [Seguridad].[Rol]([RolId]);

ALTER TABLE [Seguridad].[RolOperacionAccion] WITH CHECK ADD CONSTRAINT [FK_RolOperacionAccion_Servicio] FOREIGN KEY([ServicioId]) REFERENCES [Configuracion].[Servicio]([ServicioId]);

ALTER TABLE [Seguridad].[RolOperacionAccion] WITH CHECK ADD CONSTRAINT [FK_RolOperacionAccion_Accion] FOREIGN KEY([AccionId]) REFERENCES [Configuracion].[Accion]([AccionId]);

ALTER TABLE [Seguridad].[Usuario] WITH CHECK ADD CONSTRAINT [FK_Usuario_Rol] FOREIGN KEY([RolId]) REFERENCES [Seguridad].[Rol]([RolId]);

ALTER TABLE [Configuracion].[MenuRol] WITH CHECK ADD CONSTRAINT [FK_MenuRol_Rol] FOREIGN KEY([RolId]) REFERENCES [Seguridad].[Rol]([RolId]);

ALTER TABLE [Configuracion].[MenuRol] WITH CHECK ADD CONSTRAINT [FK_MenuRol_Menu] FOREIGN KEY([MenuId]) REFERENCES [Configuracion].[Menu]([MenuId]);

ALTER TABLE [Pages].[AuditarPagina] WITH CHECK ADD CONSTRAINT [FK_AuditarPagina_Usuario] FOREIGN KEY([UsuarioId]) REFERENCES [Seguridad].[Usuario]([UsuarioId]);

ALTER TABLE [Pages].[AuditarLog] WITH CHECK ADD CONSTRAINT [FK_AuditarLog_AuditarPagina] FOREIGN KEY([AuditarPaginaId]) REFERENCES [Pages].[AuditarPagina]([AuditarPaginaId]);





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
		
