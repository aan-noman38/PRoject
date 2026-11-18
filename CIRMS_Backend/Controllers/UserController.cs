using CIRMS_Backend.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using CIRMS_Backend.Models;

namespace CIRMS_Backend.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            int? userId =
                HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var incidents = _context.Incidents
                .Where(i => i.UserId == userId.Value)
                .OrderByDescending(i => i.ReportDate)
                .ToList();

            ViewBag.Total = incidents.Count;

            ViewBag.Pending =
                incidents.Count(i => i.Status == "Pending");

            ViewBag.Solved =
                incidents.Count(i => i.Status == "Solved");

            ViewBag.Critical =
                incidents.Count(i => i.Severity == "Critical");
                var notifications = _context.Notifications
    .Where(n => n.UserId == userId.Value)
    .OrderByDescending(n => n.CreatedAt)
    .Take(10)
    .ToList();

ViewBag.Notifications = notifications;

ViewBag.UnreadNotifications =
    notifications.Count(n => !n.IsRead);

            return View(incidents);
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            int? userId =
                HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var incident = _context.Incidents
                .FirstOrDefault(i =>
                    i.IncidentId == id &&
                    i.UserId == userId.Value);

            if (incident == null)
            {
                return NotFound();
            }

            return View(incident);
        }
        [HttpPost]
public IActionResult MarkNotificationRead(int id)
{
    int? userId = HttpContext.Session.GetInt32("UserId");

    if (userId == null)
    {
        return RedirectToAction("Login", "Account");
    }

    var notification = _context.Notifications
        .FirstOrDefault(n =>
            n.NotificationId == id &&
            n.UserId == userId.Value);

    if (notification != null)
    {
        notification.IsRead = true;
        _context.SaveChanges();
    }

    return RedirectToAction("Dashboard");
}
    }
}