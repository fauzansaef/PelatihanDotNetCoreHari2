using AutoMapper;
using PelatihanHari2Api.Dtos;
using PelatihanHari2Api.Models;

namespace PelatihanHari2Api.Mappers;

public class StudentMapper : Profile
{
    public StudentMapper()
    {
        CreateMap<StudentDto, Student>();
        CreateMap<Student, StudentDto>()
            .ForMember(x=>x.FirstMidName,
                src=>
                    src.MapFrom(x=>$"{x.FirstMidName} {x.LastName}"));
    }
}