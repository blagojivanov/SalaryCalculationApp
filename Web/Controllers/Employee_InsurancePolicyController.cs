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
    public class Employee_InsurancePolicyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Employee_InsurancePolicyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employee_InsurancePolicy
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EmployeePolicies.Include(e => e.Insurance);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Employee_InsurancePolicy/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee_InsurancePolicy = await _context.EmployeePolicies
                .Include(e => e.Insurance)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee_InsurancePolicy == null)
            {
                return NotFound();
            }

            return View(employee_InsurancePolicy);
        }

        // GET: Employee_InsurancePolicy/Create
        public IActionResult Create()
        {
            ViewData["InsurancePolicyId"] = new SelectList(_context.InsurancePolicies, "Id", "Id");
            return View();
        }

        // POST: Employee_InsurancePolicy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InsurancePolicyId,StartDate,EndDate,Id")] Employee_InsurancePolicy employee_InsurancePolicy)
        {
            if (ModelState.IsValid)
            {
                employee_InsurancePolicy.Id = Guid.NewGuid();
                _context.Add(employee_InsurancePolicy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InsurancePolicyId"] = new SelectList(_context.InsurancePolicies, "Id", "Id", employee_InsurancePolicy.InsurancePolicyId);
            return View(employee_InsurancePolicy);
        }

        // GET: Employee_InsurancePolicy/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee_InsurancePolicy = await _context.EmployeePolicies.FindAsync(id);
            if (employee_InsurancePolicy == null)
            {
                return NotFound();
            }
            ViewData["InsurancePolicyId"] = new SelectList(_context.InsurancePolicies, "Id", "Id", employee_InsurancePolicy.InsurancePolicyId);
            return View(employee_InsurancePolicy);
        }

        // POST: Employee_InsurancePolicy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("InsurancePolicyId,StartDate,EndDate,Id")] Employee_InsurancePolicy employee_InsurancePolicy)
        {
            if (id != employee_InsurancePolicy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee_InsurancePolicy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Employee_InsurancePolicyExists(employee_InsurancePolicy.Id))
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
            ViewData["InsurancePolicyId"] = new SelectList(_context.InsurancePolicies, "Id", "Id", employee_InsurancePolicy.InsurancePolicyId);
            return View(employee_InsurancePolicy);
        }

        // GET: Employee_InsurancePolicy/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee_InsurancePolicy = await _context.EmployeePolicies
                .Include(e => e.Insurance)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee_InsurancePolicy == null)
            {
                return NotFound();
            }

            return View(employee_InsurancePolicy);
        }

        // POST: Employee_InsurancePolicy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var employee_InsurancePolicy = await _context.EmployeePolicies.FindAsync(id);
            if (employee_InsurancePolicy != null)
            {
                _context.EmployeePolicies.Remove(employee_InsurancePolicy);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Employee_InsurancePolicyExists(Guid id)
        {
            return _context.EmployeePolicies.Any(e => e.Id == id);
        }
    }
}
