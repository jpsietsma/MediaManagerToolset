using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaToolsetWebCoreMVC.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediaToolsetWebCoreMVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly AuthenticatedUserInfo UserInfo;

        public UserController(AuthenticatedUserInfo _userInfo)
        {
            UserInfo = _userInfo;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoginPermissions()
        {
            return View(UserInfo);
        }
    }
}