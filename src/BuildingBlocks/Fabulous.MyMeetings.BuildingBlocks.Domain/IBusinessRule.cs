namespace Fabulous.MyMeetings.BuildingBlocks.Domain;

public interface IBusinessRule
{
    string Message { get; }
    bool IsBroken();
}