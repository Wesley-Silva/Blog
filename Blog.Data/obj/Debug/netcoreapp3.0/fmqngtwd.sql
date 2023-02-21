IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Categorias] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(200) NOT NULL,
    CONSTRAINT [PK_Categorias] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Posts] (
    [Id] uniqueidentifier NOT NULL,
    [CategoriaId] uniqueidentifier NOT NULL,
    [Titulo] varchar(200) NOT NULL,
    [Corpo] varchar(5000) NOT NULL,
    [CriadoEm] datetime2 NOT NULL,
    [AtualizadoEm] datetime2 NOT NULL,
    [Imagem] varchar(100) NOT NULL,
    CONSTRAINT [PK_Posts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Posts_Categorias_CategoriaId] FOREIGN KEY ([CategoriaId]) REFERENCES [Categorias] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Posts_CategoriaId] ON [Posts] ([CategoriaId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200416005549_Initial', N'3.0.0');

GO

ALTER TABLE [Posts] ADD [AutorId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200429224235_ColunaAutorId', N'3.0.0');

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Posts]') AND [c].[name] = N'AutorId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Posts] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Posts] DROP COLUMN [AutorId];

GO

ALTER TABLE [Posts] ADD [Email] varchar(100) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200508013602_tblcolunaEmail', N'3.0.0');

GO

CREATE TABLE [Comentarios] (
    [Id] uniqueidentifier NOT NULL,
    [Corpo] varchar(5000) NOT NULL,
    [Criado] datetime2 NOT NULL,
    [PostId] uniqueidentifier NOT NULL,
    [UsuarioId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Comentarios] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Comentarios_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Comentarios_PostId] ON [Comentarios] ([PostId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200512140447_CriadoTblComentarios', N'3.0.0');

GO

