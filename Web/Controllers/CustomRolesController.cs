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
    public class CustomRolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomRolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CustomRoles
        public async Task<IActionResult> Index()
        {
            return View(await _context.CustomRole.ToListAsync());
        }

        // GET: CustomRoles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customRole = await _context.CustomRole
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customRole == null)
            {
                return NotFound();
            }

            return View(customRole);
        }

        // GET: CustomRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NormalizedName,ConcurrencyStamp")] CustomRole customRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customRole);
        }

        // GET: CustomRoles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customRole = await _context.CustomRole.FindAsync(id);
            if (customRole == null)
            {
                return NotFound();
            }
            return View(customRole);
        }

        // POST: CustomRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,NormalizedName,ConcurrencyStamp")] CustomRole customRole)
        {
            if (id != customRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomRoleExists(customRole.Id))
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
            return View(customRole);
        }

        // GET: CustomRoles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customRole = await _context.CustomRole
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customRole == null)
            {
                return NotFound();
            }

            return View(customRole);
        }

        // POST: CustomRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var customRole = await _context.CustomRole.FindAsync(id);
            if (customRole != null)
            {
                _context.CustomRole.Remove(customRole);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomRoleExists(string id)
        {
            return _context.CustomRole.Any(e => e.Id == id);
        }
    }
}
