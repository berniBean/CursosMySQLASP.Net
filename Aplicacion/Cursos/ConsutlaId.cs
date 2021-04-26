using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Cursos
{
    public class ConsutlaId
    {
        public class CursoId : IRequest<Curso>
        {
            public int Idcurso { get; set; }
        }

        public class Handler : IRequestHandler<CursoId, Curso>
        {
            public readonly cursosbasesContext _context;
            public Handler(cursosbasesContext context)
            {
                _context = context;
            }
            public async Task<Curso> Handle(CursoId request, CancellationToken cancellationToken)
            {
                var cursoUnico = await _context.Cursos.FindAsync(request.Idcurso);
                    if(cursoUnico == null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { Curso = "El registro no existe" });
                return cursoUnico;
            }
        }

    }
}
