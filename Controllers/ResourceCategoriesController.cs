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
    public class ResourceCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResourceCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ResourceCategories
        public async Task<IActionResult> Index()
        {
              return View(await _context.ResourceCategorys.ToListAsync());
        }

        // GET: ResourceCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ResourceCategorys == null)
            {
                return NotFound();
            }

            var resourceCategory = await _context.ResourceCategorys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resourceCategory == null)
            {
                return NotFound();
            }

            return View(resourceCategory);
        }

        // GET: ResourceCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ResourceCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] ResourceCategory resourceCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resourceCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(resourceCategory);
        }

        // GET: ResourceCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ResourceCategorys == null)
            {
                return NotFound();
            }

            var resourceCategory = await _context.ResourceCategorys.FindAsync(id);
            if (resourceCategory == null)
            {
                return NotFound();
            }
            return View(resourceCategory);
        }

        // POST: ResourceCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] ResourceCategory resourceCategory)
        {
            if (id != resourceCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resourceCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResourceCategoryExists(resourceCategory.Id))
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
            return View(resourceCategory);
        }

        // GET: ResourceCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ResourceCategorys == null)
            {
                return NotFound();
            }

            var resourceCategory = await _context.ResourceCategorys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resourceCategory == null)
            {
                return NotFound();
            }

            return View(resourceCategory);
        }

        // POST: ResourceCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ResourceCategorys == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ResourceCategorys'  is null.");
            }
            var resourceCategory = await _context.ResourceCategorys.FindAsync(id);
            if (resourceCategory != null)
            {
                _context.ResourceCategorys.Remove(resourceCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResourceCategoryExists(int id)
        {
          return _context.ResourceCategorys.Any(e => e.Id == id);
        }
    }
}
