CREATE TABLE [Meetings].[MeetingComments]
(
	[Id]                    UNIQUEIDENTIFIER NOT NULL PRIMARY KEY CLUSTERED,
    [MeetingId]             UNIQUEIDENTIFIER NOT NULL,
    [AuthorId]              UNIQUEIDENTIFIER NOT NULL,
    [InReplyToCommentId]    UNIQUEIDENTIFIER NULL,
    [Comment]               VARCHAR(300)     NULL,
    [IsRemoved]             BIT              NOT NULL,
    [RemovedByReason]       VARCHAR(300)     NULL,
    [CreateDate]            DATETIME2(7)     NOT NULL,
    [EditDate]              DATETIME2(7)     NULL,
    [LikesCount]            INT DEFAULT(0)   NOT NULL
)
GO
