using Common.Service.DTOModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OauthService.Models.AuthContext;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace OauthService.Oauth.AuthHelper
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtIssuerOptions _jwtOptions;
        /// <summary>
        /// 已授权的 Token 信息集合
        /// </summary>
        private static ISet<JwtAuthorizationModel> _tokens = new HashSet<JwtAuthorizationModel>();
        /// <summary>
        /// 获取 HTTP 请求上下文
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMemoryCache _cache;

        public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions, IHttpContextAccessor httpContextAccessor, IMemoryCache cache)
        {
            _jwtOptions = jwtOptions.Value;
            _cache = cache;
            ThrowIfInvalidOptions(_jwtOptions);
        }

        public JwtAuthorizationModel GenerateEncodedToken(string userName,Guid userId ,string refreshToken, ClaimsIdentity identity)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

         

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,//创建声明信息
                Issuer = _jwtOptions.Issuer,//Jwt token 的签发者
                Audience = _jwtOptions.Audience,//Jwt token 的接收者
                Expires = _jwtOptions.Expiration,//过期时间
                SigningCredentials = _jwtOptions.SigningCredentials//创建 token
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            //存储 Token 信息
            var jwt = new JwtAuthorizationModel
            {
                UserId = userId,
                Token = tokenHandler.WriteToken(token),
                Ouths= ToUnixEpochDate(_jwtOptions.IssuedAt),
                Expires  = ToUnixEpochDate(_jwtOptions.Expiration),
                RefreshToken = refreshToken,
            };
           
            _tokens.Add(jwt);
            return jwt;
           // return JsonConvert.SerializeObject(jwt, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }

        public ClaimsIdentity GenerateClaimsIdentity(UserContext user)
        {
            //将用户信息添加到 Claim 中
            user.Email = "jianglei@105.163.com";
            user.Phone = "13501234321";
              var identity = new ClaimsIdentity(new GenericIdentity(user.UserName, "Token"));

            IEnumerable<Claim> claims = new Claim[] {
                new Claim("id", user.Id.ToString()),
                 new Claim("userType",((int)user.UserType).ToString()),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
                 new Claim(ClaimTypes.MobilePhone,user.Phone),
                new Claim(ClaimTypes.Expiration,_jwtOptions.Expiration.ToString())
            };
         
            foreach (var role in user.Roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }
            identity.AddClaims(claims);
            return identity;
        }


        /// <summary>
        /// 获取 HTTP 请求的 Token 值
        /// </summary>
        /// <returns></returns>
        private string GetCurrentAsync()
        {
            //http header
            var authorizationHeader = _httpContextAccessor
                .HttpContext.Request.Headers["authorization"];

            //token
            return authorizationHeader == StringValues.Empty
                ? string.Empty
                : authorizationHeader.Single().Split(' ').Last();// bearer tokenvalue
        }
        /// <summary>
        /// 判断是否存在当前 Token
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns></returns>
        private JwtAuthorizationModel GetExistenceToken(string token)
            => _tokens.SingleOrDefault(x => x.Token == token);
        /// <summary>
        /// 设置缓存中过期 Token 值的 key
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns></returns>
        private static string GetKey(string token)
            => $"deactivated token:{token}";
        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }
    }
}
