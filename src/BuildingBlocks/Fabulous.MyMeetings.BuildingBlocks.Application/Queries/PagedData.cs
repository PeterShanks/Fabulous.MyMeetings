namespace Fabulous.MyMeetings.BuildingBlocks.Application.Queries;

public struct PagedData(int? offset, int? next)
{
    public int? Offset { get; } = offset;
    public int? Next { get; } = next;
}