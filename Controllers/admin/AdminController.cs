using Microsoft.AspNetCore.Mvc;
using MJRPAdmin.DTO.DtoInput;
using MJRPAdmin.Entities;
using MJRPAdmin.Models;
using MJRPAdmin.Service;
using MJRPAdmin.Service.interfaces;
using MJRPAdmin.UnitOfWork;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Data;
using System.Text.Json;


namespace MJRPAdmin.Controllers.admin
{
    //[ApiController]
    //[Route("api/[controller]")]
    public class AdminController : BaseController
    {
        //private readonly JsonDocument _jsonDocumnet;
        public IUnitOfWork _unitOfWork;
        public IResultService _resultService;
        public List<AcademicProg> facultyList = new List<AcademicProg>();
        public AdminController(IUnitOfWork unitOfWork, IResultService resultService)
        {
            _unitOfWork = unitOfWork;
            _resultService = resultService;
            authorize("Index");
        }

        public async Task<IActionResult> Index()
        {
            facultyList = (await _unitOfWork.AcadProgRepo.GetAllAsync()).ToList();
            await _unitOfWork.SaveAsync();
            ViewData["facultydata"] = facultyList;
            // return View("facultydata", facultyList);
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return View("Logout");
        }

    }




}
