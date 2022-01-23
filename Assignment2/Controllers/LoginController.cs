using Assignment2.Data;
using Assignment2.Models;
using Microsoft.AspNetCore.Mvc;
using SimpleHashing;

namespace Assignment2.Controllers;

[Route("/User/SecureLogin")]
public class LoginController : Controller
{
    private readonly ModelDbContext _context;

    public LoginController(ModelDbContext context) => _context = context;

    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(string loginID, string password)
    {
        var login = await _context.Login.FindAsync(loginID);
        var customer = await _context.Customer.FindAsync(login.CustomerId);
        if(login == null || string.IsNullOrEmpty(password) || !PBKDF2.Verify(login.PasswordHash, password))
        { 
            ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
            return View(new Login { LoginId = loginID });
        }

        // Login customer.
        Console.WriteLine(nameof(Customer.CustomerId));
        Console.WriteLine(login.CustomerId);
        HttpContext.Session.SetInt32(nameof(Customer.CustomerId), login.CustomerId);
        Console.WriteLine(nameof(Customer.Name));
        Console.WriteLine(customer.Name);
        HttpContext.Session.SetString(nameof(Customer.Name), customer.Name);

        return RedirectToAction("Index", "Customer");
    }

    [Route("LogoutNow")]
    public IActionResult Logout()
    {
        // Logout customer.
        HttpContext.Session.Clear();

        return RedirectToAction("Index", "Home");
    }
}