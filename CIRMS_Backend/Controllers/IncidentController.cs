using CIRMS_Backend.Data;
using CIRMS_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CIRMS_Backend.Controllers
{
    public class IncidentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IncidentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Incident/Report
        [HttpGet]
        public IActionResult Report()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        // POST: /Incident/Report
        [HttpPost]
        public IActionResult Report(Incident incident)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            incident.UserId = userId.Value;

            incident.ReportDate = DateTime.Now;

            incident.Status = "Pending";

            _context.Incidents.Add(incident);

            _context.SaveChanges();

            return RedirectToAction("Dashboard", "User");
        }
    }
}