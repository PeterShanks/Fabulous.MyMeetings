using Fabulous.MyMeetings.Modules.UserAccess.Application.Contracts;

namespace Fabulous.MyMeetings.Modules.UserAccess.Application.Configuration.Commands
{
    public abstract class InternalCommand: ICommand
    {
        public Guid Id { get; }

        protected InternalCommand()
        {
            Id = Guid.NewGuid();
        }
    }

    public abstract class InternalCommand<TResult> : ICommand<TResult>
    {
        public Guid Id { get; }

        protected InternalCommand()
        {
            Id = Guid.NewGuid();
        }
    }
}
