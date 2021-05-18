using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Persistencia.DapperConexion.Instructor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Instructores
{
    public class EditarInstructor
    {
        public class Ejecuta : IRequest
        {
            public int Idinstructor { get; set; }
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public string Grado { get; set; }
        }

        public class EjecutaValidar : AbstractValidator<Ejecuta>
        {
            public EjecutaValidar()
            {                
                //RuleFor(x => x.Nombre).NotEmpty();
                //RuleFor(x => x.Apellidos).NotEmpty();
                //RuleFor(x => x.Grado).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Ejecuta>
        {
            private readonly IOpInstructor _iOptInstructor;
            public Handler(IOpInstructor iOptInstructor)
            {
                _iOptInstructor = iOptInstructor;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var instructor = await _iOptInstructor.ObtenerPorId(request.Idinstructor);

                if (instructor == null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontró instructor" });



                var resultado = await _iOptInstructor.Actualizar(
                        request.Idinstructor,
                    instructor.Nombre = request.Nombre ?? instructor.Nombre,
                    instructor.Apellidos = request.Apellidos ?? instructor.Apellidos,
                    instructor.Grado = request.Grado ?? instructor.Grado

                    );
                if (resultado > 0)
                    return Unit.Value;

                throw new Exception("No se puede actualizar el instructor");
            }
        }
    }
}
