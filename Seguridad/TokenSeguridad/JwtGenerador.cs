using System.Security.Claims;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Dominio;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Seguridad.TokenSeguridad
{
    public class JwtGenerador : IJwtGenerador
    {
        public string CrearToken(Usuario usuario)
        {
           var claims = new List<Claim> 
           { 
                new Claim(JwtRegisteredClaimNames.NameId, usuario.UserName)
           };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MeGustanLasTetas"));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            
            var tokenDescripcion = new SecurityTokenDescriptor { 
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(30),
                SigningCredentials = credenciales
            };

            var tokenManejador = new JwtSecurityTokenHandler();
            var token = tokenManejador.CreateToken(tokenDescripcion);

            return tokenManejador.WriteToken(token);
        }
    }
}
