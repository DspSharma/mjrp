using Microsoft.AspNetCore.Mvc;
using MJRPAdmin.DTO.DtoInput;
using MJRPAdmin.Service;
using MJRPAdmin.Service.interfaces;

namespace MJRPAdmin.Controllers.admin
{
    public class UserController : BaseController
    {
        public IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;
        const string SessionId = "Id";
        const string SessionRoleType = "Role";
        public UserController(IUserService userService, IHttpContextAccessor contextAccessor)
        {
            authorize("userProfileEdit,UserList,ShowHide,userDeleteById,userEditById,userUpdate");
            _userService = userService;
            _contextAccessor = contextAccessor;
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        public async Task<IActionResult> addUpdateUser([FromForm] UserInput value)
        {
            if(!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));
                TempData["error"] = message;
                return Redirect("../User/Register");
            }
            else
            {
                var rslt = await _userService.userAdd(value);
                if (rslt.succeed)
                {
                    TempData["Success"] = "Add Successfully";
                }
                else
                {
                    TempData["error"] = "Your email or mobile is already exits ";
                    return Redirect("../User/Register");
                }
            }
            return Redirect("../Auth/Login");
        }

        public async Task<IActionResult> addUser([FromForm] UserInput value)
        {
           if(!ModelState.IsValid)
            {
                return View();

            }
           else
            {
                var rslt = await _userService.userAdd(value);
                if (rslt.succeed)
                {
                    TempData["Success"] = "Add Successfully";
                }
                else
                {
                    TempData["error"] = "Your email or mobile is already exits ";
                   return View();
                }
            }
            return Redirect("/../User/UserList");
        }
        public async Task<IActionResult> userProfileEdit()
        {
            int id = Convert.ToInt32(HttpContext.Session.GetInt32(SessionId));
            if(!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                var rslt = await _userService.userProfileEdit(id);
                ViewData["UserData"] = rslt.data;
                return View();
            }
           
        }

        public async Task<IActionResult>UserList()
        {
            var rslt = await _userService.userList();
            ViewData["userData"] = rslt.data;
            return View();
        }
        


        public async Task<IActionResult> userDeleteById(int id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                var rslt = await _userService.userDeleteById(id);
                if (rslt.succeed)
                {
                    TempData["Success"] = "deleted";
                }
            }
            return Redirect("../UserList");
        }
        public async Task<IActionResult> userEditById(int id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                var rslt = await _userService.userEditById(id);
                ViewData["userData"] = rslt.data;
                return View();
            }
        }


        public async Task<IActionResult> userUpdate([FromForm] UserInput value)
        {
           
                var rslt = await _userService.userUpdate(value);
                if (rslt.succeed)
                {
                    TempData["success"] = "Update Successfully";
                }
                else
                {
                    TempData["error"] = "Your data not Update";
                    return View();
                }
                return Redirect("/../User/UserList");
          



            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}
            //else
            //{
            //    var rslt = await _userService.userUpdate(value);
            //    if (rslt.succeed)
            //    {
            //        TempData["success"] = "Update Successfully";
            //    }
            //    else
            //    {
            //        TempData["error"] = "Your data not Update";
            //        return View();
            //    }
            //    return Redirect("/../User/UserList");
            //}
        }


    }
}
