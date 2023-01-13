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
    public class RisksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RisksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Risks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Risks.Include(r => r.ResidualRisk).Include(r => r.Resource).Include(r => r.RiskType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Risks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Risks == null)
            {
                return NotFound();
            }

            var risk = await _context.Risks
                .Include(r => r.ResidualRisk)
                .Include(r => r.Resource)
                .Include(r => r.RiskType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (risk == null)
            {
                return NotFound();
            }

            return View(risk);
        }

        // GET: Risks/Create
        public IActionResult Create()
        {
            ViewData["ResidualRiskId"] = new SelectList(_context.ResidualRisks, "Id", "Name");
            ViewData["ResourceId"] = new SelectList(_context.Resources, "Id", "Name");
            ViewData["RiskTypeId"] = new SelectList(_context.RiskTypes, "Id", "Name");
            return View();
        }

        // POST: Risks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Danger,Vulnerability,Description,Type,Origin,Priority,CID,DangerLevel,VulnerabilityLevel,ResourceId,RiskTypeId")] Risk risk)
        {
            var r = risk.ResourceId;
            risk.Level = risk.CID * risk.VulnerabilityLevel * risk.DangerLevel;
            risk.Code = _context.Resources.Find(r)?.Name?.Substring(0, 1) + risk.ToString();
            if (risk.Level >= 1 && risk.Level <= 3)
            {
                risk.LevelRange = "LOW";
            }

            if (risk.Level >= 4 && risk.Level <= 8)
            {
                risk.LevelRange = "MEDIUM";
            }

            if (risk.Level >= 9)
            {
                risk.LevelRange = "HIGH";
                risk.Priority = true;
            }
            risk.ResidualRiskId = 1;

            if (ModelState.IsValid)
            {
                _context.Add(risk);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["ResidualRiskId"] = new SelectList(_context.ResidualRisks, "Id", "Name", risk.ResidualRiskId);
            ViewData["ResourceId"] = new SelectList(_context.Resources, "Id", "Name", risk.ResourceId);
            ViewData["RiskTypeId"] = new SelectList(_context.RiskTypes, "Id", "Name", risk.RiskTypeId);
            return View(risk);
        }

        // GET: Risks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Risks == null)
            {
                return NotFound();
            }

            var risk = await _context.Risks.FindAsync(id);
            if (risk == null)
            {
                return NotFound();
            }
            ViewData["ResidualRiskId"] = new SelectList(_context.ResidualRisks, "Id", "Name", risk.ResidualRiskId);
            ViewData["ResourceId"] = new SelectList(_context.Resources, "Id", "Name", risk.ResourceId);
            ViewData["RiskTypeId"] = new SelectList(_context.RiskTypes, "Id", "Name", risk.RiskTypeId);
            return View(risk);
        }

        // POST: Risks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Code,Danger,Vulnerability,Description,Origin,Priority,CID,DangerLevel,VulnerabilityLevel,Level,LevelRange,ResourceId,RiskTypeId,ResidualRiskId")] Risk risk)
        {
            if (id != risk.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(risk);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RiskExists(risk.Id))
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
            ViewData["ResidualRiskId"] = new SelectList(_context.ResidualRisks, "Id", "Name", risk.ResidualRiskId);
            ViewData["ResourceId"] = new SelectList(_context.Resources, "Id", "Name", risk.ResourceId);
            ViewData["RiskTypeId"] = new SelectList(_context.RiskTypes, "Id", "Name", risk.RiskTypeId);
            return View(risk);
        }

        // GET: Risks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Risks == null)
            {
                return NotFound();
            }

            var risk = await _context.Risks
                .Include(r => r.ResidualRisk)
                .Include(r => r.Resource)
                .Include(r => r.RiskType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (risk == null)
            {
                return NotFound();
            }

            return View(risk);
        }

        // POST: Risks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Risks == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Risks'  is null.");
            }
            var risk = await _context.Risks.FindAsync(id);
            if (risk != null)
            {
                _context.Risks.Remove(risk);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RiskExists(int id)
        {
            return _context.Risks.Any(e => e.Id == id);
        }
    }
}
