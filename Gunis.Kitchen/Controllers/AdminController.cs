using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gunis.Kitchen.Data;
using Gunis.Kitchen.Models;
using Microsoft.AspNetCore.Authorization;

namespace Gunis.Kitchen.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            return View(await _context.Roles.ToListAsync());
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myIdentityRole = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (myIdentityRole == null)
            {
                return NotFound();
            }

            return View(myIdentityRole);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleId,RoleName,Id,Name,NormalizedName,ConcurrencyStamp")] MyIdentityRole myIdentityRole)
        {
            if (ModelState.IsValid)
            {
                myIdentityRole.Id = Guid.NewGuid();
                _context.Add(myIdentityRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(myIdentityRole);
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myIdentityRole = await _context.Roles.FindAsync(id);
            if (myIdentityRole == null)
            {
                return NotFound();
            }
            return View(myIdentityRole);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RoleId,RoleName,Id,Name,NormalizedName,ConcurrencyStamp")] MyIdentityRole myIdentityRole)
        {
            if (id != myIdentityRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(myIdentityRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MyIdentityRoleExists(myIdentityRole.Id))
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
            return View(myIdentityRole);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myIdentityRole = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (myIdentityRole == null)
            {
                return NotFound();
            }

            return View(myIdentityRole);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var myIdentityRole = await _context.Roles.FindAsync(id);
            _context.Roles.Remove(myIdentityRole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MyIdentityRoleExists(Guid id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }
    }
}
