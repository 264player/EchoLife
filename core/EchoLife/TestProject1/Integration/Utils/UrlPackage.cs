namespace EchoLife.Tests.Integration.Utils
{
    internal static class UrlPackage
    {
        #region Account
        public static string Register() => "/api/account/register";

        public static string Login() => "/api/account/login";

        public static string Logout() => "/api/account/logout";
        #endregion

        #region OfficousWill
        public static string Wills() => "/api/wills";

        public static string Will(string willId) => $"/api/wills/{willId}";

        public static string WillVersions() => "/api/wills";

        public static string WillVersion(string willId) => $"/api/wills/{willId}";
        #endregion
    }
}
