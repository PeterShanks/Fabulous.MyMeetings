using Fabulous.MyMeetings.Api.Configuration.Authorization.Permission;
using Fabulous.MyMeetings.Api.Configuration.Authorization.Scope;
using Fabulous.MyMeetings.Modules.Administration.Application.Contracts;
using Fabulous.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.AcceptMeetingGroupProposal;
using Fabulous.MyMeetings.Modules.Administration.Application.MeetingGroupProposals.GetMeetingGroupProposals;
using Fabulous.MyMeetings.Scopes;
using Microsoft.AspNetCore.Mvc;

namespace Fabulous.MyMeetings.Api.Modules.Administration.MeetingGroupProposals;

[ApiController]
[Route("api/administration/meeting-group-proposals")]
public class MeetingGroupProposalsController(
    IAdministrationModule administrationModule): ControllerBase
{
    [HasPermission(AdministrationPermissions.AcceptMeetingGroupProposal)]
    [HasScope(Scope.User.Read, Scope.User.Write)]
    [HttpGet]
    public async Task<IActionResult> GetMeetingGroupProposals()
    {
        var meetingGroupProposals =
            await administrationModule.ExecuteQueryAsync(new GetMeetingGroupProposalsQuery());

        return Ok(meetingGroupProposals);
    }

    [HasPermission(AdministrationPermissions.AcceptMeetingGroupProposal)]
    [HasScope(Scope.User.Write)]
    [HttpPut]
    public async Task<IActionResult> AcceptMeetingGroupProposal(Guid meetingGroupProposalId)
    {
        await administrationModule.ExecuteCommandAsync(new AcceptMeetingGroupProposalCommand(
            meetingGroupProposalId)
        );
        return Ok();
    }
}