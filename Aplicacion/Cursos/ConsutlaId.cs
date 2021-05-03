using Aplicacion.ManejadorError;
using AutoMapper;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        public class CursoId : IRequest<CursosDTO>
        {
            public int Idcurso { get; set; }
        }

        public class Handler : IRequestHandler<CursoId, CursosDTO>
        {
            public readonly cursosbasesContext _context;
            public readonly IMapper _mapper;
            public Handler(cursosbasesContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<CursosDTO> Handle(CursoId request, CancellationToken cancellationToken)
            {
                var cursoUnico = await _context.Cursos
                    .Include(x => x.Cursoinstructors)
                    .ThenInclude(y => y.IdinstructorNavigation)
                    .FirstOrDefaultAsync( a=> a.Idcurso == request.Idcurso);
                    
                if(cursoUnico == null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { Curso = "El registro no existe" });

                var CursoDto = _mapper.Map<Curso, CursosDTO>(cursoUnico);
                return CursoDto;
            }
        }

    }
}
