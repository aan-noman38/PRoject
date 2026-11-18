using CIRMS_Backend.Data;
using CIRMS_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CIRMS_Backend.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AccountController(
            ApplicationDbContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string Username, string Password)
        {
             
            string adminUsername =
                _configuration["FixedAccounts:Admin:Username"];

            string adminPassword =
                _configuration["FixedAccounts:Admin:Password"];


            if (Username == adminUsername &&
                Password == adminPassword)
            {
                HttpContext.Session.SetInt32("UserId", -1);
                HttpContext.Session.SetString("Username", adminUsername);
                HttpContext.Session.SetString("Role", "Admin");

                return RedirectToAction("Dashboard", "Admin");
            }

            string socUsername =
                _configuration["FixedAccounts:SOC:Username"];

            string socPassword =
                _configuration["FixedAccounts:SOC:Password"];


            if (Username == socUsername &&
                Password == socPassword)
            {
                HttpContext.Session.SetInt32("UserId", -2);
                HttpContext.Session.SetString("Username", socUsername);
                HttpContext.Session.SetString("Role", "SOC");

                return RedirectToAction("Dashboard", "Soc");
            }


            // -------------------------
            // Normal User from Database
            // -------------------------

            var user = _context.Users.FirstOrDefault(u => u.Username == Username && u.Password == Password);


            if (user == null)
            {
                ViewBag.Error = "Incorrect Username or Password";
                return View();
            }
          
            // Save User Session
       
            HttpContext.Session.SetInt32("UserId", user.UserId);

            HttpContext.Session.SetString("Username",user.Username);

            HttpContext.Session.SetString("Role","User");


            // User Dashboard
        

            return RedirectToAction("Dashboard", "User");
        }
      
        // GET: Signup
       
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }
  
        // POST: Signup
        [HttpPost]
        public IActionResult Signup(User user)
        {
            
            user.Role = "User";

           
            ModelState.Remove("Role");


            if (!ModelState.IsValid)
            {
                return View(user);
            }


            _context.Users.Add(user);

            _context.SaveChanges();


            return RedirectToAction("Login");
        }
  
        

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }
    }
}