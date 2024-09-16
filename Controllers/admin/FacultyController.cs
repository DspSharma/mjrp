using Microsoft.AspNetCore.Mvc;
using MJRPAdmin.DTO.DtoInput;
using MJRPAdmin.Models;
using MJRPAdmin.Service.interfaces;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Data;
using System.Text.Json;


namespace MJRPAdmin.Controllers.admin
{
    //[ApiController]
    //[Route("api/[controller]")]
    public class FacultyController : BaseController
    {
        //private readonly JsonDocument _jsonDocumnet;
        public IFacultyService _facultyService;
        public FacultyController(IFacultyService facultyService)
        {
            authorize("addFaculty,FacultyDeleteByID,FacultyEditByID,facultyUpdate");
            _facultyService = facultyService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> addFaculty([FromForm] FacultyInput value)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                var rslt = await _facultyService.facultyAdd(value);
                if(rslt.succeed)
                {
                    TempData["success"] = "Add Successfully";
                }
                else
                {
                    TempData["error"] = "Your data not saved";
                }
                return Redirect("/../Faculty/getFaculty");
            }
        }

 

        public async Task<IActionResult>FacultyDeleteByID(int id)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                var rslt = await _facultyService.facultyDeleteByID(id);
                if (rslt.succeed)
                {
                    TempData["Success"] = "deleted";
                }
            }
            return Redirect("../getFaculty");
        }

        public async Task<IActionResult> FacultyEditByID(int id)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                var rslt = await _facultyService.FacultyEditByID(id);
                ViewData["FacultyData"] = rslt.data;
                return View();
            }
           
        }

        public async Task<IActionResult> facultyUpdate([FromForm] FacultyInput value)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                var rslt = await _facultyService.facultyUpdate(value);
                if (rslt.succeed)
                {
                    TempData["success"] = "Update Successfully";
                }
                else
                {
                    TempData["error"] = "Your data not Update";
                    return View();
                }
                return Redirect("/../Faculty/getFaculty");
            }
        }

        

        //public async Task<IActionResult> uploadExcelFile([FromForm]UploadExcelInput value, bool hasHeader = true)
        //{


        //        var rslt = await _facultyService.uploadExcelFile(value,hasHeader);
        //        if (rslt.succeed)
        //        {
        //            TempData["success"] = "Add Successfully";
        //        }
        //        else
        //        {
        //            TempData["error"] = "Your data not saved";
        //        }
        //        return Redirect("/../Faculty/excelFileUpload");


        //}

       

        //public async Task<IActionResult> uploadExcelFile([FromForm] UploadExcelInput value, bool hasHeader = true)
        //{
        //    var rslt = await _facultyService.uploadExcelFile(value, hasHeader);
        //    return Redirect("/../Faculty/excelFileUpload");
        //}

    }
}
