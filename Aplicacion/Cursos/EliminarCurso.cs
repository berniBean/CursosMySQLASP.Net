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
    public class EliminarCurso
    {
        public class Ejecuta : IRequest {
            public int Idcurso { get; set; }
        }

        public class Handler : IRequestHandler<Ejecuta>
        {
            public readonly cursosbasesContext _context;
            public Handler(cursosbasesContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var InsturctorDB = _context.Cursoinstructors.Where(x=> x.Idcurso == request.Idcurso);

                foreach (var instructor in InsturctorDB)
                {
                    _context.Cursoinstructors.Remove(instructor);
                }

                var comentarioDB = _context.Comentarios.Where(x => x.CursoId == request.Idcurso);
                foreach (var item in comentarioDB)
                {
                    _context.Comentarios.Remove(item);
                }

                var precioDB = _context.Precios.Where(x => x.CursoId == request.Idcurso).FirstOrDefault();
                if (precioDB != null)
                    _context.Precios.Remove(precioDB);

                var eliminaCurso = await _context.Cursos.FindAsync(request.Idcurso);
                if (eliminaCurso == null)
                    //throw new Exception("No existe el registro");
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { Curso = "El registro no existe" });

                _context.Remove(eliminaCurso);

                var resultado = await _context.SaveChangesAsync();

                if (resultado > 0)
                    return Unit.Value;

                //throw new Exception("No se pudeiron eleminar los registros");
                throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { Curso = "No se pudieron eliminar los registros" });

            }
        }
    }
}
