namespace Fabulous.MyMeetings.BuildingBlocks.Application.Exceptions;

public class InvalidCommandException(List<string> errors) : Exception
{
    public InvalidCommandException(string error): this([error])
    {
    }
    public List<string> Errors { get; } = errors;
}