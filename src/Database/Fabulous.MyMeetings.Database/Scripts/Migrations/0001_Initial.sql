CREATE DATABASE [MyMeetings];
GO

USE [MyMeetings]
GO

CREATE SCHEMA app AUTHORIZATION dbo


GO
PRINT N'Creating Schema [Users]...';


GO
CREATE SCHEMA [Users]
	AUTHORIZATION [dbo];


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
PRINT N'Creating Index [Users].[InternalCommands].[IX_Users_InternalCommands_ClusterKey]...';


GO
CREATE UNIQUE CLUSTERED INDEX [IX_Users_InternalCommands_ClusterKey]
	ON [Users].[InternalCommands]([ClusterKey] ASC);


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
PRINT N'Creating Index [Users].[OutboxMessages].[IX_Users_OutboxMessages_ClusterKey]...';


GO
CREATE UNIQUE CLUSTERED INDEX [IX_Users_OutboxMessages_ClusterKey]
	ON [Users].[OutboxMessages]([ClusterKey] ASC);


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
PRINT N'Creating Table [Users].[RolesToPermissions]...';


GO
CREATE TABLE [Users].[RolesToPermissions] (
	[ClusterKey]     INT          IDENTITY (1, 1) NOT NULL,
	[RoleCode]       VARCHAR (50) NOT NULL,
	[PermissionCode] VARCHAR (50) NOT NULL,
	CONSTRAINT [PK_RolesToPermissions_RoleCode_PermissionCode] PRIMARY KEY NONCLUSTERED ([RoleCode] ASC, [PermissionCode] ASC)
);


GO
PRINT N'Creating Index [Users].[RolesToPermissions].[IX_Users_RolesToPermissions_ClusterKey]...';


GO
CREATE UNIQUE CLUSTERED INDEX [IX_Users_RolesToPermissions_ClusterKey]
	ON [Users].[RolesToPermissions]([ClusterKey] ASC);


GO
PRINT N'Creating Table [Users].[UserRegistrations]...';


GO
CREATE TABLE [Users].[UserRegistrations] (
	[ClusterKey]    INT              IDENTITY (1, 1) NOT NULL,
	[Id]            UNIQUEIDENTIFIER NOT NULL,
	[Login]         NVARCHAR (100)   NOT NULL,
	[Email]         NVARCHAR (255)   NOT NULL,
	[Password]      NVARCHAR (255)   NOT NULL,
	[FirstName]     NVARCHAR (50)    NOT NULL,
	[LastName]      NVARCHAR (50)    NOT NULL,
	[Name]          NVARCHAR (255)   NOT NULL,
	[StatusCode]    VARCHAR (50)     NOT NULL,
	[RegisterDate]  DATETIME2 (7)    NOT NULL,
	[ConfirmedDate] DATETIME2 (7)    NULL,
	CONSTRAINT [PK_Users_UserRegistrations_Id] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Index [Users].[UserRegistrations].[IX_Users_UserRegistrations_ClusterKey]...';


GO
CREATE UNIQUE CLUSTERED INDEX [IX_Users_UserRegistrations_ClusterKey]
	ON [Users].[UserRegistrations]([ClusterKey] ASC);


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
PRINT N'Creating Table [Users].[Users]...';


GO
CREATE TABLE [Users].[Users] (
	[ClusterKey] INT              IDENTITY (1, 1) NOT NULL,
	[Id]         UNIQUEIDENTIFIER NOT NULL,
	[Login]      NVARCHAR (100)   NOT NULL,
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
PRINT N'Creating Index [Users].[InboxMessages].[IX_Users_InboxMessages_ClusterKey]...';


GO
CREATE UNIQUE CLUSTERED INDEX [IX_Users_InboxMessages_ClusterKey]
	ON [Users].[InboxMessages]([ClusterKey] ASC);


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
	INNER JOIN [Users].RolesToPermissions AS [RolesToPermission]
		ON [UserRole].RoleCode = [RolesToPermission].RoleCode
GO
PRINT N'Creating View [Users].[v_UserRegistrations]...';


GO
CREATE VIEW [Users].[v_UserRegistrations]
AS
SELECT
	[UserRegistration].[Id],
	[UserRegistration].[Login],
	[UserRegistration].[Email],
	[UserRegistration].[FirstName],
	[UserRegistration].[LastName],
	[UserRegistration].[Name],
	[UserRegistration].[StatusCode]
FROM [Users].[UserRegistrations] AS [UserRegistration]
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
PRINT N'Creating View [Users].[v_Users]...';


GO
CREATE VIEW [Users].[v_Users]
AS
SELECT
	[User].[Id],
	[User].[IsActive],
	[User].[Login],
	[User].[Password],
	[User].[Email],
	[User].[Name]
FROM [Users].[Users] AS [User]
GO

PRINT N'Update complete.';
GO
