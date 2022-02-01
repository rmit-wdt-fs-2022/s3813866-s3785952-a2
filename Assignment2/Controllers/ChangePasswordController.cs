using Assignment2.Data;
using Microsoft.AspNetCore.Mvc;
using SimpleHashing;

namespace Assignment2.Controllers;

public class ChangePassword : Controller
{
    private readonly ModelDbContext _context;

    public ChangePassword(ModelDbContext context)
    {
        _context = context;
    }
    
    public IActionResult EditPassword() => View();
    
    [HttpPost]
    public async Task<IActionResult> EditPassword(string password, string confirmPassword)
    {
        var loginId = HttpContext.Session.GetString("loginId");
        var login = await _context.Login.FindAsync(loginId);

        if (!password.Equals(confirmPassword))
        {
            ModelState.AddModelError("Password", "Passwords Do Not Match.");
            ModelState.AddModelError("ConfirmPassword", "Passwords Do Not Match.");
            return View("EditPassword");
        }
        else
        {
            login.PasswordHash = PBKDF2.Hash(password);
            await _context.SaveChangesAsync();
            ModelState.Clear();
            return View("EditPassword");
        }
    }
    
    public async Task<IActionResult> Home()
    {
        return RedirectToAction("Index", "Customer");
    }
    
}