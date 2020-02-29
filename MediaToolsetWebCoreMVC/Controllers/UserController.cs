using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Configuration.Identity.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MediaToolsetWebCoreMVC.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly AuthenticatedUserInfo UserInfo;
        private readonly SignInManager<AuthenticatedUser> SignInManager;

        public UserController(AuthenticatedUserInfo _userInfo, SignInManager<AuthenticatedUser> _signInMgr)
        {
            UserInfo = _userInfo;
            SignInManager = _signInMgr;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View(UserInfo);
        }

        [Authorize]
        public IActionResult LoginPermissions()
        {
            return View(UserInfo);
        }

       [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await SignInManager.SignOutAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}