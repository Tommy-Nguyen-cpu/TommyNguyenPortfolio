using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

using TommyNguyenPortfolio.Data;
using TommyNguyenPortfolio.Models;

namespace TommyNguyenPortfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TommyNguyenPortfolioContext _context;

        public HomeController(ILogger<HomeController> logger, TommyNguyenPortfolioContext context)
        {
            _logger = logger;
        }
        public void setClientIDFlag()
        {
            int? clientID = HttpContext.Session.GetInt32("ClientID");
            bool doesClientIDExist = clientID != null && clientID != 0 ? true : false;
            ViewData["ClientID"] = doesClientIDExist;
            ViewData["IsAdmin"] = clientID == 1 ? true : false;
        }
        public IActionResult Index( string error = null)
        {
            setClientIDFlag();
            if (!string.IsNullOrEmpty(error))
            {
                ViewData["Error"] = error;
            }

            return View();
        }

        //Returns the "View" page that has the same name as the method.
        public IActionResult Sebastian()
        {
            setClientIDFlag();
            return View();
        }

        //Returns the "View" page that has the same name as the method.
        public IActionResult RPGGame()
        {
            setClientIDFlag();
            return View();
        }

        public IActionResult DatabaseProject()
        {
            setClientIDFlag();
            return View();
        }
        //Recommendations will contain a database that holds all comments about me.
        //DONE: Make it so that only people who are authorized can add comments.
        //Also, only people who made the comment can add, edit, or remove their comments.
        //DONE: Create a separate database that holds the passwords for the individual comments/recommendations (only those who have the password can edit or delete posts).
        public IActionResult Recommendations()
        {
            setClientIDFlag();
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
