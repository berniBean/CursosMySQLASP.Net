using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DapperConexion.Instructor
{
    public class InstructorRepositorio : IOpInstructor
    {
        private readonly IFactoryConnection _factoryConnection;

        public InstructorRepositorio(IFactoryConnection factoryConnection)
        {
            _factoryConnection = factoryConnection;
        }
        public Task<int> Actualizar(InstructorModel paramaetro)
        {
            throw new NotImplementedException();
        }

        public Task<int> Eliminar(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Nuevo(string nombre, string apellidos, string titulo)
        {
            var storeProcedure = "instructor_nuevo";
            
            try
            {
                var connection = _factoryConnection.GetConnection();
              var  resultado =  await connection.ExecuteAsync(
                   storeProcedure, 
                   new
                {

                       _nombre = nombre,
                       _apellidos = apellidos,
                       _grado = titulo
                   }, commandType: CommandType.StoredProcedure);

                _factoryConnection.CloseConnection();

                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception("{No se pudo insertar instructorS } ", ex);
            }
        }

        public async Task<IEnumerable<InstructorModel>> ObtenerLista()
        {
            IEnumerable<InstructorModel> instructorList = null;
            var stProc = "obtenerInstructores";

            try
            {
                var connection = _factoryConnection.GetConnection();
                
                instructorList = await connection.QueryAsync<InstructorModel>(stProc, null,commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {

                throw new Exception("{Error en la consulta de datos } ", ex);
            }
            finally {
                _factoryConnection.CloseConnection();
            }
            return instructorList;
        }

        public Task<InstructorModel> ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
