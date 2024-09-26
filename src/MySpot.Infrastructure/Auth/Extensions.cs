using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MySpot.Application.Security;

namespace MySpot.Infrastructure.Auth;
internal static class Extensions
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new AuthOptions();
        configuration.GetSection(AuthOptions.Auth).Bind(options);


        services.Configure<AuthOptions>(configuration.GetRequiredSection("auth"));
        services.AddSingleton<IAuthenticator, Authenticator>();
        services.AddSingleton<ITokenStorage, HttpContextTokenStorage>();
        services
        .AddAuthentication((x) =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer((x) =>
        {
            x.Audience = options.Audience;
            x.IncludeErrorDetails = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = options.Issuer,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey)),
            };
        });

        services.AddAuthorization();

        return services;
    }
}