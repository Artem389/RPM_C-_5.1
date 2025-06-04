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
    public class PostavkisController : Controller
    {
        private readonly MedelStoreContext _context;

        public PostavkisController(MedelStoreContext context)
        {
            _context = context;
        }

        // GET: Postavkis
        public async Task<IActionResult> Index()
        {
            var medelStoreContext = _context.Postavkis.Include(p => p.Mebel).Include(p => p.Postavchika);
            return View(await medelStoreContext.ToListAsync());
        }

        // GET: Postavkis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postavki = await _context.Postavkis
                .Include(p => p.Mebel)
                .Include(p => p.Postavchika)
                .FirstOrDefaultAsync(m => m.IdPostavki == id);
            if (postavki == null)
            {
                return NotFound();
            }

            return View(postavki);
        }

        // GET: Postavkis/Create
        public IActionResult Create()
        {
            ViewData["MebelId"] = new SelectList(_context.Mebels, "IdMebel", "IdMebel");
            ViewData["PostavchikaId"] = new SelectList(_context.Postavhiks, "IdPostavchik", "IdPostavchik");
            return View();
        }

        // POST: Postavkis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPostavki,DatePostavki,PricePostavki,PostavchikaId,MebelId")] Postavki postavki)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postavki);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MebelId"] = new SelectList(_context.Mebels, "IdMebel", "IdMebel", postavki.MebelId);
            ViewData["PostavchikaId"] = new SelectList(_context.Postavhiks, "IdPostavchik", "IdPostavchik", postavki.PostavchikaId);
            return View(postavki);
        }

        // GET: Postavkis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postavki = await _context.Postavkis.FindAsync(id);
            if (postavki == null)
            {
                return NotFound();
            }
            ViewData["MebelId"] = new SelectList(_context.Mebels, "IdMebel", "IdMebel", postavki.MebelId);
            ViewData["PostavchikaId"] = new SelectList(_context.Postavhiks, "IdPostavchik", "IdPostavchik", postavki.PostavchikaId);
            return View(postavki);
        }

        // POST: Postavkis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPostavki,DatePostavki,PricePostavki,PostavchikaId,MebelId")] Postavki postavki)
        {
            if (id != postavki.IdPostavki)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postavki);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostavkiExists(postavki.IdPostavki))
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
            ViewData["MebelId"] = new SelectList(_context.Mebels, "IdMebel", "IdMebel", postavki.MebelId);
            ViewData["PostavchikaId"] = new SelectList(_context.Postavhiks, "IdPostavchik", "IdPostavchik", postavki.PostavchikaId);
            return View(postavki);
        }

        // GET: Postavkis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postavki = await _context.Postavkis
                .Include(p => p.Mebel)
                .Include(p => p.Postavchika)
                .FirstOrDefaultAsync(m => m.IdPostavki == id);
            if (postavki == null)
            {
                return NotFound();
            }

            return View(postavki);
        }

        // POST: Postavkis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postavki = await _context.Postavkis.FindAsync(id);
            if (postavki != null)
            {
                _context.Postavkis.Remove(postavki);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostavkiExists(int id)
        {
            return _context.Postavkis.Any(e => e.IdPostavki == id);
        }
    }
}
