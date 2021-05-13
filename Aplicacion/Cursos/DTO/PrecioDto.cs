using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Cursos.DTO
{
    public class PrecioDto
    {
        public int PrecioId { get; set; }
        
        public decimal PrecioActual { get; set; }
        
        public decimal Promocion { get; set; }
        public int CursoId { get; set; }
    }
}
