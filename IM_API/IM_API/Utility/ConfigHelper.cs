namespace IM_API.Utility
{
    public class ConfigHelper
    {
        public static IConfiguration Configuration { get; set; }
        public static string JwtKey => Configuration.GetSection("Jwt:Key").Value;
        public static string JwtIssuer => Configuration.GetSection("Jwt:Issuer").Value;
        public static string JwtAudience => Configuration.GetSection("Jwt:Audience").Value;
        public static string JwtTokenExpirationDays => Configuration.GetSection("Jwt:TokenExpirationDays").Value;
    }
}
