using auth_service.Conceretes;
using Newtonsoft.Json;

namespace auth_service.Helpers
{
    public static class CookieHelpercs
    {
        private static readonly string COOKIE_NAME = "EXAM_SYSTEM_AUTH_KEY";


        public static string? GetCookie(HttpRequest req)
        {
            var cookie = req.Cookies[COOKIE_NAME];
            if (cookie == null)
                return null;
            try
            {
                var authInfo = JsonConvert.DeserializeObject<AuthObject>(cookie);
            }
            catch
            {
                return null;
            }

            return cookie;
        }
    }
}
