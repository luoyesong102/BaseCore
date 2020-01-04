//-----------------------------------------------------------------------
// <copyright file= "DapperMiddleware.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2019/1/31 15:00:58 
// Modified by:
// Description: 调用中间件方法
//-----------------------------------------------------------------------
using Microsoft.AspNetCore.Builder;

namespace SAAS.Api.Middlewares
{
    public static class DapperMiddlewareExtensions
    {
        /// <summary>
        /// 调用中间件
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseDapper(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DapperMiddleware>();
        }
    }
}


//services.AddDistributedRedisCache(r =>
//            {
//                r.Configuration = Configuration["Redis:ConnectionString"];
//            });
//            services.AddTransient<IDataRepository, DataRepository>();

//app.UseDapper();
