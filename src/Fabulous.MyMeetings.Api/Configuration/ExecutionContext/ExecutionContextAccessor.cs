using Fabulous.MyMeetings.BuildingBlocks.Application;

namespace Fabulous.MyMeetings.Api.Configuration.ExecutionContext
{
    public class ExecutionContextAccessor : IExecutionContextAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExecutionContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <exception cref="ApplicationException" accessor="get">User context is not available</exception>
        public Guid UserId
        {
            get
            {
                var value = _httpContextAccessor
                    .HttpContext?
                    .User
                    .Claims
                    .SingleOrDefault(x => x.Type == "sub")?
                    .Value;

                return value is not null
                    ? Guid.Parse(value)
                    : throw new ApplicationException("User context is not available");
            }
        }

        public Guid CorrelationId
        {
            get
            {
                if (_httpContextAccessor.HttpContext?.Request
                       .Headers.TryGetValue(CorrelationMiddleware.CorrelationHeaderKey, out var correlationId) ?? false)
                    return Guid.Parse(correlationId!);

                throw new ApplicationException("Http context and correlation id is not available");
            }
        }

        public bool IsAvailable => _httpContextAccessor.HttpContext != null;
    }
}
