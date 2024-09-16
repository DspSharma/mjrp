using AutoMapper;
using MJRPAdmin.DTO.DtoInput;
using MJRPAdmin.DTO.DtoOutPut;
using MJRPAdmin.Entities;
using MJRPAdmin.Models;
using MJRPAdmin.Service.interfaces;
using MJRPAdmin.UnitOfWork;
using Newtonsoft.Json;

namespace MJRPAdmin.Service
{
    public class CourseService : ICourseService
    {
        private IWebHostEnvironment _environment;
        public IMapper _mapper;
        public IUnitOfWork _unitOfWork;
        

        public CourseService(IWebHostEnvironment environment, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _environment = environment;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public List<Faculty> getFacultyJson()
        {
            var rootPath = _environment.WebRootPath;
            var fullPath = Path.Combine(rootPath, "document/Faculty.json");
            using StreamReader reader = new(fullPath);
            var json = reader.ReadToEnd();
            List<Faculty> faculties = JsonConvert.DeserializeObject<List<Faculty>>(json);
            return faculties;
        }

        public List<Course> getCourseJson()
        {
            var rootPath = _environment.WebRootPath;
            var fullPath = Path.Combine(rootPath, "document/Course.json");
            using StreamReader reader = new(fullPath);
            var json = reader.ReadToEnd();
            List<Course> courses = JsonConvert.DeserializeObject<List<Course>>(json);
            return courses;
        }

        public void WriteCourseJson(List<Course> data, string filePath)
        {
            string jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);

            File.WriteAllText(filePath, jsonString);

        }

        public async Task<ApiResponseModels<CourseOutput>> addCourse(CourseInput value)
        {
            var rootPath = _environment.WebRootPath;
            var fullPath = Path.Combine(rootPath, "document/Course.json");
            var getCourse = getCourseJson();
            int nextId = getCourse.Count > 0 ? getCourse.Max(x => x.Id) + 1 : 1;
            Course formValue = new Course();
            formValue.Id = nextId;
            formValue.FacultyId = value.FacultyId;
            formValue.CourseName = value.CourseName;
            formValue.IsActive = true;
            formValue.CreatedAt = DateTime.UtcNow;
            formValue.UpdatedAt = DateTime.UtcNow;
            getCourse.Add(formValue);
            WriteCourseJson(getCourse, fullPath);

            return new ApiResponseModels<CourseOutput>
            {
                succeed = true,
                message = "success"
            };
        }

        public async Task<ApiResponseModels<List<CourseOutput>>> getCourse()
        {
            var CourseData = getCourseJson();
            var facultyData = getFacultyJson();
           List<int>courseId = CourseData.Select(x=>x.FacultyId).Distinct().ToList();
           List<Faculty> faculty = facultyData.Where(x => courseId.Contains(x.Id)).ToList();
            var courseList = from c in CourseData
                             join f in faculty on c.FacultyId equals f.Id
                             select new CourseOutput
                             {
                                 //Id = c.Id,
                                 //CourseName = c.CourseName,
                                 //IsActive = c.IsActive,
                                 //CreatedAt = c.CreatedAt,
                                 //UpdatedAt = c.UpdatedAt,
                                 //FacultyId = f.Id,
                                 //FacultyName = f.Tittle,
                             };

            var rslt = _mapper.Map<List<CourseOutput>>(courseList);
            return new ApiResponseModels<List<CourseOutput>>
            {
                succeed = true,
                message = "success",
                data = rslt
            };
        }

        public async Task<ApiResponseModels<CourseOutput>>getCourseByFacultyId(int facultyId)
        {
            //var rootPath = _environment.WebRootPath;
            //var fullPath = Path.Combine(rootPath, "document/Course.json");

            //var CourseData = getCourseJson();
            List<UploadResult> uploadResults = _unitOfWork.UploadResult.GetWhere(x => x.FacultyId == facultyId).OrderByDescending(y=>y.RecId).Take(200).ToList();

            CourseOutput courseOutput = new CourseOutput()
            {
                FacultyId = facultyId,
                UploadResult = uploadResults,

            };

           //List<Course> faculty = CourseData.Where(x => x.FacultyId == id).ToList();
           // var rslt = _mapper.Map<List<CourseOutput>>(faculty);
            return new ApiResponseModels<CourseOutput>
            {
                succeed = true,
                message = "success",
                data = courseOutput
            };
        }

        public async Task<ApiResponseModels<CourseOutput>> CourseById(int id)
        {
            var rootPath = _environment.WebRootPath;
            var fullPath = Path.Combine(rootPath, "document/Course.json");

            var CourseData = getCourseJson();
            Course course = CourseData.Where(x => x.Id == id).FirstOrDefault();
            var rslt = _mapper.Map<CourseOutput>(course);
            return new ApiResponseModels<CourseOutput>
            {
                succeed = true,
                message = "success",
                data = rslt
            };
        }

    }
}
