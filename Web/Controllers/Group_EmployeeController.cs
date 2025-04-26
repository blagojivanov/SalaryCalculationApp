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
    public class Group_EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Group_EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Group_Employee
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EmployeesInGroups.Include(g => g.Employee).Include(g => g.Group);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Group_Employee/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var group_Employee = await _context.EmployeesInGroups
                .Include(g => g.Employee)
                .Include(g => g.Group)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (group_Employee == null)
            {
                return NotFound();
            }

            return View(group_Employee);
        }

        // GET: Group_Employee/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id");
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id");
            return View();
        }

        // POST: Group_Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GroupId,EmployeeId,StartDate,EndDate,Id")] Group_Employee group_Employee)
        {
            if (ModelState.IsValid)
            {
                group_Employee.Id = Guid.NewGuid();
                _context.Add(group_Employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", group_Employee.EmployeeId);
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", group_Employee.GroupId);
            return View(group_Employee);
        }

        // GET: Group_Employee/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var group_Employee = await _context.EmployeesInGroups.FindAsync(id);
            if (group_Employee == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", group_Employee.EmployeeId);
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", group_Employee.GroupId);
            return View(group_Employee);
        }

        // POST: Group_Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("GroupId,EmployeeId,StartDate,EndDate,Id")] Group_Employee group_Employee)
        {
            if (id != group_Employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(group_Employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Group_EmployeeExists(group_Employee.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Id", group_Employee.EmployeeId);
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", group_Employee.GroupId);
            return View(group_Employee);
        }

        // GET: Group_Employee/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var group_Employee = await _context.EmployeesInGroups
                .Include(g => g.Employee)
                .Include(g => g.Group)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (group_Employee == null)
            {
                return NotFound();
            }

            return View(group_Employee);
        }

        // POST: Group_Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var group_Employee = await _context.EmployeesInGroups.FindAsync(id);
            if (group_Employee != null)
            {
                _context.EmployeesInGroups.Remove(group_Employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Group_EmployeeExists(Guid id)
        {
            return _context.EmployeesInGroups.Any(e => e.Id == id);
        }
    }
}
