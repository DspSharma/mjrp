using Microsoft.AspNetCore.Mvc;
using MJRPAdmin.Constants;
using MJRPAdmin.DTO.DtoInput;
using MJRPAdmin.Entities;
using MJRPAdmin.Misc;
using MJRPAdmin.Models;
using MJRPAdmin.Service;
using MJRPAdmin.Service.interfaces;
using MJRPAdmin.UnitOfWork;

namespace MJRPAdmin.Controllers.admin
{
    public class ResultController : BaseController
    {
        public IResultService _resultService;
        public IFacultyService _facultyService;
        public List<AcademicProg> facultyList = new List<AcademicProg>();
        public IUnitOfWork _unitOfWork;
        private IWebHostEnvironment _environment;
        public ResultController(IFacultyService facultyService, IResultService resultService, IUnitOfWork unitOfWork, IWebHostEnvironment environment)
        {
            authorize("GetResults,UploadResultByExcelFile,DeleteResult,ShowHide");
            _resultService = resultService;
            _facultyService = facultyService;
            _unitOfWork = unitOfWork;
            _environment = environment;
        }

        public async Task<IActionResult> GetResults([FromQuery] int facultyId = 1)
        {
            // var faculties = await _facultyService.getFaculty();
            facultyList = (await _unitOfWork.AcadProgRepo.GetAllAsync()).ToList();
            await _unitOfWork.SaveAsync();
            ViewData["facultydata"] = facultyList;

            var UploadExcel = await _resultService.getUploadExcelFile(facultyId);

            ViewData["UploadExcelData"] = UploadExcel.data;
            return View("UploadResult");
        }
        public async Task<IActionResult> UploadResultByExcelFile([FromForm] UploadExcelInput value)
        {
            var rslt = await _resultService.uploadResultByExcelFile(value);
            return Redirect("/../Result/GetResults");
        }

        public async Task<IActionResult> DeleteResult([FromQuery] int recId)
        {
            var resultDetail = await _resultService.deleteResult(recId);
            string excelDirPath = Path.Combine(_environment.WebRootPath, FilePath.UploadExcelFilePath);
            if (!string.IsNullOrEmpty(resultDetail.data.FileName))
            {
                MiscMethods.deleteFile(Path.Combine(_environment.WebRootPath, FilePath.UploadExcelFilePath) + "/" + resultDetail.data.FileName);
                MiscMethods.deleteFile(Path.Combine(_environment.WebRootPath, FilePath.ResultJSONPath) + "/" + resultDetail.data.RecId + ".json");
            }
            return Redirect("/../Result/GetResults?facultyId=" + resultDetail.data.FacultyId);
        }

        public async Task<IActionResult> ShowHide([FromQuery] string visible, [FromQuery] int recId)
        {
            var resultDetail = await _resultService.ShowHide((visible == "Hide" ? false : true),  recId);
            return Redirect("/../Result/GetResults?facultyId="+resultDetail.data.FacultyId);
        }





    }
}
