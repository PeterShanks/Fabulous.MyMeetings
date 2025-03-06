USE [MyMeetings];
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

PRINT N'Creating Table [UserRegistrations].[Tokens]...';
GO

CREATE TABLE [UserRegistrations].[Tokens] (
    [ClusterKey]    INT              IDENTITY (1, 1) NOT NULL,
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
    CONSTRAINT [PK_UserRegistrations_Tokens_Id] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Index [UserRegistrations].[Tokens].[IX_UserRegistrations_Tokens_ClusterKey]...';
GO

CREATE UNIQUE CLUSTERED INDEX [IX_UserRegistrations_Tokens_ClusterKey]
    ON [UserRegistrations].[Tokens]([ClusterKey] ASC);
GO

PRINT N'Creating Table [UserRegistrations].[TokenTypes]...';
GO

CREATE TABLE [UserRegistrations].[TokenTypes] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Foreign Key [UserRegistrations].[FK_UserRegistrations_Tokens_TokenTypes_TokenTypeId]...';
GO

ALTER TABLE [UserRegistrations].[Tokens] WITH NOCHECK
    ADD CONSTRAINT [FK_UserRegistrations_Tokens_TokenTypes_TokenTypeId] FOREIGN KEY ([TokenTypeId]) REFERENCES [UserRegistrations].[TokenTypes] ([Id]);
GO

PRINT N'Checking existing data against newly created constraints';
GO

USE [MyMeetings];
GO

ALTER TABLE [UserRegistrations].[Tokens] WITH CHECK CHECK CONSTRAINT [FK_UserRegistrations_Tokens_TokenTypes_TokenTypeId];
GO

PRINT N'Update complete.';