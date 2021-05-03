using Aplicacion.Seguridad;
using Dominio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UsuarioController : MiControllerBase
    {
        // http:localhost:5000/api/Usuario/login
        [HttpPost("login")]
        public async Task<ActionResult<UsuarioData>> Login(Login.Ejecuta parametros)
        {
            return await Mediator.Send(parametros);

        }
        // http:localhost:5000/api/Usuario/SingUp
        [HttpPost("SingUp")]
        public async Task<ActionResult<UsuarioData>> Registrar(Registrar.Ejecuta parametros)
        {
            return await Mediator.Send(parametros);
        }
        [HttpGet]
        public async Task<ActionResult<UsuarioData>> DevolverUsuario()
        {
            return await Mediator.Send(new UsuarioActual.Ejecuta());
        }

    }
}
