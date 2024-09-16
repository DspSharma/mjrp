using AutoMapper;
using MJRPAdmin.DTO.DtoInput;
using MJRPAdmin.DTO.DtoOutPut;
using MJRPAdmin.Entities;
using MJRPAdmin.Models;
using User = MJRPAdmin.Models.User;

namespace MJRPAdmin.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        {
            CreateMap<UserLoginInput, User>();
            CreateMap<LoginInUserModels, User>();
            CreateMap<User,LoginInUserModels>();

            CreateMap<FacultyInput, Faculty>();
            CreateMap<FacultyOutPut,Faculty> ();
            CreateMap<Faculty, FacultyOutPut>();

            CreateMap<UserInput, User>();
            CreateMap<UserOutPut, User>();
            CreateMap<User,UserOutPut>();

            CreateMap<CourseInput, Course>();
            CreateMap<CourseOutput, Course>();
            CreateMap<Course,CourseOutput>();

            CreateMap<UploadExcelInput, UploadExcel>();
            CreateMap<UploadExcelOutPut, UploadExcel>();
            CreateMap<UploadExcel,UploadExcelOutPut>();
            CreateMap<UploadResult, UploadResultOutput>();
        }
        
    }
}
