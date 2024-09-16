using Microsoft.AspNetCore.Mvc;
using MJRPAdmin.Constants;
using MJRPAdmin.Models;
using MJRPAdmin.Service.interfaces;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MJRPAdmin.Controllers.Web
{
    public class HomeController : Controller
    {
        public IFacultyService _facultyService;
        public ICourseService _courseService;
        public IDocumentService _documentService;
        private readonly IHttpContextAccessor _contextAccessor;

        public HomeController(IFacultyService facultyService, IHttpContextAccessor contextAccessor, ICourseService courseService, IDocumentService documentService)
        {
            _facultyService = facultyService;
            _contextAccessor = contextAccessor;
            _courseService = courseService;
            _documentService = documentService;
        }
        public async Task<IActionResult> home()
        {
          
            return View();
        }


        public async Task<IActionResult> Course(int id)
        {
            var rslt = await _courseService.getCourseByFacultyId(id);
            ViewData["courseData"] = rslt.data;
            var Id = id;
            ViewData["id"] = Id;
            return View("Course");
        }


        public async Task<IActionResult> Result([FromQuery] int resultId, [FromQuery] string rollNumber)
        {
            if (resultId == 0 || String.IsNullOrEmpty(rollNumber))
            {
                ViewData["courseData"] = null;
                return View("ShowResultNew");
            }
            var filePath = Path.Combine(FilePath.ResultJSONPath, resultId + ".json");
            var json = _documentService.readDocment<ResultData>(filePath);// (await _facultyService.readTextFile("excelDocument/ResultJSON/" + resultId + ".txt")).message;


            //  string data = clsIO.readTextFile(Server.MapPath(Request.ApplicationPath) + "/upload/NewResults/" + Request.QueryString["RecId"] + ".txt");
            //ResultData json = JsonConvert.DeserializeObject<ResultData>(data);
            string header = json.header;
            string result = json.result.Where(x => x.rollno == rollNumber).FirstOrDefault().data;

            string strResult = "<table class='rslt'>";
            strResult += header;
            strResult += result;
            strResult += "</table>";

            ViewData["courseData"] = strResult;


            return View("ShowResultNew");
        }
    }
}
