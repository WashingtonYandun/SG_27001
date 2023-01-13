using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SG.Data;
using SG.Models;

namespace SG.Controllers
{
    public class ResourcesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResourcesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Resources
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Resources.Include(r => r.RelatedArea).Include(r => r.ResourceCategory).Include(r => r.ResourceType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Resources/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Resources == null)
            {
                return NotFound();
            }

            var resource = await _context.Resources
                .Include(r => r.RelatedArea)
                .Include(r => r.ResourceCategory)
                .Include(r => r.ResourceType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resource == null)
            {
                return NotFound();
            }

            return View(resource);
        }

        // GET: Resources/Create
        public IActionResult Create()
        {
            ViewData["RelatedAreaId"] = new SelectList(_context.RelatedAreas, "Id", "Name");
            ViewData["ResourceCategoryId"] = new SelectList(_context.ResourceCategorys, "Id", "Name");
            ViewData["ResourceTypeId"] = new SelectList(_context.ResourceTypes, "Id", "Name");
            return View();
        }

        // POST: Resources/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Owner,Guardian,Integrity,Confidentiality,Availavility,RelatedAreaId,ResourceTypeId,ResourceCategoryId")] Resource resource)
        {
            if (ModelState.IsValid)
            {
                resource.Value = resource.Integrity + resource.Confidentiality + resource.Availavility;
                _context.Add(resource);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RelatedAreaId"] = new SelectList(_context.RelatedAreas, "Id", "Name", resource.RelatedAreaId);
            ViewData["ResourceCategoryId"] = new SelectList(_context.ResourceCategorys, "Id", "Name", resource.ResourceCategoryId);
            ViewData["ResourceTypeId"] = new SelectList(_context.ResourceTypes, "Id", "Name", resource.ResourceTypeId);
            return View(resource);
        }

        // GET: Resources/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Resources == null)
            {
                return NotFound();
            }

            var resource = await _context.Resources.FindAsync(id);
            if (resource == null)
            {
                return NotFound();
            }
            ViewData["RelatedAreaId"] = new SelectList(_context.RelatedAreas, "Id", "Name", resource.RelatedAreaId);
            ViewData["ResourceCategoryId"] = new SelectList(_context.ResourceCategorys, "Id", "Name", resource.ResourceCategoryId);
            ViewData["ResourceTypeId"] = new SelectList(_context.ResourceTypes, "Id", "Name", resource.ResourceTypeId);
            return View(resource);
        }

        // POST: Resources/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Owner,Guardian,Integrity,Confidentiality,Availavility,Value,RelatedAreaId,ResourceTypeId,ResourceCategoryId")] Resource resource)
        {
            if (id != resource.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resource);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResourceExists(resource.Id))
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
            ViewData["RelatedAreaId"] = new SelectList(_context.RelatedAreas, "Id", "Name", resource.RelatedAreaId);
            ViewData["ResourceCategoryId"] = new SelectList(_context.ResourceCategorys, "Id", "Name", resource.ResourceCategoryId);
            ViewData["ResourceTypeId"] = new SelectList(_context.ResourceTypes, "Id", "Name", resource.ResourceTypeId);
            return View(resource);
        }

        // GET: Resources/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Resources == null)
            {
                return NotFound();
            }

            var resource = await _context.Resources
                .Include(r => r.RelatedArea)
                .Include(r => r.ResourceCategory)
                .Include(r => r.ResourceType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resource == null)
            {
                return NotFound();
            }

            return View(resource);
        }

        // POST: Resources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Resources == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Resources'  is null.");
            }
            var resource = await _context.Resources.FindAsync(id);
            if (resource != null)
            {
                _context.Resources.Remove(resource);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResourceExists(int id)
        {
          return _context.Resources.Any(e => e.Id == id);
        }
    }
}
