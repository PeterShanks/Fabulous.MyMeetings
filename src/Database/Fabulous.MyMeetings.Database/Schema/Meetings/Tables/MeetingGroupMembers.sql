CREATE TABLE [Meetings].[MeetingGroupMembers]
(
	[MeetingGroupId] UNIQUEIDENTIFIER NOT NULL,
	[MemberId] UNIQUEIDENTIFIER NOT NULL,
	[JoinedDate] DATETIME2(7) NOT NULL,
	[RoleCode] VARCHAR(50) NOT NULL,   
    [IsActive] BIT NOT NULL,
    [LeaveDate] DATETIME2(7) NULL,
	CONSTRAINT [PK_Meetings_MeetingGroupMembers_MeetingGroupId_MemberId_JoinedDate] PRIMARY KEY CLUSTERED ([MeetingGroupId] ASC, [MemberId] ASC, [JoinedDate] ASC)
)
GO
