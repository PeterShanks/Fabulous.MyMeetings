CREATE TABLE [Meetings].[MeetingCommentingConfigurations]
(
	[Id]                    UNIQUEIDENTIFIER NOT NULL PRIMARY KEY CLUSTERED,
    [MeetingId]             UNIQUEIDENTIFIER NOT NULL,
    [IsCommentingEnabled]   BIT              NOT NULL,
    CONSTRAINT [FK_Meetings_MeetingCommentingConfigurations_Meetings] FOREIGN KEY ([MeetingId]) REFERENCES Meetings.Meetings ([Id])
)
GO