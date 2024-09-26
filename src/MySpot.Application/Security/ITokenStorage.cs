using MySpot.Application.DTO;

namespace MySpot.Application.Security;
public interface ITokenStorage
{
    public void Set(JwtDto jwt);

    public JwtDto GetJwtDto();
}