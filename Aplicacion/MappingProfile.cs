using Aplicacion.Cursos;
using AutoMapper;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Curso, CursosDTO>()
                .ForMember(x => x.Instructores, y => y.MapFrom(z => z.Cursoinstructors.Select(a => a.IdinstructorNavigation).ToList()));
            CreateMap<Cursoinstructor, CursoInstructorDto>();
            CreateMap<Instructor, InstructorDto>();
        }
    }
}
