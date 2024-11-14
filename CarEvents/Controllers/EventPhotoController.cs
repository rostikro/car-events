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
    public class EventPhotoController : Controller
    {
        private readonly DBContext _context;

        public EventPhotoController(DBContext context)
        {
            _context = context;
        }

        // GET: EventPhoto
        public async Task<IActionResult> Index()
        {
            var dBContext = _context.EventPhotos.Include(e => e.Event);
            return View(await dBContext.ToListAsync());
        }

        // GET: EventPhoto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventPhoto = await _context.EventPhotos
                .Include(e => e.Event)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventPhoto == null)
            {
                return NotFound();
            }

            return View(eventPhoto);
        }

        // GET: EventPhoto/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id");
            return View();
        }

        // POST: EventPhoto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EventId,PhotoUrl")] EventPhoto eventPhoto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventPhoto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", eventPhoto.EventId);
            return View(eventPhoto);
        }

        // GET: EventPhoto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventPhoto = await _context.EventPhotos.FindAsync(id);
            if (eventPhoto == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", eventPhoto.EventId);
            return View(eventPhoto);
        }

        // POST: EventPhoto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EventId,PhotoUrl")] EventPhoto eventPhoto)
        {
            if (id != eventPhoto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventPhoto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventPhotoExists(eventPhoto.Id))
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
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", eventPhoto.EventId);
            return View(eventPhoto);
        }

        // GET: EventPhoto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventPhoto = await _context.EventPhotos
                .Include(e => e.Event)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventPhoto == null)
            {
                return NotFound();
            }

            return View(eventPhoto);
        }

        // POST: EventPhoto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventPhoto = await _context.EventPhotos.FindAsync(id);
            if (eventPhoto != null)
            {
                _context.EventPhotos.Remove(eventPhoto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventPhotoExists(int id)
        {
            return _context.EventPhotos.Any(e => e.Id == id);
        }
    }
}
