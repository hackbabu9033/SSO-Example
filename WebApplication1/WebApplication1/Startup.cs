using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo($"{Environment.CurrentDirectory}").Parent.Parent)
            .SetApplicationName("SharedCookieApp");

            services.AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth", config =>
                {
                    config.Cookie.Name = "Identity.Cookie";
                    config.Cookie.Path = "/";
                    config.Events = new CookieAuthenticationEvents()
                    {
                        OnRedirectToLogin = context =>
                        {
                            // 看導回來應該要導到首頁還是別的頁面比較好
                            // hack: 最好是能替換成config參數或用其他方式取得domainName
                            var requestUrl = "https://localhost:44335/";// 本機的localhost
                            context.Response.Redirect($"http://localhost:58375/Home/Login?DirectFrom={requestUrl}");// SSO的host
                            return Task.CompletedTask;
                        },
                    };
                });
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
