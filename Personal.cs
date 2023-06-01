namespace Decanat
{
    internal class Personal
    {
        private static string _UserFullname;
        private static string _UserStatus;
        private static bool _UserIsAuth = false;

        public static string UserFullname { get => _UserFullname; set => _UserFullname = value; }
        public static string UserStatus { get => _UserStatus; set => _UserStatus = value; }
        public static bool UserIsAuth { get => _UserIsAuth; set => _UserIsAuth = value; }
    }
}
