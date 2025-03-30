namespace EchoLife.Tests.Integration.Utils
{
    internal static class UrlPackage
    {
        #region BaseUser
        public static string bRegister() => "api/BaseUser/Register";

        public static string Login0() => "api/BaseUser/Login";

        public static string Me() => "api/me";
        #endregion
        #region Account
        public static string Register() => "api/account/register";

        public static string Login() => "api/account/login";

        public static string Logout() => "api/account/logout";
        #endregion
    }
}
