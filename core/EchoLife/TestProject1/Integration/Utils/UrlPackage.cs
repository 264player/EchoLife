namespace EchoLife.Tests.Integration.Utils
{
    internal static class UrlPackage
    {
        #region Account
        public static string Register() => "/api/account/register";

        public static string Login() => "/api/account/login";

        public static string Logout() => "/api/account/logout";

        public static string UserInfo() => "/api/account/userinfo";
        #endregion

        #region OfficousWill
        public static string Wills() => "/api/wills";

        public static string Will(string willId) => $"/api/wills/{willId}";

        public static string Wills(int count, string? cursorId) =>
            $"/api/wills?count={count}&cursorId={cursorId}";

        public static string WillVersions(string willId) => $"/api/wills/{willId}/versions";

        public static string WillVersions(string willId, int count, string? cursorId) =>
            $"/api/wills/{willId}/versions?count={count}&cursorId={cursorId}";

        public static string WillVersion(string versionId) => $"/api/wills/versions/{versionId}";
        #endregion

        #region Will Review
        public static string HumanReview(string versionId) =>
            $"/api/wills/versions/{versionId}/reviews/human";

        public static string Review(string reviwId) => $"/api/wills/versions/reviews/{reviwId}";

        public static string MyReviews(int count, string? cursorId = null) =>
            $"/api/wills/versions/reviews?count={count}&cusrorId={cursorId}";

        public static string MyReviewRequests(int count, string? cursorId = null) =>
            $"/api/wills/versions/reviews/requests?count={count}&cusrorId={cursorId}";

        public static string AllReviewRequests(int count, string? cursorId = null) =>
            $"/api/wills/versions/reviews/requests/pendding?count={count}&cusrorId={cursorId}";

        public static string ProcessReview(string reviewId) =>
            $"/api/wills/versions/reviews/{reviewId}/human/process";

        public static string CompleteReview(string reviewId) =>
            $"/api/wills/versions/reviews/{reviewId}/human/complete";
        #endregion
    }
}
