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
    public class ResourceTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResourceTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ResourceTypes
        public async Task<IActionResult> Index()
        {
              return View(await _context.ResourceTypes.ToListAsync());
        }

        // GET: ResourceTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ResourceTypes == null)
            {
                return NotFound();
            }

            var resourceType = await _context.ResourceTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resourceType == null)
            {
                return NotFound();
            }

            return View(resourceType);
        }

        // GET: ResourceTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ResourceTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ResourceType resourceType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resourceType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(resourceType);
        }

        // GET: ResourceTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ResourceTypes == null)
            {
                return NotFound();
            }

            var resourceType = await _context.ResourceTypes.FindAsync(id);
            if (resourceType == null)
            {
                return NotFound();
            }
            return View(resourceType);
        }

        // POST: ResourceTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ResourceType resourceType)
        {
            if (id != resourceType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resourceType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResourceTypeExists(resourceType.Id))
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
            return View(resourceType);
        }

        // GET: ResourceTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ResourceTypes == null)
            {
                return NotFound();
            }

            var resourceType = await _context.ResourceTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resourceType == null)
            {
                return NotFound();
            }

            return View(resourceType);
        }

        // POST: ResourceTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ResourceTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ResourceTypes'  is null.");
            }
            var resourceType = await _context.ResourceTypes.FindAsync(id);
            if (resourceType != null)
            {
                _context.ResourceTypes.Remove(resourceType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResourceTypeExists(int id)
        {
          return _context.ResourceTypes.Any(e => e.Id == id);
        }
    }
}
