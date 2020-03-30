using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Entities.Configuration.Identity.User;
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

            if (UserInfo.IsRoleMember("ContentViewer") || UserInfo.IsRoleMember("Administrator") || UserInfo.IsRoleMember("SuperAdmin"))
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
                        },
                        new
                        {
                            text = "Rescan Library",
                            url = "/Television/RescanLibrary"
                        }
                    }
                });

                NavMenuItems.Add(new
                {
                    Category = "Movies",
                    text = "Movies"
                });
            }            
                        
            if (UserInfo.IsRoleMember("ContentModerator") || UserInfo.IsRoleMember("Administrator") || UserInfo.IsRoleMember("SuperAdmin"))
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
                                text = "Dashboard",
                                url = "/Sort/Dashboard"
                            },

                            new
                            {
                                text = "Current Queue",
                                url = "/Sort"
                            },

                            new
                            {
                                text = "ReScan Contents",
                                url = "/Sort/RescanContents"
                            },

                            new
                            {
                                text = "Classify Contents"
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
                            text = "The Movie DB",
                            items = new List<object>()
                            {
                                new
                                {
                                    text = "Television",
                                    items = new List<object>()
                                    {
                                        new
                                        {
                                            text = "Find Television Shows",
                                            url = "/Television/MovieDbSearchMultiple"
                                        }
                                    }
                                },
                                
                                new
                                {
                                    text = "Movies",
                                    items = new List<object>()
                                    {
                                        new
                                        {
                                            text = "Find Movies",
                                            url = "/Movies/MovieDbSearchMultiple"
                                        }
                                    }
                                }
                            }
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

            if (UserInfo.IsRoleMember("Administrator") || UserInfo.IsRoleMember("SuperAdmin"))
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
                                    text = "Accounts",
                                    items = new List<object>()
                                    {
                                        new
                                        {
                                            text = "Create Account",
                                            url = "/User/Create"
                                        },
                                        new
                                        {
                                            text = "Manage",
                                            url = "/User/Manage"
                                        }
                                    }
                                },
                                new
                                {
                                    text = "Roles",
                                    items = new List<object>()
                                    {
                                        new
                                        {
                                            text = "Manage Role Membership",
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
                        },
                        new
                        {
                            text = "Notifications & Logging",
                            url = "/Administrator/AdminLogs"
                        }
                    }
                });
            }

            NavMenuItems.Add(new
            {
                text = UserInfo.UserName,
                items = new List<object>()
                {
                    new
                    {
                        text = "Profile",
                        url = "/User/Profile"
                    },
                    new
                    {
                        text = "Roles",
                        items = new List<object>
                        {
                            new
                            {
                                text = "My Roles",
                                url = "/User/MyRoles"
                            }
                        }
                    },
                    new
                    {
                        text = "Login Permissions",
                        url = "/User/LoginPermissions"
                    },
                    new
                    {
                        text = "Logout",
                        url = "/User/Logout"
                    }
                }
            });

            return NavMenuItems;
        }


    }
}