using Fabulous.MyMeetings.BuildingBlocks.Application.Events;
using Fabulous.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals.Events;

namespace Fabulous.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.AcceptMeetingGroupProposal;

public class MeetingGroupProposalAcceptedNotification(
    Guid id, 
    MeetingGroupProposalAcceptedDomainEvent domainEvent) : DomainEventNotification<MeetingGroupProposalAcceptedDomainEvent>(domainEvent, id);