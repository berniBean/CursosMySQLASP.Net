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
        public async Task<int> Actualizar(int Idinstructor, string nombre, string apellidos, string titulo)
        {
            var storeProcedure = "instructor_editar";

            try
            {
                var connection = _factoryConnection.GetConnection();
                var resultado = await connection.ExecuteAsync(
                                  storeProcedure,
                                  new
                                  {
                                      
                                      _Idinstructor  = Idinstructor,
                                      _nombre = nombre,
                                      _apellidos = apellidos,
                                      _grado = titulo
                                  }, commandType: CommandType.StoredProcedure);

                _factoryConnection.CloseConnection();
                return resultado;
            }
            catch (Exception ex)
            {

                throw new Exception("{No se pudo modificar instructorS } ", ex);
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }
        }

        public async Task<int> Eliminar(int Idinstructor)
        {
            var storeProcedure = "eliminarInstructor";

            try
            {
                var connection = _factoryConnection.GetConnection();
                var resultado = await connection.ExecuteAsync(
                                 storeProcedure,
                                 new
                                 {

                                     _Idinstructor = Idinstructor

                                 }, commandType: CommandType.StoredProcedure);

                _factoryConnection.CloseConnection();
                return resultado;

            }
            catch (Exception ex)
            {

                throw new Exception("{No se pudo eliminar instructor } ", ex);
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }
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
            finally
            {
                _factoryConnection.CloseConnection();
            }
        }

        public async Task<IEnumerable<InstructorModel>> ObtenerLista()
        {
            IEnumerable<InstructorModel> instructorList = null;
            var stProc = "obtenerInstructores";

            try
            {
                var connection = _factoryConnection.GetConnection();
                
                instructorList = await connection.QueryAsync<InstructorModel>(stProc,
                    null,commandType: CommandType.StoredProcedure);

                _factoryConnection.CloseConnection();
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

        public async Task<InstructorModel> ObtenerPorId(int id)
        {
            var storeProcedure = "buscarInstructor";
            InstructorModel instructor = null;
            try
            {
                var connection = _factoryConnection.GetConnection();

                instructor = await connection.QueryFirstAsync<InstructorModel>(storeProcedure, 
                    new
                    {
                        _idInstructor = id
                    },
                    commandType: CommandType.StoredProcedure);

                _factoryConnection.CloseConnection();
                

                
            }
            catch (Exception ex)
            {

                throw new Exception("{No se encontró el dato } ", ex);
            }
            finally
            {
                _factoryConnection.CloseConnection();
            }
            return instructor;

        }
    }
}
