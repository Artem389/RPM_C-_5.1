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
    public class SostavZakazasController : Controller
    {
        private readonly MedelStoreContext _context;

        public SostavZakazasController(MedelStoreContext context)
        {
            _context = context;
        }

        // GET: SostavZakazas
        public async Task<IActionResult> Index()
        {
            var medelStoreContext = _context.SostavZakazas.Include(s => s.Mebel);
            return View(await medelStoreContext.ToListAsync());
        }

        // GET: SostavZakazas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sostavZakaza = await _context.SostavZakazas
                .Include(s => s.Mebel)
                .FirstOrDefaultAsync(m => m.IdSostavZakaza == id);
            if (sostavZakaza == null)
            {
                return NotFound();
            }

            return View(sostavZakaza);
        }

        // GET: SostavZakazas/Create
        public IActionResult Create()
        {
            ViewData["MebelId"] = new SelectList(_context.Mebels, "IdMebel", "IdMebel");
            return View();
        }

        // POST: SostavZakazas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSostavZakaza,Price,MebelId")] SostavZakaza sostavZakaza)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sostavZakaza);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MebelId"] = new SelectList(_context.Mebels, "IdMebel", "IdMebel", sostavZakaza.MebelId);
            return View(sostavZakaza);
        }

        // GET: SostavZakazas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sostavZakaza = await _context.SostavZakazas.FindAsync(id);
            if (sostavZakaza == null)
            {
                return NotFound();
            }
            ViewData["MebelId"] = new SelectList(_context.Mebels, "IdMebel", "IdMebel", sostavZakaza.MebelId);
            return View(sostavZakaza);
        }

        // POST: SostavZakazas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSostavZakaza,Price,MebelId")] SostavZakaza sostavZakaza)
        {
            if (id != sostavZakaza.IdSostavZakaza)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sostavZakaza);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SostavZakazaExists(sostavZakaza.IdSostavZakaza))
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
            ViewData["MebelId"] = new SelectList(_context.Mebels, "IdMebel", "IdMebel", sostavZakaza.MebelId);
            return View(sostavZakaza);
        }

        // GET: SostavZakazas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sostavZakaza = await _context.SostavZakazas
                .Include(s => s.Mebel)
                .FirstOrDefaultAsync(m => m.IdSostavZakaza == id);
            if (sostavZakaza == null)
            {
                return NotFound();
            }

            return View(sostavZakaza);
        }

        // POST: SostavZakazas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sostavZakaza = await _context.SostavZakazas.FindAsync(id);
            if (sostavZakaza != null)
            {
                _context.SostavZakazas.Remove(sostavZakaza);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SostavZakazaExists(int id)
        {
            return _context.SostavZakazas.Any(e => e.IdSostavZakaza == id);
        }
    }
}
