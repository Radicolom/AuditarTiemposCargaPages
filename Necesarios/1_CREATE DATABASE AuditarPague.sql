-- Siempre es buena práctica asegurarse de estar en el contexto de 'master'
USE master;
GO

IF DB_ID('AuditarPague') IS NOT NULL
BEGIN
    ALTER DATABASE AuditarPague SET SINGLE_USER WITH ROLLBACK IMMEDIATE;

    DROP DATABASE AuditarPague;
    PRINT 'Base de datos AuditarPague existente fue eliminada.';
END
GO

CREATE DATABASE AuditarPague;
PRINT 'Base de datos AuditarPague creada exitosamente.';
GO
