using System.Text;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Domain.Tokens
{
    internal static class StreamExtensions
    {
        internal static readonly Encoding DefaultEncoding = new UTF8Encoding(false, true);

        public static BinaryReader CreateReader(this Stream stream)
        {
            return new BinaryReader(stream, DefaultEncoding, true);
        }

        public static BinaryWriter CreateWriter(this Stream stream)
        {
            return new BinaryWriter(stream, DefaultEncoding, true);
        }

        public static DateTime ReadDateTime(this BinaryReader reader)
        {
            return new DateTime(reader.ReadInt64(), DateTimeKind.Utc);
        }

        public static void Write(this BinaryWriter writer, DateTime value)
        {
            writer.Write(value.Ticks);
        }
    }
}
