using Aplicacion.Contratos;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Seguridad
{
    public class Login
    {
        public class Ejecuta : IRequest<UsuarioData>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
        private class validarDatos : AbstractValidator<Ejecuta>
        {
            public validarDatos()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Ejecuta, UsuarioData>
        {
            public readonly UserManager<Usuario> _userManager;
            public readonly SignInManager<Usuario> _signInManager;
            public readonly IJwtGenerador _jwtGenerador;

            public Handler(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IJwtGenerador jwtGenerador)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _jwtGenerador = jwtGenerador;
            }

            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var usario = await _userManager.FindByEmailAsync(request.Email);
                if(usario == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.Unauthorized);
                }
                var resultado = await _signInManager.CheckPasswordSignInAsync(usario, request.Password, false);
                if (resultado.Succeeded)
                {
                    return new UsuarioData {
                        NombreCompleto = usario.NombreUsuario,
                        Token = _jwtGenerador.CrearToken(usario),
                        UserName = usario.UserName,
                        Email = usario.Email,
                        Imagen = null
                    };
                }

                throw new ManejadorExcepcion(HttpStatusCode.Unauthorized);
            }
        }
    }
}
