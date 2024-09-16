using Microsoft.AspNetCore.Mvc;
using MJRPAdmin.DTO.DtoInput;
using MJRPAdmin.Service.interfaces;

namespace MJRPAdmin.Controllers.admin
{
    public class AuthController : Controller
    {
        public ILoginService _loginService;
        private readonly IHttpContextAccessor _contextAccessor;
        const string SessionId = "Id";
        const string SessionEmail = "Email";
        const string SessionRole = "UserType";
        
        public AuthController(ILoginService loginService, IHttpContextAccessor contextAccessor)
        {
            _loginService = loginService;
            _contextAccessor = contextAccessor;
        }

        public async Task <IActionResult> Login([FromForm] UserLoginInput value)
        {

            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}
            //else
            //{
            //    var rslt = await _loginService.Login(value);
            //    if (rslt.succeed)
            //    {
            //        ViewData["userData"] = rslt.data.UserType;
            //        if (rslt.data.UserType == 1)
            //        {
            //            return Redirect("/../Faculty/getFaculty");
            //        }
            //    }
            //    else
            //    {
            //        TempData["error"] = "Your Email Id Or Password Is Incorect";
            //    }
            //    return View();
            //}

            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                var rslt = await _loginService.Login(value);
                if (rslt.succeed)
                {
                    _contextAccessor.HttpContext.Session.SetInt32("Id", rslt.data.Id);
                    _contextAccessor.HttpContext.Session.SetString("Name", rslt.data.Name);
                    _contextAccessor.HttpContext.Session.SetString("Email", rslt.data.Email);
                    _contextAccessor.HttpContext.Session.SetString("Mobile", rslt.data.Mobile);
                    _contextAccessor.HttpContext.Session.SetInt32("UserType", rslt.data.UserType);
                   
                    ViewData["UserData"] = rslt.data.UserType;
                    if (rslt.data.UserType == 1)
                    {
                        // TempData["Success"] = "You have Successfully Login";
                        return Redirect("/../Admin/Index");
                    }
                    
                }
                else
                {
                    TempData["error"] = "Your Email Id Or Password Is Incorect";
                }
                return View();
            }

        }

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("../Auth/Login");
        }
    }
}
