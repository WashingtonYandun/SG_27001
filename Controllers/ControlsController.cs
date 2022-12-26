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
    public class ControlsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ControlsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Controls
        public async Task<IActionResult> Index()
        {
              return View(await _context.Controls.ToListAsync());
        }

        // GET: Controls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Controls == null)
            {
                return NotFound();
            }

            var control = await _context.Controls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (control == null)
            {
                return NotFound();
            }

            return View(control);
        }

        // GET: Controls/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Controls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImplementationTime,Description,isEffective")] Control control)
        {
            if (ModelState.IsValid)
            {
                _context.Add(control);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(control);
        }

        // GET: Controls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Controls == null)
            {
                return NotFound();
            }

            var control = await _context.Controls.FindAsync(id);
            if (control == null)
            {
                return NotFound();
            }
            return View(control);
        }

        // POST: Controls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImplementationTime,Description,isEffective")] Control control)
        {
            if (id != control.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(control);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ControlExists(control.Id))
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
            return View(control);
        }

        // GET: Controls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Controls == null)
            {
                return NotFound();
            }

            var control = await _context.Controls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (control == null)
            {
                return NotFound();
            }

            return View(control);
        }

        // POST: Controls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Controls == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Controls'  is null.");
            }
            var control = await _context.Controls.FindAsync(id);
            if (control != null)
            {
                _context.Controls.Remove(control);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ControlExists(int id)
        {
          return _context.Controls.Any(e => e.Id == id);
        }
    }
}
