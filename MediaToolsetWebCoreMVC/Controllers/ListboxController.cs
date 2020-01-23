using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediaToolsetWebCoreMVC.Areas.Identity.Data;
using MediaToolsetWebCoreMVC.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;

namespace MediaToolsetWebCoreMVC.Controllers
{
    public class ListboxController : Controller
    {
        SignInManager<AuthenticatedUser> SignInManager;
        UserManager<AuthenticatedUser> UserManager;
        RoleManager<IdentityRole> RoleManager;
        IHttpContextAccessor HttpContextAccessor;
        AuthenticatedUserInfo UserInfo;


        public ListboxController(SignInManager<AuthenticatedUser> _signinManager, UserManager<AuthenticatedUser> _userManager, RoleManager<IdentityRole> _roleManager, IHttpContextAccessor _httpContextAccessor, AuthenticatedUserInfo _userInfo)
        {
            SignInManager = _signinManager;
            UserManager = _userManager;
            RoleManager = _roleManager;
            HttpContextAccessor = _httpContextAccessor;
            UserInfo = _userInfo;
        }

        [Authorize]
        public List<object> GetAuthorizedNavItems()
        {
            List<object> NavMenuItems = new List<object>();

            if (UserInfo.IsRoleMember("ContentViewer") || UserInfo.IsRoleMember("Administrator"))
            {
                NavMenuItems.Add(new
                {
                    Category = "Television",
                    text = "Television",
                    items = new List<object>()
                {
                    new
                    {
                        text = "Show Library",
                        url = "/Television/Library"
                    }                    
                }
                });

                NavMenuItems.Add(new
                {
                    Category = "Movies",
                    text = "Movies"
                });
            }            
                        
            if (UserInfo.IsRoleMember("ContentModerator") || UserInfo.IsRoleMember("Administrator"))
            {
                NavMenuItems.Add(
                    new
                    {
                        Category = "Sort",
                        text = "Sort Queue",
                        items = new List<object>()
                    {
                    new
                    {
                        text = "Current Queue"
                    },

                    new
                    {
                        text = "ReScan Contents"
                    },

                    new
                    {
                        text = "Classify Contents"
                    },

                    new
                    {
                        text = "Admin Dashboard"
                    }
                }
                });

                NavMenuItems.Add(new
                {
                    Category = "Media Lookup",
                    text = "Media Lookup",
                    items = new List<object>()
                {
                    new
                    {
                        text = "Tv Maze"
                    },

                    new
                    {
                        text = "The Movie DB"
                    },

                    new
                    {
                        text = "IMDB"
                    }
                }
                });

                NavMenuItems.Add(new
                {
                    Category = "Media Acquisition",
                    text = "Media Acquisition",
                    items = new List<object>()
                {
                    new
                    {
                        text = "EzTV",
                        items = new List<object>()
                        {
                            new
                            {
                                text = "Add Download"
                            }
                        }
                    },

                    new
                    {
                        text = "Classification"
                    }
                }
                });
                
            }            

            if (UserInfo.IsRoleMember("Administrator"))
            {
                NavMenuItems.Add(new
                {
                    Category = "Administration",
                    text = "Administration",
                    items = new List<object>()
                {
                    new
                    {
                        text = "Settings"
                    },

                    new
                    {
                        text = "Classification"
                    },
                    new
                    {
                        Category = "Administration",
                        text = "User Management",
                        items = new List<object>()
                        {
                            new
                            {
                                text = "User Accounts"
                            },

                            new
                            {
                                text = "Manage Roles",
                                url = "/Role"
                            },
                            new
                            {
                                text = "Create Role",
                                url = "/Role/Create"
                            }
                        }
                    }
                }
                });
            }

            return NavMenuItems;
        }


    }
}