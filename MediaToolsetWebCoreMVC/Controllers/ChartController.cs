using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.EJ2.Charts;

namespace MediaToolsetWebCoreMVC.Controllers
{
    public partial class ChartController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            List<PieData> chartData = new List<PieData>
            {
                new PieData { xValue =  "Free Space", yValue = 223, text = "33%" },
                new PieData { xValue =  "Used Space", yValue = 446, text = "66%" }
            };
            ViewBag.dataSource = chartData;
            return View();
        }

        public class PieData
        {
            public string xValue;
            public double yValue;
            public string text;
        }

    }
}