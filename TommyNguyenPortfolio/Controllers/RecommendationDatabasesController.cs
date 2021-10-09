using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TommyNguyenPortfolio.Data;
using TommyNguyenPortfolio.Models;

namespace TommyNguyenPortfolio.Controllers
{
    public class RecommendationDatabasesController : Controller
    {
        private readonly TommyNguyenPortfolioContext _context;

        public RecommendationDatabasesController(TommyNguyenPortfolioContext context)
        {
            _context = context;
        }

        // GET: RecommendationDatabases
        public async Task<IActionResult> Recommendations()
        {
            int? clientID = HttpContext.Session.GetInt32("ClientID");
            if (clientID != null)
            {
                ViewData["SpecificClient"] = await _context.PasswordTable.Where(passwordID => passwordID.PasswordTableId == (int)clientID).Select(client => client.PasswordTableId).FirstOrDefaultAsync();
            }
            setClientIDFlag();
            var listOfRecommendationID = await _context.RecommendationDatabase.Select(ex => ex.passwordTableID).ToListAsync();

            return View(await _context.RecommendationDatabase.ToListAsync());
        }

        // GET: RecommendationDatabases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            setClientIDFlag();
            if (id == null)
            {
                return NotFound();
            }

            var recommendationDatabase = await _context.RecommendationDatabase
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recommendationDatabase == null)
            {
                return NotFound();
            }

            return View(recommendationDatabase);
        }

        // GET: RecommendationDatabases/Create
        public IActionResult Create()
        {
            setClientIDFlag();
            return View();
        }
        public void setClientIDFlag()
        {
            int? clientID = HttpContext.Session.GetInt32("ClientID");
            System.Diagnostics.Debug.WriteLine("ClientID: " + clientID);
            bool doesClientIDExist = clientID != null && clientID != 0 ? true : false;
            ViewData["ClientID"] = doesClientIDExist;
        }
        // POST: RecommendationDatabases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Recommender,CompanyWorkedFor,RelationToStudent,Recommendation,DateRecommended")] RecommendationDatabase recommendationDatabase)
        {
            int? clientID = HttpContext.Session.GetInt32("ClientID");
            setClientIDFlag();
            recommendationDatabase.passwordTableID = await _context.PasswordTable.Where(ex => ex.PasswordTableId == (int)clientID).FirstOrDefaultAsync();
            if (ModelState.IsValid)
            {
                _context.Add(recommendationDatabase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Recommendations));
            }
            return View(recommendationDatabase);
        }

        // GET: RecommendationDatabases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            setClientIDFlag();
            if (id == null)
            {
                return NotFound();
            }

            var recommendationDatabase = await _context.RecommendationDatabase.FindAsync(id);
            if (recommendationDatabase == null)
            {
                return NotFound();
            }
            return View(recommendationDatabase);
        }

        // POST: RecommendationDatabases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Recommender,CompanyWorkedFor,RelationToStudent,Recommendation,DateRecommended")] RecommendationDatabase recommendationDatabase)
        {
            setClientIDFlag();
            if (id != recommendationDatabase.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recommendationDatabase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecommendationDatabaseExists(recommendationDatabase.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Recommendations));
            }
            return View(recommendationDatabase);
        }

        // GET: RecommendationDatabases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            setClientIDFlag();
            if (id == null)
            {
                return NotFound();
            }

            var recommendationDatabase = await _context.RecommendationDatabase
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recommendationDatabase == null)
            {
                return NotFound();
            }

            return View(recommendationDatabase);
        }

        // POST: RecommendationDatabases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            setClientIDFlag();
            var recommendationDatabase = await _context.RecommendationDatabase.FindAsync(id);
            _context.RecommendationDatabase.Remove(recommendationDatabase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Recommendations));
        }

        private bool RecommendationDatabaseExists(int id)
        {
            return _context.RecommendationDatabase.Any(e => e.Id == id);
        }
    }
}
