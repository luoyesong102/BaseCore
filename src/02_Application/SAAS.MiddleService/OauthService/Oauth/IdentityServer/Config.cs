using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Models;
using IdentityServer4.Test;

using static IdentityServer4.IdentityServerConstants;

namespace OauthService.OauthConfig
{
    /// <summary>
    /// Idnetity配置，初始化Identityserver
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 定义要保护的资源（webapi）
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
               
                new ApiResource("api", "My API"){ ApiSecrets = { new IdentityServer4.Models.Secret("apipwd".Sha256()) }}
            };
        }

        // 被保护的 IdentityResource
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
            // 如果要请求 OIDC 预设的 scope 就必须要加上 OpenId(),
            // 加上他表示这个是一个 OIDC 协议的请求
            // Profile Address Phone Email 全部是属于 OIDC 预设的 scope
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Address(),
            new IdentityResources.Phone(),
            new IdentityResources.Email()
            };
        }



        /// <summary>
        /// 定义可以访问该API的客户端
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                //resource owner password grant client
                new Client
                {
                    ClientId = "pwd_client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AccessTokenType = AccessTokenType.Reference,

                    ClientSecrets =
                    {
                        new Secret("pwd_secret".Sha256())
                    },
                     AllowOfflineAccess = true,//是否获取刷新token  Client Credentials模式不支持RefreshToken的，因此不需要设置OfflineAccess
                   // RequireClientSecret=false,//是否要求密码
                   AccessTokenLifetime = 5,//设置token时间
                    AllowedScopes = { "api" }
                }
            };
        }

        /// <summary>
        /// 配置测试用户
        /// </summary>
        /// <returns></returns>
        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId="1",
                    Username="jgl",
                    Password="123"
                }
            };
        }
    }

    
}
