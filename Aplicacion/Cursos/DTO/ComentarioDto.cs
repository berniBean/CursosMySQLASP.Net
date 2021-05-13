using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Cursos.DTO
{
    public class ComentarioDto
    {
        public int Idcomentario { get; set; }
        public string AlumnoName { get; set; }
        public int? Puntaje { get; set; }
        public string Comentario1 { get; set; }
        public int? CursoId { get; set; }
    }
}
