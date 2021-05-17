using FluentValidation;
using MediatR;
using Persistencia.DapperConexion.Instructor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Instructores
{
    public class NuevoInstructor
    {
        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public string Grado { get; set; }

        }

        public class EjecutaValidar : AbstractValidator<Ejecuta>
        {
            public EjecutaValidar()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellidos).NotEmpty();
                RuleFor(x => x.Grado).NotEmpty();
            }
        }

        public class handler : IRequestHandler<Ejecuta>
        {
            private readonly InstructorRepositorio _instructorRepositorio;
            public handler(InstructorRepositorio instructorRepositorio)
            {
                _instructorRepositorio = instructorRepositorio;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
              var resultado =  await _instructorRepositorio.Nuevo(
                       request.Nombre,
                       request.Apellidos,
                       request.Grado
                   );

                if (resultado > 0)
                    return Unit.Value;

                throw new Exception("No se pudo insertar el instructor");
            }
        }

    }
}
