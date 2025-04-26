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
    public class PointPricesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PointPricesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PointPrices
        public async Task<IActionResult> Index()
        {
            return View(await _context.PointPrices.ToListAsync());
        }

        // GET: PointPrices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointPrice = await _context.PointPrices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pointPrice == null)
            {
                return NotFound();
            }

            return View(pointPrice);
        }

        // GET: PointPrices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PointPrices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Start,End,Price,Id")] PointPrice pointPrice)
        {
            if (ModelState.IsValid)
            {
                pointPrice.Id = Guid.NewGuid();
                _context.Add(pointPrice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pointPrice);
        }

        // GET: PointPrices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointPrice = await _context.PointPrices.FindAsync(id);
            if (pointPrice == null)
            {
                return NotFound();
            }
            return View(pointPrice);
        }

        // POST: PointPrices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Start,End,Price,Id")] PointPrice pointPrice)
        {
            if (id != pointPrice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pointPrice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PointPriceExists(pointPrice.Id))
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
            return View(pointPrice);
        }

        // GET: PointPrices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointPrice = await _context.PointPrices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pointPrice == null)
            {
                return NotFound();
            }

            return View(pointPrice);
        }

        // POST: PointPrices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var pointPrice = await _context.PointPrices.FindAsync(id);
            if (pointPrice != null)
            {
                _context.PointPrices.Remove(pointPrice);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PointPriceExists(Guid id)
        {
            return _context.PointPrices.Any(e => e.Id == id);
        }
    }
}
