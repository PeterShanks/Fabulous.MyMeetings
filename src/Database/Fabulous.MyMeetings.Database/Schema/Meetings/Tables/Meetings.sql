CREATE TABLE [Meetings].[Meetings]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY CLUSTERED,
    [MeetingGroupId] UNIQUEIDENTIFIER NOT NULL,
    [CreatorId] UNIQUEIDENTIFIER NOT NULL,
    [CreateDate] DATETIME2(7) NOT NULL,
    [Title] NVARCHAR(200) NOT NULL,
    [Description] NVARCHAR(4000) NOT NULL,
    [TermStartDate] DATETIME2(7) NOT NULL,
    [TermEndDate] DATETIME2(7) NOT NULL,
    [LocationName] NVARCHAR(200) NOT NULL,
    [LocationAddress] NVARCHAR(200) NOT NULL,
    [LocationPostalCode] NVARCHAR(200) NULL,
    [LocationCity] NVARCHAR(50) NOT NULL,
    [AttendeesLimit] INT NULL,
    [GuestsLimit] INT NOT NULL,
    [RSVPTermStartDate] DATETIME2(7) NULL,
    [RSVPTermEndDate] DATETIME2(7) NULL,
    [EventFeeValue] DECIMAL(5, 0) NULL,
    [EventFeeCurrency] VARCHAR(3) NULL,
	[ChangeDate] DATETIME2(7) NULL,
	[ChangeMemberId] UNIQUEIDENTIFIER NULL,
	[CancelDate] DATETIME2(7) NULL,
	[CancelMemberId] UNIQUEIDENTIFIER NULL,
	[IsCanceled] BIT NOT NULL
)
GO