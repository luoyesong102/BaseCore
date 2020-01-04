
using Common.Service.DTOModel;
using OauthService.Models.AuthContext;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OauthService.Oauth.AuthHelper
{
    public interface IJwtFactory
    {
        JwtAuthorizationModel GenerateEncodedToken(string userName, Guid userId, string refreshToken, ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(UserContext user);
    }
}
