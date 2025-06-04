using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class MebelsController : Controller
    {
        private readonly MedelStoreContext _context;

        public MebelsController(MedelStoreContext context)
        {
            _context = context;
        }

        // GET: Mebels
        public async Task<IActionResult> Index()
        {
            var medelStoreContext = _context.Mebels.Include(m => m.Categori);
            return View(await medelStoreContext.ToListAsync());
        }

        // GET: Mebels/Details/5
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

        // GET: Mebels/Create
        public IActionResult Create()
        {
            ViewData["CategoriId"] = new SelectList(_context.Categoris, "IdCategori", "IdCategori");
            return View();
        }

        // POST: Mebels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            ViewData["CategoriId"] = new SelectList(_context.Categoris, "IdCategori", "IdCategori", mebel.CategoriId);
            return View(mebel);
        }

        // GET: Mebels/Edit/5
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
            ViewData["CategoriId"] = new SelectList(_context.Categoris, "IdCategori", "IdCategori", mebel.CategoriId);
            return View(mebel);
        }

        // POST: Mebels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            ViewData["CategoriId"] = new SelectList(_context.Categoris, "IdCategori", "IdCategori", mebel.CategoriId);
            return View(mebel);
        }

        // GET: Mebels/Delete/5
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

        // POST: Mebels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mebel = await _context.Mebels.FindAsync(id);
            if (mebel != null)
            {
                _context.Mebels.Remove(mebel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MebelExists(int id)
        {
            return _context.Mebels.Any(e => e.IdMebel == id);
        }
    }
}
