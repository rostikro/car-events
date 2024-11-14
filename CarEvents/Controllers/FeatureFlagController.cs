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
    public class FeatureFlagController : Controller
    {
        private readonly DBContext _context;

        public FeatureFlagController(DBContext context)
        {
            _context = context;
        }

        // GET: FeatureFlag
        public async Task<IActionResult> Index()
        {
            return View(await _context.FeatureFlags.ToListAsync());
        }

        // GET: FeatureFlag/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var featureFlag = await _context.FeatureFlags
                .FirstOrDefaultAsync(m => m.Id == id);
            if (featureFlag == null)
            {
                return NotFound();
            }

            return View(featureFlag);
        }

        // GET: FeatureFlag/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FeatureFlag/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FeatureName")] FeatureFlag featureFlag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(featureFlag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(featureFlag);
        }

        // GET: FeatureFlag/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var featureFlag = await _context.FeatureFlags.FindAsync(id);
            if (featureFlag == null)
            {
                return NotFound();
            }
            return View(featureFlag);
        }

        // POST: FeatureFlag/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FeatureName")] FeatureFlag featureFlag)
        {
            if (id != featureFlag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(featureFlag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeatureFlagExists(featureFlag.Id))
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
            return View(featureFlag);
        }

        // GET: FeatureFlag/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var featureFlag = await _context.FeatureFlags
                .FirstOrDefaultAsync(m => m.Id == id);
            if (featureFlag == null)
            {
                return NotFound();
            }

            return View(featureFlag);
        }

        // POST: FeatureFlag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var featureFlag = await _context.FeatureFlags.FindAsync(id);
            if (featureFlag != null)
            {
                _context.FeatureFlags.Remove(featureFlag);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeatureFlagExists(int id)
        {
            return _context.FeatureFlags.Any(e => e.Id == id);
        }
    }
}
