CREATE TABLE [Meetings].[MeetingMemberCommentLikes]
(
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [MemberId]  UNIQUEIDENTIFIER NOT NULL,
    [MeetingCommentId] UNIQUEIDENTIFIER NOT NULL,
    [LikedDate] DATETIME2(7) NOT NULL,
    CONSTRAINT [PK_Meetings_MeetingMemberCommentLikes_Id] PRIMARY KEY CLUSTERED (Id ASC),
    CONSTRAINT [FK_Meetings_MeetingMemberCommentLikes_Members] FOREIGN KEY ([MemberId]) REFERENCES Meetings.Members ([Id]),
    CONSTRAINT [FK_Meetings_MeetingMemberCommentLikes_MeetingComments] FOREIGN KEY ([MeetingCommentId]) REFERENCES meetings.MeetingComments ([Id])
)
GO
