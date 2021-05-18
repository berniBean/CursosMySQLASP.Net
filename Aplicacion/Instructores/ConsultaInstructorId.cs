using Aplicacion.ManejadorError;
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
    public class ConsultaInstructorId
    {
        public class Ejecuta : IRequest<InstructorModel>
        {
            public int Idinstructor { get; set; }
        }

        public class Handler : IRequestHandler<Ejecuta, InstructorModel>
        {
            private readonly IOpInstructor _iOptInstructor;
            public Handler(IOpInstructor iOptInstructor)
            {
                _iOptInstructor = iOptInstructor;
            }

            public async Task<InstructorModel> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var instructor = await _iOptInstructor.ObtenerPorId(request.Idinstructor);

                if (instructor == null)
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontró instructor" });

                return instructor;
            }
        }
    }
}

