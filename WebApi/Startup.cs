using Aplicacion.Contratos;
using Aplicacion.Cursos;
using Dominio;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistencia.DapperConexion;
using Persistencia.DapperConexion.Instructor;
using Persistencia.Models;
using Seguridad.TokenSeguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.MiddleWare;

namespace WebApi
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
            services.AddDbContext<cursosbasesContext>(options => 
            {
                options.UseMySQL(Configuration.GetConnectionString("DefaultConnection"));
            });

            //configurando dapper
            services.AddOptions();
            services.Configure<ConexionConfiguracion>(Configuration.GetSection("ConnectionStrings"));
            //Agregando el servicio de MediatR
            services.AddMediatR(typeof(Consulta.Handler).Assembly);
            //agregando el servicio de Fluent Validation //inyectar authorize para todos los controladores
            services.AddControllers(
                    //los controles tiene la autorizacion antes de recibir un request del cliente 
                    opt =>
                    {
                        var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                        opt.Filters.Add(new AuthorizeFilter(policy));
                    }
                ).AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<NuevoCurso>());
            
            //configurar Identity dentro de WebApi
            var builder = services.AddIdentityCore<Usuario>();
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identityBuilder.AddEntityFrameworkStores<cursosbasesContext>();
            identityBuilder.AddSignInManager<SignInManager<Usuario>>();
            services.TryAddSingleton<ISystemClock, SystemClock>();

            //inyectar servicios de seguridad IJwtGenerador jsonToken
            services.AddScoped<IJwtGenerador, JwtGenerador>();
            //seguridad para que no se pueda usar si no tienen token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MeGustanLasTetas"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                opt => opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateAudience = false,
                    ValidateIssuer = false
                }
                ) ;
            //Agregar data usuario
            services.AddScoped<IUsuarioSesion, UsuarioSesion>();
            //Agregar AutoMapper como servicio
            services.AddAutoMapper(typeof(Consulta.Handler));
            //agregar Dapper como servicio
            services.AddTransient<IFactoryConnection, FactoryConnection>();
            services.AddScoped<IOpInstructor, InstructorRepositorio>();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
                c.CustomSchemaIds(c => c.FullName);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseMiddleware<ManejadorErrorMiddleWare>();
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
