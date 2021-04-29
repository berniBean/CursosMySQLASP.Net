using Dominio;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistencia;
using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostServer = CreateHostBuilder(args).Build();

            using (var ambiente = hostServer.Services.CreateScope())
            {
                var services = ambiente.ServiceProvider;
                try
                {
                    //para creación de usuarios de prueba
                    var userManager = services.GetRequiredService<UserManager<Usuario>>();
                    var context = services.GetRequiredService<cursosbasesContext>();
                    context.Database.Migrate();
                    DataPrueba.InsertarData(context, userManager).Wait();
                }
                catch (Exception ex)
                {

                    var loggin = services.GetRequiredService<ILogger<Program>>();
                    loggin.LogError(ex, "Ocurrio un error en la migracion");
                }

            }

            hostServer.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
