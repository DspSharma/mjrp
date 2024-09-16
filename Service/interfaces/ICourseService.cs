using MJRPAdmin.DTO.DtoInput;
using MJRPAdmin.DTO.DtoOutPut;
using MJRPAdmin.Models;

namespace MJRPAdmin.Service.interfaces
{
    public interface ICourseService
    {
        public  Task<ApiResponseModels<CourseOutput>> addCourse(CourseInput value);
        //public  Task<ApiResponseModels<List<CourseOutput>>> CourseGetById(int id);
        Task<ApiResponseModels<CourseOutput>> getCourseByFacultyId(int facultyId);
        public  Task<ApiResponseModels<CourseOutput>> CourseById(int id);
        public  Task<ApiResponseModels<List<CourseOutput>>> getCourse();
    }
}
