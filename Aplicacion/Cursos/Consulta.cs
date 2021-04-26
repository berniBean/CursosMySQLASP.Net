using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Cursos
{
    public class Consulta
    {
        public class ListadoCursos : IRequest<List<Curso>>
        {

        }

        public class Handler : IRequestHandler<ListadoCursos, List<Curso>>
        {
            public readonly cursosbasesContext _context;
            public Handler(cursosbasesContext context)
            {
                _context = context;
            }
            public async Task<List<Curso>> Handle(ListadoCursos request, CancellationToken cancellationToken)
            {
                var cursos = await _context.Cursos.ToListAsync();
                return cursos;
            }
        }

    }
}
