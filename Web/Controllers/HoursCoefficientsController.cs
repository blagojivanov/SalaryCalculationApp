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
    public class HoursCoefficientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HoursCoefficientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HoursCoefficients
        public async Task<IActionResult> Index()
        {
            return View(await _context.HoursCoefficients.ToListAsync());
        }

        // GET: HoursCoefficients/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoursCoefficient = await _context.HoursCoefficients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hoursCoefficient == null)
            {
                return NotFound();
            }

            return View(hoursCoefficient);
        }

        // GET: HoursCoefficients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HoursCoefficients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Start,End,OvertimeCoefficient,NightCoefficient,Id")] HoursCoefficient hoursCoefficient)
        {
            if (ModelState.IsValid)
            {
                hoursCoefficient.Id = Guid.NewGuid();
                _context.Add(hoursCoefficient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hoursCoefficient);
        }

        // GET: HoursCoefficients/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoursCoefficient = await _context.HoursCoefficients.FindAsync(id);
            if (hoursCoefficient == null)
            {
                return NotFound();
            }
            return View(hoursCoefficient);
        }

        // POST: HoursCoefficients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Start,End,OvertimeCoefficient,NightCoefficient,Id")] HoursCoefficient hoursCoefficient)
        {
            if (id != hoursCoefficient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoursCoefficient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoursCoefficientExists(hoursCoefficient.Id))
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
            return View(hoursCoefficient);
        }

        // GET: HoursCoefficients/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoursCoefficient = await _context.HoursCoefficients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hoursCoefficient == null)
            {
                return NotFound();
            }

            return View(hoursCoefficient);
        }

        // POST: HoursCoefficients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var hoursCoefficient = await _context.HoursCoefficients.FindAsync(id);
            if (hoursCoefficient != null)
            {
                _context.HoursCoefficients.Remove(hoursCoefficient);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoursCoefficientExists(Guid id)
        {
            return _context.HoursCoefficients.Any(e => e.Id == id);
        }
    }
}
