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
    public class InsuranceItemPoliciesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InsuranceItemPoliciesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InsuranceItemPolicies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InsuranceItemsInPolicies.Include(i => i.InsItem).Include(i => i.InsPolicy);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InsuranceItemPolicies/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceItemPolicy = await _context.InsuranceItemsInPolicies
                .Include(i => i.InsItem)
                .Include(i => i.InsPolicy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insuranceItemPolicy == null)
            {
                return NotFound();
            }

            return View(insuranceItemPolicy);
        }

        // GET: InsuranceItemPolicies/Create
        public IActionResult Create()
        {
            ViewData["InsuranceItemId"] = new SelectList(_context.InsuranceItems, "Id", "Id");
            ViewData["InsurancePolicyId"] = new SelectList(_context.InsurancePolicies, "Id", "Id");
            return View();
        }

        // POST: InsuranceItemPolicies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InsuranceItemId,InsurancePolicyId,Id")] InsuranceItemPolicy insuranceItemPolicy)
        {
            if (ModelState.IsValid)
            {
                insuranceItemPolicy.Id = Guid.NewGuid();
                _context.Add(insuranceItemPolicy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InsuranceItemId"] = new SelectList(_context.InsuranceItems, "Id", "Id", insuranceItemPolicy.InsuranceItemId);
            ViewData["InsurancePolicyId"] = new SelectList(_context.InsurancePolicies, "Id", "Id", insuranceItemPolicy.InsurancePolicyId);
            return View(insuranceItemPolicy);
        }

        // GET: InsuranceItemPolicies/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceItemPolicy = await _context.InsuranceItemsInPolicies.FindAsync(id);
            if (insuranceItemPolicy == null)
            {
                return NotFound();
            }
            ViewData["InsuranceItemId"] = new SelectList(_context.InsuranceItems, "Id", "Id", insuranceItemPolicy.InsuranceItemId);
            ViewData["InsurancePolicyId"] = new SelectList(_context.InsurancePolicies, "Id", "Id", insuranceItemPolicy.InsurancePolicyId);
            return View(insuranceItemPolicy);
        }

        // POST: InsuranceItemPolicies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("InsuranceItemId,InsurancePolicyId,Id")] InsuranceItemPolicy insuranceItemPolicy)
        {
            if (id != insuranceItemPolicy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insuranceItemPolicy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceItemPolicyExists(insuranceItemPolicy.Id))
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
            ViewData["InsuranceItemId"] = new SelectList(_context.InsuranceItems, "Id", "Id", insuranceItemPolicy.InsuranceItemId);
            ViewData["InsurancePolicyId"] = new SelectList(_context.InsurancePolicies, "Id", "Id", insuranceItemPolicy.InsurancePolicyId);
            return View(insuranceItemPolicy);
        }

        // GET: InsuranceItemPolicies/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceItemPolicy = await _context.InsuranceItemsInPolicies
                .Include(i => i.InsItem)
                .Include(i => i.InsPolicy)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insuranceItemPolicy == null)
            {
                return NotFound();
            }

            return View(insuranceItemPolicy);
        }

        // POST: InsuranceItemPolicies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var insuranceItemPolicy = await _context.InsuranceItemsInPolicies.FindAsync(id);
            if (insuranceItemPolicy != null)
            {
                _context.InsuranceItemsInPolicies.Remove(insuranceItemPolicy);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceItemPolicyExists(Guid id)
        {
            return _context.InsuranceItemsInPolicies.Any(e => e.Id == id);
        }
    }
}
