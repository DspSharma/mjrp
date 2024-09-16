using Microsoft.AspNetCore.Mvc;
using MJRPAdmin.DTO.DtoInput;
using MJRPAdmin.Service.interfaces;

namespace MJRPAdmin.Controllers.admin
{
    public class CourseController : BaseController
    {
        public ICourseService _courseService;
        public IFacultyService _faultyService;
        public CourseController(ICourseService courseService, IFacultyService faultyService)
        {
            authorize("courseList");
            _courseService = courseService;
            _faultyService = faultyService;
        }
     

        public async Task<IActionResult>courseList()
        {
            var rslt = await _courseService.getCourse();
            ViewData["coursedata"] = rslt.data;
            return View();
        }


    }
}
