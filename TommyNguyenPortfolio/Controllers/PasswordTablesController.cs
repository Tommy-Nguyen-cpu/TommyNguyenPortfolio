using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TommyNguyenPortfolio.Data;
using TommyNguyenPortfolio.Models;

namespace TommyNguyenPortfolio.Controllers
{
    public class PasswordTablesController : Controller
    {
        private readonly TommyNguyenPortfolioContext _context;

        public PasswordTablesController(TommyNguyenPortfolioContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Used to check to see whether or not the client is still logged in. Sets ViewData flag for clientID and IsAdmin (used to determine whether or not they can access certain parts of site).
        /// </summary>
        public void setClientIDFlag()
        {
            int? clientID = HttpContext.Session.GetInt32("ClientID");
            bool doesClientIDExist = clientID != null && clientID != 0 ? true : false;
            ViewData["ClientID"] = doesClientIDExist;
            ViewData["IsAdmin"] = clientID == 1 ? true : false;
        }

        public IActionResult Logout()
        {
            HttpContext.Session.SetInt32("ClientID", 0);
            return RedirectToAction("Index", "Home");
        }
        // GET: PasswordTables
        public async Task<IActionResult> Index()
        {
            //DONE: configure the HttpContext for this project. It will throw an exception otherwise.
                    //TODO: Will have to make sure user allows the site to use cookies.
            int? clientID = HttpContext.Session.GetInt32("ClientID");
            if(clientID == null)
            {
                //ClientID == null means that they either haven't logged in or there is some issues with login.
                return RedirectToAction("Index", "Home", new {error="There is no user associated with this session. Please login to be verified so you can have access to this page" });
            }
            setClientIDFlag();
            int permissionLevel = await _context.PasswordTable.Where(passwordID => passwordID.PasswordTableId == (int)clientID).Select(level => level.PermissionLevel).FirstOrDefaultAsync();
            if(permissionLevel < 2)
            {
                //Users below admin should not be able to view user account info.
                return RedirectToAction("Index", "Home", new { error = "You do not have permission to view this page. You need to be admin to view this page." });
            }

            return View(await _context.PasswordTable.ToListAsync());
        }

        public async Task<IActionResult> Login([Bind("Password,Username")] PasswordTable passwordTable, string error = null)
        {
            //If the "error" parameter was passed (i.e. there was an error) then we set the viewdata for Error so the layout.cshtml can create the alert.
            if(error != null)
            {
                ViewData["Error"] = error;
            }

            //If user entered password.
            if (passwordTable.Password != null)
            {
                //Hash the password enters and verified with passord in database.
                var userPassword = await _context.PasswordTable.Where(ex => ex.Username == passwordTable.Username).Select(e => e.Password).FirstOrDefaultAsync();
                //If we cannot find a user within the database, then there was an issue.
                if(userPassword == null)
                {
                    return RedirectToAction("Login", "PasswordTables", new { error = "The username you entered does not exist. " });
                }
                var verifyPassword = new PasswordHasher<object>().VerifyHashedPassword(passwordTable.Username, userPassword, passwordTable.Password);
                //System.Diagnostics.Debug.WriteLine("Verify: " + verifyPassword);

                //If password matches any in the database, then we know they entered correctly.
                if (verifyPassword == PasswordVerificationResult.Success)
                {
                    var idWhereUserNameAndPasswordMatches = await _context.PasswordTable.Where(ex => ex.Username == passwordTable.Username).Select(ex => ex.PasswordTableId).FirstOrDefaultAsync();
                    HttpContext.Session.SetInt32("ClientID", idWhereUserNameAndPasswordMatches);
                    setClientIDFlag();
                    return RedirectToAction(nameof(RecommendationDatabasesController.Recommendations), "RecommendationDatabases");
                }
                else
                {
                    //Otherwise, redirect them to Home page.
                    return RedirectToAction("Index", "Home", new { error = "The user could not be found. Please make sure you typed the username and password correctly." });
                }
            }
            return View();
        }

        // GET: PasswordTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            int? clientID = HttpContext.Session.GetInt32("ClientID");
            if (clientID == null)
            {
                //ClientID == null means that they either haven't logged in or there is some issues with login.
                return RedirectToAction("Index", "Home", new { error = "There is no user associated with this session. Please login to be verified so you can have access to this page" });
            }
            setClientIDFlag();
            if (id == null)
            {
                return NotFound();
            }

            var passwordTable = await _context.PasswordTable
                .FirstOrDefaultAsync(m => m.PasswordTableId == id);
            if (passwordTable == null)
            {
                return NotFound();
            }

            return View(passwordTable);
        }

        // GET: PasswordTables/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: PasswordTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PasswordTableId,Username,Email,Password")] PasswordTable passwordTable)
        {

            //Check to see if username entered matches any existing users.
            var doesUsernameExist = _context.PasswordTable.Where(ex => ex.Username == passwordTable.Username).FirstOrDefault();

            //If there is an existing user, we 
            if(doesUsernameExist != null)
            {
                ViewData["Error"] = "The username already exist. Please enter a new one.";
                return View(passwordTable);
            }
            var doesEmailExist = _context.PasswordTable.Where(ex => ex.Email == passwordTable.Email).FirstOrDefault();
            if (doesEmailExist != null)
            {
                ViewData["Error"] = "There is an existing account with this email address. Please enter a new one.";
                return View(passwordTable);
            }
            string hashedPassword = new PasswordHasher<object>().HashPassword(passwordTable.Username, passwordTable.Password);
            var initialResultOfHash = new PasswordHasher<object>().VerifyHashedPassword(passwordTable.Username, hashedPassword, passwordTable.Password);
            passwordTable.Password = hashedPassword;
            //Fixed Error: Turns out I needed to set the ClientID to something instead of leaving it as null.
            passwordTable.ClientID = 0;

            if (ModelState.IsValid)
            {
                _context.Add(passwordTable);
                await _context.SaveChangesAsync();
                return RedirectToAction("Recommendations", "RecommendationDatabases");
            }
            return View(passwordTable);
        }

        // GET: PasswordTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            int? clientID = HttpContext.Session.GetInt32("ClientID");
            if (clientID == null)
            {
                //ClientID == null means that they either haven't logged in or there is some issues with login.
                return RedirectToAction("Index", "Home", new { error = "There is no user associated with this session. Please login to be verified so you can have access to this page" });
            }
            setClientIDFlag();
            if (id == null)
            {
                return NotFound();
            }

            var passwordTable = await _context.PasswordTable.FindAsync(id);
            if (passwordTable == null)
            {
                return NotFound();
            }
            return View(passwordTable);
        }

        // POST: PasswordTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PasswordTableId,Password,Username")] PasswordTable passwordTable)
        {
            setClientIDFlag();
            if (id != passwordTable.PasswordTableId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(passwordTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PasswordTableExists(passwordTable.PasswordTableId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(passwordTable);
        }

        // GET: PasswordTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            int? clientID = HttpContext.Session.GetInt32("ClientID");
            if (clientID == null)
            {
                //ClientID == null means that they either haven't logged in or there is some issues with login.
                return RedirectToAction("Index", "Home", new { error = "There is no user associated with this session. Please login to be verified so you can have access to this page" });
            }
            setClientIDFlag();
            if (id == null)
            {
                return NotFound();
            }

            var passwordTable = await _context.PasswordTable
                .FirstOrDefaultAsync(m => m.PasswordTableId == id);
            if (passwordTable == null)
            {
                return NotFound();
            }

            return View(passwordTable);
        }

        // POST: PasswordTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            int? clientID = HttpContext.Session.GetInt32("ClientID");
            if (clientID == null)
            {
                //ClientID == null means that they either haven't logged in or there is some issues with login.
                return RedirectToAction("Index", "Home", new { error = "There is no user associated with this session. Please login to be verified so you can have access to this page" });
            }
            setClientIDFlag();
            var passwordTable = await _context.PasswordTable.FindAsync(id);
            _context.PasswordTable.Remove(passwordTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PasswordTableExists(int id)
        {
            return _context.PasswordTable.Any(e => e.PasswordTableId == id);
        }


    }
}
