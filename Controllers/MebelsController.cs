using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication5.Controllers
{
    [Authorize]
    public class MebelsController : Controller
    {
        private readonly MedelStoreContext _context;

        public MebelsController(MedelStoreContext context)
        {
            _context = context;
        }

        // GET: Mebels (только для Staff)
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Index()
        {
            if (!User.IsInRole("Staff"))
            {
                return RedirectToAction("List");
            }

            var mebels = await _context.Mebels
                .Include(m => m.Categori)
                .ToListAsync();

            return View(mebels);
        }

        // GET: Mebels/List (для всех авторизованных)
        public async Task<IActionResult> List()
        {
            var mebels = await _context.Mebels
                .Include(m => m.Categori)
                .ToListAsync();

            return View("ReadOnlyList", mebels);
        }

        // GET: Mebels/Details/5 (для всех авторизованных)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mebel = await _context.Mebels
                .Include(m => m.Categori)
                .FirstOrDefaultAsync(m => m.IdMebel == id);

            if (mebel == null)
            {
                return NotFound();
            }

            return View(mebel);
        }

        // GET: Mebels/Create (только для Staff)
        [Authorize(Roles = "Staff")]
        public IActionResult Create()
        {
            ViewData["CategoriId"] = new SelectList(_context.Categoris, "IdCategori", "NameCategori");
            return View();
        }

        // POST: Mebels/Create (только для Staff)
        [Authorize(Roles = "Staff")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMebel,ProductName,CategoriId")] Mebel mebel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mebel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoriId"] = new SelectList(_context.Categoris, "IdCategori", "NameCategori", mebel.CategoriId);
            return View(mebel);
        }

        // GET: Mebels/Edit/5 (только для Staff)
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mebel = await _context.Mebels.FindAsync(id);
            if (mebel == null)
            {
                return NotFound();
            }

            ViewData["CategoriId"] = new SelectList(_context.Categoris, "IdCategori", "NameCategori", mebel.CategoriId);
            return View(mebel);
        }

        // POST: Mebels/Edit/5 (только для Staff)
        [Authorize(Roles = "Staff")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMebel,ProductName,CategoriId")] Mebel mebel)
        {
            if (id != mebel.IdMebel)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mebel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MebelExists(mebel.IdMebel))
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

            ViewData["CategoriId"] = new SelectList(_context.Categoris, "IdCategori", "NameCategori", mebel.CategoriId);
            return View(mebel);
        }

        // GET: Mebels/Delete/5 (только для Staff)
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mebel = await _context.Mebels
                .Include(m => m.Categori)
                .FirstOrDefaultAsync(m => m.IdMebel == id);

            if (mebel == null)
            {
                return NotFound();
            }

            return View(mebel);
        }

        // POST: Mebels/Delete/5 (только для Staff)
        [Authorize(Roles = "Staff")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mebel = await _context.Mebels.FindAsync(id);
            if (mebel != null)
            {
                _context.Mebels.Remove(mebel);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool MebelExists(int id)
        {
            return _context.Mebels.Any(e => e.IdMebel == id);
        }
    }
}