USE [MyMeetings];
GO

PRINT N'Creating Schema [Users]...';

GO
CREATE SCHEMA [Users]
	AUTHORIZATION [dbo];


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
PRINT N'Creating Table [Users].[QRTZ_BLOB_TRIGGERS]...';


GO
CREATE TABLE [Users].[QRTZ_BLOB_TRIGGERS] (
	[SCHED_NAME]    NVARCHAR (120)  NOT NULL,
	[TRIGGER_NAME]  NVARCHAR (150)  NOT NULL,
	[TRIGGER_GROUP] NVARCHAR (150)  NOT NULL,
	[BLOB_DATA]     VARBINARY (MAX) NULL,
	CONSTRAINT [PK_Users_QRTZ_BLOB_TRIGGERS] PRIMARY KEY CLUSTERED ([SCHED_NAME] ASC, [TRIGGER_NAME] ASC, [TRIGGER_GROUP] ASC)
);


GO
PRINT N'Creating Table [Users].[QRTZ_CALENDARS]...';


GO
CREATE TABLE [Users].[QRTZ_CALENDARS] (
	[SCHED_NAME]    NVARCHAR (120)  NOT NULL,
	[CALENDAR_NAME] NVARCHAR (200)  NOT NULL,
	[CALENDAR]      VARBINARY (MAX) NOT NULL,
	CONSTRAINT [PK_Users_QRTZ_CALENDARS] PRIMARY KEY CLUSTERED ([SCHED_NAME] ASC, [CALENDAR_NAME] ASC)
);


GO
PRINT N'Creating Table [Users].[QRTZ_CRON_TRIGGERS]...';


GO
CREATE TABLE [Users].[QRTZ_CRON_TRIGGERS] (
	[SCHED_NAME]      NVARCHAR (120) NOT NULL,
	[TRIGGER_NAME]    NVARCHAR (150) NOT NULL,
	[TRIGGER_GROUP]   NVARCHAR (150) NOT NULL,
	[CRON_EXPRESSION] NVARCHAR (120) NOT NULL,
	[TIME_ZONE_ID]    NVARCHAR (80)  NULL,
	CONSTRAINT [PK_Users_QRTZ_CRON_TRIGGERS] PRIMARY KEY CLUSTERED ([SCHED_NAME] ASC, [TRIGGER_NAME] ASC, [TRIGGER_GROUP] ASC)
);


GO
PRINT N'Creating Table [Users].[QRTZ_FIRED_TRIGGERS]...';


GO
CREATE TABLE [Users].[QRTZ_FIRED_TRIGGERS] (
	[SCHED_NAME]        NVARCHAR (120) NOT NULL,
	[ENTRY_ID]          NVARCHAR (140) NOT NULL,
	[TRIGGER_NAME]      NVARCHAR (150) NOT NULL,
	[TRIGGER_GROUP]     NVARCHAR (150) NOT NULL,
	[INSTANCE_NAME]     NVARCHAR (200) NOT NULL,
	[FIRED_TIME]        BIGINT         NOT NULL,
	[SCHED_TIME]        BIGINT         NOT NULL,
	[PRIORITY]          INT            NOT NULL,
	[STATE]             NVARCHAR (16)  NOT NULL,
	[JOB_NAME]          NVARCHAR (150) NULL,
	[JOB_GROUP]         NVARCHAR (150) NULL,
	[IS_NONCONCURRENT]  BIT            NULL,
	[REQUESTS_RECOVERY] BIT            NULL,
	CONSTRAINT [PK_Users_QRTZ_FIRED_TRIGGERS] PRIMARY KEY CLUSTERED ([SCHED_NAME] ASC, [ENTRY_ID] ASC)
);


GO
PRINT N'Creating Index [Users].[QRTZ_FIRED_TRIGGERS].[IDX_Users_QRTZ_FT_INST_JOB_REQ_RCVRY]...';


GO
CREATE NONCLUSTERED INDEX [IDX_Users_QRTZ_FT_INST_JOB_REQ_RCVRY]
	ON [Users].[QRTZ_FIRED_TRIGGERS]([SCHED_NAME] ASC, [INSTANCE_NAME] ASC, [REQUESTS_RECOVERY] ASC);


GO
PRINT N'Creating Index [Users].[QRTZ_FIRED_TRIGGERS].[IDX_Users_QRTZ_FT_G_J]...';


GO
CREATE NONCLUSTERED INDEX [IDX_Users_QRTZ_FT_G_J]
	ON [Users].[QRTZ_FIRED_TRIGGERS]([SCHED_NAME] ASC, [JOB_GROUP] ASC, [JOB_NAME] ASC);


GO
PRINT N'Creating Index [Users].[QRTZ_FIRED_TRIGGERS].[IDX_Users_QRTZ_FT_G_T]...';


GO
CREATE NONCLUSTERED INDEX [IDX_Users_QRTZ_FT_G_T]
	ON [Users].[QRTZ_FIRED_TRIGGERS]([SCHED_NAME] ASC, [TRIGGER_GROUP] ASC, [TRIGGER_NAME] ASC);


GO
PRINT N'Creating Table [Users].[QRTZ_JOB_DETAILS]...';


GO
CREATE TABLE [Users].[QRTZ_JOB_DETAILS] (
	[SCHED_NAME]        NVARCHAR (120)  NOT NULL,
	[JOB_NAME]          NVARCHAR (150)  NOT NULL,
	[JOB_GROUP]         NVARCHAR (150)  NOT NULL,
	[DESCRIPTION]       NVARCHAR (250)  NULL,
	[JOB_CLASS_NAME]    NVARCHAR (250)  NOT NULL,
	[IS_DURABLE]        BIT             NOT NULL,
	[IS_NONCONCURRENT]  BIT             NOT NULL,
	[IS_UPDATE_DATA]    BIT             NOT NULL,
	[REQUESTS_RECOVERY] BIT             NOT NULL,
	[JOB_DATA]          VARBINARY (MAX) NULL,
	CONSTRAINT [PK_Users_QRTZ_JOB_DETAILS] PRIMARY KEY CLUSTERED ([SCHED_NAME] ASC, [JOB_NAME] ASC, [JOB_GROUP] ASC)
);


GO
PRINT N'Creating Table [Users].[QRTZ_LOCKS]...';


GO
CREATE TABLE [Users].[QRTZ_LOCKS] (
	[SCHED_NAME] NVARCHAR (120) NOT NULL,
	[LOCK_NAME]  NVARCHAR (40)  NOT NULL,
	CONSTRAINT [PK_Users_QRTZ_LOCKS] PRIMARY KEY CLUSTERED ([SCHED_NAME] ASC, [LOCK_NAME] ASC)
);


GO
PRINT N'Creating Table [Users].[QRTZ_PAUSED_TRIGGER_GRPS]...';


GO
CREATE TABLE [Users].[QRTZ_PAUSED_TRIGGER_GRPS] (
	[SCHED_NAME]    NVARCHAR (120) NOT NULL,
	[TRIGGER_GROUP] NVARCHAR (150) NOT NULL,
	CONSTRAINT [PK_Users_QRTZ_PAUSED_TRIGGER_GRPS] PRIMARY KEY CLUSTERED ([SCHED_NAME] ASC, [TRIGGER_GROUP] ASC)
);


GO
PRINT N'Creating Table [Users].[QRTZ_SCHEDULER_STATE]...';


GO
CREATE TABLE [Users].[QRTZ_SCHEDULER_STATE] (
	[SCHED_NAME]        NVARCHAR (120) NOT NULL,
	[INSTANCE_NAME]     NVARCHAR (200) NOT NULL,
	[LAST_CHECKIN_TIME] BIGINT         NOT NULL,
	[CHECKIN_INTERVAL]  BIGINT         NOT NULL,
	CONSTRAINT [PK_Users_QRTZ_SCHEDULER_STATE] PRIMARY KEY CLUSTERED ([SCHED_NAME] ASC, [INSTANCE_NAME] ASC)
);


GO
PRINT N'Creating Table [Users].[QRTZ_SIMPLE_TRIGGERS]...';


GO
CREATE TABLE [Users].[QRTZ_SIMPLE_TRIGGERS] (
	[SCHED_NAME]      NVARCHAR (120) NOT NULL,
	[TRIGGER_NAME]    NVARCHAR (150) NOT NULL,
	[TRIGGER_GROUP]   NVARCHAR (150) NOT NULL,
	[REPEAT_COUNT]    INT            NOT NULL,
	[REPEAT_INTERVAL] BIGINT         NOT NULL,
	[TIMES_TRIGGERED] INT            NOT NULL,
	CONSTRAINT [PK_Users_QRTZ_SIMPLE_TRIGGERS] PRIMARY KEY CLUSTERED ([SCHED_NAME] ASC, [TRIGGER_NAME] ASC, [TRIGGER_GROUP] ASC)
);


GO
PRINT N'Creating Table [Users].[QRTZ_SIMPROP_TRIGGERS]...';


GO
CREATE TABLE [Users].[QRTZ_SIMPROP_TRIGGERS] (
	[SCHED_NAME]    NVARCHAR (120)  NOT NULL,
	[TRIGGER_NAME]  NVARCHAR (150)  NOT NULL,
	[TRIGGER_GROUP] NVARCHAR (150)  NOT NULL,
	[STR_PROP_1]    NVARCHAR (512)  NULL,
	[STR_PROP_2]    NVARCHAR (512)  NULL,
	[STR_PROP_3]    NVARCHAR (512)  NULL,
	[INT_PROP_1]    INT             NULL,
	[INT_PROP_2]    INT             NULL,
	[LONG_PROP_1]   BIGINT          NULL,
	[LONG_PROP_2]   BIGINT          NULL,
	[DEC_PROP_1]    NUMERIC (13, 4) NULL,
	[DEC_PROP_2]    NUMERIC (13, 4) NULL,
	[BOOL_PROP_1]   BIT             NULL,
	[BOOL_PROP_2]   BIT             NULL,
	[TIME_ZONE_ID]  NVARCHAR (80)   NULL,
	CONSTRAINT [PK_Users_QRTZ_SIMPROP_TRIGGERS] PRIMARY KEY CLUSTERED ([SCHED_NAME] ASC, [TRIGGER_NAME] ASC, [TRIGGER_GROUP] ASC)
);


GO
PRINT N'Creating Table [Users].[QRTZ_TRIGGERS]...';


GO
CREATE TABLE [Users].[QRTZ_TRIGGERS] (
	[SCHED_NAME]     NVARCHAR (120)  NOT NULL,
	[TRIGGER_NAME]   NVARCHAR (150)  NOT NULL,
	[TRIGGER_GROUP]  NVARCHAR (150)  NOT NULL,
	[JOB_NAME]       NVARCHAR (150)  NOT NULL,
	[JOB_GROUP]      NVARCHAR (150)  NOT NULL,
	[DESCRIPTION]    NVARCHAR (250)  NULL,
	[NEXT_FIRE_TIME] BIGINT          NULL,
	[PREV_FIRE_TIME] BIGINT          NULL,
	[PRIORITY]       INT             NULL,
	[TRIGGER_STATE]  NVARCHAR (16)   NOT NULL,
	[TRIGGER_TYPE]   NVARCHAR (8)    NOT NULL,
	[START_TIME]     BIGINT          NOT NULL,
	[END_TIME]       BIGINT          NULL,
	[CALENDAR_NAME]  NVARCHAR (200)  NULL,
	[MISFIRE_INSTR]  INT             NULL,
	[JOB_DATA]       VARBINARY (MAX) NULL,
	CONSTRAINT [PK_Users_QRTZ_TRIGGERS] PRIMARY KEY CLUSTERED ([SCHED_NAME] ASC, [TRIGGER_NAME] ASC, [TRIGGER_GROUP] ASC)
);


GO
PRINT N'Creating Index [Users].[QRTZ_TRIGGERS].[IDX_Users_QRTZ_T_G_J]...';


GO
CREATE NONCLUSTERED INDEX [IDX_Users_QRTZ_T_G_J]
	ON [Users].[QRTZ_TRIGGERS]([SCHED_NAME] ASC, [JOB_GROUP] ASC, [JOB_NAME] ASC);


GO
PRINT N'Creating Index [Users].[QRTZ_TRIGGERS].[IDX_Users_QRTZ_T_C]...';


GO
CREATE NONCLUSTERED INDEX [IDX_Users_QRTZ_T_C]
	ON [Users].[QRTZ_TRIGGERS]([SCHED_NAME] ASC, [CALENDAR_NAME] ASC);


GO
PRINT N'Creating Index [Users].[QRTZ_TRIGGERS].[IDX_Users_QRTZ_T_N_G_STATE]...';


GO
CREATE NONCLUSTERED INDEX [IDX_Users_QRTZ_T_N_G_STATE]
	ON [Users].[QRTZ_TRIGGERS]([SCHED_NAME] ASC, [TRIGGER_GROUP] ASC, [TRIGGER_STATE] ASC);


GO
PRINT N'Creating Index [Users].[QRTZ_TRIGGERS].[IDX_Users_QRTZ_T_STATE]...';


GO
CREATE NONCLUSTERED INDEX [IDX_Users_QRTZ_T_STATE]
	ON [Users].[QRTZ_TRIGGERS]([SCHED_NAME] ASC, [TRIGGER_STATE] ASC);


GO
PRINT N'Creating Index [Users].[QRTZ_TRIGGERS].[IDX_Users_QRTZ_T_N_STATE]...';


GO
CREATE NONCLUSTERED INDEX [IDX_Users_QRTZ_T_N_STATE]
	ON [Users].[QRTZ_TRIGGERS]([SCHED_NAME] ASC, [TRIGGER_NAME] ASC, [TRIGGER_GROUP] ASC, [TRIGGER_STATE] ASC);


GO
PRINT N'Creating Index [Users].[QRTZ_TRIGGERS].[IDX_Users_QRTZ_T_NEXT_FIRE_TIME]...';


GO
CREATE NONCLUSTERED INDEX [IDX_Users_QRTZ_T_NEXT_FIRE_TIME]
	ON [Users].[QRTZ_TRIGGERS]([SCHED_NAME] ASC, [NEXT_FIRE_TIME] ASC);


GO
PRINT N'Creating Index [Users].[QRTZ_TRIGGERS].[IDX_Users_QRTZ_T_NFT_ST]...';


GO
CREATE NONCLUSTERED INDEX [IDX_Users_QRTZ_T_NFT_ST]
	ON [Users].[QRTZ_TRIGGERS]([SCHED_NAME] ASC, [TRIGGER_STATE] ASC, [NEXT_FIRE_TIME] ASC);


GO
PRINT N'Creating Index [Users].[QRTZ_TRIGGERS].[IDX_Users_QRTZ_T_NFT_ST_MISFIRE]...';


GO
CREATE NONCLUSTERED INDEX [IDX_Users_QRTZ_T_NFT_ST_MISFIRE]
	ON [Users].[QRTZ_TRIGGERS]([SCHED_NAME] ASC, [MISFIRE_INSTR] ASC, [NEXT_FIRE_TIME] ASC, [TRIGGER_STATE] ASC);


GO
PRINT N'Creating Index [Users].[QRTZ_TRIGGERS].[IDX_Users_QRTZ_T_NFT_ST_MISFIRE_GRP]...';


GO
CREATE NONCLUSTERED INDEX [IDX_Users_QRTZ_T_NFT_ST_MISFIRE_GRP]
	ON [Users].[QRTZ_TRIGGERS]([SCHED_NAME] ASC, [MISFIRE_INSTR] ASC, [NEXT_FIRE_TIME] ASC, [TRIGGER_GROUP] ASC, [TRIGGER_STATE] ASC);


GO
PRINT N'Creating Foreign Key [Users].[FK_Users_RolePermissions_PermissionCode]...';


GO
ALTER TABLE [Users].[RolePermissions]
	ADD CONSTRAINT [FK_Users_RolePermissions_PermissionCode] FOREIGN KEY ([PermissionCode]) REFERENCES [Users].[Permissions] ([Code]);


GO
PRINT N'Creating Foreign Key [Users].[FK_Users_UserRoles_UserId]...';


GO
ALTER TABLE [Users].[UserRoles]
	ADD CONSTRAINT [FK_Users_UserRoles_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users].[Users] ([Id]);


GO
PRINT N'Creating Foreign Key [Users].[FK_Users_QRTZ_CRON_TRIGGERS_QRTZ_TRIGGERS]...';


GO
ALTER TABLE [Users].[QRTZ_CRON_TRIGGERS]
	ADD CONSTRAINT [FK_Users_QRTZ_CRON_TRIGGERS_QRTZ_TRIGGERS] FOREIGN KEY ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP]) REFERENCES [Users].[QRTZ_TRIGGERS] ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP]) ON DELETE CASCADE;


GO
PRINT N'Creating Foreign Key [Users].[FK_Users_QRTZ_SIMPLE_TRIGGERS_QRTZ_TRIGGERS]...';


GO
ALTER TABLE [Users].[QRTZ_SIMPLE_TRIGGERS]
	ADD CONSTRAINT [FK_Users_QRTZ_SIMPLE_TRIGGERS_QRTZ_TRIGGERS] FOREIGN KEY ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP]) REFERENCES [Users].[QRTZ_TRIGGERS] ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP]) ON DELETE CASCADE;


GO
PRINT N'Creating Foreign Key [Users].[FK_Users_QRTZ_SIMPROP_TRIGGERS_QRTZ_TRIGGERS]...';


GO
ALTER TABLE [Users].[QRTZ_SIMPROP_TRIGGERS]
	ADD CONSTRAINT [FK_Users_QRTZ_SIMPROP_TRIGGERS_QRTZ_TRIGGERS] FOREIGN KEY ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP]) REFERENCES [Users].[QRTZ_TRIGGERS] ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP]) ON DELETE CASCADE;


GO
PRINT N'Creating Foreign Key [Users].[FK_Users_QRTZ_TRIGGERS_QRTZ_JOB_DETAILS]...';


GO
ALTER TABLE [Users].[QRTZ_TRIGGERS]
	ADD CONSTRAINT [FK_Users_QRTZ_TRIGGERS_QRTZ_JOB_DETAILS] FOREIGN KEY ([SCHED_NAME], [JOB_NAME], [JOB_GROUP]) REFERENCES [Users].[QRTZ_JOB_DETAILS] ([SCHED_NAME], [JOB_NAME], [JOB_GROUP]);


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
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
	AND (((@@microsoftversion / power(2, 24) = 9)
		  AND (@@microsoftversion & 0xffff >= 3024))
		 OR ((@@microsoftversion / power(2, 24) = 10)
			 AND (@@microsoftversion & 0xffff >= 1600))))
	SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
	BEGIN
		EXECUTE sp_db_vardecimal_storage_format N'MyMeetings', 'ON';
	END


GO
PRINT N'Update complete.';


GO
