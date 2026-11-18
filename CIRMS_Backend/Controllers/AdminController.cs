using CIRMS_Backend.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CIRMS_Backend.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            string? role = HttpContext.Session.GetString("Role");

            if (role != "Admin")
            {
                return RedirectToAction("Login", "Account");
            }

            // Incident statistics
            var incidents = _context.Incidents.ToList();

            ViewBag.TotalIncidents = incidents.Count;

            ViewBag.Pending = incidents.Count(i => i.Status == "Pending");

            ViewBag.InProgress = incidents.Count(i => i.Status == "In Progress");

            ViewBag.Solved = incidents.Count(i => i.Status == "Solved");

            ViewBag.Critical = incidents.Count(i => i.Severity == "Critical");


            // User statistics
            ViewBag.TotalUsers = _context.Users.Count();

            return View();
        }
        [HttpGet]
public IActionResult Users()
{
    string? role = HttpContext.Session.GetString("Role");

    if (role != "Admin")
    {
        return RedirectToAction("Login", "Account");
    }

    var users = _context.Users
        .Where(u => u.Role == "User")
        .OrderBy(u => u.UserId)
        .ToList();

    return View(users);
}
[HttpGet]
public IActionResult Incidents()
{
    string? role = HttpContext.Session.GetString("Role");

    if (role != "Admin")
    {
        return RedirectToAction("Login", "Account");
    }

    var incidents = _context.Incidents
        .OrderByDescending(i => i.ReportDate)
        .ToList();

    return View(incidents);
}
    }
}