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
    public class ApplicationsController : Controller
    {
        private readonly MedelStoreContext _context;

        public ApplicationsController(MedelStoreContext context)
        {
            _context = context;
        }

        // GET: Applications (только для Staff)
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Index()
        {
            if (!User.IsInRole("Staff"))
            {
                return RedirectToAction("List");
            }

            var applications = await _context.Applications
                .Include(a => a.Clients)
                .Include(a => a.Mebel)
                .Include(a => a.Staff)
                .ToListAsync();

            return View(applications);
        }

        // GET: Applications/List (для всех авторизованных)
        public async Task<IActionResult> List()
        {
            var applications = await _context.Applications
                .Include(a => a.Clients)
                .Include(a => a.Mebel)
                .Include(a => a.Staff)
                .ToListAsync();

            return View("ReadOnlyList", applications);
        }

        // GET: Applications/Details/5 (для всех авторизованных)
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

        // GET: Applications/Create (только для Staff)
        [Authorize(Roles = "Staff")]
        public IActionResult Create()
        {
            ViewData["ClientsId"] = new SelectList(_context.Clients, "IdClients", "Name");
            ViewData["MebelId"] = new SelectList(_context.Mebels, "IdMebel", "ProductName");
            ViewData["StaffId"] = new SelectList(_context.Staff, "IdStaff", "Name");
            return View();
        }

        // POST: Applications/Create (только для Staff)
        [Authorize(Roles = "Staff")]
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

            ViewData["ClientsId"] = new SelectList(_context.Clients, "IdClients", "Name", application.ClientsId);
            ViewData["MebelId"] = new SelectList(_context.Mebels, "IdMebel", "ProductName", application.MebelId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "IdStaff", "Name", application.StaffId);
            return View(application);
        }

        // GET: Applications/Edit/5 (только для Staff)
        [Authorize(Roles = "Staff")]
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

            ViewData["ClientsId"] = new SelectList(_context.Clients, "IdClients", "Name", application.ClientsId);
            ViewData["MebelId"] = new SelectList(_context.Mebels, "IdMebel", "ProductName", application.MebelId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "IdStaff", "Name", application.StaffId);
            return View(application);
        }

        // POST: Applications/Edit/5 (только для Staff)
        [Authorize(Roles = "Staff")]
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

            ViewData["ClientsId"] = new SelectList(_context.Clients, "IdClients", "Name", application.ClientsId);
            ViewData["MebelId"] = new SelectList(_context.Mebels, "IdMebel", "ProductName", application.MebelId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "IdStaff", "Name", application.StaffId);
            return View(application);
        }

        // GET: Applications/Delete/5 (только для Staff)
        [Authorize(Roles = "Staff")]
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

        // POST: Applications/Delete/5 (только для Staff)
        [Authorize(Roles = "Staff")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application != null)
            {
                _context.Applications.Remove(application);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationExists(int id)
        {
            return _context.Applications.Any(e => e.IdApplications == id);
        }
    }
}