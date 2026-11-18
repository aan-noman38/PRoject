 using CIRMS_Backend.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using CIRMS_Backend.Models;

namespace CIRMS_Backend.Controllers
{
    public class SocController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SocController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Dashboard()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            string? role = HttpContext.Session.GetString("Role");

            if (userId == null || role != "SOC")
            {
                return RedirectToAction("Login", "Account");
            }

            var incidents = _context.Incidents
                .OrderByDescending(i => i.ReportDate)
                .ToList();

            ViewBag.Total = incidents.Count;
            ViewBag.Pending =
                incidents.Count(i => i.Status == "Pending");
            ViewBag.Solved =
                incidents.Count(i => i.Status == "Solved");

            return View(incidents);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            string? role = HttpContext.Session.GetString("Role");

            if (userId == null || role != "SOC")
            {
                return RedirectToAction("Login", "Account");
            }

            var incident = _context.Incidents
                .FirstOrDefault(i => i.IncidentId == id);

            if (incident == null)
            {
                return NotFound();
            }

            return View(incident);
        }
        [HttpPost]
[HttpPost]
public IActionResult UpdateStatus(int id, string status, string socReport)
{
    var incident = _context.Incidents
        .FirstOrDefault(i => i.IncidentId == id);

    if (incident == null)
    {
        return NotFound();
    }

    // Check whether SOC actually changed anything
    bool statusChanged = incident.Status != status;
    bool reportChanged = incident.SocReport != socReport;

    // Update incident
    incident.Status = status;
    incident.SocReport = socReport;

    // Create notification only when there is a real update
    if (statusChanged || reportChanged)
    {
        string message;

        if (status == "Solved")
        {
            message =
                $"Incident INC{incident.IncidentId:000} has been solved by SOC.";
        }
        else
        {
            message =
                $"SOC has updated your incident INC{incident.IncidentId:000}.";
        }

        var notification = new Notification
        {
            UserId = incident.UserId,
            IncidentId = incident.IncidentId,
            Message = message,
            IsRead = false,
            CreatedAt = DateTime.Now
        };

        _context.Notifications.Add(notification);
    }

    _context.SaveChanges();

    return RedirectToAction("Details", new { id = incident.IncidentId });
}
    }
}