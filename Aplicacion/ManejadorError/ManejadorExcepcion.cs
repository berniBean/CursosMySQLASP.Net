using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.ManejadorError
{
    public class ManejadorExcepcion : Exception
    {
        public HttpStatusCode _Codigo { get; }
        public object _Errores { get; }

        public ManejadorExcepcion(HttpStatusCode codigo , object errores = null)
        {
            _Codigo = codigo;
            _Errores = errores;
        }

    }
}
