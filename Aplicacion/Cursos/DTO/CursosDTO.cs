using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Cursos
{
    public class CursosDTO
    {
        public int Idcurso { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public string Fotoportada { get; set; }

        public ICollection<InstructorDto> Instructores { get; set; }
    }
}
