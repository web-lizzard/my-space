namespace MySpot.Infrastructure.Auth;
public class AuthOptions
{
    public const string Auth = "AUTH";
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SigningKey { get; set; }
    public TimeSpan? Expiry { get; set; }

}