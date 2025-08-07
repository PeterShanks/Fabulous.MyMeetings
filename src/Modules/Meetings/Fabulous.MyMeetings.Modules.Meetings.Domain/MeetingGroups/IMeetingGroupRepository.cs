namespace Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;

public interface IMeetingGroupRepository
{
    Task AddAsync(MeetingGroup meetingGroup);
    Task<MeetingGroup?> GetByIdAsync(MeetingGroupId meetingGroupId);
}