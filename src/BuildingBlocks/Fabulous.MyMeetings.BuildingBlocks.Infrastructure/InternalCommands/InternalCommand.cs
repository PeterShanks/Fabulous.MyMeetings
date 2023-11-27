namespace Fabulous.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands
{
    public class InternalCommand
    {
        public required Guid Id { get; set; }

        public required string Type { get; set; }

        public required string Data { get; set; }

        public DateTime? ProcessedDate { get; set; }
    }
}
