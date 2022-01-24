using Assignment2.Data;
using Assignment2.Filters;
using Assignment2.Models;
using Assignment2.Utility;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers;

[CustomerAuth]
public class CustomerController : Controller
{
    private readonly ModelDbContext _context;

    private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerId)).Value;

    public CustomerController(ModelDbContext context) => _context = context;

    public async Task<IActionResult> Index()
    {
        // Lazy loading.
        // The Customer.Accounts property will be lazy loaded upon demand.
        var customer = await _context.Customer.FindAsync(CustomerID);

        // OR
        // Eager loading.
        //var customer = await _context.Customers.Include(x => x.Accounts).
        //    FirstOrDefaultAsync(x => x.CustomerID == _customerID);

        return View(customer);
    }

    public async Task<IActionResult> Deposit(int id) => View(await _context.Account.FindAsync(id));

    [HttpPost]
    public async Task<IActionResult> Deposit(int id, decimal amount)
    {
        var account = await _context.Account.FindAsync(id);

        if (amount <= 0)
            ModelState.AddModelError(nameof(amount), "Amount must be positive.");
        if (amount.TwoDecimalPlacesCheck())
            ModelState.AddModelError(nameof(amount), "Amount cannot have more than 2 decimal places.");
        if (!ModelState.IsValid)
        {
            ViewBag.Amount = amount;
            return View(account);
        }
        
        account.Transactions.Add(
            new Transaction
            {
                TransactionType = 'D',
                Amount = amount,
                TransactionTimeUtc = DateTime.UtcNow
            });

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}