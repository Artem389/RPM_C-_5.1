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
    public class ApplicationsController : Controller
    {
        private readonly MedelStoreContext _context;

        public ApplicationsController(MedelStoreContext context)
        {
            _context = context;
        }

        // GET: Applications
        public async Task<IActionResult> Index()
        {
            var medelStoreContext = _context.Applications.Include(a => a.Clients).Include(a => a.Mebel).Include(a => a.Staff);
            return View(await medelStoreContext.ToListAsync());
        }

        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.Clients)
                .Include(a => a.Mebel)
                .Include(a => a.Staff)
                .FirstOrDefaultAsync(m => m.IdApplications == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // GET: Applications/Create
        public IActionResult Create()
        {
            ViewData["ClientsId"] = new SelectList(_context.Clients, "IdClients", "IdClients");
            ViewData["MebelId"] = new SelectList(_context.Mebels, "IdMebel", "IdMebel");
            ViewData["StaffId"] = new SelectList(_context.Staff, "IdStaff", "IdStaff");
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdApplications,DateOfApplicationSubmission,ApplicationStatus,MebelId,ClientsId,StaffId")] Application application)
        {
            if (ModelState.IsValid)
            {
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientsId"] = new SelectList(_context.Clients, "IdClients", "IdClients", application.ClientsId);
            ViewData["MebelId"] = new SelectList(_context.Mebels, "IdMebel", "IdMebel", application.MebelId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "IdStaff", "IdStaff", application.StaffId);
            return View(application);
        }

        // GET: Applications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            ViewData["ClientsId"] = new SelectList(_context.Clients, "IdClients", "IdClients", application.ClientsId);
            ViewData["MebelId"] = new SelectList(_context.Mebels, "IdMebel", "IdMebel", application.MebelId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "IdStaff", "IdStaff", application.StaffId);
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdApplications,DateOfApplicationSubmission,ApplicationStatus,MebelId,ClientsId,StaffId")] Application application)
        {
            if (id != application.IdApplications)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(application);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.IdApplications))
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
            ViewData["ClientsId"] = new SelectList(_context.Clients, "IdClients", "IdClients", application.ClientsId);
            ViewData["MebelId"] = new SelectList(_context.Mebels, "IdMebel", "IdMebel", application.MebelId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "IdStaff", "IdStaff", application.StaffId);
            return View(application);
        }

        // GET: Applications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.Clients)
                .Include(a => a.Mebel)
                .Include(a => a.Staff)
                .FirstOrDefaultAsync(m => m.IdApplications == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application != null)
            {
                _context.Applications.Remove(application);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationExists(int id)
        {
            return _context.Applications.Any(e => e.IdApplications == id);
        }
    }
}
