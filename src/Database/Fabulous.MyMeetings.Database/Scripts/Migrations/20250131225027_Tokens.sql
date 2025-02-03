USE [MyMeetings];
GO

PRINT N'Creating Schema [App]...';
GO

CREATE SCHEMA [App]
    AUTHORIZATION [dbo];
GO

PRINT N'Creating Table [App].[Tokens]...';
GO

CREATE TABLE [App].[Tokens] (
    [ClusterKey]  INT              IDENTITY (1, 1) NOT NULL,
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [UserId]      UNIQUEIDENTIFIER NOT NULL,
    [Value]       NVARCHAR (255)   NOT NULL,
    [TokenTypeId] INT              NOT NULL,
    [CreatedAt]   DATETIME2 (7)    NOT NULL,
    [ExpiresAt]   DATETIME2 (7)    NOT NULL,
    [IsUsed]      BIT              NOT NULL,
    [UsedAt]      DATETIME2 (7)    NULL,
    CONSTRAINT [PK_App_Tokens_Id] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Index [App].[Tokens].[IX_App_Tokens_ClusterKey]...';
GO

CREATE UNIQUE CLUSTERED INDEX [IX_App_Tokens_ClusterKey]
    ON [App].[Tokens]([ClusterKey] ASC);
GO

PRINT N'Creating Table [App].[TokenTypes]...';
GO

CREATE TABLE [App].[TokenTypes] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Table [dbo].[DataProtectionKeys]...';
GO

CREATE TABLE [App].[DataProtectionKeys] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [FriendlyName] NVARCHAR (255) NULL,
    [Xml]          NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

PRINT N'Creating Foreign Key [App].[FK_App_Tokens_TokenTypes_TokenTypeId]...';
GO

ALTER TABLE [App].[Tokens] WITH NOCHECK
    ADD CONSTRAINT [FK_App_Tokens_TokenTypes_TokenTypeId] FOREIGN KEY ([TokenTypeId]) REFERENCES [App].[TokenTypes] ([Id]);
GO

PRINT N'Checking existing data against newly created constraints';
GO

USE [MyMeetings];
GO

ALTER TABLE [App].[Tokens] WITH CHECK CHECK CONSTRAINT [FK_App_Tokens_TokenTypes_TokenTypeId];
GO


PRINT N'Update complete.';