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
    public class Permission_RolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Permission_RolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Permission_Roles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PermissionRoles.Include(p => p.Permission);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Permission_Roles/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permission_Roles = await _context.PermissionRoles
                .Include(p => p.Permission)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (permission_Roles == null)
            {
                return NotFound();
            }

            return View(permission_Roles);
        }

        // GET: Permission_Roles/Create
        public IActionResult Create()
        {
            ViewData["PermissionId"] = new SelectList(_context.Permissions, "Id", "Id");
            return View();
        }

        // POST: Permission_Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PermissionId,Id")] Permission_Roles permission_Roles)
        {
            if (ModelState.IsValid)
            {
                permission_Roles.Id = Guid.NewGuid();
                _context.Add(permission_Roles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PermissionId"] = new SelectList(_context.Permissions, "Id", "Id", permission_Roles.PermissionId);
            return View(permission_Roles);
        }

        // GET: Permission_Roles/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permission_Roles = await _context.PermissionRoles.FindAsync(id);
            if (permission_Roles == null)
            {
                return NotFound();
            }
            ViewData["PermissionId"] = new SelectList(_context.Permissions, "Id", "Id", permission_Roles.PermissionId);
            return View(permission_Roles);
        }

        // POST: Permission_Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PermissionId,Id")] Permission_Roles permission_Roles)
        {
            if (id != permission_Roles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(permission_Roles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Permission_RolesExists(permission_Roles.Id))
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
            ViewData["PermissionId"] = new SelectList(_context.Permissions, "Id", "Id", permission_Roles.PermissionId);
            return View(permission_Roles);
        }

        // GET: Permission_Roles/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permission_Roles = await _context.PermissionRoles
                .Include(p => p.Permission)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (permission_Roles == null)
            {
                return NotFound();
            }

            return View(permission_Roles);
        }

        // POST: Permission_Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var permission_Roles = await _context.PermissionRoles.FindAsync(id);
            if (permission_Roles != null)
            {
                _context.PermissionRoles.Remove(permission_Roles);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Permission_RolesExists(Guid id)
        {
            return _context.PermissionRoles.Any(e => e.Id == id);
        }
    }
}
