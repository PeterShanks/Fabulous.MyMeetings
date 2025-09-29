CREATE TABLE [Administration].[MeetingGroupProposals]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY CLUSTERED,
	[Name] NVARCHAR(255) NOT NULL,
	[Description] VARCHAR(200) NULL,
    [LocationCity] NVARCHAR(50) NOT NULL,
    [LocationCountryCode] NVARCHAR(3) NOT NULL,
    [ProposalUserId] UNIQUEIDENTIFIER NOT NULL,
    [ProposalDate] DATETIME2(7) NOT NULL,
    [StatusCode] NVARCHAR(50) NOT NULL,
    [DecisionDate] DATETIME2(7) NULL,
    [DecisionUserId] UNIQUEIDENTIFIER NULL,
    [DecisionCode] NVARCHAR(50) NULL,
    [DecisionRejectReason] NVARCHAR(250) NULL
)
GO