USE [MyMeetings];
GO

IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'MyMeetings')
    BEGIN
        ALTER DATABASE [MyMeetings]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL 
            WITH ROLLBACK IMMEDIATE;
    END
GO

IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'MyMeetings')
    BEGIN
        ALTER DATABASE [MyMeetings]
            SET PAGE_VERIFY NONE,
                DISABLE_BROKER 
            WITH ROLLBACK IMMEDIATE;
    END
GO

ALTER DATABASE [MyMeetings]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;
GO

IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'MyMeetings')
    BEGIN
        ALTER DATABASE [MyMeetings]
            SET QUERY_STORE (QUERY_CAPTURE_MODE = ALL, MAX_STORAGE_SIZE_MB = 100) 
            WITH ROLLBACK IMMEDIATE;
    END
GO

IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'MyMeetings')
    BEGIN
        ALTER DATABASE [MyMeetings]
            SET QUERY_STORE = OFF 
            WITH ROLLBACK IMMEDIATE;
    END
GO

PRINT N'Creating Schema [UserRegistrations]...';
GO

CREATE SCHEMA [UserRegistrations]
    AUTHORIZATION [dbo];
GO

PRINT N'Creating Schema [Users]...';
GO

CREATE SCHEMA [Users]
    AUTHORIZATION [dbo];
GO

PRINT N'Creating Table [UserRegistrations].[InboxMessages]...';
GO

CREATE TABLE [UserRegistrations].[InboxMessages] (
    [ClusterKey]    INT              IDENTITY (1, 1) NOT NULL,
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [OccurredOn]    DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    CONSTRAINT [PK_Registrations_InboxMessages_Id] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Index [UserRegistrations].[InboxMessages].[IX_Registrations_InboxMessages_OccurredOn_ClusterKey]...';
GO

CREATE UNIQUE CLUSTERED INDEX [IX_Registrations_InboxMessages_OccurredOn_ClusterKey]
    ON [UserRegistrations].[InboxMessages]([OccurredOn] ASC, [ClusterKey] ASC);
GO

PRINT N'Creating Index [UserRegistrations].[InboxMessages].[IX_Registrations_InboxMessages_ProcessedDate]...';
GO

CREATE NONCLUSTERED INDEX [IX_Registrations_InboxMessages_ProcessedDate]
    ON [UserRegistrations].[InboxMessages]([ProcessedDate] ASC) WHERE ProcessedDate IS NULL;
GO

PRINT N'Creating Table [UserRegistrations].[OutboxMessages]...';
GO

CREATE TABLE [UserRegistrations].[OutboxMessages] (
    [ClusterKey]    INT              IDENTITY (1, 1) NOT NULL,
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [OccurredOn]    DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    CONSTRAINT [PK_Registrations_OutboxMessages_Id] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Index [UserRegistrations].[OutboxMessages].[IX_Registrations_OutboxMessages_OccurredOn_ClusterKey]...';
GO

CREATE UNIQUE CLUSTERED INDEX [IX_Registrations_OutboxMessages_OccurredOn_ClusterKey]
    ON [UserRegistrations].[OutboxMessages]([OccurredOn] ASC, [ClusterKey] ASC);
GO

PRINT N'Creating Index [UserRegistrations].[OutboxMessages].[IX_Registrations_OutboxMessages_ProcessedDate]...';
GO

CREATE NONCLUSTERED INDEX [IX_Registrations_OutboxMessages_ProcessedDate]
    ON [UserRegistrations].[OutboxMessages]([ProcessedDate] ASC) WHERE ProcessedDate IS NULL;
GO

PRINT N'Creating Table [UserRegistrations].[InternalCommands]...';
GO

CREATE TABLE [UserRegistrations].[InternalCommands] (
    [ClusterKey]    INT              IDENTITY (1, 1) NOT NULL,
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [EnqueueDate]   DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    [Error]         NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_Registrations_InternalCommands_Id] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Index [UserRegistrations].[InternalCommands].[IX_Registrations_InternalCommands_EnqueueDate_ClusterKey]...';
GO

CREATE UNIQUE CLUSTERED INDEX [IX_Registrations_InternalCommands_EnqueueDate_ClusterKey]
    ON [UserRegistrations].[InternalCommands]([EnqueueDate] ASC, [ClusterKey] ASC);
GO

PRINT N'Creating Index [UserRegistrations].[InternalCommands].[IX_Registrations_InternalCommands_ProcessedDate]...';
GO

CREATE NONCLUSTERED INDEX [IX_Registrations_InternalCommands_ProcessedDate]
    ON [UserRegistrations].[InternalCommands]([ProcessedDate] ASC) WHERE ProcessedDate IS NULL;
GO

PRINT N'Creating Table [UserRegistrations].[UserRegistrations]...';
GO

CREATE TABLE [UserRegistrations].[UserRegistrations] (
    [ClusterKey]    INT              IDENTITY (1, 1) NOT NULL,
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [Email]         NVARCHAR (255)   NOT NULL,
    [Password]      NVARCHAR (255)   NOT NULL,
    [FirstName]     NVARCHAR (50)    NOT NULL,
    [LastName]      NVARCHAR (50)    NOT NULL,
    [Name]          NVARCHAR (255)   NOT NULL,
    [StatusCode]    VARCHAR (50)     NOT NULL,
    [RegisterDate]  DATETIME2 (7)    NOT NULL,
    [ConfirmedDate] DATETIME2 (7)    NULL,
    CONSTRAINT [PK_Registrations_UserRegistrations_Id] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Index [UserRegistrations].[UserRegistrations].[IX_Registrations_UserRegistrations_ClusterKey]...';
GO

CREATE UNIQUE CLUSTERED INDEX [IX_Registrations_UserRegistrations_ClusterKey]
    ON [UserRegistrations].[UserRegistrations]([ClusterKey] ASC);
GO

PRINT N'Creating Table [Users].[OutboxMessages]...';
GO

CREATE TABLE [Users].[OutboxMessages] (
    [ClusterKey]    INT              IDENTITY (1, 1) NOT NULL,
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [OccurredOn]    DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    CONSTRAINT [PK_Users_OutboxMessages_Id] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Index [Users].[OutboxMessages].[IX_Users_OutboxMessages_OccurredOn_ClusterKey]...';
GO

CREATE UNIQUE CLUSTERED INDEX [IX_Users_OutboxMessages_OccurredOn_ClusterKey]
    ON [Users].[OutboxMessages]([OccurredOn] ASC, [ClusterKey] ASC);
GO

PRINT N'Creating Index [Users].[OutboxMessages].[IX_Users_OutboxMessages_ProcessedDate]...';
GO

CREATE NONCLUSTERED INDEX [IX_Users_OutboxMessages_ProcessedDate]
    ON [Users].[OutboxMessages]([ProcessedDate] ASC) WHERE ProcessedDate IS NULL;
GO

PRINT N'Creating Table [Users].[Permissions]...';
GO

CREATE TABLE [Users].[Permissions] (
    [ClusterKey]  INT           IDENTITY (1, 1) NOT NULL,
    [Code]        VARCHAR (50)  NOT NULL,
    [Name]        VARCHAR (100) NOT NULL,
    [Description] VARCHAR (255) NULL,
    CONSTRAINT [PK_Users_Permissions_Code] PRIMARY KEY NONCLUSTERED ([Code] ASC)
);
GO

PRINT N'Creating Index [Users].[Permissions].[IX_Users_Permissions_ClusterKey]...';
GO

CREATE UNIQUE CLUSTERED INDEX [IX_Users_Permissions_ClusterKey]
    ON [Users].[Permissions]([ClusterKey] ASC);
GO

PRINT N'Creating Table [Users].[InternalCommands]...';
GO

CREATE TABLE [Users].[InternalCommands] (
    [ClusterKey]    INT              IDENTITY (1, 1) NOT NULL,
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [EnqueueDate]   DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    [Error]         NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_Users_InternalCommands_Id] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Index [Users].[InternalCommands].[IX_Users_InternalCommands_EnqueueDate_ClusterKey]...';
GO

CREATE UNIQUE CLUSTERED INDEX [IX_Users_InternalCommands_EnqueueDate_ClusterKey]
    ON [Users].[InternalCommands]([EnqueueDate] ASC, [ClusterKey] ASC);
GO

PRINT N'Creating Index [Users].[InternalCommands].[IX_Users_InternalCommands_ProcessedDate]...';
GO

CREATE NONCLUSTERED INDEX [IX_Users_InternalCommands_ProcessedDate]
    ON [Users].[InternalCommands]([ProcessedDate] ASC) WHERE ProcessedDate IS NULL;
GO

PRINT N'Creating Table [Users].[InboxMessages]...';
GO

CREATE TABLE [Users].[InboxMessages] (
    [ClusterKey]    INT              IDENTITY (1, 1) NOT NULL,
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [OccurredOn]    DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    CONSTRAINT [PK_Users_InboxMessages_Id] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Index [Users].[InboxMessages].[IX_Users_InboxMessages_OccurredOn_ClusterKey]...';
GO

CREATE UNIQUE CLUSTERED INDEX [IX_Users_InboxMessages_OccurredOn_ClusterKey]
    ON [Users].[InboxMessages]([OccurredOn] ASC, [ClusterKey] ASC);
GO

PRINT N'Creating Index [Users].[InboxMessages].[IX_Users_InboxMessages_ProcessedDate]...';
GO

CREATE NONCLUSTERED INDEX [IX_Users_InboxMessages_ProcessedDate]
    ON [Users].[InboxMessages]([ProcessedDate] ASC) WHERE ProcessedDate IS NULL;
GO

PRINT N'Creating Table [Users].[Users]...';
GO

CREATE TABLE [Users].[Users] (
    [ClusterKey] INT              IDENTITY (1, 1) NOT NULL,
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [Email]      NVARCHAR (255)   NOT NULL,
    [Password]   NVARCHAR (255)   NOT NULL,
    [IsActive]   BIT              NOT NULL,
    [FirstName]  NVARCHAR (50)    NOT NULL,
    [LastName]   NVARCHAR (50)    NOT NULL,
    [Name]       NVARCHAR (255)   NOT NULL,
    CONSTRAINT [PK_Users_Users_Id] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Index [Users].[Users].[IX_Users_Users_ClusterKey]...';
GO

CREATE UNIQUE CLUSTERED INDEX [IX_Users_Users_ClusterKey]
    ON [Users].[Users]([ClusterKey] ASC);
GO

PRINT N'Creating Table [Users].[UserRoles]...';
GO

CREATE TABLE [Users].[UserRoles] (
    [ClusterKey] INT              IDENTITY (1, 1) NOT NULL,
    [UserId]     UNIQUEIDENTIFIER NOT NULL,
    [RoleCode]   NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_Users_UserRoles_UserId_RoleCode] PRIMARY KEY NONCLUSTERED ([UserId] ASC, [RoleCode] ASC)
);
GO

PRINT N'Creating Index [Users].[UserRoles].[IX_Users_UserRoles_ClusterKey]...';
GO

CREATE UNIQUE CLUSTERED INDEX [IX_Users_UserRoles_ClusterKey]
    ON [Users].[UserRoles]([ClusterKey] ASC);
GO

PRINT N'Creating Table [Users].[RolePermissions]...';
GO

CREATE TABLE [Users].[RolePermissions] (
    [ClusterKey]     INT          IDENTITY (1, 1) NOT NULL,
    [RoleCode]       VARCHAR (50) NOT NULL,
    [PermissionCode] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_RolePermissions_RoleCode_PermissionCode] PRIMARY KEY NONCLUSTERED ([RoleCode] ASC, [PermissionCode] ASC)
);
GO

PRINT N'Creating Index [Users].[RolePermissions].[IX_Users_RolePermissions_ClusterKey]...';
GO

CREATE UNIQUE CLUSTERED INDEX [IX_Users_RolePermissions_ClusterKey]
    ON [Users].[RolePermissions]([ClusterKey] ASC);
GO

PRINT N'Creating Foreign Key [Users].[FK_Users_UserRoles_UserId]...';
GO

ALTER TABLE [Users].[UserRoles] WITH NOCHECK
    ADD CONSTRAINT [FK_Users_UserRoles_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users].[Users] ([Id]);
GO

PRINT N'Creating Foreign Key [Users].[FK_Users_RolePermissions_PermissionCode]...';
GO

ALTER TABLE [Users].[RolePermissions] WITH NOCHECK
    ADD CONSTRAINT [FK_Users_RolePermissions_PermissionCode] FOREIGN KEY ([PermissionCode]) REFERENCES [Users].[Permissions] ([Code]);
GO

PRINT N'Creating View [UserRegistrations].[v_UserRegistrations]...';
GO

CREATE VIEW [UserRegistrations].[v_UserRegistrations]
AS
SELECT
    [UserRegistration].[Id],
    [UserRegistration].[Email],
    [UserRegistration].[FirstName],
    [UserRegistration].[LastName],
    [UserRegistration].[Name],
    [UserRegistration].[StatusCode]
FROM [UserRegistrations].[UserRegistrations] AS [UserRegistration]
GO

PRINT N'Creating View [Users].[v_UserRoles]...';
GO

CREATE VIEW [Users].[v_UserRoles]
AS
SELECT
    [UserRole].[UserId],
    [UserRole].[RoleCode]
FROM [Users].[UserRoles] AS [UserRole]
GO

PRINT N'Creating View [Users].[v_UserPermissions]...';
GO

CREATE VIEW [Users].[v_UserPermissions]
AS
SELECT 
	DISTINCT
	[UserRole].UserId,
	[RolesToPermission].PermissionCode
FROM [Users].UserRoles AS [UserRole]
	INNER JOIN [Users].RolePermissions AS [RolesToPermission]
		ON [UserRole].RoleCode = [RolesToPermission].RoleCode
GO

PRINT N'Creating View [Users].[v_Users]...';
GO

CREATE VIEW [Users].[v_Users]
AS
SELECT
    [User].[Id],
    [User].[IsActive],
    [User].[Password],
    [User].[Email],
    [User].[Name]
FROM [Users].[Users] AS [User]
GO

PRINT N'Checking existing data against newly created constraints';
GO

USE [MyMeetings];
GO

ALTER TABLE [Users].[UserRoles] WITH CHECK CHECK CONSTRAINT [FK_Users_UserRoles_UserId];

ALTER TABLE [Users].[RolePermissions] WITH CHECK CHECK CONSTRAINT [FK_Users_RolePermissions_PermissionCode];
GO

PRINT N'Update complete.';