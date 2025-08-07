using Fabulous.MyMeetings.Api.Configuration.Authorization.Permission;
using Fabulous.MyMeetings.Modules.Meetings.Application.Contracts;
using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfigurations.DisableMeetingCommentingConfiguration;
using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingCommentingConfigurations.EnableMeetingCommentingConfiguration;
using Microsoft.AspNetCore.Mvc;

namespace Fabulous.MyMeetings.Api.Modules.Meetings.MeetingCommentingConfiguration;

[Route("api/meetings/meetings/{meetingId}/configuration/commenting")]
[ApiController]
public class MeetingCommentingConfigurationController(IMeetingsModule meetingsModule) : ControllerBase
{
    [HttpPatch("disable")]
    [HasPermission(MeetingsPermissions.DisableMeetingCommenting)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DisableCommenting(Guid meetingId)
    {
        await meetingsModule.ExecuteCommandAsync(new DisableMeetingCommentingConfigurationCommand(meetingId));
        return Ok();
    }

    [HttpPatch("enable")]
    [HasPermission(MeetingsPermissions.EnableMeetingCommenting)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> EnableCommenting(Guid meetingId)
    {
        await meetingsModule.ExecuteCommandAsync(new EnableMeetingCommentingConfigurationCommand(meetingId));
        return Ok();
    }
}