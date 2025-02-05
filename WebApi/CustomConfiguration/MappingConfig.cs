using AutoMapper;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.CustomConfiguration
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {

            CreateMap<Student, StudentDto>()
                .ForMember(dest => dest.ProfileImageBase64, opt => opt.MapFrom(src => src.ProfileImage != null ? Convert.ToBase64String(src.ProfileImage) : null))
                .ReverseMap()
                .ForMember(dest => dest.ProfileImage, opt => opt.Ignore()); 

            CreateMap<CreateStudentDto, Student>()
                .ForMember(dest => dest.ProfileImage, opt => opt.Ignore());

            CreateMap<UpdateStudentDto, Student>()
                .ForMember(dest => dest.ProfileImage, opt => opt.Ignore());

        }
    }
}
