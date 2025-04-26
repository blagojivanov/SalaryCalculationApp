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
    public class Employee_PensionInsuranceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Employee_PensionInsuranceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employee_PensionInsurance
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EmployeeInsurances.Include(e => e.PensionInsurance);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Employee_PensionInsurance/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee_PensionInsurance = await _context.EmployeeInsurances
                .Include(e => e.PensionInsurance)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee_PensionInsurance == null)
            {
                return NotFound();
            }

            return View(employee_PensionInsurance);
        }

        // GET: Employee_PensionInsurance/Create
        public IActionResult Create()
        {
            ViewData["PensionInsuranceId"] = new SelectList(_context.PensionInsurances, "Id", "Id");
            return View();
        }

        // POST: Employee_PensionInsurance/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PensionInsuranceId,Id")] Employee_PensionInsurance employee_PensionInsurance)
        {
            if (ModelState.IsValid)
            {
                employee_PensionInsurance.Id = Guid.NewGuid();
                _context.Add(employee_PensionInsurance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PensionInsuranceId"] = new SelectList(_context.PensionInsurances, "Id", "Id", employee_PensionInsurance.PensionInsuranceId);
            return View(employee_PensionInsurance);
        }

        // GET: Employee_PensionInsurance/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee_PensionInsurance = await _context.EmployeeInsurances.FindAsync(id);
            if (employee_PensionInsurance == null)
            {
                return NotFound();
            }
            ViewData["PensionInsuranceId"] = new SelectList(_context.PensionInsurances, "Id", "Id", employee_PensionInsurance.PensionInsuranceId);
            return View(employee_PensionInsurance);
        }

        // POST: Employee_PensionInsurance/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PensionInsuranceId,Id")] Employee_PensionInsurance employee_PensionInsurance)
        {
            if (id != employee_PensionInsurance.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee_PensionInsurance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Employee_PensionInsuranceExists(employee_PensionInsurance.Id))
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
            ViewData["PensionInsuranceId"] = new SelectList(_context.PensionInsurances, "Id", "Id", employee_PensionInsurance.PensionInsuranceId);
            return View(employee_PensionInsurance);
        }

        // GET: Employee_PensionInsurance/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee_PensionInsurance = await _context.EmployeeInsurances
                .Include(e => e.PensionInsurance)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee_PensionInsurance == null)
            {
                return NotFound();
            }

            return View(employee_PensionInsurance);
        }

        // POST: Employee_PensionInsurance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var employee_PensionInsurance = await _context.EmployeeInsurances.FindAsync(id);
            if (employee_PensionInsurance != null)
            {
                _context.EmployeeInsurances.Remove(employee_PensionInsurance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Employee_PensionInsuranceExists(Guid id)
        {
            return _context.EmployeeInsurances.Any(e => e.Id == id);
        }
    }
}
