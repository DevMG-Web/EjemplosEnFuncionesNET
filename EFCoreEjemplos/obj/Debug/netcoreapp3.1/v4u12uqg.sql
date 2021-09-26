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

