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

namespace Aplicacion.Cursos
{
    public class NuevoCurso
    {
        public class nuevo : IRequest
        {
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public DateTime? FechaPublicacion { get; set; }
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

        
        public class Handler : IRequestHandler<nuevo>
        {
            public readonly cursosbasesContext _context;
            public Handler(cursosbasesContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(nuevo request, CancellationToken cancellationToken)
            {
                var insertar = new Curso
                {
                    Nombre = request.Nombre,
                    Descripcion = request.Descripcion,
                    FechaPublicacion = request.FechaPublicacion
                };

                _context.Cursos.Add(insertar);

                var valor = await _context.SaveChangesAsync();

                if(valor >0)
                    return Unit.Value;

                throw new Exception("Error al agregar curso");


            }
        }

    }
}
