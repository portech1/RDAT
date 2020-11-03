using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RDAT.Data;
using RDAT.Models;

namespace RDAT.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CreateBatchesController : Controller
    {
        private readonly RDATContext _context;

        public CreateBatchesController(RDATContext context)
        {
            _context = context;
        }

        // GET: CreateBatches
        public async Task<IActionResult> Index()
        {
            return View(await _context.CreateBatch.ToListAsync());
        }

        // GET: CreateBatches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var createBatch = await _context.CreateBatch
                .FirstOrDefaultAsync(m => m.id == id);
            if (createBatch == null)
            {
                return NotFound();
            }

            return View(createBatch);
        }

        // GET: CreateBatches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CreateBatches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,ActivePool,DrugPercentage,DrugTestDate,AlcoholPercentage,AlcoholTestDate")] CreateBatch createBatch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(createBatch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(createBatch);
        }

        // GET: CreateBatches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var createBatch = await _context.CreateBatch.FindAsync(id);
            if (createBatch == null)
            {
                return NotFound();
            }
            return View(createBatch);
        }

        // POST: CreateBatches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,ActivePool,DrugPercentage,DrugTestDate,AlcoholPercentage,AlcoholTestDate")] CreateBatch createBatch)
        {
            if (id != createBatch.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(createBatch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CreateBatchExists(createBatch.id))
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
            return View(createBatch);
        }

        // GET: CreateBatches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var createBatch = await _context.CreateBatch
                .FirstOrDefaultAsync(m => m.id == id);
            if (createBatch == null)
            {
                return NotFound();
            }

            return View(createBatch);
        }

        // POST: CreateBatches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var createBatch = await _context.CreateBatch.FindAsync(id);
            _context.CreateBatch.Remove(createBatch);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CreateBatchExists(int id)
        {
            return _context.CreateBatch.Any(e => e.id == id);
        }
    }
}
