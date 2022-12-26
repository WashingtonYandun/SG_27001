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
    public class ResidualRisksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResidualRisksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ResidualRisks
        public async Task<IActionResult> Index()
        {
              return View(await _context.ResidualRisks.ToListAsync());
        }

        // GET: ResidualRisks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ResidualRisks == null)
            {
                return NotFound();
            }

            var residualRisk = await _context.ResidualRisks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (residualRisk == null)
            {
                return NotFound();
            }

            return View(residualRisk);
        }

        // GET: ResidualRisks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ResidualRisks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Danger,Vulnerability,Type,Origin,Dimension,Priority,CID,DangerLevel,VulnerabilityLevel,Level,LevelRange")] ResidualRisk residualRisk)
        {
            if (ModelState.IsValid)
            {
                _context.Add(residualRisk);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(residualRisk);
        }

        // GET: ResidualRisks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ResidualRisks == null)
            {
                return NotFound();
            }

            var residualRisk = await _context.ResidualRisks.FindAsync(id);
            if (residualRisk == null)
            {
                return NotFound();
            }
            return View(residualRisk);
        }

        // POST: ResidualRisks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Danger,Vulnerability,Type,Origin,Dimension,Priority,CID,DangerLevel,VulnerabilityLevel,Level,LevelRange")] ResidualRisk residualRisk)
        {
            if (id != residualRisk.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(residualRisk);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResidualRiskExists(residualRisk.Id))
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
            return View(residualRisk);
        }

        // GET: ResidualRisks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ResidualRisks == null)
            {
                return NotFound();
            }

            var residualRisk = await _context.ResidualRisks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (residualRisk == null)
            {
                return NotFound();
            }

            return View(residualRisk);
        }

        // POST: ResidualRisks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ResidualRisks == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ResidualRisks'  is null.");
            }
            var residualRisk = await _context.ResidualRisks.FindAsync(id);
            if (residualRisk != null)
            {
                _context.ResidualRisks.Remove(residualRisk);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResidualRiskExists(int id)
        {
          return _context.ResidualRisks.Any(e => e.Id == id);
        }
    }
}
