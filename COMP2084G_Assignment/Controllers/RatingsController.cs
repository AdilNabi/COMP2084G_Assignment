using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using COMP2084G_Assignment.Data;
using COMP2084G_Assignment.Models;
using Microsoft.AspNetCore.Authorization;

namespace COMP2084G_Assignment.Controllers
{
    [Authorize]
    public class RatingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RatingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ratings
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Ratings.Include(r => r.Book).OrderBy(b => b.Heading);
            return View("Index", await applicationDbContext.ToListAsync());
        }

        // GET: Ratings/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("404");
            }

            var rating = await _context.Ratings
                .Include(r => r.Book)
                .FirstOrDefaultAsync(m => m.RatingId == id);
            if (rating == null)
            {
                return View("404");
            }

            return View("Details", rating);
        }

        // GET: Ratings/Create
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "Title");
            return View("Create");
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RatingId,BookId,Name,Heading,Score,Body")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rating);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "Title", rating.BookId);
            return View("Create",rating);
        }

        // GET: Ratings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View("404");
            }

            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null)
            {
                return View("404");
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "Title", rating.BookId);
            return View("Edit",rating);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RatingId,BookId,Name,Heading,Score,Body")] Rating rating)
        {
            if (id != rating.RatingId)
            {
                return View("404");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingExists(rating.RatingId))
                    {
                        return View("404");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Books, "BookId", "Title", rating.BookId);
            return View(rating);
        }

        // GET: Ratings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View("404");
            }

            var rating = await _context.Ratings
                .Include(r => r.Book)
                .FirstOrDefaultAsync(m => m.RatingId == id);
            if (rating == null)
            {
                return View("404");
            }

            return View("Delete",rating);
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RatingExists(int id)
        {
            return _context.Ratings.Any(e => e.RatingId == id);
        }
    }
}
