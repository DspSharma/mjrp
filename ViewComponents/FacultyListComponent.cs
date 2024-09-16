using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using MJRPAdmin.Entities;
using MJRPAdmin.Models;
using MJRPAdmin.UnitOfWork;

namespace MJRPAdmin.ViewComponents
{
    [ViewComponent(Name = "FacultyList")]
    public class CategoryViewComponent : ViewComponent
    {
        public IUnitOfWork _unitOfWork;
        public List<AcademicProg> facultyList = new List<AcademicProg>();
        public CategoryViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            facultyList = (await _unitOfWork.AcadProgRepo.GetAllAsync()).ToList();
            await _unitOfWork.SaveAsync();
            return View("FacultyList", facultyList);
        }
    }
}

