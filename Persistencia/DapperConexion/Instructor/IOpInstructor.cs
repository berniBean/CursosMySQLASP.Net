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
        Task<int> Nuevo(string nombre, string apellidos, string titulo);
        Task<int> Actualizar(int Idinstructor, string nombre, string apellidos, string titulo);
        Task<int> Eliminar(int id);
    }
}
