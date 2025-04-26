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
    public class Department_PositionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Department_PositionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Department_Position
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PositionsInDepartments.Include(d => d.Department);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Department_Position/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department_Position = await _context.PositionsInDepartments
                .Include(d => d.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department_Position == null)
            {
                return NotFound();
            }

            return View(department_Position);
        }

        // GET: Department_Position/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id");
            return View();
        }

        // POST: Department_Position/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartmentId,PositionCount,FreeSpaces,Id")] Department_Position department_Position)
        {
            if (ModelState.IsValid)
            {
                department_Position.Id = Guid.NewGuid();
                _context.Add(department_Position);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", department_Position.DepartmentId);
            return View(department_Position);
        }

        // GET: Department_Position/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department_Position = await _context.PositionsInDepartments.FindAsync(id);
            if (department_Position == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", department_Position.DepartmentId);
            return View(department_Position);
        }

        // POST: Department_Position/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DepartmentId,PositionCount,FreeSpaces,Id")] Department_Position department_Position)
        {
            if (id != department_Position.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department_Position);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Department_PositionExists(department_Position.Id))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Id", department_Position.DepartmentId);
            return View(department_Position);
        }

        // GET: Department_Position/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department_Position = await _context.PositionsInDepartments
                .Include(d => d.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department_Position == null)
            {
                return NotFound();
            }

            return View(department_Position);
        }

        // POST: Department_Position/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var department_Position = await _context.PositionsInDepartments.FindAsync(id);
            if (department_Position != null)
            {
                _context.PositionsInDepartments.Remove(department_Position);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Department_PositionExists(Guid id)
        {
            return _context.PositionsInDepartments.Any(e => e.Id == id);
        }
    }
}
