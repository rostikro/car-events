using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarEvents.Models;

namespace CarEvents.Controllers
{
    public class FeatureUserController : Controller
    {
        private readonly DBContext _context;

        public FeatureUserController(DBContext context)
        {
            _context = context;
        }

        // GET: FeatureUser
        public async Task<IActionResult> Index()
        {
            var dBContext = _context.FeatureUsers.Include(f => f.Feature).Include(f => f.User);
            return View(await dBContext.ToListAsync());
        }

        // GET: FeatureUser/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var featureUser = await _context.FeatureUsers
                .Include(f => f.Feature)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (featureUser == null)
            {
                return NotFound();
            }

            return View(featureUser);
        }

        // GET: FeatureUser/Create
        public IActionResult Create()
        {
            ViewData["FeatureId"] = new SelectList(_context.FeatureFlags, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: FeatureUser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FeatureId,UserId")] FeatureUser featureUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(featureUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FeatureId"] = new SelectList(_context.FeatureFlags, "Id", "Id", featureUser.FeatureId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", featureUser.UserId);
            return View(featureUser);
        }

        // GET: FeatureUser/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var featureUser = await _context.FeatureUsers.FindAsync(id);
            if (featureUser == null)
            {
                return NotFound();
            }
            ViewData["FeatureId"] = new SelectList(_context.FeatureFlags, "Id", "Id", featureUser.FeatureId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", featureUser.UserId);
            return View(featureUser);
        }

        // POST: FeatureUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FeatureId,UserId")] FeatureUser featureUser)
        {
            if (id != featureUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(featureUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeatureUserExists(featureUser.Id))
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
            ViewData["FeatureId"] = new SelectList(_context.FeatureFlags, "Id", "Id", featureUser.FeatureId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", featureUser.UserId);
            return View(featureUser);
        }

        // GET: FeatureUser/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var featureUser = await _context.FeatureUsers
                .Include(f => f.Feature)
                .Include(f => f.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (featureUser == null)
            {
                return NotFound();
            }

            return View(featureUser);
        }

        // POST: FeatureUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var featureUser = await _context.FeatureUsers.FindAsync(id);
            if (featureUser != null)
            {
                _context.FeatureUsers.Remove(featureUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeatureUserExists(int id)
        {
            return _context.FeatureUsers.Any(e => e.Id == id);
        }
    }
}
