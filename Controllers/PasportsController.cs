using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication5.Controllers
{
    [Authorize]
    public class PasportsController : Controller
    {
        private readonly MedelStoreContext _context;

        public PasportsController(MedelStoreContext context)
        {
            _context = context;
        }

        // GET: Pasports (только для Staff)
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Index()
        {
            if (!User.IsInRole("Staff"))
            {
                return RedirectToAction("List");
            }
            return View(await _context.Pasports.ToListAsync());
        }

        // GET: Pasports/List (для всех авторизованных)
        public async Task<IActionResult> List()
        {
            return View("ReadOnlyList", await _context.Pasports.ToListAsync());
        }

        // GET: Pasports/Details/5 (для всех авторизованных)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pasport = await _context.Pasports
                .FirstOrDefaultAsync(m => m.IdPasports == id);
            if (pasport == null)
            {
                return NotFound();
            }

            return View(pasport);
        }

        // GET: Pasports/Create (только для Staff)
        [Authorize(Roles = "Staff")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pasports/Create (только для Staff)
        [Authorize(Roles = "Staff")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPasports,Serial,Number,DateOfIssue")] Pasport pasport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pasport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pasport);
        }

        // GET: Pasports/Edit/5 (только для Staff)
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pasport = await _context.Pasports.FindAsync(id);
            if (pasport == null)
            {
                return NotFound();
            }
            return View(pasport);
        }

        // POST: Pasports/Edit/5 (только для Staff)
        [Authorize(Roles = "Staff")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPasports,Serial,Number,DateOfIssue")] Pasport pasport)
        {
            if (id != pasport.IdPasports)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pasport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PasportExists(pasport.IdPasports))
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
            return View(pasport);
        }

        // GET: Pasports/Delete/5 (только для Staff)
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pasport = await _context.Pasports
                .FirstOrDefaultAsync(m => m.IdPasports == id);
            if (pasport == null)
            {
                return NotFound();
            }

            return View(pasport);
        }

        // POST: Pasports/Delete/5 (только для Staff)
        [Authorize(Roles = "Staff")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pasport = await _context.Pasports.FindAsync(id);
            if (pasport != null)
            {
                _context.Pasports.Remove(pasport);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PasportExists(int id)
        {
            return _context.Pasports.Any(e => e.IdPasports == id);
        }
    }
}