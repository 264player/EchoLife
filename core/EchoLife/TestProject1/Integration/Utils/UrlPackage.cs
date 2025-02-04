namespace EchoLife.Tests.Integration.Utils
{
    internal static class UrlPackage
    {
        #region BaseUser
        public static string Register() => "api/BaseUser/Register";

        public static string Login() => "api/BaseUser/Login";

        public static string Me() => "api/me";
        #endregion
    }
}
