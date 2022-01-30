using Assignment2.Data;
using Assignment2.Filters;
using Assignment2.Models;
using Assignment2.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using X.PagedList;

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
    public async Task<IActionResult> Deposit(int id, decimal amount, string? comment)
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
                Comment = comment,
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
    public async Task<IActionResult> Transfer(int id, decimal amount, int AccountNumber, string? comment)
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
                Comment = comment,
                DestinationAccountNumber = AccountNumber,
                TransactionTimeUtc = DateTime.UtcNow
            });
        DestinationAccount.Transactions.Add( new Transaction
        {
            TransactionType = Constants.Transfer,
            Amount = amount,
            Comment = comment,
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
    
    // public async Task<IActionResult> MyTransactions(int id) => View(await _context.Account.FindAsync(id));
    [HttpPost]
    public async Task<IActionResult> IndexToTransactions(int accountNum)
    {
        HttpContext.Session.SetInt32(nameof(Account.AccountNumber), accountNum);
        return RedirectToAction(nameof(MyTransactions));
    }
    
    public async Task<IActionResult> MyTransactions(int? page = 1)
    {
        var accountNum = HttpContext.Session.GetInt32(nameof(Account.AccountNumber));
        var account = await _context.Account.FindAsync(accountNum);
        if(account == null)
            return RedirectToAction(nameof(Index)); // OR return BadRequest();
        
        ViewBag.Account = account;

        // Page the orders, maximum of 3 per page.
        const int pageSize = 4;
        var pagedList = await _context.Transaction.Where(x => x.AccountNumber == account.AccountNumber).
            OrderByDescending(x => x.TransactionTimeUtc).ToPagedListAsync(page, pageSize);

        return View(pagedList);
    }
}