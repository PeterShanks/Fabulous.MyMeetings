using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.ChangeMeetingMainAttributes;

public class ChangeMeetingMainAttributesCommandHandler(
    IMemberContext memberContext,
    IMeetingRepository meetingRepository): ICommandHandler<ChangeMeetingMainAttributesCommand>
{
    public async Task Handle(ChangeMeetingMainAttributesCommand request, CancellationToken cancellationToken)
    {
        var meeting = await meetingRepository.GetByIdAsync(new MeetingId(request.MeetingId));

        NotFoundException.ThrowIfNull(meeting, $"Meeting with Id {request.MeetingId} was not found");

        meeting.ChangeMainAttributes(
            request.Title,
            MeetingTerm.CreateNewBetweenDates(request.TermStartDate, request.TermEndDate),
            request.Description,
            MeetingLocation.CreateNew(request.MeetingLocationName, request.MeetingLocationAddress, request.MeetingLocationPostalCode, request.MeetingLocationCity),
            MeetingLimits.Create(request.AttendeesLimit, request.GuestsLimit),
            Term.CreateNewBetweenDates(request.RsvpTermStartDate, request.RsvpTermEndDate),
            request.EventFeeValue.HasValue ? MoneyValue.Of(request.EventFeeValue.Value, request.EventFeeCurrency) : MoneyValue.Undefined,
            memberContext.MemberId);
    }
}