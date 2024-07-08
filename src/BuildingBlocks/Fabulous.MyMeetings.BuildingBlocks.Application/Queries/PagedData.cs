namespace Fabulous.MyMeetings.BuildingBlocks.Application.Queries;

public struct PagedData
{
    public PagedData(int? offset, int? next)
    {
        Offset = offset;
        Next = next;
    }

    public int? Offset { get; }
    public int? Next { get; }
}