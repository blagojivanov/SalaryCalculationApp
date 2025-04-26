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
    public class InsuranceItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InsuranceItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InsuranceItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.InsuranceItems.ToListAsync());
        }

        // GET: InsuranceItems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceItem = await _context.InsuranceItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insuranceItem == null)
            {
                return NotFound();
            }

            return View(insuranceItem);
        }

        // GET: InsuranceItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InsuranceItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Price,Description,Id")] InsuranceItem insuranceItem)
        {
            if (ModelState.IsValid)
            {
                insuranceItem.Id = Guid.NewGuid();
                _context.Add(insuranceItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insuranceItem);
        }

        // GET: InsuranceItems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceItem = await _context.InsuranceItems.FindAsync(id);
            if (insuranceItem == null)
            {
                return NotFound();
            }
            return View(insuranceItem);
        }

        // POST: InsuranceItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Price,Description,Id")] InsuranceItem insuranceItem)
        {
            if (id != insuranceItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insuranceItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceItemExists(insuranceItem.Id))
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
            return View(insuranceItem);
        }

        // GET: InsuranceItems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceItem = await _context.InsuranceItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insuranceItem == null)
            {
                return NotFound();
            }

            return View(insuranceItem);
        }

        // POST: InsuranceItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var insuranceItem = await _context.InsuranceItems.FindAsync(id);
            if (insuranceItem != null)
            {
                _context.InsuranceItems.Remove(insuranceItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceItemExists(Guid id)
        {
            return _context.InsuranceItems.Any(e => e.Id == id);
        }
    }
}
