CREATE TABLE [App].[DataProtectionKeys] (
    [Id]        INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [FriendlyName] NVARCHAR(255) NULL,
    [Xml]       NVARCHAR(MAX) NOT NULL
);
