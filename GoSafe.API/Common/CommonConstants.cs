namespace GoSafe.API.Common
{
    public static class CommonConstants
    {
        public static string dbConnectionString = string.Empty;
        public static string TokenSecretKey = "JWTAuthenticationHIGHsecuredPasswordjikiefdkjfurjx";
        public static int TokenValidationInMinutes = 30;
        public static string TokenIssuer = "http://localhost:5000";
        public static string TokenValidAudience = "http://localhost:4200";
    }
}
