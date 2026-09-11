namespace VibeTrack.Api.Extensions
{
    public  static class CookieExtensions
    {
        public static void AppendJwtCookie(this HttpResponse response, string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(7)
            };

            response.Cookies.Append("Jwt", token, cookieOptions);
        }
        public static void DeleteJwtCookie(this HttpResponse response)
        {
            response.Cookies.Delete("Jwt");
        }
    }
}
