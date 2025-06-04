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
    public class PostavhiksController : Controller
    {
        private readonly MedelStoreContext _context;

        public PostavhiksController(MedelStoreContext context)
        {
            _context = context;
        }

        // GET: Postavhiks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Postavhiks.ToListAsync());
        }

        // GET: Postavhiks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postavhik = await _context.Postavhiks
                .FirstOrDefaultAsync(m => m.IdPostavchik == id);
            if (postavhik == null)
            {
                return NotFound();
            }

            return View(postavhik);
        }

        // GET: Postavhiks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Postavhiks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPostavchik,Name,Contact")] Postavhik postavhik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postavhik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(postavhik);
        }

        // GET: Postavhiks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postavhik = await _context.Postavhiks.FindAsync(id);
            if (postavhik == null)
            {
                return NotFound();
            }
            return View(postavhik);
        }

        // POST: Postavhiks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPostavchik,Name,Contact")] Postavhik postavhik)
        {
            if (id != postavhik.IdPostavchik)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postavhik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostavhikExists(postavhik.IdPostavchik))
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
            return View(postavhik);
        }

        // GET: Postavhiks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postavhik = await _context.Postavhiks
                .FirstOrDefaultAsync(m => m.IdPostavchik == id);
            if (postavhik == null)
            {
                return NotFound();
            }

            return View(postavhik);
        }

        // POST: Postavhiks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postavhik = await _context.Postavhiks.FindAsync(id);
            if (postavhik != null)
            {
                _context.Postavhiks.Remove(postavhik);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostavhikExists(int id)
        {
            return _context.Postavhiks.Any(e => e.IdPostavchik == id);
        }
    }
}
