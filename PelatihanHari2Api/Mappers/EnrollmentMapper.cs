using AutoMapper;
using PelatihanHari2Api.Dtos;
using PelatihanHari2Api.Models;

namespace PelatihanHari2Api.Mappers;

public class EnrollmentMapper : Profile
{
    public EnrollmentMapper()
    {
        CreateMap<EnrollmentDto, Enrollment>();
        CreateMap<Enrollment, EnrollmentDto>();
    }
}