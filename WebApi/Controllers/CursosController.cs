using Aplicacion.Cursos;
using Dominio;
using MediatR;
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
    public class CursosController : MiControllerBase
    {


        [HttpGet]        
        public async Task<ActionResult<List<Curso>>> Get()
        {
            return await Mediator.Send(new Consulta.ListadoCursos());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> BuscarCurso(int id)
        {
            return await Mediator.Send(new ConsutlaId.CursoId { Idcurso = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> NuevoCurso(NuevoCurso.nuevo datos)
        {
            return await Mediator.Send(datos);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> EditaCurso(int id, ModificaCurso.Ejecuta dato)
        {
            dato.Idcurso = id;
            return await Mediator.Send(dato);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> EliminarCurso(int id)
        {
            return await Mediator.Send(new EliminarCurso.Ejecuta { Idcurso = id });
        }

    }
}
