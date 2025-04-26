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
    public class Employee_PositionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Employee_PositionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employee_Position
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Employee_Positions.Include(e => e.Employee).Include(e => e.Position);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Employee_Position/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee_Position = await _context.Employee_Positions
                .Include(e => e.Employee)
                .Include(e => e.Position)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee_Position == null)
            {
                return NotFound();
            }

            return View(employee_Position);
        }

        // GET: Employee_Position/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id");
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Id");
            return View();
        }

        // POST: Employee_Position/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,PositionId,Start_Date,End_Date,Id")] Employee_Position employee_Position)
        {
            if (ModelState.IsValid)
            {
                employee_Position.Id = Guid.NewGuid();
                _context.Add(employee_Position);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", employee_Position.EmployeeId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Id", employee_Position.PositionId);
            return View(employee_Position);
        }

        // GET: Employee_Position/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee_Position = await _context.Employee_Positions.FindAsync(id);
            if (employee_Position == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", employee_Position.EmployeeId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Id", employee_Position.PositionId);
            return View(employee_Position);
        }

        // POST: Employee_Position/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("EmployeeId,PositionId,Start_Date,End_Date,Id")] Employee_Position employee_Position)
        {
            if (id != employee_Position.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee_Position);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Employee_PositionExists(employee_Position.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", employee_Position.EmployeeId);
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Id", employee_Position.PositionId);
            return View(employee_Position);
        }

        // GET: Employee_Position/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee_Position = await _context.Employee_Positions
                .Include(e => e.Employee)
                .Include(e => e.Position)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee_Position == null)
            {
                return NotFound();
            }

            return View(employee_Position);
        }

        // POST: Employee_Position/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var employee_Position = await _context.Employee_Positions.FindAsync(id);
            if (employee_Position != null)
            {
                _context.Employee_Positions.Remove(employee_Position);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Employee_PositionExists(Guid id)
        {
            return _context.Employee_Positions.Any(e => e.Id == id);
        }
    }
}
