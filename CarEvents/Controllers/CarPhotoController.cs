using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarEvents.Models;
using CarEvents.Services;

namespace CarEvents.Controllers
{
    public class CarPhotoController : Controller
    {
        private readonly ImageUploadService _imageUploadService;
        private readonly DBContext _context;

        public CarPhotoController(ImageUploadService imageUploadService, DBContext context)
        {
            _imageUploadService = imageUploadService;
            _context = context;
        }

        // GET: CarPhoto
        public async Task<IActionResult> Index()
        {
            var dBContext = _context.CarPhotos.Include(c => c.Car);
            return View(await dBContext.ToListAsync());
        }

        // GET: CarPhoto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carPhoto = await _context.CarPhotos
                .Include(c => c.Car)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carPhoto == null)
            {
                return NotFound();
            }

            return View(carPhoto);
        }

        // GET: CarPhoto/Create
        public IActionResult Create()
        {
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int carId, IFormFile photo)
        {
            if (photo != null && photo.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);

                using (var stream = photo.OpenReadStream())
                {
                    var photoUrl = await _imageUploadService.UploadImageAsync(stream, fileName);
                    Console.WriteLine(photoUrl);
                    var carPhoto = new CarPhoto
                    {
                        CarId = carId,
                        PhotoUrl = photoUrl
                    };

                    _context.CarPhotos.Add(carPhoto);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Details", "Car", new { id = carId });
            }

            return BadRequest("No photo uploaded");
        }

        // GET: CarPhoto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carPhoto = await _context.CarPhotos.FindAsync(id);
            if (carPhoto == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", carPhoto.CarId);
            return View(carPhoto);
        }

        // POST: CarPhoto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CarId,PhotoUrl")] CarPhoto carPhoto)
        {
            if (id != carPhoto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carPhoto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarPhotoExists(carPhoto.Id))
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
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", carPhoto.CarId);
            return View(carPhoto);
        }

        // GET: CarPhoto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carPhoto = await _context.CarPhotos
                .Include(c => c.Car)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carPhoto == null)
            {
                return NotFound();
            }

            return View(carPhoto);
        }

        // POST: CarPhoto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carPhoto = await _context.CarPhotos.FindAsync(id);
            if (carPhoto != null)
            {
                _context.CarPhotos.Remove(carPhoto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarPhotoExists(int id)
        {
            return _context.CarPhotos.Any(e => e.Id == id);
        }
    }
}
