using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RDAT.Data;
using RDAT.Models;

namespace RDAT.Controllers
{
    public class TestingController : Controller
    {
        private readonly RDATContext _context;

        public TestingController(RDATContext context)
        {
            _context = context;
        }

        // GET: Testing
        public async Task<IActionResult> Index()
        {
            return View(await _context.TestingLogs.ToListAsync());
        }

        // GET: Testing/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testingLog = await _context.TestingLogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testingLog == null)
            {
                return NotFound();
            }

            return View(testingLog);
        }

        // GET: Testing/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Testing/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Batch_Id,Selectiondatedrug,Selectiondatealcohol,Reported_Results,ResultsDate,SSN,Specimen_Id,ClosedDate,Test_Process_Id,Driver_Id,Test_Type,Drug_Percentage,Alcohol_Percentage,Created,Modified,Status,isDelete")] TestingLog testingLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testingLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(testingLog);
        }

        // GET: Testing/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testingLog = await _context.TestingLogs.FindAsync(id);
            if (testingLog == null)
            {
                return NotFound();
            }
            return View(testingLog);
        }

        // POST: Testing/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Batch_Id,Selectiondatedrug,Selectiondatealcohol,Reported_Results,ResultsDate,SSN,Specimen_Id,ClosedDate,Test_Process_Id,Driver_Id,Test_Type,Drug_Percentage,Alcohol_Percentage,Created,Modified,Status,isDelete")] TestingLog testingLog)
        {
            if (id != testingLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testingLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestingLogExists(testingLog.Id))
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
            return View(testingLog);
        }

        // GET: Testing/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testingLog = await _context.TestingLogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testingLog == null)
            {
                return NotFound();
            }

            return View(testingLog);
        }

        // POST: Testing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testingLog = await _context.TestingLogs.FindAsync(id);
            _context.TestingLogs.Remove(testingLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestingLogExists(int id)
        {
            return _context.TestingLogs.Any(e => e.Id == id);
        }
    }
}
