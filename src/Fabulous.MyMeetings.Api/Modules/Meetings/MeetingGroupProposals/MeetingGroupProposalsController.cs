using Fabulous.MyMeetings.Api.Configuration.Authorization.Permission;
using Fabulous.MyMeetings.Modules.Meetings.Application.Contracts;
using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetAllMeetingGroupProposals;
using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetMeetingGroupProposal;
using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.GetMemberMeetingGroupProposals;
using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingGroupProposals.ProposeMeetingGroup;
using Microsoft.AspNetCore.Mvc;

namespace Fabulous.MyMeetings.Api.Modules.Meetings.MeetingGroupProposals;

[Route("api/meetings/[controller]")]
[ApiController]
public class MeetingGroupProposalsController(IMeetingsModule meetingsModule) : ControllerBase
{
    [HttpGet("")]
    [HasPermission(MeetingsPermissions.GetMeetingGroupProposals)]
    [ProducesResponseType(typeof(List<MeetingGroupProposalDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMemberMeetingGroupProposals()
    {
        var meetingGroupProposals = await meetingsModule.ExecuteQueryAsync(
            new GetMemberMeetingGroupProposalsQuery());

        return Ok(meetingGroupProposals);
    }

    [HttpGet("all")]
    [HasPermission(MeetingsPermissions.GetMeetingGroupProposals)]
    [ProducesResponseType(typeof(List<MeetingGroupProposalDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllMeetingGroupProposals(int? page, int? perPage)
    {
        var meetingGroupProposals = await meetingsModule.ExecuteQueryAsync(
            new GetAllMeetingGroupProposalsQuery(page, perPage));

        return Ok(meetingGroupProposals);
    }

    [HttpPost("")]
    [HasPermission(MeetingsPermissions.ProposeMeetingGroup)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ProposeMeetingGroup(ProposeMeetingGroupRequest request)
    {
        await meetingsModule.ExecuteCommandAsync(
            new ProposeMeetingGroupCommand(
                request.Name,
                request.Description,
                request.LocationCity,
                request.LocationCountryCode));

        return Ok();
    }
}