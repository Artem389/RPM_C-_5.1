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
    public class StaffsController : Controller
    {
        private readonly MedelStoreContext _context;

        public StaffsController(MedelStoreContext context)
        {
            _context = context;
        }

        // GET: Staffs
        public async Task<IActionResult> Index()
        {
            var medelStoreContext = _context.Staff.Include(s => s.Passport).Include(s => s.Positions);
            return View(await medelStoreContext.ToListAsync());
        }

        // GET: Staffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff
                .Include(s => s.Passport)
                .Include(s => s.Positions)
                .FirstOrDefaultAsync(m => m.IdStaff == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // GET: Staffs/Create
        public IActionResult Create()
        {
            ViewData["PassportId"] = new SelectList(_context.Pasports, "IdPasports", "IdPasports");
            ViewData["PositionsId"] = new SelectList(_context.Positions, "IdPositions", "IdPositions");
            return View();
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdStaff,Suname,Name,Fatherland,PassportId,PositionsId")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PassportId"] = new SelectList(_context.Pasports, "IdPasports", "IdPasports", staff.PassportId);
            ViewData["PositionsId"] = new SelectList(_context.Positions, "IdPositions", "IdPositions", staff.PositionsId);
            return View(staff);
        }

        // GET: Staffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            ViewData["PassportId"] = new SelectList(_context.Pasports, "IdPasports", "IdPasports", staff.PassportId);
            ViewData["PositionsId"] = new SelectList(_context.Positions, "IdPositions", "IdPositions", staff.PositionsId);
            return View(staff);
        }

        // POST: Staffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdStaff,Suname,Name,Fatherland,PassportId,PositionsId")] Staff staff)
        {
            if (id != staff.IdStaff)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffExists(staff.IdStaff))
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
            ViewData["PassportId"] = new SelectList(_context.Pasports, "IdPasports", "IdPasports", staff.PassportId);
            ViewData["PositionsId"] = new SelectList(_context.Positions, "IdPositions", "IdPositions", staff.PositionsId);
            return View(staff);
        }

        // GET: Staffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staff
                .Include(s => s.Passport)
                .Include(s => s.Positions)
                .FirstOrDefaultAsync(m => m.IdStaff == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff != null)
            {
                _context.Staff.Remove(staff);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffExists(int id)
        {
            return _context.Staff.Any(e => e.IdStaff == id);
        }
    }
}
