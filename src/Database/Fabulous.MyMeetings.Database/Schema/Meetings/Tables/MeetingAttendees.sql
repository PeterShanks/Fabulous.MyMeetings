CREATE TABLE [Meetings].[MeetingAttendees]
(
	[MeetingId] UNIQUEIDENTIFIER NOT NULL,
	[AttendeeId] UNIQUEIDENTIFIER NOT NULL,
	[DecisionDate] DATETIME2(7) NOT NULL,
	[RoleCode] VARCHAR(50) NULL,   
    [GuestsNumber] INT NULL,
    [DecisionChanged] BIT NOT NULL,
    [DecisionChangeDate] DATETIME2(7) NULL,
	[IsRemoved] BIT NOT NULL,
	[RemovingMemberId] UNIQUEIDENTIFIER NULL,
	[RemovingReason] NVARCHAR(500) NULL,
	[RemovedDate] DATETIME2(7) NULL,
	[BecameNotAttendeeDate] DATETIME2(7) NULL,
	[FeeValue] DECIMAL(5, 0) NULL,
	[FeeCurrency] VARCHAR(3) NULL,
	[IsFeePaid] BIT NOT NULL,
	CONSTRAINT [PK_meetings_MeetingAttendees_Id] PRIMARY KEY CLUSTERED ([MeetingId] ASC, [AttendeeId] ASC, [DecisionDate] ASC)
)
GO