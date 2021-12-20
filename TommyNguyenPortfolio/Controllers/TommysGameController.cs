using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TommyNguyenPortfolio.Controllers
{
    public class TommysGameController : Controller
    {

        public IActionResult Start(string currentCollegeTommyGoesTo = null)
        {
            if (currentCollegeTommyGoesTo.ToLower().Equals("wentworth institute of technology") || currentCollegeTommyGoesTo.ToLower().Equals("wit"))
            {
                ViewData["IsOnGamePage"] = "yes";
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult Game2()
        {
            ViewData["IsOnGamePage"] = "yes";
            return View();
        }

        public IActionResult BluePillPicked()
        {
            ViewData["IsOnGamePage"] = "yes";
            return View();
        }

        public IActionResult RedPillPicked()
        {
            ViewData["IsOnGamePage"] = "yes";
            return View();
        }
    }
}
