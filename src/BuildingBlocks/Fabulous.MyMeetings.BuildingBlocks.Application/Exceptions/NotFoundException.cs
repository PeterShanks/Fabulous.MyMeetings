using System.Diagnostics.CodeAnalysis;

namespace Fabulous.MyMeetings.BuildingBlocks.Application.Exceptions
{
    public class NotFoundException(string message) : Exception(message)
    {
        public static void ThrowIfNull<T>([NotNull] T? obj)
        {
            if (obj == null)
                Throw($"An object of {typeof(T).Name} was not found");
        }

        [DoesNotReturn]
        private static void Throw(string message) => throw new NotFoundException(message);
    }
}
