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
        var customer = await _context.Customer.FindAsync(CustomerID);
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
                TransactionType = Constants.Deposit,
                Amount = amount,
                TransactionTimeUtc = DateTime.UtcNow
            });

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Withdraw(int id) => View(await _context.Account.FindAsync(id));
    
    [HttpPost]
    public async Task<IActionResult> Withdraw(int id, decimal amount)
    {
        var account = await _context.Account.FindAsync(id);

        if (amount <= 0)
            ModelState.AddModelError(nameof(amount), "Amount must be positive.");
        if (amount.TwoDecimalPlacesCheck())
            ModelState.AddModelError(nameof(amount), "Amount cannot have more than 2 decimal places.");
        if (account.Transactions.CalculateAccountBalance() - (amount + Constants.WithdrawFee) < Constants.SavingMinimumBalance && account.AccountType == Constants.SavingAccType)
        {
            ModelState.AddModelError(nameof(amount), "Not enough funds for withdraw.");
        }
        if (account.Transactions.CalculateAccountBalance() - (amount + Constants.WithdrawFee) < Constants.CheckingMinimumBalance && account.AccountType == Constants.CheckingAccType)
        {
            ModelState.AddModelError(nameof(amount), "Not enough funds for withdraw.");
        }
        if (!ModelState.IsValid)
        {
            ViewBag.Amount = amount;
            return View(account);
        }
        
        account.Transactions.Add(
            new Transaction
            {
                TransactionType = Constants.Withdraw,
                Amount = amount,
                TransactionTimeUtc = DateTime.UtcNow
            });
        account.Transactions.Add( new Transaction
        {
            TransactionType = Constants.ServiceFee,
            Amount = Constants.WithdrawFee,
            TransactionTimeUtc = DateTime.UtcNow
        });

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
    
    public async Task<IActionResult> Transfer(int id) => View(await _context.Account.FindAsync(id));
    
    [HttpPost]
    public async Task<IActionResult> Transfer(int id, decimal amount, int AccountNumber)
    {
        var account = await _context.Account.FindAsync(id);
        var DestinationAccount = await _context.Account.FindAsync(AccountNumber);

        if (DestinationAccount == null)
        {
            ModelState.AddModelError(nameof(AccountNumber), "Account does not exist.");
        }
        if (amount <= 0)
            ModelState.AddModelError(nameof(amount), "Amount must be positive.");
        if (amount.TwoDecimalPlacesCheck())
            ModelState.AddModelError(nameof(amount), "Amount cannot have more than 2 decimal places.");
        if (account.Transactions.CalculateAccountBalance() - (amount + Constants.TransferFee) < Constants.SavingMinimumBalance && account.AccountType == Constants.SavingAccType)
        {
            ModelState.AddModelError(nameof(amount), "Not enough funds for Transfer.");
        }
        if (account.Transactions.CalculateAccountBalance() - (amount + Constants.TransferFee) < Constants.CheckingMinimumBalance && account.AccountType == Constants.CheckingAccType)
        {
            ModelState.AddModelError(nameof(amount), "Not enough funds for Transfer.");
        }
        if (!ModelState.IsValid)
        {
            ViewBag.Amount = amount;
            ViewBag.AccontNumber = AccountNumber;
            return View(account);
        }

        account.Transactions.Add(
            new Transaction
            {
                TransactionType = Constants.Transfer,
                Amount = amount,
                DestinationAccountNumber = AccountNumber,
                TransactionTimeUtc = DateTime.UtcNow
            });
        account.Transactions.Add( new Transaction
        {
            TransactionType = Constants.Transfer,
            Amount = Constants.TransferFee,
            TransactionTimeUtc = DateTime.UtcNow
        });
        account.Transactions.Add( new Transaction
        {
            TransactionType = Constants.ServiceFee,
            Amount = Constants.TransferFee,
            TransactionTimeUtc = DateTime.UtcNow
        });

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
    
}