using Dominio;
using Microsoft.AspNetCore.Identity;
using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public class DataPrueba
    {
        public static async Task InsertarData(cursosbasesContext context, UserManager<Usuario> usuarioManager)
        {
            if (!usuarioManager.Users.Any())
            {
                var usuario = new Usuario
                {
                    NombreUsuario = "Berni Palomino",
                    UserName = "bp",
                    Email = "berni@gmail.com"
                };
                await usuarioManager.CreateAsync(usuario, "Efloresp2013$");
            }
        }
    }
}
