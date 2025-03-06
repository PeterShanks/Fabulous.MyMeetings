namespace Fabulous.MyMeetings.BuildingBlocks.Application.Exceptions;

public class InvalidCommandException(List<string> errors) : Exception
{
    public List<string> Errors { get; } = errors;
}