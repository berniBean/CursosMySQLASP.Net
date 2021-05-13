using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DapperConexion.Instructor
{
    public interface IOpInstructor
    {
        Task<IEnumerable<InstructorModel>> ObtenerLista();
        Task<InstructorModel> ObtenerPorId(int id);
        Task<int> Nuevo(InstructorModel parametro);
        Task<int> Actualizar(InstructorModel paramaetro);
        Task<int> Eliminar(int id);
    }
}
