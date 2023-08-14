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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230503114535_Initial')
BEGIN
    IF SCHEMA_ID(N'Security') IS NULL EXEC(N'CREATE SCHEMA [Security];');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230503114535_Initial')
BEGIN
    CREATE TABLE [Security].[Users] (
        [Id] bigint NOT NULL IDENTITY,
        [UserName] nvarchar(64) NOT NULL,
        [PasswordHash] nvarchar(64) NOT NULL,
        [Salt] nvarchar(64) NOT NULL,
        [AccessToken] nvarchar(64) NOT NULL,
        [Enabled] bit NOT NULL,
        [Role] int NOT NULL,
        [EmailAddress] nvarchar(128) NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230503114535_Initial')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessToken', N'EmailAddress', N'Enabled', N'PasswordHash', N'Role', N'Salt', N'UserName') AND [object_id] = OBJECT_ID(N'[Security].[Users]'))
        SET IDENTITY_INSERT [Security].[Users] ON;
    EXEC(N'INSERT INTO [Security].[Users] ([Id], [AccessToken], [EmailAddress], [Enabled], [PasswordHash], [Role], [Salt], [UserName])
    VALUES (CAST(1 AS bigint), N''a0f0a2ffd0f37c955fda023ed287c12fab375bfc0c3e58f96114c9eeb20066b0'', N''info@collini.it'', CAST(1 AS bit), N''d96eb91d532452667aff24191055ebba05d8a30853367821502a55ca4b1532db'', 0, N''f3064d73de0ca6b806ad24df65a59e1eb692393fc3f0b0297e37df522610b58b'', N''administrator'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessToken', N'EmailAddress', N'Enabled', N'PasswordHash', N'Role', N'Salt', N'UserName') AND [object_id] = OBJECT_ID(N'[Security].[Users]'))
        SET IDENTITY_INSERT [Security].[Users] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230503114535_Initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230503114535_Initial', N'7.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    IF SCHEMA_ID(N'Docs') IS NULL EXEC(N'CREATE SCHEMA [Docs];');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    IF SCHEMA_ID(N'Registry') IS NULL EXEC(N'CREATE SCHEMA [Registry];');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    ALTER TABLE [Security].[Users] ADD [ColorHex] nvarchar(16) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    ALTER TABLE [Security].[Users] ADD [Name] nvarchar(128) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    ALTER TABLE [Security].[Users] ADD [Surname] nvarchar(128) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    CREATE TABLE [Registry].[Contacts] (
        [Id] bigint NOT NULL IDENTITY,
        [Type] int NOT NULL,
        [CompanyName] nvarchar(256) NULL,
        [Name] nvarchar(64) NULL,
        [Surname] nvarchar(64) NULL,
        [FiscalType] int NOT NULL,
        [ErpCode] nvarchar(16) NULL,
        [Alert] bit NOT NULL,
        [CreatedOn] datetimeoffset(3) NOT NULL,
        [CreatedBy] nvarchar(max) NULL,
        [CreatedById] bigint NULL,
        [EditedOn] datetimeoffset(3) NULL,
        [EditedBy] nvarchar(max) NULL,
        [EditedById] bigint NULL,
        [DeletedOn] datetimeoffset(3) NULL,
        [DeletedBy] nvarchar(max) NULL,
        [DeletedById] bigint NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_Contacts] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    CREATE TABLE [Registry].[JobSources] (
        [Id] bigint NOT NULL IDENTITY,
        [Name] nvarchar(128) NOT NULL,
        CONSTRAINT [PK_JobSources] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    CREATE TABLE [Registry].[ProductTypes] (
        [Id] bigint NOT NULL IDENTITY,
        [Name] nvarchar(128) NOT NULL,
        CONSTRAINT [PK_ProductTypes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    CREATE TABLE [Registry].[ContactAddresses] (
        [Id] bigint NOT NULL IDENTITY,
        [Description] nvarchar(256) NULL,
        [City] nvarchar(128) NOT NULL,
        [StreetAddress] nvarchar(256) NOT NULL,
        [Province] nvarchar(128) NOT NULL,
        [ZipCode] nvarchar(16) NOT NULL,
        [Telephone] nvarchar(32) NULL,
        [Email] nvarchar(128) NULL,
        [IsMainAddress] bit NOT NULL,
        [ContactId] bigint NOT NULL,
        [CreatedOn] datetimeoffset(3) NOT NULL,
        [CreatedBy] nvarchar(max) NULL,
        [CreatedById] bigint NULL,
        [EditedOn] datetimeoffset(3) NULL,
        [EditedBy] nvarchar(max) NULL,
        [EditedById] bigint NULL,
        [DeletedOn] datetimeoffset(3) NULL,
        [DeletedBy] nvarchar(max) NULL,
        [DeletedById] bigint NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_ContactAddresses] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ContactAddresses_Contacts_ContactId] FOREIGN KEY ([ContactId]) REFERENCES [Registry].[Contacts] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    CREATE TABLE [Docs].[Jobs] (
        [Id] bigint NOT NULL IDENTITY,
        [Number] int NOT NULL,
        [Year] int NOT NULL,
        [ExpirationDate] datetimeoffset(3) NULL,
        [Description] nvarchar(max) NOT NULL,
        [Status] int NOT NULL,
        [StatusChangedOn] datetimeoffset(3) NULL,
        [CustomerId] bigint NOT NULL,
        [CustomerAddressId] bigint NOT NULL,
        [SourceId] bigint NOT NULL,
        [ProductTypeId] bigint NOT NULL,
        [CreatedOn] datetimeoffset(3) NOT NULL,
        [CreatedBy] nvarchar(max) NULL,
        [CreatedById] bigint NULL,
        [EditedOn] datetimeoffset(3) NULL,
        [EditedBy] nvarchar(max) NULL,
        [EditedById] bigint NULL,
        [DeletedOn] datetimeoffset(3) NULL,
        [DeletedBy] nvarchar(max) NULL,
        [DeletedById] bigint NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_Jobs] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Jobs_ContactAddresses_CustomerAddressId] FOREIGN KEY ([CustomerAddressId]) REFERENCES [Registry].[ContactAddresses] ([Id]),
        CONSTRAINT [FK_Jobs_Contacts_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Registry].[Contacts] ([Id]),
        CONSTRAINT [FK_Jobs_JobSources_SourceId] FOREIGN KEY ([SourceId]) REFERENCES [Registry].[JobSources] ([Id]),
        CONSTRAINT [FK_Jobs_ProductTypes_ProductTypeId] FOREIGN KEY ([ProductTypeId]) REFERENCES [Registry].[ProductTypes] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    CREATE TABLE [Docs].[Activities] (
        [Id] bigint NOT NULL IDENTITY,
        [Description] nvarchar(max) NOT NULL,
        [Start] datetimeoffset(3) NOT NULL,
        [End] datetimeoffset(3) NOT NULL,
        [Status] int NOT NULL,
        [StatusChangedOn] datetimeoffset(3) NULL,
        [OperatorId] bigint NOT NULL,
        [JobId] bigint NOT NULL,
        [CreatedOn] datetimeoffset(3) NOT NULL,
        [CreatedBy] nvarchar(max) NULL,
        [CreatedById] bigint NULL,
        [EditedOn] datetimeoffset(3) NULL,
        [EditedBy] nvarchar(max) NULL,
        [EditedById] bigint NULL,
        [DeletedOn] datetimeoffset(3) NULL,
        [DeletedBy] nvarchar(max) NULL,
        [DeletedById] bigint NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_Activities] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Activities_Jobs_JobId] FOREIGN KEY ([JobId]) REFERENCES [Docs].[Jobs] ([Id]),
        CONSTRAINT [FK_Activities_Users_OperatorId] FOREIGN KEY ([OperatorId]) REFERENCES [Security].[Users] ([Id]) ON DELETE NO ACTION
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    CREATE TABLE [Docs].[Orders] (
        [Id] bigint NOT NULL IDENTITY,
        [Code] nvarchar(16) NULL,
        [Description] nvarchar(max) NULL,
        [ExpirationDate] datetimeoffset(3) NULL,
        [Status] int NOT NULL,
        [StatusChangedOn] datetimeoffset(3) NULL,
        [JobId] bigint NOT NULL,
        [SupplierId] bigint NOT NULL,
        [CreatedOn] datetimeoffset(3) NOT NULL,
        [CreatedBy] nvarchar(max) NULL,
        [CreatedById] bigint NULL,
        [EditedOn] datetimeoffset(3) NULL,
        [EditedBy] nvarchar(max) NULL,
        [EditedById] bigint NULL,
        [DeletedOn] datetimeoffset(3) NULL,
        [DeletedBy] nvarchar(max) NULL,
        [DeletedById] bigint NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Orders_Contacts_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [Registry].[Contacts] ([Id]),
        CONSTRAINT [FK_Orders_Jobs_JobId] FOREIGN KEY ([JobId]) REFERENCES [Docs].[Jobs] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    CREATE TABLE [Docs].[Quotations] (
        [Id] bigint NOT NULL IDENTITY,
        [Amount] decimal(14,2) NOT NULL,
        [ExpirationDate] datetimeoffset(3) NULL,
        [Status] int NOT NULL,
        [StatusChangedOn] datetimeoffset(3) NULL,
        [JobId] bigint NOT NULL,
        [CreatedOn] datetimeoffset(3) NOT NULL,
        [CreatedBy] nvarchar(max) NULL,
        [CreatedById] bigint NULL,
        [EditedOn] datetimeoffset(3) NULL,
        [EditedBy] nvarchar(max) NULL,
        [EditedById] bigint NULL,
        [DeletedOn] datetimeoffset(3) NULL,
        [DeletedBy] nvarchar(max) NULL,
        [DeletedById] bigint NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_Quotations] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Quotations_Jobs_JobId] FOREIGN KEY ([JobId]) REFERENCES [Docs].[Jobs] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    CREATE TABLE [Docs].[Notes] (
        [Id] bigint NOT NULL IDENTITY,
        [Value] nvarchar(max) NOT NULL,
        [JobId] bigint NULL,
        [OrderId] bigint NULL,
        [QuotationId] bigint NULL,
        [ActivityId] bigint NULL,
        [CreatedOn] datetimeoffset(3) NOT NULL,
        [CreatedBy] nvarchar(max) NULL,
        [CreatedById] bigint NULL,
        [EditedOn] datetimeoffset(3) NULL,
        [EditedBy] nvarchar(max) NULL,
        [EditedById] bigint NULL,
        [DeletedOn] datetimeoffset(3) NULL,
        [DeletedBy] nvarchar(max) NULL,
        [DeletedById] bigint NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_Notes] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Notes_Activities_ActivityId] FOREIGN KEY ([ActivityId]) REFERENCES [Docs].[Activities] ([Id]),
        CONSTRAINT [FK_Notes_Jobs_JobId] FOREIGN KEY ([JobId]) REFERENCES [Docs].[Jobs] ([Id]),
        CONSTRAINT [FK_Notes_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Docs].[Orders] ([Id]),
        CONSTRAINT [FK_Notes_Quotations_QuotationId] FOREIGN KEY ([QuotationId]) REFERENCES [Docs].[Quotations] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    CREATE TABLE [Docs].[NoteAttachments] (
        [Id] bigint NOT NULL IDENTITY,
        [DisplayName] nvarchar(256) NOT NULL,
        [FileName] nvarchar(64) NOT NULL,
        [NoteId] bigint NOT NULL,
        [CreatedOn] datetimeoffset(3) NOT NULL,
        [CreatedBy] nvarchar(max) NULL,
        [CreatedById] bigint NULL,
        [EditedOn] datetimeoffset(3) NULL,
        [EditedBy] nvarchar(max) NULL,
        [EditedById] bigint NULL,
        [DeletedOn] datetimeoffset(3) NULL,
        [DeletedBy] nvarchar(max) NULL,
        [DeletedById] bigint NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_NoteAttachments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_NoteAttachments_Notes_NoteId] FOREIGN KEY ([NoteId]) REFERENCES [Docs].[Notes] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    EXEC(N'UPDATE [Security].[Users] SET [ColorHex] = NULL, [Name] = NULL, [Surname] = NULL
    WHERE [Id] = CAST(1 AS bigint);
    SELECT @@ROWCOUNT');
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    CREATE INDEX [IX_Activities_JobId] ON [Docs].[Activities] ([JobId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    CREATE INDEX [IX_Activities_OperatorId] ON [Docs].[Activities] ([OperatorId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    CREATE INDEX [IX_ContactAddresses_ContactId] ON [Registry].[ContactAddresses] ([ContactId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    CREATE INDEX [IX_Jobs_CustomerAddressId] ON [Docs].[Jobs] ([CustomerAddressId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    CREATE INDEX [IX_Jobs_CustomerId] ON [Docs].[Jobs] ([CustomerId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    CREATE INDEX [IX_Jobs_ProductTypeId] ON [Docs].[Jobs] ([ProductTypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    CREATE INDEX [IX_Jobs_SourceId] ON [Docs].[Jobs] ([SourceId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    CREATE INDEX [IX_NoteAttachments_NoteId] ON [Docs].[NoteAttachments] ([NoteId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    CREATE INDEX [IX_Notes_ActivityId] ON [Docs].[Notes] ([ActivityId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    CREATE INDEX [IX_Notes_JobId] ON [Docs].[Notes] ([JobId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    CREATE INDEX [IX_Notes_OrderId] ON [Docs].[Notes] ([OrderId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    CREATE INDEX [IX_Notes_QuotationId] ON [Docs].[Notes] ([QuotationId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    CREATE INDEX [IX_Orders_JobId] ON [Docs].[Orders] ([JobId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    CREATE INDEX [IX_Orders_SupplierId] ON [Docs].[Orders] ([SupplierId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    CREATE INDEX [IX_Quotations_JobId] ON [Docs].[Quotations] ([JobId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230510161705_Add_Domain_Entities')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230510161705_Add_Domain_Entities', N'7.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230718123251_release1')
BEGIN
    update docs.jobs set ExpirationDate = getUtcDate() where ExpirationDate is null;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230718123251_release1')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Docs].[Jobs]') AND [c].[name] = N'ExpirationDate');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Docs].[Jobs] DROP CONSTRAINT [' + @var0 + '];');
    EXEC(N'UPDATE [Docs].[Jobs] SET [ExpirationDate] = ''0001-01-01T00:00:00.000+00:00'' WHERE [ExpirationDate] IS NULL');
    ALTER TABLE [Docs].[Jobs] ALTER COLUMN [ExpirationDate] datetimeoffset(3) NOT NULL;
    ALTER TABLE [Docs].[Jobs] ADD DEFAULT '0001-01-01T00:00:00.000+00:00' FOR [ExpirationDate];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230718123251_release1')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Docs].[Jobs]') AND [c].[name] = N'CustomerAddressId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Docs].[Jobs] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Docs].[Jobs] ALTER COLUMN [CustomerAddressId] bigint NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230718123251_release1')
BEGIN
    ALTER TABLE [Docs].[Jobs] ADD [JobDate] datetimeoffset(3) NOT NULL DEFAULT '0001-01-01T00:00:00.000+00:00';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230718123251_release1')
BEGIN
    update docs.jobs set jobDate = createdOn;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230718123251_release1')
BEGIN
    ALTER TABLE [Registry].[Contacts] ADD [Email] nvarchar(128) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230718123251_release1')
BEGIN
    ALTER TABLE [Registry].[Contacts] ADD [Telephone] nvarchar(32) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230718123251_release1')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230718123251_release1', N'7.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230728092843_release2')
BEGIN
    ALTER TABLE [Docs].[Jobs] ADD [ResultNote] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230728092843_release2')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Registry].[ContactAddresses]') AND [c].[name] = N'ZipCode');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Registry].[ContactAddresses] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Registry].[ContactAddresses] ALTER COLUMN [ZipCode] nvarchar(16) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230728092843_release2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230728092843_release2', N'7.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230814072650_release3')
BEGIN
    CREATE TABLE [Docs].[QuotationAttachments] (
        [Id] bigint NOT NULL IDENTITY,
        [DisplayName] nvarchar(256) NOT NULL,
        [FileName] nvarchar(64) NOT NULL,
        [QuotationId] bigint NOT NULL,
        [CreatedOn] datetimeoffset(3) NOT NULL,
        [CreatedBy] nvarchar(max) NULL,
        [CreatedById] bigint NULL,
        [EditedOn] datetimeoffset(3) NULL,
        [EditedBy] nvarchar(max) NULL,
        [EditedById] bigint NULL,
        [DeletedOn] datetimeoffset(3) NULL,
        [DeletedBy] nvarchar(max) NULL,
        [DeletedById] bigint NULL,
        [IsDeleted] bit NOT NULL,
        CONSTRAINT [PK_QuotationAttachments] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_QuotationAttachments_Quotations_QuotationId] FOREIGN KEY ([QuotationId]) REFERENCES [Docs].[Quotations] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230814072650_release3')
BEGIN
    CREATE UNIQUE INDEX [IX_QuotationAttachments_QuotationId] ON [Docs].[QuotationAttachments] ([QuotationId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230814072650_release3')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230814072650_release3', N'7.0.5');
END;
GO

COMMIT;
GO

