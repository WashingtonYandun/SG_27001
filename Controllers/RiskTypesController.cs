﻿using System;
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
    public class RiskTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RiskTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RiskTypes
        public async Task<IActionResult> Index()
        {
              return View(await _context.RiskTypes.ToListAsync());
        }

        // GET: RiskTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RiskTypes == null)
            {
                return NotFound();
            }

            var riskType = await _context.RiskTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (riskType == null)
            {
                return NotFound();
            }

            return View(riskType);
        }

        // GET: RiskTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RiskTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] RiskType riskType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(riskType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(riskType);
        }

        // GET: RiskTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RiskTypes == null)
            {
                return NotFound();
            }

            var riskType = await _context.RiskTypes.FindAsync(id);
            if (riskType == null)
            {
                return NotFound();
            }
            return View(riskType);
        }

        // POST: RiskTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] RiskType riskType)
        {
            if (id != riskType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(riskType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RiskTypeExists(riskType.Id))
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
            return View(riskType);
        }

        // GET: RiskTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RiskTypes == null)
            {
                return NotFound();
            }

            var riskType = await _context.RiskTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (riskType == null)
            {
                return NotFound();
            }

            return View(riskType);
        }

        // POST: RiskTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RiskTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.RiskTypes'  is null.");
            }
            var riskType = await _context.RiskTypes.FindAsync(id);
            if (riskType != null)
            {
                _context.RiskTypes.Remove(riskType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RiskTypeExists(int id)
        {
          return _context.RiskTypes.Any(e => e.Id == id);
        }
    }
}
