namespace Fabulous.MyMeetings.Modules.UserAccess.Domain.Users
{
    public class UserRole : ValueObject
    {
        public static UserRole Member => new(nameof(Member));

        public static UserRole Administrator => new(nameof(Administrator));

        public string Value { get; }

        private UserRole(string value)
        {
            Value = value;
        }
    }
}
