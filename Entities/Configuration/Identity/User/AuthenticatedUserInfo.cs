using Entities.Data.EF_Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Entities.Configuration.Identity.User
{
    public class AuthenticatedUserInfo
    {
        private readonly IHttpContextAccessor HttpContextAccessor;
        private readonly UserManager<AuthenticatedUser> UserManager;
        private readonly AuthenticatedUser AuthenticatedUser;
        private readonly DatabaseContext DatabaseContext;

        public string Id { get; private set; }
        public List<string> UserRoles { get; private set; }
        public string EmailAddress { get; private set; }
        public string UserName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public DateTime? RegistrationDate { get; private set; }
        public List<AuthenticatedUserLoginPermission> LoginPermissions { get; private set; }

        public AuthenticatedUserInfo(IHttpContextAccessor _httpContextAccessor, UserManager<AuthenticatedUser> _userManager, DatabaseContext _identityContext)
        {            
            HttpContextAccessor = _httpContextAccessor;
            UserManager = _userManager;
            DatabaseContext = _identityContext;

            if (_httpContextAccessor.HttpContext.User.Claims.Count() > 0)
            {
                var CurrentUserId = HttpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                AuthenticatedUser = UserManager.FindByIdAsync(CurrentUserId).Result;

                RetrieveUserDetails();
            }                    
        }               

        private void RetrieveUserDetails()
        {            
            Id = AuthenticatedUser.Id;            
            EmailAddress = AuthenticatedUser.Email;
            UserName = AuthenticatedUser.UserName;
            PhoneNumber = AuthenticatedUser.PhoneNumber;
            FirstName = AuthenticatedUser.FirstName;
            LastName = AuthenticatedUser.LastName;
            RegistrationDate = AuthenticatedUser.RegistrationDate;
            UserRoles = UserManager.GetRolesAsync(AuthenticatedUser).Result.ToList();
            LoginPermissions = DatabaseContext.GetLoginPermissions(Id);
        }

        public bool IsRoleMember(string roleName)
        {
            if (UserRoles.Contains(roleName))
                return true;
            else
                return false;
        }

        public bool HasLoginPermission(string permissionName)
        {
            var permission = LoginPermissions.Where(p => p.Name == permissionName).FirstOrDefault();
            
            if (permission != null)
            {
                if (permission.HasPermission)
                    return true;
                else
                    return false;
            }

            return false;
        }
                
    }
}
