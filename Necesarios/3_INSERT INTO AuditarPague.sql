USE AuditarPague
GO
	BEGIN TRANSACTION;
		BEGIN TRY 

		
			-- Insertar Roles
			INSERT INTO Seguridad.Rol (NombreRol, EstadoRol) VALUES ('Administrador', 1);
			INSERT INTO Seguridad.Rol (NombreRol, EstadoRol) VALUES ('Usuario', 1);

			INSERT INTO Seguridad.Usuario (NombreUsuario, ApellidoUsuario, DocumentoUsuario, CorreoUsuario, PasswordUsuario, RolId, EmailConfirmed)
			VALUES ('Admin', 'Principal', '123456789', 'Administrador@admin.com', '$2a$11$.7tOwnQLkkT9FNocj2ZEhem49xa5XUI042l.nOvqfwjFrZxfQmG1S', 1, 1);

			-- Insertar Acciones
			INSERT INTO Configuracion.Accion (NombreAccion) VALUES ('Crear'), ('Leer'), ('Actualizar'), ('Eliminar');

			
			-- Insertar Men�s
			SET IDENTITY_INSERT Configuracion.Menu ON;

			INSERT INTO Configuracion.Menu (MenuId, NombreMenu, UrlMenu, IconoMenu, EstadoMenu) VALUES (1,'Dashboard', '/', 'home',1);
			INSERT INTO Configuracion.Menu (MenuId, NombreMenu, UrlMenu, IconoMenu, EstadoMenu) VALUES (2,'Gestión Paginas Auditadar', '/GestionPaguesAuditar', 'description',1);
			INSERT INTO Configuracion.Menu (MenuId, NombreMenu, UrlMenu, IconoMenu, EstadoMenu) VALUES (3,'Gestión de Usuarios', '/GestionUnsuarios', 'group',1);
			INSERT INTO Configuracion.Menu (MenuId, NombreMenu, UrlMenu, IconoMenu, EstadoMenu) VALUES (4,'Auditar Paginas', '/AuditarPague', 'search',1);

			SET IDENTITY_INSERT Configuracion.Menu OFF;

			delete Configuracion.Menu
			select * from Configuracion.Accion
			select * from Configuracion.Menu
			select * from Seguridad.Rol

	COMMIT TRANSACTION;
		END TRY 
		BEGIN CATCH 
			BEGIN 
				ROLLBACK TRANSACTION;
				SELECT ERROR_MESSAGE()
			END 
		END CATCH