
namespace GoSafe.API.Common
{
    public static class CommonConstants
    {
        public static string dbConnectionString = string.Empty;
        public static string TokenSecretKey;
        public static int TokenValidationInMinutes = 30;
        public static string TokenIssuer = "http://localhost:5000";
        public static string TokenValidAudience = "http://localhost:4200";

        public static bool EnableDevMode = true;
        public static string ProductionErrorMessage = "Something went wrong! Please contact administrator";
        public static string FirebaseDbUrl = "https://gosafe-9c8e7-default-rtdb.asia-southeast1.firebasedatabase.app/";

        public static int PASSWORD_SALT_LENGTH = 10;
        public static int PAGE_SIZE = 10;

        public static void LoadConfig()
        {
            var configBuilder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json");

            var configuration = configBuilder.Build();
            dbConnectionString = configuration.GetValue<string>("ConnectionStrings:Default")!;
            TokenValidationInMinutes = configuration.GetValue<int>("JWT:TokenValidationInMinutes")!;
            TokenSecretKey = configuration.GetValue<string>("JWT:TokenSecretKey")!;
            TokenIssuer = configuration.GetValue<string>("JWT:TokenIssuer")!;
            TokenValidAudience = configuration.GetValue<string>("JWT:TokenValidAudience")!;

            EnableDevMode = configuration.GetValue<bool>("Mode:EnableDevMode")!;
            ProductionErrorMessage = configuration.GetValue<string>("Message:ProductionErrorMessage")!;
        }
    }
}
