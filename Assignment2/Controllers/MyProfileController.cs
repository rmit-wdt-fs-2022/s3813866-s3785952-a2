using AssignmentClassLibrary.Data;
using AssignmentClassLibrary.Models;
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

    public async Task<IActionResult> Home()
    {
        return RedirectToAction("Index", "Customer");
    }
}