USE AuditarPague
GO
	BEGIN TRANSACTION;
		BEGIN TRY 

		select * from Seguridad.Rol

			INSERT INTO Seguridad.Rol (NombreRol, EstadoRol) VALUES ('Administrador', 1);
			INSERT INTO Seguridad.Rol (NombreRol, EstadoRol) VALUES ('Usuario', 1);

			INSERT INTO Seguridad.Usuario (NombreUsuario, ApellidoUsuario, DocumentoUsuario, CorreoUsuario, PasswordUsuario, RolId, EmailConfirmed)
			VALUES ('Admin', 'Principal', '123456789', 'admin@inspectia.com', '$2a$12$DfgseghSGSGEHhrthtrhth.u5nEcJfuP6rB4iG2b0gH.RzB4iG2b0', 1, 1);

			-- Insertar Acciones
			INSERT INTO Configuracion.Accion (NombreAccion) VALUES ('Crear'), ('Leer'), ('Actualizar'), ('Eliminar');

			
			-- Insertar Men�s
			INSERT INTO Configuracion.Menu (NombreMenu, UrlMenu, IconoMenu) VALUES ('Dashboard', '/Inicio', 'home');
			INSERT INTO Configuracion.Menu (NombreMenu, UrlMenu, IconoMenu) VALUES ('Paginas Auditadas', '/auditar', 'description');
			INSERT INTO Configuracion.Menu (NombreMenu, UrlMenu, IconoMenu) VALUES ('Gestion de Usuarios', '/usuarios', 'group');
			INSERT INTO Configuracion.Menu (NombreMenu, UrlMenu, IconoMenu) VALUES ('Auditar Paginas', '/auditar', 'search');

			-- Insertar Roles
			INSERT INTO Seguridad.Rol (NombreRol, EstadoRol) VALUES ('Administrador', 1);
			INSERT INTO Seguridad.Rol (NombreRol, EstadoRol) VALUES ('Usuario', 1);

			
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