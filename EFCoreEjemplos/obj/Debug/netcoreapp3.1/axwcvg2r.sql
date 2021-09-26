BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Estudiantes].[apellido]', N'Apellido', N'COLUMN';
GO

ALTER TABLE [Estudiantes] ADD [Edad] int NOT NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210124044248_Estudiante-Agrego-Edad', N'5.0.2');
GO

COMMIT;
GO

