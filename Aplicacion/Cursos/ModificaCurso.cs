using Aplicacion.ManejadorError;
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


    public class ModificaCurso
    {
        public class Ejecuta : IRequest
        {
            public int Idcurso { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public DateTime? FechaPublicacion { get; set; }

            public List<int> ListaInstructor { get; set; }
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
                var curso = await _context.Cursos.FindAsync(request.Idcurso);

                    if (curso == null)
                        //throw new Exception("No existe el registro");
                        throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { Curso = "El registro no existe" });

                curso.Nombre = request.Nombre ?? curso.Nombre;
                curso.Descripcion = request.Descripcion ?? curso.Descripcion;
                curso.FechaPublicacion = request.FechaPublicacion ?? curso.FechaPublicacion;

                if(request.ListaInstructor != null)
                {
                    if(request.ListaInstructor.Count > 0)
                    {
                        /*Eliminar instructores*/
                        var instructoresBD = _context.Cursoinstructors.Where(x => x.Idcurso == request.Idcurso).ToList();
                        foreach(var instructorEliminar in instructoresBD)
                        {
                            _context.Cursoinstructors.Remove(instructorEliminar);
                        }


                        /*Agregar instructores de clientes*/
                        foreach (var id in request.ListaInstructor)
                        {
                            var nuevoInstructor = new Cursoinstructor
                            {
                                Idcurso = request.Idcurso,
                                Idinstructor = id
                            };
                            _context.Cursoinstructors.Add(nuevoInstructor);
                        }
                    }
                }


                var resultado = await _context.SaveChangesAsync();

                if (resultado > 0)
                    return Unit.Value;
                else
                    throw new Exception("No se pudo editar");

            }
        }

    }
}
