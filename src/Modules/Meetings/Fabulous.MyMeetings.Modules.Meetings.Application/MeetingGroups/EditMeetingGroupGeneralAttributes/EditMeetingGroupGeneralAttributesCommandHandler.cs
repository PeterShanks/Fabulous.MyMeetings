using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;

namespace Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroups.EditMeetingGroupGeneralAttributes;

public class EditMeetingGroupGeneralAttributesCommandHandler(IMeetingGroupRepository meetingGroupRepository): ICommandHandler<EditMeetingGroupGeneralAttributesCommand>
{
    public async Task Handle(EditMeetingGroupGeneralAttributesCommand request, CancellationToken cancellationToken)
    {
        var meetingGroup = await meetingGroupRepository.GetByIdAsync(new MeetingGroupId(request.MeetingGroupId));

        if (meetingGroup == null)
            throw new InvalidCommandException(["Meeting group must exist."]);

        meetingGroup.EditGeneralAttributes(
            request.Name,
            request.Description,
            MeetingGroupLocation.CreateNew(request.LocationCity, request.LocationCountry));
    }
}