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
    public class CategorisController : Controller
    {
        private readonly MedelStoreContext _context;

        public CategorisController(MedelStoreContext context)
        {
            _context = context;
        }

        // GET: Categoris (только для Staff)
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Index()
        {
            if (!User.IsInRole("Staff"))
            {
                return RedirectToAction("List");
            }
            return View(await _context.Categoris.ToListAsync());
        }

        // GET: Categoris/List (для всех авторизованных)
        public async Task<IActionResult> List()
        {
            return View("ReadOnlyList", await _context.Categoris.ToListAsync());
        }

        // GET: Categoris/Details/5 (для всех авторизованных)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categori = await _context.Categoris
                .FirstOrDefaultAsync(m => m.IdCategori == id);
            if (categori == null)
            {
                return NotFound();
            }

            return View(categori);
        }

        // GET: Categoris/Create (только для Staff)
        [Authorize(Roles = "Staff")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categoris/Create (только для Staff)
        [Authorize(Roles = "Staff")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCategori,NameCategori")] Categori categori)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categori);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categori);
        }

        // GET: Categoris/Edit/5 (только для Staff)
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categori = await _context.Categoris.FindAsync(id);
            if (categori == null)
            {
                return NotFound();
            }
            return View(categori);
        }

        // POST: Categoris/Edit/5 (только для Staff)
        [Authorize(Roles = "Staff")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCategori,NameCategori")] Categori categori)
        {
            if (id != categori.IdCategori)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categori);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriExists(categori.IdCategori))
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
            return View(categori);
        }

        // GET: Categoris/Delete/5 (только для Staff)
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categori = await _context.Categoris
                .FirstOrDefaultAsync(m => m.IdCategori == id);
            if (categori == null)
            {
                return NotFound();
            }

            return View(categori);
        }

        // POST: Categoris/Delete/5 (только для Staff)
        [Authorize(Roles = "Staff")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categori = await _context.Categoris.FindAsync(id);
            if (categori != null)
            {
                _context.Categoris.Remove(categori);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CategoriExists(int id)
        {
            return _context.Categoris.Any(e => e.IdCategori == id);
        }
    }
}