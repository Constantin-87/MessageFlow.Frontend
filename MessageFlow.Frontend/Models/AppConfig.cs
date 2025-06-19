namespace MessageFlow.Frontend.Models
{
    public class AppConfig
    {
        public string IdentityApiUrl { get; set; }
        public string ServerApiUrl { get; set; }
        public SocialLinks SocialLinks { get; set; } = new();
    }
}