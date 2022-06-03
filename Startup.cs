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
            /*builder.Services.AddDbContext<HistoriasClinicasContext>(options =>
                                options.UseInMemoryDatabase("HistoriasClinicasDB")
                            );*/

            builder.Services.AddDbContext<HistoriasClinicasContext>(options =>
                                options.UseSqlServer(builder.Configuration.GetConnectionString("HistoriasClinicasDB"))
                            );

            #region Identity

            builder.Services.AddIdentity<Persona, IdentityRole<int>>().AddEntityFrameworkStores<HistoriasClinicasContext>();

            builder.Services.Configure<IdentityOptions>(opciones =>
            {
                opciones.Password.RequireLowercase = false;
                opciones.Password.RequireNonAlphanumeric = false;
                opciones.Password.RequireUppercase = false;
                opciones.Password.RequireDigit = false;
                opciones.Password.RequiredLength = 6;           
            });

            // Configuraciones por defecto para Password en Identity:
            //options.Password.RequireDigit = true;
            //options.Password.RequireLowercase = true;
            //options.Password.RequireNonAlphanumeric = true;
            //options.Password.RequireUppercase = true;
            //options.Password.RequiredLength = 6;
            //options.Password.RequiredUniqueChars = 1;
            //
            //Una Password válida sería Password1! que cumple todos los requerimientos
            #endregion

            builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
            opciones =>
            {
                opciones.LoginPath = "/Account/IniciarSesion";
                opciones.AccessDeniedPath = "/Account/AccesoDenegado";
                opciones.Cookie.Name = "HistoriasClinicasCookie";
            });

            builder.Services.AddScoped<DbInitializer>();
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

            using (var servicescope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = servicescope.ServiceProvider.GetService<HistoriasClinicasContext>();
                dbContext.Database.EnsureCreated();
                servicescope.ServiceProvider.GetService<DbInitializer>().Seed();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            
            // El run de la aplicación, para que la clase program, sea la que la ejecuta la aplicación. No es responsabilidad de esta clase ejecutarla.
        }
    }
}