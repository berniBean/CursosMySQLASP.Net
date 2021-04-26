using Aplicacion.Cursos;
using Dominio;
using MediatR;
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
    public class CursosController : ControllerBase
    {
        public readonly IMediator _mediator;

        public CursosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Curso>>> Get()
        {
            return await _mediator.Send(new Consulta.ListadoCursos());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> BuscarCurso(int id)
        {
            return await _mediator.Send(new ConsutlaId.CursoId { Idcurso = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> NuevoCurso(NuevoCurso.nuevo datos)
        {
            return await _mediator.Send(datos);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> EditaCurso(int id, ModificaCurso.Ejecuta dato)
        {
            dato.Idcurso = id;
            return await _mediator.Send(dato);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> EliminarCurso(int id)
        {
            return await _mediator.Send(new EliminarCurso.Ejecuta { Idcurso = id });
        }

    }
}
