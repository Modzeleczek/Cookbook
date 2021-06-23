using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Cookbook.DAL;
using Microsoft.AspNetCore.Http;
using Cookbook.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Cookbook
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
            services.AddMvc().AddSessionStateTempDataProvider(); // włączamy przechowywanie w Session danych z TempData
            services.AddAuthentication("CookieAuthentication").AddCookie("CookieAuthentication", config => // włączamy autentykację
            {
                config.Cookie.HttpOnly = true;
                config.Cookie.SecurePolicy = CookieSecurePolicy.None;
                config.Cookie.Name = "UserLoginCookie";
                config.LoginPath = "/Authentication/Login";
                config.Cookie.SameSite = SameSiteMode.Strict;
                // config.Cookie.MaxAge = TimeSpan.FromMinutes(1); // ustawia, do kiedy ważne jest ciasteczko
            });
            services.AddRazorPages().AddRazorPagesOptions(options =>
            {
                options.Conventions.AuthorizeFolder("/Admin/"); // wszystkie strony z katalogu Admin wymagają logowania
            }).AddMvcOptions(options =>
            {
                options.Filters.Add(new GlobalPageFilter(Configuration)); // rejestrujemy globalny filtr
            });

            services.AddDbContextPool<CookbookContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("CookbookDB")); // rejestrujemy kontekst bazy danych używany przez Entity Framework
            });
            services.Add(new ServiceDescriptor(typeof(ShoppingCart), new ShoppingCart())); // rejestrujemy serwis koszyka
            services.Add(new ServiceDescriptor(typeof(UserSqlDB), new UserSqlDB(Configuration))); // rejestrujemy serwis bazy danych użytkowników

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20); // usuwamy dane użytkownika z Session (koszyk i TempData) z pamięci procesu aplikacji www po 20 minutach nieaktywności
            });
            services.AddMemoryCache(); // włączamy przechowywanie Session w pamięci procesu aplikacji www
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();

            // rejestrujemy serwis uwierzytelniający
            app.UseCookiePolicy(); // koniecznie musi być w tym miejscu (kolejność wywołań ma znaczenie)
            app.UseAuthentication(); // koniecznie musi być w tym miejscu

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            app.UseImageMiddleware(); // aktywujemy nasz komponent pośredniczący
        }
    }
}
