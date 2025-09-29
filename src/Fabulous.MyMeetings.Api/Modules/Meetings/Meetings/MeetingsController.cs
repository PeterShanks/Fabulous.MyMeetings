using Fabulous.MyMeetings.Api.Configuration.Authorization.Permission;
using Fabulous.MyMeetings.Api.Configuration.Authorization.Scope;
using Fabulous.MyMeetings.Modules.Meetings.Application.Contracts;
using Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.AddMeetingAttendee;
using Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.AddMeetingNotAttendee;
using Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.CancelMeeting;
using Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.ChangeMeetingMainAttributes;
using Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.ChangeNotAttendeeDecision;
using Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.CreateMeeting;
using Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.GetAuthenticatedMemberMeetings;
using Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.GetMeetingAttendees;
using Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.GetMeetingDetails;
using Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.RemoveMeetingAttendee;
using Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.SetMeetingAttendeeRole;
using Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.SetMeetingHostRole;
using Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.SignOffMemberFromWaitlist;
using Fabulous.MyMeetings.Modules.Meetings.Application.Meetings.SignUpMemberToWaitlist;
using Fabulous.MyMeetings.Scopes;
using Microsoft.AspNetCore.Mvc;

namespace Fabulous.MyMeetings.Api.Modules.Meetings.Meetings;

[Route("api/meetings/meetings")]
[ApiController]
public class MeetingsController(IMeetingsModule meetingsModule) : ControllerBase
{
    [HttpGet("")]
    [HasPermission(MeetingsPermissions.GetAuthenticatedMemberMeetings)]
    [HasScope(Scope.User.Read)]
    [ProducesResponseType(typeof(List<MemberMeetingDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAuthenticatedMemberMeetings()
    {
        var meetings = await meetingsModule.ExecuteQueryAsync(new GetAuthenticatedMemberMeetingsQuery());

        return Ok(meetings);
    }

    [HttpGet("{meetingId}")]
    [HasPermission(MeetingsPermissions.GetMeetingDetails)]
    [HasScope(Scope.User.Read)]
    [ProducesResponseType(typeof(MeetingDetailsDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMeetingDetails(Guid meetingId)
    {
        var meetingDetails = await meetingsModule.ExecuteQueryAsync(new GetMeetingDetailsQuery(meetingId));

        return Ok(meetingDetails);
    }

    [HttpPost("")]
    [HasPermission(MeetingsPermissions.CreateNewMeeting)]
    [HasScope(Scope.User.Write)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateNewMeeting([FromBody] CreateMeetingRequest request)
    {
        await meetingsModule.ExecuteCommandAsync(new CreateMeetingCommand(
            request.MeetingGroupId,
            request.Title,
            request.TermStartDate,
            request.TermEndDate,
            request.Description,
            request.MeetingLocationName,
            request.MeetingLocationAddress,
            request.MeetingLocationPostalCode,
            request.MeetingLocationCity,
            request.AttendeesLimit,
            request.GuestsLimit,
            request.RSVPTermStartDate,
            request.RSVPTermEndDate,
            request.EventFeeValue,
            request.EventFeeCurrency,
            request.HostMemberIds));

        return Ok();
    }

    [HttpPut("{meetingId}")]
    [HasPermission(MeetingsPermissions.EditMeeting)]
    [HasScope(Scope.User.Write)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> EditMeeting(
        [FromRoute] Guid meetingId,
        [FromBody] ChangeMeetingMainAttributesRequest mainAttributesRequest)
    {
        await meetingsModule.ExecuteCommandAsync(new ChangeMeetingMainAttributesCommand(
            meetingId,
            mainAttributesRequest.Title,
            mainAttributesRequest.TermStartDate,
            mainAttributesRequest.TermEndDate,
            mainAttributesRequest.Description,
            mainAttributesRequest.MeetingLocationName,
            mainAttributesRequest.MeetingLocationAddress,
            mainAttributesRequest.MeetingLocationPostalCode,
            mainAttributesRequest.MeetingLocationCity,
            mainAttributesRequest.AttendeesLimit,
            mainAttributesRequest.GuestsLimit,
            mainAttributesRequest.RSVPTermStartDate,
            mainAttributesRequest.RSVPTermEndDate,
            mainAttributesRequest.EventFeeValue,
            mainAttributesRequest.EventFeeCurrency));

        return Ok();
    }

    [HttpGet("{meetingId}/attendees")]
    [HasPermission(MeetingsPermissions.GetMeetingAttendees)]
    [HasScope(Scope.User.Read)]
    [ProducesResponseType(typeof(List<MeetingAttendeeDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMeetingAttendees(Guid meetingId)
    {
        var meetingAttendees = await meetingsModule.ExecuteQueryAsync(new GetMeetingAttendeesQuery(meetingId));

        return Ok(meetingAttendees);
    }

    [HttpPost("{meetingId}/attendees")]
    [HasPermission(MeetingsPermissions.AddMeetingAttendee)]
    [HasScope(Scope.User.Write)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddMeetingAttendee(
        [FromRoute] Guid meetingId,
        [FromBody] AddMeetingAttendeeRequest attendeeRequest)
    {
        await meetingsModule.ExecuteCommandAsync(new AddMeetingAttendeeCommand(
            meetingId,
            attendeeRequest.GuestsNumber));

        return Ok();
    }

    [HttpDelete("{meetingId}/attendees/{attendeeId}")]
    [HasPermission(MeetingsPermissions.RemoveMeetingAttendee)]
    [HasScope(Scope.User.Write)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RemoveMeetingAttendee(
        Guid meetingId,
        Guid attendeeId,
        RemoveMeetingAttendeeRequest request)
    {
        await meetingsModule.ExecuteCommandAsync(
            new RemoveMeetingAttendeeCommand(meetingId, attendeeId, request.RemovingReason));

        return Ok();
    }

    [HttpPost("{meetingId}/notAttendees")]
    [HasPermission(MeetingsPermissions.AddNotAttendee)]
    [HasScope(Scope.User.Write)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddNotAttendee(Guid meetingId)
    {
        await meetingsModule.ExecuteCommandAsync(new AddMeetingNotAttendeeCommand(meetingId));

        return Ok();
    }

    [HttpDelete("{meetingId}/notAttendees")]
    [HasPermission(MeetingsPermissions.ChangeNotAttendeeDecision)]
    [HasScope(Scope.User.Write)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ChangeNotAttendeeDecision(Guid meetingId)
    {
        await meetingsModule.ExecuteCommandAsync(new ChangeNotAttendeeDecisionCommand(meetingId));

        return Ok();
    }

    [HttpPost("{meetingId}/waitlistMembers")]
    [HasPermission(MeetingsPermissions.SignUpMemberToWaitlist)]
    [HasScope(Scope.User.Write)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SignUpMemberToWaitlist(Guid meetingId)
    {
        await meetingsModule.ExecuteCommandAsync(new SignUpMemberToWaitlistCommand(meetingId));

        return Ok();
    }

    [HttpDelete("{meetingId}/waitlistMembers")]
    [HasPermission(MeetingsPermissions.SignOffMemberFromWaitlist)]
    [HasScope(Scope.User.Write)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SignOffMemberFromWaitlist(Guid meetingId)
    {
        await meetingsModule.ExecuteCommandAsync(new SignOffMemberFromWaitlistCommand(meetingId));

        return Ok();
    }

    [HttpPost("{meetingId}/hosts")]
    [HasPermission(MeetingsPermissions.SetMeetingHostRole)]
    [HasScope(Scope.User.Write)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SetMeetingHostRole(Guid meetingId, SetMeetingHostRequest request)
    {
        await meetingsModule.ExecuteCommandAsync(new SetMeetingHostRoleCommand(request.AttendeeId, meetingId));

        return Ok();
    }

    [HttpPost("{meetingId}/attendees/attendeeRole")]
    [HasPermission(MeetingsPermissions.SetMeetingAttendeeRole)]
    [HasScope(Scope.User.Write)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SetMeetingAttendeeRole(Guid meetingId, SetMeetingHostRequest request)
    {
        await meetingsModule.ExecuteCommandAsync(new SetMeetingAttendeeRoleCommand(request.AttendeeId, meetingId));

        return Ok();
    }

    [HttpPatch("{meetingId}/cancel")]
    [HasPermission(MeetingsPermissions.CancelMeeting)]
    [HasScope(Scope.User.Write)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CancelMeeting(Guid meetingId)
    {
        await meetingsModule.ExecuteCommandAsync(new CancelMeetingCommand(meetingId));

        return Ok();
    }
}