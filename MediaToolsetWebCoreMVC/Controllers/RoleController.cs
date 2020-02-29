using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Entities.Configuration.Identity.Role;
using Entities.Configuration.Identity.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
 
namespace Identity.Controllers
{
    public class RoleController : Controller
    {
        private RoleManager<IdentityRole> RoleManager;
        private UserManager<AuthenticatedUser> UserManager;

        public RoleController(RoleManager<IdentityRole> _roleMgr, UserManager<AuthenticatedUser> _userMrg)
        {
            RoleManager = _roleMgr;
            UserManager = _userMrg;
        }
 
        public ViewResult Index() => View(RoleManager.Roles);
 
        public IActionResult Create() => View();
 
        [HttpPost]
        public async Task<IActionResult> Create([Required]string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await RoleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            return View(name);
        }
 
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await RoleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await RoleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "No role found");
            return View("Index", RoleManager.Roles);
        }

        public async Task<IActionResult> Update(string id)
        {
            IdentityRole role = await RoleManager.FindByIdAsync(id);
            List<AuthenticatedUser> members = new List<AuthenticatedUser>();
            List<AuthenticatedUser> nonMembers = new List<AuthenticatedUser>();
            foreach (AuthenticatedUser user in UserManager.Users)
            {
                var list = await UserManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }
            return View(new RoleEdit
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        [HttpPost]
        public async Task<IActionResult> Update(RoleModification model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.AddIds ?? new string[] { })
                {
                    AuthenticatedUser user = await UserManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await UserManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
                foreach (string userId in model.DeleteIds ?? new string[] { })
                {
                    AuthenticatedUser user = await UserManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await UserManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
            }

            if (ModelState.IsValid)
                return RedirectToAction(nameof(Index));
            else
                return await Update(model.RoleId);
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}