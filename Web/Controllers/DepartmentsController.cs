using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using Domain.DataTransferObjects;
using Web.Data;

namespace Web.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Departments.ToListAsync());
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.Include(d => d.Employees)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            var departmentPositions = _context.PositionsInDepartments.Where(d => d.DepartmentId == id)
                .Include(d => d.Position).ToList();

            var departmentPositionDTOs = departmentPositions.Select(deptPos => new DepartmentPositionDTO
                { FreeSlots = deptPos.FreeSpaces, PositionName = deptPos.Position?.Name, }).ToList();

            var departmentDetailsDTO = new DepartmentDetailsDTO
            {
                Department = department,
                Employees = department.Employees,
                Positions = departmentPositionDTOs
            };

            return View(departmentDetailsDTO);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id")] Department department)
        {
            if (ModelState.IsValid)
            {
                department.Id = Guid.NewGuid();
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Id")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.Id))
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

            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddPositionToDepartment(Guid id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            var dto = new DepartmentPositionDTO
            {
                DepartmentId = id,
                Positions = await _context.Positions.ToListAsync()
            };


            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Name");
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> AddPositionToDepartment(Guid id, DepartmentPositionDTO dto)
        {
            var department = await _context.Departments.FindAsync(id);
            var position = await _context.Positions.FindAsync(dto.PositionId);
            if (department == null || position == null)
                return NotFound();

            Department_Position dp = new Department_Position()
            {
                DepartmentId = department.Id,
                PositionId = position.Id,
                Position = position,
                Department = department,
                PositionCount = dto.PositionCount,
                FreeSpaces = dto.PositionCount
            };
            _context.PositionsInDepartments.Add(dp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(Guid id)
        {
            return _context.Departments.Any(e => e.Id == id);
        }
    }
}