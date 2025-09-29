using Fabulous.MyMeetings.Api.Configuration.Authorization.Permission;
using Fabulous.MyMeetings.Api.Configuration.Authorization.Scope;
using Fabulous.MyMeetings.Modules.Meetings.Application.Contracts;
using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroups.EditMeetingGroupGeneralAttributes;
using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroups.GetAllMeetingGroups;
using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroups.GetAuthenticationMemberMeetingGroups;
using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroups.GetMeetingGroupDetails;
using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroups.JoinToGroup;
using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroups.LeaveMeetingGroup;
using Fabulous.MyMeetings.Scopes;
using Microsoft.AspNetCore.Mvc;

namespace Fabulous.MyMeetings.Api.Modules.Meetings.MeetingGroups;

[Route("api/meetings/[controller]")]
[ApiController]
public class MeetingGroupsController(IMeetingsModule meetingsModule) : ControllerBase
{
    [HttpGet("")]
    [HasPermission(MeetingsPermissions.GetAuthenticatedMemberMeetingGroups)]
    [HasScope(Scope.User.Read)]
    [ProducesResponseType(typeof(List<MemberMeetingGroupDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAuthenticatedMemberMeetingGroups()
    {
        var meetingGroups = await meetingsModule.ExecuteQueryAsync(
            new GetAuthenticationMemberMeetingGroupsQuery());

        return Ok(meetingGroups);
    }

    [HttpGet("{meetingGroupId}")]
    [HasPermission(MeetingsPermissions.GetMeetingGroupDetails)]
    [HasScope(Scope.User.Read)]
    [ProducesResponseType(typeof(MeetingGroupDetailsDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMeetingGroupDetails(Guid meetingGroupId)
    {
        var meetingGroupDetails = await meetingsModule.ExecuteQueryAsync(
            new GetMeetingGroupDetailsQuery(meetingGroupId));

        return Ok(meetingGroupDetails);
    }

    [HttpGet("all")]
    [HasPermission(MeetingsPermissions.GetAllMeetingGroups)]
    [HasScope(Scope.User.Read)]
    [ProducesResponseType(typeof(List<MeetingGroupDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllMeetingGroups()
    {
        var meetingGroups = await meetingsModule.ExecuteQueryAsync(new GetAllMeetingGroupsQuery());

        return Ok(meetingGroups);
    }

    [HttpPut("{meetingGroupId}")]
    [HasPermission(MeetingsPermissions.EditMeetingGroupGeneralAttributes)]
    [HasScope(Scope.User.Write)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> EditMeetingGroupGeneralAttributes(
        [FromRoute] Guid meetingGroupId,
        [FromBody] EditMeetingGroupGeneralAttributesRequest request)
    {
        await meetingsModule.ExecuteCommandAsync(new EditMeetingGroupGeneralAttributesCommand(
            meetingGroupId,
            request.Name,
            request.Description,
            request.LocationCity,
            request.LocationCountry));

        return Ok();
    }

    [HttpPost("{meetingGroupId}/members")]
    [HasPermission(MeetingsPermissions.JoinToGroup)]
    [HasScope(Scope.User.Write)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> JoinToGroup(Guid meetingGroupId)
    {
        await meetingsModule.ExecuteCommandAsync(new JoinToGroupCommand(meetingGroupId));

        return Ok();
    }

    [HttpDelete("{meetingGroupId}/members")]
    [HasPermission(MeetingsPermissions.LeaveMeetingGroup)]
    [HasScope(Scope.User.Write)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> LeaveMeetingGroup(Guid meetingGroupId)
    {
        await meetingsModule.ExecuteCommandAsync(new LeaveMeetingGroupCommand(meetingGroupId));

        return Ok();
    }
}