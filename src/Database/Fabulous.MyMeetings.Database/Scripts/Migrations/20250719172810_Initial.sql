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

PRINT N'Creating Schema [Administration]...';
GO

CREATE SCHEMA [Administration]
    AUTHORIZATION [dbo];
GO

PRINT N'Creating Schema [Meetings]...';
GO

CREATE SCHEMA [Meetings]
    AUTHORIZATION [dbo];
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

PRINT N'Creating Table [Administration].[OutboxMessages]...';
GO

CREATE TABLE [Administration].[OutboxMessages] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [OccurredOn]    DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Index [Administration].[OutboxMessages].[IX_Administration_OutboxMessages_Unprocessed]...';
GO

CREATE NONCLUSTERED INDEX [IX_Administration_OutboxMessages_Unprocessed]
    ON [Administration].[OutboxMessages]([OccurredOn] ASC, [Id] ASC)
    INCLUDE([Type], [Data], [ProcessedDate]) WHERE ProcessedDate IS NULL;
GO

PRINT N'Creating Table [Administration].[Members]...';
GO

CREATE TABLE [Administration].[Members] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [Email]     NVARCHAR (255)   NOT NULL,
    [FirstName] NVARCHAR (50)    NOT NULL,
    [LastName]  NVARCHAR (50)    NOT NULL,
    [Name]      NVARCHAR (255)   NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Table [Administration].[MeetingGroupProposals]...';
GO

CREATE TABLE [Administration].[MeetingGroupProposals] (
    [Id]                   UNIQUEIDENTIFIER NOT NULL,
    [Name]                 NVARCHAR (255)   NOT NULL,
    [Description]          VARCHAR (200)    NULL,
    [LocationCity]         NVARCHAR (50)    NOT NULL,
    [LocationCountryCode]  NVARCHAR (3)     NOT NULL,
    [ProposalUserId]       UNIQUEIDENTIFIER NOT NULL,
    [ProposalDate]         DATETIME2 (7)    NOT NULL,
    [StatusCode]           NVARCHAR (50)    NOT NULL,
    [DecisionDate]         DATETIME2 (7)    NULL,
    [DecisionUserId]       UNIQUEIDENTIFIER NULL,
    [DecisionCode]         NVARCHAR (50)    NULL,
    [DecisionRejectReason] NVARCHAR (250)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Table [Administration].[InternalCommands]...';
GO

CREATE TABLE [Administration].[InternalCommands] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [EnqueueDate]   DATETIME2 (7)    NOT NULL,
    [Type]          NVARCHAR (255)   NOT NULL,
    [Data]          NVARCHAR (MAX)   NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    [Error]         NVARCHAR (MAX)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Index [Administration].[InternalCommands].[IX_Meetings_InternalCommands_Unprocessed]...';
GO

CREATE NONCLUSTERED INDEX [IX_Meetings_InternalCommands_Unprocessed]
    ON [Administration].[InternalCommands]([EnqueueDate] ASC, [Id] ASC)
    INCLUDE([Type], [Data], [ProcessedDate], [Error]) WHERE ProcessedDate IS NULL;
GO

PRINT N'Creating Table [Administration].[InboxMessages]...';
GO

CREATE TABLE [Administration].[InboxMessages] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [OccurredOn]    DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Index [Administration].[InboxMessages].[IX_Administration_InboxMessages_Unprocessed]...';
GO

CREATE NONCLUSTERED INDEX [IX_Administration_InboxMessages_Unprocessed]
    ON [Administration].[InboxMessages]([OccurredOn] ASC, [Id] ASC)
    INCLUDE([Type], [Data], [ProcessedDate]) WHERE ProcessedDate IS NULL;
GO

PRINT N'Creating Table [Meetings].[Meetings]...';
GO

CREATE TABLE [Meetings].[Meetings] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [MeetingGroupId]     UNIQUEIDENTIFIER NOT NULL,
    [CreatorId]          UNIQUEIDENTIFIER NOT NULL,
    [CreateDate]         DATETIME2 (7)    NOT NULL,
    [Title]              NVARCHAR (200)   NOT NULL,
    [Description]        NVARCHAR (4000)  NOT NULL,
    [TermStartDate]      DATETIME2 (7)    NOT NULL,
    [TermEndDate]        DATETIME2 (7)    NOT NULL,
    [LocationName]       NVARCHAR (200)   NOT NULL,
    [LocationAddress]    NVARCHAR (200)   NOT NULL,
    [LocationPostalCode] NVARCHAR (200)   NULL,
    [LocationCity]       NVARCHAR (50)    NOT NULL,
    [AttendeesLimit]     INT              NULL,
    [GuestsLimit]        INT              NOT NULL,
    [RSVPTermStartDate]  DATETIME2 (7)    NULL,
    [RSVPTermEndDate]    DATETIME2 (7)    NULL,
    [EventFeeValue]      DECIMAL (5)      NULL,
    [EventFeeCurrency]   VARCHAR (3)      NULL,
    [ChangeDate]         DATETIME2 (7)    NULL,
    [ChangeMemberId]     UNIQUEIDENTIFIER NULL,
    [CancelDate]         DATETIME2 (7)    NULL,
    [CancelMemberId]     UNIQUEIDENTIFIER NULL,
    [IsCanceled]         BIT              NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Table [Meetings].[MeetingWaitlistMembers]...';
GO

CREATE TABLE [Meetings].[MeetingWaitlistMembers] (
    [MeetingId]            UNIQUEIDENTIFIER NOT NULL,
    [MemberId]             UNIQUEIDENTIFIER NOT NULL,
    [SignUpDate]           DATETIME2 (7)    NOT NULL,
    [IsSignedOff]          BIT              NOT NULL,
    [SignOffDate]          DATETIME2 (7)    NULL,
    [IsMovedToAttendees]   BIT              NOT NULL,
    [MovedToAttendeesDate] DATETIME2 (7)    NULL,
    CONSTRAINT [PK_Meetings_MeetingWaitlistMembers_MeetingId_MemberId_SignUpDate] PRIMARY KEY CLUSTERED ([MeetingId] ASC, [MemberId] ASC, [SignUpDate] ASC)
);
GO

PRINT N'Creating Table [Meetings].[Countries]...';
GO

CREATE TABLE [Meetings].[Countries] (
    [Code] CHAR (2)      NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Code] ASC)
);
GO

PRINT N'Creating Table [Meetings].[MeetingAttendees]...';
GO

CREATE TABLE [Meetings].[MeetingAttendees] (
    [MeetingId]             UNIQUEIDENTIFIER NOT NULL,
    [AttendeeId]            UNIQUEIDENTIFIER NOT NULL,
    [DecisionDate]          DATETIME2 (7)    NOT NULL,
    [RoleCode]              VARCHAR (50)     NULL,
    [GuestsNumber]          INT              NULL,
    [DecisionChanged]       BIT              NOT NULL,
    [DecisionChangeDate]    DATETIME2 (7)    NULL,
    [IsRemoved]             BIT              NOT NULL,
    [RemovingMemberId]      UNIQUEIDENTIFIER NULL,
    [RemovingReason]        NVARCHAR (500)   NULL,
    [RemovedDate]           DATETIME2 (7)    NULL,
    [BecameNotAttendeeDate] DATETIME2 (7)    NULL,
    [FeeValue]              DECIMAL (5)      NULL,
    [FeeCurrency]           VARCHAR (3)      NULL,
    [IsFeePaid]             BIT              NOT NULL,
    CONSTRAINT [PK_meetings_MeetingAttendees_Id] PRIMARY KEY CLUSTERED ([MeetingId] ASC, [AttendeeId] ASC, [DecisionDate] ASC)
);
GO

PRINT N'Creating Table [Meetings].[MeetingMemberCommentLikes]...';
GO

CREATE TABLE [Meetings].[MeetingMemberCommentLikes] (
    [Id]               UNIQUEIDENTIFIER NOT NULL,
    [MemberId]         UNIQUEIDENTIFIER NOT NULL,
    [MeetingCommentId] UNIQUEIDENTIFIER NOT NULL,
    [LikedDate]        DATETIME2 (7)    NOT NULL,
    CONSTRAINT [PK_Meetings_MeetingMemberCommentLikes_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Table [Meetings].[InboxMessages]...';
GO

CREATE TABLE [Meetings].[InboxMessages] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [OccurredOn]    DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Index [Meetings].[InboxMessages].[IX_Meetings_InboxMessages_Unprocessed]...';
GO

CREATE NONCLUSTERED INDEX [IX_Meetings_InboxMessages_Unprocessed]
    ON [Meetings].[InboxMessages]([OccurredOn] ASC, [Id] ASC)
    INCLUDE([Type], [Data], [ProcessedDate]) WHERE ProcessedDate IS NULL;
GO

PRINT N'Creating Table [Meetings].[MeetingGroups]...';
GO

CREATE TABLE [Meetings].[MeetingGroups] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [Name]                NVARCHAR (255)   NOT NULL,
    [Description]         VARCHAR (200)    NULL,
    [LocationCity]        NVARCHAR (50)    NOT NULL,
    [LocationCountryCode] NVARCHAR (3)     NOT NULL,
    [CreatorId]           UNIQUEIDENTIFIER NOT NULL,
    [CreateDate]          DATETIME2 (7)    NOT NULL,
    [PaymentDateTo]       DATE             NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Table [Meetings].[OutboxMessages]...';
GO

CREATE TABLE [Meetings].[OutboxMessages] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [OccurredOn]    DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Index [Meetings].[OutboxMessages].[IX_Meetings_OutboxMessages_Unprocessed]...';
GO

CREATE NONCLUSTERED INDEX [IX_Meetings_OutboxMessages_Unprocessed]
    ON [Meetings].[OutboxMessages]([OccurredOn] ASC, [Id] ASC)
    INCLUDE([Type], [Data], [ProcessedDate]) WHERE ProcessedDate IS NULL;
GO

PRINT N'Creating Table [Meetings].[Members]...';
GO

CREATE TABLE [Meetings].[Members] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [Email]     NVARCHAR (255)   NOT NULL,
    [FirstName] NVARCHAR (50)    NOT NULL,
    [LastName]  NVARCHAR (50)    NOT NULL,
    [Name]      NVARCHAR (255)   NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Table [Meetings].[InternalCommands]...';
GO

CREATE TABLE [Meetings].[InternalCommands] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [EnqueueDate]   DATETIME2 (7)    NOT NULL,
    [Type]          NVARCHAR (255)   NOT NULL,
    [Data]          NVARCHAR (MAX)   NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    [Error]         NVARCHAR (MAX)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Index [Meetings].[InternalCommands].[IX_Meetings_InternalCommands_Unprocessed]...';
GO

CREATE NONCLUSTERED INDEX [IX_Meetings_InternalCommands_Unprocessed]
    ON [Meetings].[InternalCommands]([EnqueueDate] ASC, [Id] ASC)
    INCLUDE([Type], [Data], [ProcessedDate], [Error]) WHERE ProcessedDate IS NULL;
GO

PRINT N'Creating Table [Meetings].[MeetingCommentingConfigurations]...';
GO

CREATE TABLE [Meetings].[MeetingCommentingConfigurations] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [MeetingId]           UNIQUEIDENTIFIER NOT NULL,
    [IsCommentingEnabled] BIT              NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Table [Meetings].[MemberSubscriptions]...';
GO

CREATE TABLE [Meetings].[MemberSubscriptions] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [ExpirationDate] DATETIME2 (7)    NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Table [Meetings].[MeetingNotAttendees]...';
GO

CREATE TABLE [Meetings].[MeetingNotAttendees] (
    [MeetingId]          UNIQUEIDENTIFIER NOT NULL,
    [MemberId]           UNIQUEIDENTIFIER NOT NULL,
    [DecisionDate]       DATETIME2 (7)    NOT NULL,
    [DecisionChanged]    BIT              NOT NULL,
    [DecisionChangeDate] DATETIME2 (7)    NULL,
    CONSTRAINT [PK_Meetings_MeetingNotAttendees_Id] PRIMARY KEY CLUSTERED ([MeetingId] ASC, [MemberId] ASC, [DecisionDate] ASC)
);
GO

PRINT N'Creating Table [Meetings].[MeetingGroupProposals]...';
GO

CREATE TABLE [Meetings].[MeetingGroupProposals] (
    [Id]                  UNIQUEIDENTIFIER NOT NULL,
    [Name]                NVARCHAR (255)   NOT NULL,
    [Description]         VARCHAR (200)    NULL,
    [LocationCity]        NVARCHAR (50)    NOT NULL,
    [LocationCountryCode] NVARCHAR (3)     NOT NULL,
    [ProposalUserId]      UNIQUEIDENTIFIER NOT NULL,
    [ProposalDate]        DATETIME2 (7)    NOT NULL,
    [StatusCode]          NVARCHAR (50)    NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Table [Meetings].[MeetingGroupMembers]...';
GO

CREATE TABLE [Meetings].[MeetingGroupMembers] (
    [MeetingGroupId] UNIQUEIDENTIFIER NOT NULL,
    [MemberId]       UNIQUEIDENTIFIER NOT NULL,
    [JoinedDate]     DATETIME2 (7)    NOT NULL,
    [RoleCode]       VARCHAR (50)     NOT NULL,
    [IsActive]       BIT              NOT NULL,
    [LeaveDate]      DATETIME2 (7)    NULL,
    CONSTRAINT [PK_Meetings_MeetingGroupMembers_MeetingGroupId_MemberId_JoinedDate] PRIMARY KEY CLUSTERED ([MeetingGroupId] ASC, [MemberId] ASC, [JoinedDate] ASC)
);
GO

PRINT N'Creating Table [Meetings].[MeetingComments]...';
GO

CREATE TABLE [Meetings].[MeetingComments] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [MeetingId]          UNIQUEIDENTIFIER NOT NULL,
    [AuthorId]           UNIQUEIDENTIFIER NOT NULL,
    [InReplyToCommentId] UNIQUEIDENTIFIER NULL,
    [Comment]            VARCHAR (300)    NULL,
    [IsRemoved]          BIT              NOT NULL,
    [RemovedByReason]    VARCHAR (300)    NULL,
    [CreateDate]         DATETIME2 (7)    NOT NULL,
    [EditDate]           DATETIME2 (7)    NULL,
    [LikesCount]         INT              NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Table [UserRegistrations].[UserRegistrations]...';
GO

CREATE TABLE [UserRegistrations].[UserRegistrations] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [Email]         NVARCHAR (255)   NOT NULL,
    [Password]      NVARCHAR (255)   NOT NULL,
    [FirstName]     NVARCHAR (50)    NOT NULL,
    [LastName]      NVARCHAR (50)    NOT NULL,
    [Name]          NVARCHAR (255)   NOT NULL,
    [StatusCode]    VARCHAR (50)     NOT NULL,
    [RegisterDate]  DATETIME2 (7)    NOT NULL,
    [ConfirmedDate] DATETIME2 (7)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Table [UserRegistrations].[TokenTypes]...';
GO

CREATE TABLE [UserRegistrations].[TokenTypes] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Table [UserRegistrations].[Tokens]...';
GO

CREATE TABLE [UserRegistrations].[Tokens] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [UserId]        UNIQUEIDENTIFIER NOT NULL,
    [Value]         NVARCHAR (255)   NOT NULL,
    [TokenTypeId]   INT              NOT NULL,
    [CreatedAt]     DATETIME2 (7)    NOT NULL,
    [ExpiresAt]     DATETIME2 (7)    NOT NULL,
    [IsUsed]        BIT              NOT NULL,
    [UsedAt]        DATETIME2 (7)    NULL,
    [IsInvalidated] BIT              NOT NULL,
    [InvalidatedAt] DATETIME2 (7)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Table [UserRegistrations].[DataProtectionKeys]...';
GO

CREATE TABLE [UserRegistrations].[DataProtectionKeys] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [FriendlyName] NVARCHAR (255) NULL,
    [Xml]          NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Table [UserRegistrations].[InboxMessages]...';
GO

CREATE TABLE [UserRegistrations].[InboxMessages] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [OccurredOn]    DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Index [UserRegistrations].[InboxMessages].[IX_Registrations_InboxMessages_Unprocessed]...';
GO

CREATE NONCLUSTERED INDEX [IX_Registrations_InboxMessages_Unprocessed]
    ON [UserRegistrations].[InboxMessages]([OccurredOn] ASC, [Id] ASC)
    INCLUDE([Type], [Data], [ProcessedDate]) WHERE ProcessedDate IS NULL;
GO

PRINT N'Creating Table [UserRegistrations].[OutboxMessages]...';
GO

CREATE TABLE [UserRegistrations].[OutboxMessages] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [OccurredOn]    DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Index [UserRegistrations].[OutboxMessages].[IX_Registrations_OutboxMessages_Unprocessed]...';
GO

CREATE NONCLUSTERED INDEX [IX_Registrations_OutboxMessages_Unprocessed]
    ON [UserRegistrations].[OutboxMessages]([OccurredOn] ASC, [Id] ASC)
    INCLUDE([Type], [Data], [ProcessedDate]) WHERE ProcessedDate IS NULL;
GO

PRINT N'Creating Table [UserRegistrations].[InternalCommands]...';
GO

CREATE TABLE [UserRegistrations].[InternalCommands] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [EnqueueDate]   DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    [Error]         NVARCHAR (MAX)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Index [UserRegistrations].[InternalCommands].[IX_Registrations_InternalCommands_Unprocessed]...';
GO

CREATE NONCLUSTERED INDEX [IX_Registrations_InternalCommands_Unprocessed]
    ON [UserRegistrations].[InternalCommands]([EnqueueDate] ASC, [Id] ASC)
    INCLUDE([Type], [Data], [ProcessedDate], [Error]) WHERE ProcessedDate IS NULL;
GO

PRINT N'Creating Table [Users].[InternalCommands]...';
GO

CREATE TABLE [Users].[InternalCommands] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [EnqueueDate]   DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    [Error]         NVARCHAR (MAX)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Index [Users].[InternalCommands].[IX_Users_InternalCommands_Unprocessed]...';
GO

CREATE NONCLUSTERED INDEX [IX_Users_InternalCommands_Unprocessed]
    ON [Users].[InternalCommands]([EnqueueDate] ASC, [Id] ASC)
    INCLUDE([Type], [Data], [ProcessedDate], [Error]) WHERE ProcessedDate IS NULL;
GO

PRINT N'Creating Table [Users].[UserRoles]...';
GO

CREATE TABLE [Users].[UserRoles] (
    [UserId]   UNIQUEIDENTIFIER NOT NULL,
    [RoleCode] NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_Users_UserRoles_UserId_RoleCode] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleCode] ASC)
);
GO

PRINT N'Creating Table [Users].[OutboxMessages]...';
GO

CREATE TABLE [Users].[OutboxMessages] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [OccurredOn]    DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Index [Users].[OutboxMessages].[IX_Users_OutboxMessages_Unprocessed]...';
GO

CREATE NONCLUSTERED INDEX [IX_Users_OutboxMessages_Unprocessed]
    ON [Users].[OutboxMessages]([OccurredOn] ASC, [Id] ASC)
    INCLUDE([Type], [Data], [ProcessedDate]) WHERE ProcessedDate IS NULL;
GO

PRINT N'Creating Table [Users].[Permissions]...';
GO

CREATE TABLE [Users].[Permissions] (
    [Code]        VARCHAR (50)  NOT NULL,
    [Name]        VARCHAR (100) NOT NULL,
    [Description] VARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([Code] ASC)
);
GO

PRINT N'Creating Table [Users].[InboxMessages]...';
GO

CREATE TABLE [Users].[InboxMessages] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [OccurredOn]    DATETIME2 (7)    NOT NULL,
    [Type]          VARCHAR (255)    NOT NULL,
    [Data]          VARCHAR (MAX)    NOT NULL,
    [ProcessedDate] DATETIME2 (7)    NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Index [Users].[InboxMessages].[IX_Users_InboxMessages_Unprocessed]...';
GO

CREATE NONCLUSTERED INDEX [IX_Users_InboxMessages_Unprocessed]
    ON [Users].[InboxMessages]([OccurredOn] ASC, [Id] ASC)
    INCLUDE([Type], [Data], [ProcessedDate]) WHERE ProcessedDate IS NULL;
GO

PRINT N'Creating Table [Users].[RolePermissions]...';
GO

CREATE TABLE [Users].[RolePermissions] (
    [RoleCode]       VARCHAR (50) NOT NULL,
    [PermissionCode] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_RolePermissions_RoleCode_PermissionCode] PRIMARY KEY CLUSTERED ([RoleCode] ASC, [PermissionCode] ASC)
);
GO

PRINT N'Creating Table [Users].[Users]...';
GO

CREATE TABLE [Users].[Users] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [Email]     NVARCHAR (255)   NOT NULL,
    [Password]  NVARCHAR (255)   NOT NULL,
    [IsActive]  BIT              NOT NULL,
    [FirstName] NVARCHAR (50)    NOT NULL,
    [LastName]  NVARCHAR (50)    NOT NULL,
    [Name]      NVARCHAR (255)   NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Default Constraint unnamed constraint on [Meetings].[MeetingComments]...';
GO

ALTER TABLE [Meetings].[MeetingComments]
    ADD DEFAULT (0) FOR [LikesCount];
GO

PRINT N'Creating Foreign Key [Meetings].[FK_Meetings_MeetingMemberCommentLikes_Members]...';
GO

ALTER TABLE [Meetings].[MeetingMemberCommentLikes] WITH NOCHECK
    ADD CONSTRAINT [FK_Meetings_MeetingMemberCommentLikes_Members] FOREIGN KEY ([MemberId]) REFERENCES [Meetings].[Members] ([Id]);
GO

PRINT N'Creating Foreign Key [Meetings].[FK_Meetings_MeetingMemberCommentLikes_MeetingComments]...';
GO

ALTER TABLE [Meetings].[MeetingMemberCommentLikes] WITH NOCHECK
    ADD CONSTRAINT [FK_Meetings_MeetingMemberCommentLikes_MeetingComments] FOREIGN KEY ([MeetingCommentId]) REFERENCES [Meetings].[MeetingComments] ([Id]);
GO

PRINT N'Creating Foreign Key [Meetings].[FK_Meetings_MeetingCommentingConfigurations_Meetings]...';
GO

ALTER TABLE [Meetings].[MeetingCommentingConfigurations] WITH NOCHECK
    ADD CONSTRAINT [FK_Meetings_MeetingCommentingConfigurations_Meetings] FOREIGN KEY ([MeetingId]) REFERENCES [Meetings].[Meetings] ([Id]);
GO

PRINT N'Creating Foreign Key [UserRegistrations].[FK_UserRegistrations_Tokens_TokenTypes_TokenTypeId]...';
GO

ALTER TABLE [UserRegistrations].[Tokens] WITH NOCHECK
    ADD CONSTRAINT [FK_UserRegistrations_Tokens_TokenTypes_TokenTypeId] FOREIGN KEY ([TokenTypeId]) REFERENCES [UserRegistrations].[TokenTypes] ([Id]);
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

PRINT N'Creating View [Administration].[v_Members]...';
GO

CREATE VIEW [Administration].[v_Members]
AS
SELECT
    [Member].[Id],
    [Member].[Email],
    [Member].[FirstName],
    [Member].[LastName],
    [Member].[Name]
FROM [Administration].[Members] AS [Member]
GO

PRINT N'Creating View [Administration].[v_MeetingGroupProposals]...';
GO

CREATE VIEW [Administration].[v_MeetingGroupProposals]
AS
SELECT
    [MeetingGroupProposal].[Id],
    [MeetingGroupProposal].[Name],
    [MeetingGroupProposal].[Description],
    [MeetingGroupProposal].[LocationCity],
    [MeetingGroupProposal].[LocationCountryCode],
    [MeetingGroupProposal].[ProposalUserId],
    [MeetingGroupProposal].[ProposalDate],
    [MeetingGroupProposal].[StatusCode],
    [MeetingGroupProposal].[DecisionDate],
    [MeetingGroupProposal].[DecisionUserId],
    [MeetingGroupProposal].[DecisionCode],
    [MeetingGroupProposal].[DecisionRejectReason]
FROM [Administration].[MeetingGroupProposals] AS [MeetingGroupProposal]
GO

PRINT N'Creating View [Meetings].[v_Countries]...';
GO

CREATE VIEW [Meetings].[v_Countries]
AS
SELECT
    [Country].[Code],
    [Country].[Name]
FROM Meetings.Countries AS [Country]
GO

PRINT N'Creating View [Meetings].[v_MeetingAttendees]...';
GO

CREATE VIEW [Meetings].[v_MeetingAttendees]
AS
SELECT
    [MeetingAttendee].[MeetingId],
    [MeetingAttendee].[AttendeeId],
    [MeetingAttendee].[DecisionDate],
    [MeetingAttendee].[RoleCode],
    [MeetingAttendee].[GuestsNumber],

    [Member].[FirstName],
    [Member].[LastName]
FROM [Meetings].[MeetingAttendees] AS [MeetingAttendee]
    INNER JOIN [Meetings].[Members] AS [Member]
        ON [MeetingAttendee].[AttendeeId] = [Member].[Id]
GO

PRINT N'Creating View [Meetings].[v_MemberMeetingGroups]...';
GO

CREATE VIEW [Meetings].[v_MemberMeetingGroups]
AS
SELECT
    [MeetingGroup].Id,
    [MeetingGroup].[Name],
    [MeetingGroup].[Description],
    [MeetingGroup].[LocationCountryCode],
    [MeetingGroup].[LocationCity],

    [MeetingGroupMember].[MemberId],
    [MeetingGroupMember].[RoleCode],
    [MeetingGroupMember].[IsActive],
    [MeetingGroupMember].[JoinedDate]
FROM meetings.MeetingGroups AS [MeetingGroup]
    INNER JOIN [Meetings].[MeetingGroupMembers] AS [MeetingGroupMember]
        ON [MeetingGroup].[Id] = [MeetingGroupMember].[MeetingGroupId]
GO

PRINT N'Creating View [Meetings].[v_MeetingGroupMembers]...';
GO

CREATE VIEW [Meetings].[v_MeetingGroupMembers]
AS
SELECT
    [MeetingGroupMember].MeetingGroupId,
    [MeetingGroupMember].MemberId,
    [MeetingGroupMember].RoleCode
FROM [Meetings].MeetingGroupMembers AS [MeetingGroupMember]
GO

PRINT N'Creating View [Meetings].[v_MeetingComments]...';
GO

CREATE VIEW [Meetings].[v_MeetingComments]
AS
SELECT
    [MeetingComments].Id,
    [MeetingComments].MeetingId,
    [MeetingComments].AuthorId,
    [MeetingComments].InReplyToCommentId,
    [MeetingComments].Comment,
    [MeetingComments].CreateDate,
    [MeetingComments].EditDate,
    [MeetingComments].LikesCount
FROM [Meetings].[MeetingComments] AS [MeetingComments]
WHERE [MeetingComments].IsRemoved = 0
GO

PRINT N'Creating View [Meetings].[v_MeetingGroups]...';
GO

CREATE VIEW [Meetings].[v_MeetingGroups]
AS
SELECT
    [MeetingGroup].Id,
    [MeetingGroup].[Name],
    [MeetingGroup].[Description],
    [MeetingGroup].[LocationCountryCode],
    [MeetingGroup].[LocationCity]
FROM [Meetings].MeetingGroups AS [MeetingGroup]
GO

PRINT N'Creating View [Meetings].[v_MemberMeetings]...';
GO

CREATE VIEW [Meetings].[v_MemberMeetings]
AS
SELECT
	[Meeting].[Id],
    [Meeting].[Title],
    [Meeting].[Description],
    [Meeting].[LocationAddress],
    [Meeting].[LocationCity],
    [Meeting].[LocationPostalCode],
    [Meeting].[TermStartDate],
    [Meeting].[TermEndDate],

    [MeetingAttendee].[AttendeeId],
    [MeetingAttendee].[IsRemoved],
    [MeetingAttendee].[RoleCode]
FROM [Meetings].[Meetings] AS [Meeting]
    INNER JOIN [Meetings].[MeetingAttendees] AS [MeetingAttendee]
        ON [Meeting].[Id] = [MeetingAttendee].[MeetingId]
GO

PRINT N'Creating View [Meetings].[v_MeetingDetails]...';
GO

CREATE VIEW [Meetings].[v_MeetingDetails]
AS
SELECT
    [Meeting].[Id],
    [Meeting].[MeetingGroupId],
    [Meeting].[Title],
    [Meeting].[TermStartDate],
    [Meeting].[TermEndDate],
    [Meeting].[Description],
    [Meeting].[LocationName],
    [Meeting].[LocationAddress],
    [Meeting].[LocationPostalCode],
    [Meeting].[LocationCity],
    [Meeting].[AttendeesLimit],
    [Meeting].[GuestsLimit],
    [Meeting].[RSVPTermStartDate],
    [Meeting].[RSVPTermEndDate],
    [Meeting].[EventFeeValue],
    [Meeting].[EventFeeCurrency]
FROM [Meetings].[Meetings] AS [Meeting]
GO

PRINT N'Creating View [Meetings].[v_Members]...';
GO

CREATE VIEW [Meetings].[v_Members]
AS
SELECT
    [Member].Id,
    [Member].[Name],
    [Member].[Email]
FROM [Meetings].Members AS [Member]
GO

PRINT N'Creating View [Meetings].[v_MeetingGroupProposals]...';
GO

CREATE VIEW [Meetings].[v_MeetingGroupProposals]
AS
SELECT
    [MeetingGroupProposal].[Id],
    [MeetingGroupProposal].[Name],
    [MeetingGroupProposal].[Description],
    [MeetingGroupProposal].[LocationCity],
    [MeetingGroupProposal].[LocationCountryCode],
    [MeetingGroupProposal].[ProposalUserId],
    [MeetingGroupProposal].[ProposalDate],
    [MeetingGroupProposal].[StatusCode]
FROM [Meetings].[MeetingGroupProposals] AS [MeetingGroupProposal]
GO

PRINT N'Creating View [Meetings].[v_Meetings]...';
GO

CREATE VIEW [Meetings].[v_Meetings]
AS
SELECT
	Meeting.[Id],
    Meeting.[Title],
    Meeting.[Description],
    Meeting.LocationAddress,
    Meeting.LocationCity,
    Meeting.LocationPostalCode,
    Meeting.TermStartDate,
    Meeting.TermEndDate
FROM [Meetings].Meetings AS [Meeting]
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

ALTER TABLE [Meetings].[MeetingMemberCommentLikes] WITH CHECK CHECK CONSTRAINT [FK_Meetings_MeetingMemberCommentLikes_Members];

ALTER TABLE [Meetings].[MeetingMemberCommentLikes] WITH CHECK CHECK CONSTRAINT [FK_Meetings_MeetingMemberCommentLikes_MeetingComments];

ALTER TABLE [Meetings].[MeetingCommentingConfigurations] WITH CHECK CHECK CONSTRAINT [FK_Meetings_MeetingCommentingConfigurations_Meetings];

ALTER TABLE [UserRegistrations].[Tokens] WITH CHECK CHECK CONSTRAINT [FK_UserRegistrations_Tokens_TokenTypes_TokenTypeId];

ALTER TABLE [Users].[UserRoles] WITH CHECK CHECK CONSTRAINT [FK_Users_UserRoles_UserId];

ALTER TABLE [Users].[RolePermissions] WITH CHECK CHECK CONSTRAINT [FK_Users_RolePermissions_PermissionCode];
GO

PRINT N'Update complete.';