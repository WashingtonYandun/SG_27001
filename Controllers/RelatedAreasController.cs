using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SG.Data;
using SG.Models;

namespace SG.Controllers
{
    //[Authorize]
    public class RelatedAreasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RelatedAreasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RelatedAreas
        public async Task<IActionResult> Index()
        {
              return View(await _context.RelatedAreas.ToListAsync());
        }

        // GET: RelatedAreas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RelatedAreas == null)
            {
                return NotFound();
            }

            var relatedArea = await _context.RelatedAreas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (relatedArea == null)
            {
                return NotFound();
            }

            return View(relatedArea);
        }

        // GET: RelatedAreas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RelatedAreas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] RelatedArea relatedArea)
        {
            if (ModelState.IsValid)
            {
                _context.Add(relatedArea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(relatedArea);
        }

        // GET: RelatedAreas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RelatedAreas == null)
            {
                return NotFound();
            }

            var relatedArea = await _context.RelatedAreas.FindAsync(id);
            if (relatedArea == null)
            {
                return NotFound();
            }
            return View(relatedArea);
        }

        // POST: RelatedAreas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] RelatedArea relatedArea)
        {
            if (id != relatedArea.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(relatedArea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RelatedAreaExists(relatedArea.Id))
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
            return View(relatedArea);
        }

        // GET: RelatedAreas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RelatedAreas == null)
            {
                return NotFound();
            }

            var relatedArea = await _context.RelatedAreas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (relatedArea == null)
            {
                return NotFound();
            }

            return View(relatedArea);
        }

        // POST: RelatedAreas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RelatedAreas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RelatedAreas'  is null.");
            }
            var relatedArea = await _context.RelatedAreas.FindAsync(id);
            if (relatedArea != null)
            {
                _context.RelatedAreas.Remove(relatedArea);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RelatedAreaExists(int id)
        {
          return _context.RelatedAreas.Any(e => e.Id == id);
        }
    }
}
