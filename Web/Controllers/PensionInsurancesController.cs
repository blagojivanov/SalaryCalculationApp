using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using Web.Data;

namespace Web.Controllers
{
    public class PensionInsurancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PensionInsurancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PensionInsurances
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PensionInsurances.Include(p => p.Type);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PensionInsurances/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pensionInsurance = await _context.PensionInsurances
                .Include(p => p.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pensionInsurance == null)
            {
                return NotFound();
            }

            return View(pensionInsurance);
        }

        // GET: PensionInsurances/Create
        public IActionResult Create()
        {
            ViewData["TypeId"] = new SelectList(_context.PensionInsuranceTypes, "Id", "Id");
            return View();
        }

        // POST: PensionInsurances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeId,Id")] PensionInsurance pensionInsurance)
        {
            if (ModelState.IsValid)
            {
                pensionInsurance.Id = Guid.NewGuid();
                _context.Add(pensionInsurance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeId"] = new SelectList(_context.PensionInsuranceTypes, "Id", "Id", pensionInsurance.TypeId);
            return View(pensionInsurance);
        }

        // GET: PensionInsurances/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pensionInsurance = await _context.PensionInsurances.FindAsync(id);
            if (pensionInsurance == null)
            {
                return NotFound();
            }
            ViewData["TypeId"] = new SelectList(_context.PensionInsuranceTypes, "Id", "Id", pensionInsurance.TypeId);
            return View(pensionInsurance);
        }

        // POST: PensionInsurances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TypeId,Id")] PensionInsurance pensionInsurance)
        {
            if (id != pensionInsurance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pensionInsurance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PensionInsuranceExists(pensionInsurance.Id))
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
            ViewData["TypeId"] = new SelectList(_context.PensionInsuranceTypes, "Id", "Id", pensionInsurance.TypeId);
            return View(pensionInsurance);
        }

        // GET: PensionInsurances/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pensionInsurance = await _context.PensionInsurances
                .Include(p => p.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pensionInsurance == null)
            {
                return NotFound();
            }

            return View(pensionInsurance);
        }

        // POST: PensionInsurances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pensionInsurance = await _context.PensionInsurances.FindAsync(id);
            if (pensionInsurance != null)
            {
                _context.PensionInsurances.Remove(pensionInsurance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PensionInsuranceExists(Guid id)
        {
            return _context.PensionInsurances.Any(e => e.Id == id);
        }
    }
}
