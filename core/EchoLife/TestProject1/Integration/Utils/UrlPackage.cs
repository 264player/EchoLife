namespace EchoLife.Tests.Integration.Utils
{
    internal static class UrlPackage
    {
        #region BaseUser
        public static string bRegister() => "api/BaseUser/Register";

        public static string Login() => "api/BaseUser/Login";

        public static string Me() => "api/me";
        #endregion
        #region Account
        public static string Register() => "api/account/register";
        #endregion
    }
}
