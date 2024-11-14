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
    public class CarEventController : Controller
    {
        private readonly DBContext _context;

        public CarEventController(DBContext context)
        {
            _context = context;
        }

        // GET: CarEvent
        public async Task<IActionResult> Index()
        {
            var dBContext = _context.CarEvents.Include(c => c.Car).Include(c => c.Event);
            return View(await dBContext.ToListAsync());
        }

        // GET: CarEvent/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carEvent = await _context.CarEvents
                .Include(c => c.Car)
                .Include(c => c.Event)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carEvent == null)
            {
                return NotFound();
            }

            return View(carEvent);
        }

        // GET: CarEvent/Create
        public IActionResult Create()
        {
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id");
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id");
            return View();
        }

        // POST: CarEvent/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CarId,EventId")] CarEvent carEvent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", carEvent.CarId);
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", carEvent.EventId);
            return View(carEvent);
        }

        // GET: CarEvent/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carEvent = await _context.CarEvents.FindAsync(id);
            if (carEvent == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", carEvent.CarId);
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", carEvent.EventId);
            return View(carEvent);
        }

        // POST: CarEvent/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CarId,EventId")] CarEvent carEvent)
        {
            if (id != carEvent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarEventExists(carEvent.Id))
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
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", carEvent.CarId);
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", carEvent.EventId);
            return View(carEvent);
        }

        // GET: CarEvent/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carEvent = await _context.CarEvents
                .Include(c => c.Car)
                .Include(c => c.Event)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carEvent == null)
            {
                return NotFound();
            }

            return View(carEvent);
        }

        // POST: CarEvent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carEvent = await _context.CarEvents.FindAsync(id);
            if (carEvent != null)
            {
                _context.CarEvents.Remove(carEvent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarEventExists(int id)
        {
            return _context.CarEvents.Any(e => e.Id == id);
        }
    }
}
