using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ConfigurationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using OauthService.Models.AuthContext;
using OauthService.Oauth.AuthHelper;
using OauthService.OauthModel;
using SAAS.Api.Validate;
using SAAS.FrameWork.Module.Api.Data;
using SAAS.FrameWork.Util.Exp;
using User.Service;

namespace SAAS.Api
{
    /// <summary>
    /// 认证
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly IUserService _userService;
        private readonly IMemoryCache _cache;

        public AuthController(IJwtFactory jwtFactory, IOptions<JwtIssuerOptions> jwtOptions, IUserService userService, IMemoryCache cache)
        {
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
            _userService = userService;
            _cache = cache;
        }

        /// <summary>
        /// Log in
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public ApiReturn<JwtAuthorizationModel> Login(LoginRequest request)
        {
           // LoginRequest request = new LoginRequest();
            var user = _userService.GetUserByName(request.UserName);
            if (user == null)
            {
               throw new BaseException("login_failure", "Invalid username.");
              
            }
            if (!request.PassWord.Equals(user.Password))
            {
                throw new BaseException("login_failure", "Invalid password.");
            }

            string refreshToken = Guid.NewGuid().ToString();
            var claimsIdentity = _jwtFactory.GenerateClaimsIdentity(user);

            _cache.Set(refreshToken, user.UserName, TimeSpan.FromMinutes(11));
            var token =  _jwtFactory.GenerateEncodedToken(user.UserName, user.Id, refreshToken, claimsIdentity);
            return token;
        }

        /// <summary>
        /// RefreshToken
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ApiReturn<JwtAuthorizationModel>> RefreshToken(RefreshTokenRequest request)
        {
            string userName;
            if (!_cache.TryGetValue(request.RefreshToken, out userName))
            {
                throw new BaseException("refreshtoken_failure", "Invalid refreshtoken.");
               
            }
            if (!request.UserName.Equals(userName))
            {
                throw new BaseException("refreshtoken_failure", "Invalid userName.");
              
            }

            var user = _userService.GetUserByName(request.UserName);
            string newRefreshToken = Guid.NewGuid().ToString();
            var claimsIdentity = _jwtFactory.GenerateClaimsIdentity(user);

            _cache.Remove(request.RefreshToken);
            _cache.Set(newRefreshToken, user.Id, TimeSpan.FromMinutes(11));

            var token =  _jwtFactory.GenerateEncodedToken(user.UserName, user.Id, newRefreshToken, claimsIdentity);
            return token;
        }

        /// <summary>
        /// Get User Info
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        [Authorize]
        public ApiReturn<object> GetUserInfo()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            return claimsIdentity.Claims.ToList().Select(r=> new { r.Type, r.Value}).ToList();
        }
    }
}