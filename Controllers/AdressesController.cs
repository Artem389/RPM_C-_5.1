using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication5.Controllers
{
    [Authorize] // Требуем авторизации для всех методов
    public class AdressesController : Controller
    {
        private readonly MedelStoreContext _context;

        public AdressesController(MedelStoreContext context)
        {
            _context = context;
        }

        // GET: Adresses (только для Staff)
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Index()
        {
            if (!User.IsInRole("Staff"))
            {
                // Для клиентов перенаправляем на версию только для чтения
                return RedirectToAction("List");
            }
            return View(await _context.Adresses.ToListAsync());
        }

        // GET: Adresses/List (для всех авторизованных)
        public async Task<IActionResult> List()
        {
            return View("ReadOnlyList", await _context.Adresses.ToListAsync());
        }

        // GET: Adresses/Details/5 (для всех авторизованных)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adress = await _context.Adresses
                .FirstOrDefaultAsync(m => m.IdAdress == id);
            if (adress == null)
            {
                return NotFound();
            }

            return View(adress);
        }

        // GET: Adresses/Create (только для Staff)
        [Authorize(Roles = "Staff")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Adresses/Create (только для Staff)
        [Authorize(Roles = "Staff")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAdress,Street,City,Country")] Adress adress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adress);
        }

        // GET: Adresses/Edit/5 (только для Staff)
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adress = await _context.Adresses.FindAsync(id);
            if (adress == null)
            {
                return NotFound();
            }
            return View(adress);
        }

        // POST: Adresses/Edit/5 (только для Staff)
        [Authorize(Roles = "Staff")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdAdress,Street,City,Country")] Adress adress)
        {
            if (id != adress.IdAdress)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdressExists(adress.IdAdress))
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
            return View(adress);
        }

        // GET: Adresses/Delete/5 (только для Staff)
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adress = await _context.Adresses
                .FirstOrDefaultAsync(m => m.IdAdress == id);
            if (adress == null)
            {
                return NotFound();
            }

            return View(adress);
        }

        // POST: Adresses/Delete/5 (только для Staff)
        [Authorize(Roles = "Staff")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adress = await _context.Adresses.FindAsync(id);
            if (adress != null)
            {
                _context.Adresses.Remove(adress);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdressExists(int id)
        {
            return _context.Adresses.Any(e => e.IdAdress == id);
        }
    }
}