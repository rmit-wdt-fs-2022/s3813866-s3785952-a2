using AssignmentClassLibrary.Data;
using AssignmentClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using SimpleHashing;

namespace Assignment2.Controllers;

[Route("/User/SecureLogin")]
public class LoginController : Controller
{
    private readonly ModelDbContext _context;

    public LoginController(ModelDbContext context)
    {
        _context = context;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string loginID, string password)
    {
        var login = await _context.Login.FindAsync(loginID);
        // var customer = await _context.Customer.FindAsync(login.CustomerId);

        if (login == null || string.IsNullOrEmpty(password) || !PBKDF2.Verify(login.PasswordHash, password))
        {
            ModelState.AddModelError("LoginFailed", "Login failed, please try again.");
            return View(new Login {LoginId = loginID});
        }

        // Login customer.
        HttpContext.Session.SetInt32(nameof(Customer.CustomerId), login.CustomerId);
        HttpContext.Session.SetString(nameof(Customer.Name), login.Customer.Name);
        HttpContext.Session.SetString("loginId", loginID);

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