using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MediaToolsetWebCoreMVC.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Create()
        {

            return View();
        }

        public IActionResult Delete(int id)
        {

            return View();
        }

        public IActionResult Send()
        {

            return View();
        }

        public IActionResult Read(int id)
        {

            return View();
        }
    }
}