using Dominio;
using FluentValidation;
using MediatR;
using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Cursos
{
    public class NuevoCurso
    {
        public class nuevo : IRequest
        {
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public DateTime? FechaPublicacion { get; set; }
            public Decimal Precio { get; set; }
            public Decimal Promocion { get; set; }

            public List<int> listaInstructor { get; set; }
            

        }

        public class EjecutaValidator : AbstractValidator<nuevo>
        {
            public EjecutaValidator()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Descripcion).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();
            }
        }

        //transaccion 
        public class Handler : IRequestHandler<nuevo>
        {
            public readonly cursosbasesContext _context;
            public Handler(cursosbasesContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(nuevo request, CancellationToken cancellationToken)
            {
                int _cursoId =  await _context.Cursos.OrderByDescending(p => p.Idcurso).Select(r => r.Idcurso).FirstOrDefaultAsync();
                int _precioId = await _context.Precios.OrderByDescending(p=>p.PrecioId).Select(r=>r.PrecioId).FirstOrDefaultAsync();
                _cursoId++;
                _precioId++;
                var insertar = new Curso
                {
                    Idcurso = _cursoId,
                    Nombre = request.Nombre,
                    Descripcion = request.Descripcion,
                    FechaPublicacion = request.FechaPublicacion
                };

                _context.Cursos.Add(insertar);

                if(request.listaInstructor != null)
                {
                   
                    foreach(var id in request.listaInstructor)
                    {
                        var cursoInstructor = new Cursoinstructor
                        {
                            Idcurso = _cursoId,
                            Idinstructor = id
                        };
                        _context.Cursoinstructors.Add(cursoInstructor);
                    }
                }

                //insertar precio de un curso
                var precioEntidad = new Precio
                {
                    PrecioId = _precioId,
                    CursoId = _cursoId,
                    PrecioActual = request.Precio,
                    Promocion = request.Promocion
                };

                _context.Precios.Add(precioEntidad);
                var valor = await _context.SaveChangesAsync();

                if(valor >0)
                    return Unit.Value;

                throw new Exception("Error al agregar curso");


            }
        }

    }
}
