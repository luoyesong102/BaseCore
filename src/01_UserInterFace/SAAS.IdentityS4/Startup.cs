using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using IdentityS4.Data;
using IdentityS4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;

namespace IdentityS4
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Config.Configuration = this.Configuration;
            var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            services.AddDbContext<EFContext>(options=>options.UseSqlServer(Configuration.GetConnectionString("conn")));//注入DbContext
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddIdentityServer()//Ids4服务
                                        // .AddDeveloperSigningCredential()
                .AddSigningCredential(new X509Certificate2(Path.Combine(basePath, Configuration["Certificates:CerPath"]), Configuration["Certificates:Password"]))
                .AddInMemoryIdentityResources(Config.GetIdentityResources()) ////配置单点资源
                 .AddInMemoryApiResources(Config.GetApiResources())  //配置认证资源
                .AddInMemoryClients(Config.GetClients())////把配置文件的Client配置资源放到内存
                .AddTestUsers(Config.GetTestUsers()); //配置用户密码的默认测试用户类似超级用户;  

            services.AddTransient<IAdminService,AdminService>();//service注入
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseIdentityServer();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
