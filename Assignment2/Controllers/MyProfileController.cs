using Assignment2.Data;
using Assignment2.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers;

public class MyProfile : Controller
{
    private readonly ModelDbContext _context;

    public MyProfile(ModelDbContext context)
    {
        _context = context;
    }

    private int CustomerId => HttpContext.Session.GetInt32(nameof(Customer.CustomerId))!.Value;

    [HttpGet]
    public async Task<IActionResult> EditAccount()
    {
        var customer = await _context.Customer.FindAsync(CustomerId);

        var model = new Customer
        {
            CustomerId = customer.CustomerId,
            Name = customer.Name,
            TFN = customer.TFN,
            Address = customer.Address,
            Suburb = customer.Suburb,
            State = customer.State,
            PostCode = customer.PostCode,
            Mobile = customer.Mobile
        };


        return View("MyProfile", model);
    }

    [HttpPost]
    public async Task<IActionResult> EditAccount(Customer model)
    {
        var customer = await _context.Customer.FindAsync(CustomerId);

        if (ModelState.IsValid)
        {
            customer.Name = model.Name;
            customer.TFN = model.TFN;
            customer.Address = model.Address;
            customer.Suburb = model.Suburb;
            customer.State = model.State;
            customer.PostCode = model.PostCode;
            customer.Mobile = model.Mobile;
            customer.Accounts = model.Accounts;

            await _context.SaveChangesAsync();

            ModelState.Clear();

            return View("MyProfile", model);
        }

        return View("MyProfile");
    }

    // to be done for page no customer in db additional check
    /*if (customer == null)
    {
        ViewBag.ErrorMessage = $"User with Id = {customer} cannot be found";
        return View("NotFound");
    }#1#
    //else
    //{
}


[HttpPost]
public async Task<IActionResult> EditPassword(string password, string confirmPassword)
{
    var login = await _context.Login.FindAsync(CustomerId);

    if (password.Equals(confirmPassword))
    {
        login.PasswordHash = PBKDF2.Hash(password);
        await _context.SaveChangesAsync();
        ModelState.Clear();
        return View("ChangePassword");


    }
    return View("ChangePassword");

    // to be done for page no customer in db additional check
    /*if (customer == null)
    {
        ViewBag.ErrorMessage = $"User with Id = {customer} cannot be found";
        return View("NotFound");
    }#1#
    //else
    //{
}*/

    public async Task<IActionResult> Home()
    {
        return RedirectToAction("Index", "Customer");
    }
}