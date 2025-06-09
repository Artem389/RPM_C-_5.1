using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication5.Controllers
{
    [Authorize(Roles = "Staff")]
    public class AdminController : Controller
    {
        private readonly MedelStoreContext _context;

        public AdminController(MedelStoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ViewClients()
        {
            var clients = await _context.Clients.ToListAsync();
            return View(clients);
        }

        public async Task<IActionResult> ViewStaff()
        {
            var staff = await _context.Staff.ToListAsync();
            return View(staff);
        }
    }
}