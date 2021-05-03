using AutoMapper;
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
        public class ListadoCursos : IRequest<List<CursosDTO>>
        {

        }

        public class Handler : IRequestHandler<ListadoCursos, List<CursosDTO>>
        {
            private readonly cursosbasesContext _context;
            private readonly IMapper _mapper;
            public Handler(cursosbasesContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<CursosDTO>> Handle(ListadoCursos request, CancellationToken cancellationToken)
            {
                var cursos = await _context.Cursos
                            .Include(x => x.Cursoinstructors)
                            .ThenInclude(y => y.IdinstructorNavigation).ToListAsync();
                
                var cursosDto = _mapper.Map<List<Curso>, List<CursosDTO>> (cursos);

                return cursosDto;
            }
        }

    }
}
