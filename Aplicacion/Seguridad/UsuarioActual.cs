using Aplicacion.Contratos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Seguridad
{
    public class UsuarioActual
    {
        public class Ejecuta : IRequest<UsuarioData>
        {

        }

        public class Handler : IRequestHandler<Ejecuta, UsuarioData>
        {
            public readonly UserManager<Usuario> _userManager;
            public readonly IJwtGenerador _jwtGenerador;
            public readonly IUsuarioSesion _usuarioSesion;

            public Handler(UserManager<Usuario> userManager, IJwtGenerador jwtGenerador, IUsuarioSesion usuarioSesion)
            {
                _userManager = userManager;
                _jwtGenerador = jwtGenerador;
                _usuarioSesion = usuarioSesion;
            }
            
            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
               var usuario =  await _userManager.FindByNameAsync(_usuarioSesion.obtenerUsuarioSesion());
                return new UsuarioData
                {
                    NombreCompleto = usuario.NombreUsuario,
                    UserName = usuario.UserName,
                    Token = _jwtGenerador.CrearToken(usuario),
                    Imagen = null,
                    Email = usuario.Email
                };
            }   
        }
    }
}
