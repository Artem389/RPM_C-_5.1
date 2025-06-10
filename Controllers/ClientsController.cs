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
    public class ClientsController : Controller
    {
        private readonly MedelStoreContext _context;

        public ClientsController(MedelStoreContext context)
        {
            _context = context;
        }

        // GET: Clients (только для Staff)
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Index()
        {
            if (!User.IsInRole("Staff"))
            {
                return RedirectToAction("List");
            }

            var clients = await _context.Clients
                .Include(c => c.Adress)
                .Include(c => c.Passport)
                .ToListAsync();

            return View(clients);
        }

        // GET: Clients/List (для всех авторизованных)
        public async Task<IActionResult> List()
        {
            var clients = await _context.Clients
                .Include(c => c.Adress)
                .Include(c => c.Passport)
                .ToListAsync();

            return View("ReadOnlyList", clients);
        }

        // GET: Clients/Details/5 (для всех авторизованных)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.Adress)
                .Include(c => c.Passport)
                .FirstOrDefaultAsync(m => m.IdClients == id);

            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create (только для Staff)
        [Authorize(Roles = "Staff")]
        public IActionResult Create()
        {
            ViewData["AdressId"] = new SelectList(_context.Adresses, "IdAdress", "Street");
            ViewData["PassportId"] = new SelectList(_context.Pasports, "IdPasports", "Number");
            return View();
        }

        // POST: Clients/Create (только для Staff)
        [Authorize(Roles = "Staff")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdClients,Suname,Name,Fatherland,DateOfBirth,AdressId,PassportId")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["AdressId"] = new SelectList(_context.Adresses, "IdAdress", "Street", client.AdressId);
            ViewData["PassportId"] = new SelectList(_context.Pasports, "IdPasports", "Number", client.PassportId);
            return View(client);
        }

        // GET: Clients/Edit/5 (только для Staff)
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            ViewData["AdressId"] = new SelectList(_context.Adresses, "IdAdress", "Street", client.AdressId);
            ViewData["PassportId"] = new SelectList(_context.Pasports, "IdPasports", "Number", client.PassportId);
            return View(client);
        }

        // POST: Clients/Edit/5 (только для Staff)
        [Authorize(Roles = "Staff")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdClients,Suname,Name,Fatherland,DateOfBirth,AdressId,PassportId")] Client client)
        {
            if (id != client.IdClients)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.IdClients))
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

            ViewData["AdressId"] = new SelectList(_context.Adresses, "IdAdress", "Street", client.AdressId);
            ViewData["PassportId"] = new SelectList(_context.Pasports, "IdPasports", "Number", client.PassportId);
            return View(client);
        }

        // GET: Clients/Delete/5 (только для Staff)
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.Adress)
                .Include(c => c.Passport)
                .FirstOrDefaultAsync(m => m.IdClients == id);

            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5 (только для Staff)
        [Authorize(Roles = "Staff")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.IdClients == id);
        }
    }
}