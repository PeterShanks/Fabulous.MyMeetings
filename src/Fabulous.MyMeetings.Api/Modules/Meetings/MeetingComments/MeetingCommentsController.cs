using Fabulous.MyMeetings.Api.Configuration.Authorization.Permission;
using Fabulous.MyMeetings.Api.Configuration.Authorization.Scope;
using Fabulous.MyMeetings.Modules.Meetings.Application.Contracts;
using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingComment;
using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingCommentLike;
using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingComments.AddMeetingCommentReply;
using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingComments.EditMeetingComment;
using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingComments.RemoveMeetingComment;
using Fabulous.MyMeetings.Modules.Meetings.Application.MeetingComments.RemoveMeetingCommentLike;
using Fabulous.MyMeetings.Scopes;
using Microsoft.AspNetCore.Mvc;

namespace Fabulous.MyMeetings.Api.Modules.Meetings.MeetingComments;

[Route("api/meetings/[controller]")]
[ApiController]
public class MeetingCommentsController(IMeetingsModule meetingModule) : ControllerBase
{
    [HttpPost]
    [HasPermission(MeetingsPermissions.AddMeetingComment)]
    [HasScope(Scope.User.Read)]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddComment([FromBody] AddMeetingCommentRequest request)
    {
        var commentId =
            await meetingModule.ExecuteCommandAsync(new AddMeetingCommentCommand(
                request.MeetingId,
                request.Comment));

        return Ok(commentId);
    }

    [HttpPut("{meetingCommentId}")]
    [HasPermission(MeetingsPermissions.EditMeetingComment)]
    [HasScope(Scope.User.Read)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> EditComment(
        [FromRoute] Guid meetingCommentId,
        [FromBody] EditMeetingCommentRequest request)
    {
        await meetingModule.ExecuteCommandAsync(new EditMeetingCommentCommand(
            meetingCommentId,
            request.EditedComment));

        return Ok();
    }

    [HttpDelete("{meetingCommentId}")]
    [HasPermission(MeetingsPermissions.RemoveMeetingComment)]
    [HasScope(Scope.User.Read)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteComment([FromRoute] Guid meetingCommentId, [FromQuery] string reason)
    {
        await meetingModule.ExecuteCommandAsync(
            new RemoveMeetingCommentCommand(meetingCommentId, reason));

        return Ok();
    }

    [HttpPost("{meetingCommentId}/replies")]
    [HasPermission(MeetingsPermissions.AddMeetingCommentReply)]
    [HasScope(Scope.User.Read)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddReply([FromRoute] Guid meetingCommentId, [FromBody] string reply)
    {
        await meetingModule.ExecuteCommandAsync(new AddReplyToMeetingCommentCommand(meetingCommentId, reply));

        return Ok();
    }

    [HttpPost("{meetingCommentId}/likes")]
    [HasPermission(MeetingsPermissions.LikeMeetingComment)]
    [HasScope(Scope.User.Write)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> LikeComment([FromRoute] Guid meetingCommentId)
    {
        await meetingModule.ExecuteCommandAsync(
            new AddMeetingCommentLikeCommand(meetingCommentId));

        return Ok();
    }

    [HttpDelete("{meetingCommentId}/likes")]
    [HasPermission(MeetingsPermissions.UnlikeMeetingComment)]
    [HasScope(Scope.User.Write)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UnlikeComment([FromRoute] Guid meetingCommentId)
    {
        await meetingModule.ExecuteCommandAsync(
            new RemoveMeetingCommentLikeCommand(meetingCommentId));

        return Ok();
    }
}