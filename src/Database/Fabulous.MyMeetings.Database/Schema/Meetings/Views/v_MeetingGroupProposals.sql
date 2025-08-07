CREATE VIEW [Meetings].[v_MeetingGroupProposals]
AS
SELECT
    [MeetingGroupProposal].[Id],
    [MeetingGroupProposal].[Name],
    [MeetingGroupProposal].[Description],
    [MeetingGroupProposal].[LocationCity],
    [MeetingGroupProposal].[LocationCountryCode],
    [MeetingGroupProposal].[ProposalUserId],
    [MeetingGroupProposal].[ProposalDate],
    [MeetingGroupProposal].[StatusCode]
FROM [Meetings].[MeetingGroupProposals] AS [MeetingGroupProposal]
GO