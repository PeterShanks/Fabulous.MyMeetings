CREATE TABLE [Meetings].[MeetingWaitlistMembers]
(
	[MeetingId] UNIQUEIDENTIFIER NOT NULL,
	[MemberId] UNIQUEIDENTIFIER NOT NULL,
    [SignUpDate] DATETIME2(7) NOT NULL,
    [IsSignedOff] BIT NOT NULL,
    [SignOffDate] DATETIME2(7) NULL,
    [IsMovedToAttendees] BIT NOT NULL,
    [MovedToAttendeesDate] DATETIME2(7) NULL,
	CONSTRAINT [PK_Meetings_MeetingWaitlistMembers_MeetingId_MemberId_SignUpDate] PRIMARY KEY CLUSTERED ([MeetingId] ASC, [MemberId] ASC, [SignUpDate] ASC)
)
GO