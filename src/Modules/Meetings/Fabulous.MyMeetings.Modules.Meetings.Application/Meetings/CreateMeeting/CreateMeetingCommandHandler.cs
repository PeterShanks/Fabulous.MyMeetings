using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.CreateMeeting;

public class CreateMeetingCommandHandler(
    IMemberContext memberContext,
    IMeetingRepository meetingRepository,
    IMeetingGroupRepository meetingGroupRepository) : ICommandHandler<CreateMeetingCommand, Guid>
{
    public async Task<Guid> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
    {
        var meetingGroup = await meetingGroupRepository.GetByIdAsync(new MeetingGroupId(request.MeetingGroupId));

        NotFoundException.ThrowIfNull(meetingGroup, $"Meeting group with Id {request.MeetingGroupId} was not found");

        var hostsMemberIds = request.HostMemberIds.Select(x => new MemberId(x)).ToList();

        var meeting = meetingGroup.CreateMeeting(
            request.Title,
            MeetingTerm.CreateNewBetweenDates(request.TermStartDate, request.TermEndDate),
            request.Description,
            MeetingLocation.CreateNew(request.MeetingLocationName, request.MeetingLocationAddress, request.MeetingLocationPostalCode, request.MeetingLocationCity),
            request.AttendeesLimit,
            request.GuestsLimit,
            Term.CreateNewBetweenDates(request.RsvpTermStartDate, request.RsvpTermEndDate),
            request.EventFeeValue.HasValue ? MoneyValue.Of(request.EventFeeValue.Value, request.EventFeeCurrency) : MoneyValue.Undefined,
            hostsMemberIds,
            memberContext.MemberId);

        await meetingRepository.AddAsync(meeting);

        return meeting.Id;
    }
}