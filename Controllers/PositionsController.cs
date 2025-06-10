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
    public class PositionsController : Controller
    {
        private readonly MedelStoreContext _context;

        public PositionsController(MedelStoreContext context)
        {
            _context = context;
        }

        // GET: Positions (только для Staff)
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Index()
        {
            if (!User.IsInRole("Staff"))
            {
                return RedirectToAction("List");
            }
            return View(await _context.Positions.ToListAsync());
        }

        // GET: Positions/List (для всех авторизованных)
        public async Task<IActionResult> List()
        {
            return View("ReadOnlyList", await _context.Positions.ToListAsync());
        }

        // GET: Positions/Details/5 (для всех авторизованных)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var position = await _context.Positions
                .FirstOrDefaultAsync(m => m.IdPositions == id);
            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }

        // GET: Positions/Create (только для Staff)
        [Authorize(Roles = "Staff")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Positions/Create (только для Staff)
        [Authorize(Roles = "Staff")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPositions,Positions,Salary")] Position position)
        {
            if (ModelState.IsValid)
            {
                _context.Add(position);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(position);
        }

        // GET: Positions/Edit/5 (только для Staff)
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var position = await _context.Positions.FindAsync(id);
            if (position == null)
            {
                return NotFound();
            }
            return View(position);
        }

        // POST: Positions/Edit/5 (только для Staff)
        [Authorize(Roles = "Staff")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPositions,Positions,Salary")] Position position)
        {
            if (id != position.IdPositions)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(position);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PositionExists(position.IdPositions))
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
            return View(position);
        }

        // GET: Positions/Delete/5 (только для Staff)
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var position = await _context.Positions
                .FirstOrDefaultAsync(m => m.IdPositions == id);
            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }

        // POST: Positions/Delete/5 (только для Staff)
        [Authorize(Roles = "Staff")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var position = await _context.Positions.FindAsync(id);
            if (position != null)
            {
                _context.Positions.Remove(position);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PositionExists(int id)
        {
            return _context.Positions.Any(e => e.IdPositions == id);
        }
    }
}