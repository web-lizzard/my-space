using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MySpot.Application.DTO;
using MySpot.Application.Security;
using MySpot.Core.Time;
using MySpot.Infrastructure.Auth;

internal sealed class Authenticator(IOptions<AuthOptions> options, IClock clock) : IAuthenticator
{
    private readonly IOptions<AuthOptions> _options = options;
    private readonly IClock _clock = clock;
    private readonly JwtSecurityTokenHandler _jwtSecurityHandler = new JwtSecurityTokenHandler();

    public JwtDto CreateToken(Guid userId, string role, string jobTitle, string fullName)
    {
        var now = _clock.Current();
        var expires = now.Add(_options.Value.Expiry ?? TimeSpan.FromHours(1));
        var claims = new List<Claim> {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new(ClaimTypes.Role, role),
            new(ClaimsType.JobTitleClaim, jobTitle),
            new(ClaimsType.FullNameClaim, fullName)
        };

        var jwt = new JwtSecurityToken(
            _options.Value.Issuer,
            _options.Value.Audience,
            claims,
            now,
            expires,
            GetSigningCredentials());

        return new()
        {
            AccessToken = _jwtSecurityHandler.WriteToken(jwt)
        };
    }


    private SigningCredentials GetSigningCredentials()
    {
        return new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.Value.SigningKey)
            ),
            SecurityAlgorithms.HmacSha256
        );
    }
}
