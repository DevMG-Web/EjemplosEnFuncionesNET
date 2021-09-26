IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Estudiantes] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NULL,
    CONSTRAINT [PK_Estudiantes] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210123211922_Initial', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Estudiantes] ADD [apellido] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210123230756_modificacion', N'5.0.2');
GO

COMMIT;
GO

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

BEGIN TRANSACTION;
GO

CREATE TABLE [Direcciones] (
    [Id] int NOT NULL IDENTITY,
    [Calle] nvarchar(max) NULL,
    [EstudianteId] int NOT NULL,
    CONSTRAINT [PK_Direcciones] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Direcciones_Estudiantes_EstudianteId] FOREIGN KEY ([EstudianteId]) REFERENCES [Estudiantes] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_Direcciones_EstudianteId] ON [Direcciones] ([EstudianteId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210125212903_direcciones', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Estudiantes] ADD [InstitucionId] int NOT NULL DEFAULT 0;
GO

CREATE TABLE [Instituciones] (
    [Id] int NOT NULL IDENTITY,
    [Nombre] nvarchar(max) NULL,
    CONSTRAINT [PK_Instituciones] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_Estudiantes_InstitucionId] ON [Estudiantes] ([InstitucionId]);
GO

ALTER TABLE [Estudiantes] ADD CONSTRAINT [FK_Estudiantes_Instituciones_InstitucionId] FOREIGN KEY ([InstitucionId]) REFERENCES [Instituciones] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210126003751_Institucion', N'5.0.2');
GO

COMMIT;
GO

