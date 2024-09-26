using Microsoft.AspNetCore.Http;
using MySpot.Application.DTO;
using MySpot.Application.Security;

namespace MySpot.Infrastructure.Auth;
internal sealed class HttpContextTokenStorage(IHttpContextAccessor httpContextAccessor) : ITokenStorage
{
    private const string TokenKey = "jwt";
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    public JwtDto GetJwtDto()
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            return null;
        }

        if (_httpContextAccessor.HttpContext.Items.TryGetValue(TokenKey, out var jwt))
        {
            return jwt as JwtDto;
        }

        return null;
    }

    public void Set(JwtDto jwt)
    {
        _httpContextAccessor.HttpContext?.Items.TryAdd(TokenKey, jwt);
    }
}