namespace Fabulous.MyMeetings.Scopes;

public static class Scope
{
    public static class User
    {
        public const string Read = "user:read";
        public const string Write = "user:write";
        public const string Authenticate = "user:authenticate";
    }
}