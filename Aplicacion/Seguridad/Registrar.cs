using Aplicacion.Contratos;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Seguridad
{
    public class Registrar
    {
        public class Ejecuta : IRequest<UsuarioData>
        {
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string UserName { get; set; }
        }

        public class Validador : AbstractValidator<Ejecuta>
        {
            public Validador()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellidos).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.UserName).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly cursosbasesContext _cursosbasesContext;
            private readonly UserManager<Usuario> _userManager;
            private readonly IJwtGenerador _jwtGenerador;

            public Handler(cursosbasesContext context, UserManager<Usuario> userManager, IJwtGenerador jwtGenerador)
            {
                _cursosbasesContext = context;
                _userManager = userManager;
                _jwtGenerador = jwtGenerador;
            }
            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var existe = await _cursosbasesContext.Users.Where(x => x.Email == request.Email).AnyAsync();
                if (existe)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "El Email ya existe, con un usuario registrado" });
                }

                var existeUserName = await _cursosbasesContext.Users.Where(y => y.UserName == request.UserName).AnyAsync();
                if (existeUserName)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "El UserName ya existe, con un usuario registrado" });
                }

                var usuario = new Usuario
                {
                    NombreUsuario = request.Nombre+" "+ request.Apellidos,
                    Email = request.Email,
                    UserName = request.UserName
                };

                var resultado = await _userManager.CreateAsync(usuario, request.Password);
                if (resultado.Succeeded)
                {
                    return new UsuarioData { 
                        NombreCompleto = usuario.NombreUsuario,
                        Token = _jwtGenerador.CrearToken(usuario),
                        UserName = usuario.UserName,
                        Email = usuario.Email
                    };
                }

                throw new Exception("No se puede agregar al usuario");

            }
        }
    }
}
