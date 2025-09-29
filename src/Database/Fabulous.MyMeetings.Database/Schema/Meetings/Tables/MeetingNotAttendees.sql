CREATE TABLE [Meetings].[MeetingNotAttendees]
(
	[MeetingId] UNIQUEIDENTIFIER NOT NULL,
	[MemberId] UNIQUEIDENTIFIER NOT NULL,
    [DecisionDate] DATETIME2(7) NOT NULL,
    [DecisionChanged] BIT NOT NULL,
    [DecisionChangeDate] DATETIME2(7) NULL,
	CONSTRAINT [PK_Meetings_MeetingNotAttendees_Id] PRIMARY KEY CLUSTERED ([MeetingId] ASC, [MemberId] ASC, [DecisionDate] ASC)
)
GO