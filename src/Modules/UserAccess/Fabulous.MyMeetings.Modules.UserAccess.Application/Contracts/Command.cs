namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts
{
    public abstract class Command: ICommand
    {
        public Guid Id { get; }

        protected Command()
        {
            Id = Guid.NewGuid();
        }
    }

    public abstract class Command<TResult> : ICommand<TResult>
    {
        public Guid Id { get; }

        protected Command()
        {
            Id = Guid.NewGuid();
        }
    }
}
