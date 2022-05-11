using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Historias_Clinicas_D.Data; 
using Historias_Clinicas_D.Models;

namespace Historias_Clinicas_D
{
    public static class Startup
    {
        public static WebApplication InicializarApp(string[] args)
        {
            // Creamos una nueva instancia de nuestro servidor web
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder); // Lo configuramos, con sus respectivos servicios
            
            
            var app = builder.Build(); // Sobre este app, configuraremos los middleware
            Configure(app); // Configuramos los middleware.
            
            return app; // Retornamos la App, para que pueda ser ejecutada por Program.
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
         
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Modificar el generico por la clase de su Contexto
            builder.Services.AddDbContext<HistoriasClinicasContext>(options =>
                                options.UseInMemoryDatabase("HistoriasClinicasDB")
                            );
        }

        private static void Configure(WebApplication app)
        {
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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            
            // El run de la aplicación, para que la clase program, sea la que la ejecuta la aplicación. No es responsabilidad de esta clase ejecutarla.
        }
    }
}