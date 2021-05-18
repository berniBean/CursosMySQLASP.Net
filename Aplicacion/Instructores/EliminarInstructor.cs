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
    public class EliminarInstructor
    {
        public class Ejecuta : IRequest
        {
            public int Idinstructor { get; set; }
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
                var resultado = await _iOptInstructor.Eliminar(
                        request.Idinstructor
                    );

                if (resultado > 0)
                    return Unit.Value;

                throw new Exception("No se puede Eliminar el instructor");
            }
        }
    }
}
