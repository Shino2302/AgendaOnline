using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;

namespace AgendaOnline
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuracion para eliminar la cache
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(
                new ResponseCacheAttribute
                {
                    NoStore = true,
                    Location = ResponseCacheLocation.None,
                });
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Principal/Principal";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            //agregado para el uso de cookies
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=LoginRegister}/{action=Index}/{id?}");

            app.Run();
        }
    }
}