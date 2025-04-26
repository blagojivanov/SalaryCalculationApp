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
    public class PensionInsuranceTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PensionInsuranceTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PensionInsuranceTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PensionInsuranceTypes.ToListAsync());
        }

        // GET: PensionInsuranceTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pensionInsuranceType = await _context.PensionInsuranceTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pensionInsuranceType == null)
            {
                return NotFound();
            }

            return View(pensionInsuranceType);
        }

        // GET: PensionInsuranceTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PensionInsuranceTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,PercentOff,Id")] PensionInsuranceType pensionInsuranceType)
        {
            if (ModelState.IsValid)
            {
                pensionInsuranceType.Id = Guid.NewGuid();
                _context.Add(pensionInsuranceType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pensionInsuranceType);
        }

        // GET: PensionInsuranceTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pensionInsuranceType = await _context.PensionInsuranceTypes.FindAsync(id);
            if (pensionInsuranceType == null)
            {
                return NotFound();
            }
            return View(pensionInsuranceType);
        }

        // POST: PensionInsuranceTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,PercentOff,Id")] PensionInsuranceType pensionInsuranceType)
        {
            if (id != pensionInsuranceType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pensionInsuranceType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PensionInsuranceTypeExists(pensionInsuranceType.Id))
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
            return View(pensionInsuranceType);
        }

        // GET: PensionInsuranceTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pensionInsuranceType = await _context.PensionInsuranceTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pensionInsuranceType == null)
            {
                return NotFound();
            }

            return View(pensionInsuranceType);
        }

        // POST: PensionInsuranceTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pensionInsuranceType = await _context.PensionInsuranceTypes.FindAsync(id);
            if (pensionInsuranceType != null)
            {
                _context.PensionInsuranceTypes.Remove(pensionInsuranceType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PensionInsuranceTypeExists(Guid id)
        {
            return _context.PensionInsuranceTypes.Any(e => e.Id == id);
        }
    }
}
