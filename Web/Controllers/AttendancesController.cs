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
    public class AttendancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private Attendance CountHours(Attendance attendance)
        {
            var totalHours = (attendance.End.Value - attendance.Start.Value).TotalHours;

            var nightStart = new DateTime(
                attendance.Start.Value.Year,
                attendance.Start.Value.Month,
                attendance.Start.Value.Day,
                22, 0, 0);

            attendance.Overtime = (int)Math.Max(0, totalHours - 8);

            if (DateTime.Compare(attendance.End.Value, nightStart) > 0)
            {
                var nightHours = (int)(attendance.End.Value - nightStart).TotalHours;
                attendance.NightHours = nightHours;
            }
            else
            {
                attendance.NightHours = 0;
            }

            return attendance;
        }

        public AttendancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Attendances
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Attendances.Include(a => a.Employee).Include(a => a.HoursCoefficient);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Attendances/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .Include(a => a.Employee)
                .Include(a => a.HoursCoefficient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // GET: Attendances/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Email");
            ViewData["HoursCoefficientId"] = new SelectList(_context.HoursCoefficients, "Id", "Start");
            return View();
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("EmployeeId,HoursCoefficientId,Start,End,Id")]
            Attendance attendance)
        {
            attendance.Id = Guid.NewGuid();
            attendance = CountHours(attendance);

            _context.Add(attendance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Attendances/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }

            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Email", attendance.EmployeeId);
            ViewData["HoursCoefficientId"] =
                new SelectList(_context.HoursCoefficients, "Id", "Start", attendance.HoursCoefficientId);
            return View(attendance);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("EmployeeId,HoursCoefficientId,Start,End,Id")]
            Attendance attendance)
        {
            if (id != attendance.Id)
            {
                return NotFound();
            }

            try
            {
                attendance = CountHours(attendance);
                _context.Update(attendance);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceExists(attendance.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));

            // ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "Email", attendance.EmployeeId);
            // ViewData["HoursCoefficientId"] =
            //     new SelectList(_context.HoursCoefficients, "Id", "Start", attendance.HoursCoefficientId);
            // return View(attendance);
        }

        // GET: Attendances/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .Include(a => a.Employee)
                .Include(a => a.HoursCoefficient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance != null)
            {
                _context.Attendances.Remove(attendance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceExists(Guid id)
        {
            return _context.Attendances.Any(e => e.Id == id);
        }
    }
}