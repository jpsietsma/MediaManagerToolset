using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Entities.Configuration.Identity.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MediaToolsetWebCoreMVC.Controllers
{
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

        [Authorize(Roles = "SuperAdmin, Administrator, UserModerator, ContentModerator, ContentViewer")]
        public IActionResult Profile()
        {
            return View();
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

        [Authorize]
        public async Task<IActionResult> UploadProfilePhoto(IFormFile file)
        {
            await Task.Run(() => {

                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    var fileContent = reader.ReadToEnd();
                    var parsedContentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                    var fileName = parsedContentDisposition.FileName;
                }

            });

            return View("Profile", "User");

        }

    }
}