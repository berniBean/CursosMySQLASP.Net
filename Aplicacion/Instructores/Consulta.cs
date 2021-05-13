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
    public class Consulta
    {
        public class Lista : IRequest<List<InstructorModel>>
        {

        }

        public class Handler : IRequestHandler<Lista, List<InstructorModel>>
        {

            private readonly IOpInstructor _opInstructor;
            public Handler(IOpInstructor opInstructor)
            {
                _opInstructor = opInstructor;
            }
            public async Task<List<InstructorModel>> Handle(Lista request, CancellationToken cancellationToken)
            {
                var resultado = await _opInstructor.ObtenerLista();
                return resultado.ToList();

                 

                
            }
        }
    }
}
