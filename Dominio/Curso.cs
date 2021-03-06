using System;
using System.Collections.Generic;

#nullable disable

namespace Dominio

{
    public partial class Curso
    {
        public Curso()
        {
            Comentarios = new HashSet<Comentario>();
            Cursoinstructors = new HashSet<Cursoinstructor>();
        }

        public int Idcurso { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaPublicacion { get; set; }
        public string Fotoportada { get; set; }

        public Precio PrecioPromocion { get; set; }

        public virtual ICollection<Comentario> Comentarios { get; set; }
        public virtual ICollection<Cursoinstructor> Cursoinstructors { get; set; }
    }
}
