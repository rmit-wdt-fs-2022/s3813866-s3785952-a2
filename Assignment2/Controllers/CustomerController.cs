using Assignment2.Data;
using Assignment2.Filters;
using Assignment2.Models;
using Assignment2.Models.ViewModels;
using Assignment2.Utility;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Assignment2.Controllers;

[CustomerAuth]
public class CustomerController : Controller
{
    private readonly ModelDbContext _context;

    public CustomerController(ModelDbContext context)
    {
        _context = context;
    }

    private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerId)).Value;

    public async Task<IActionResult> Index()
    {
        var customer = await _context.Customer.FindAsync(CustomerID);
        return View(customer);
    }

    public async Task<IActionResult> Deposit(int id)
    {
        var Account = await _context.Account.FindAsync(id);
        var viewModel = new DepositWithdrawViewModel
        {
            CurrentAccount = Account
        };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Deposit(DepositWithdrawViewModel deposit)
    {
        if (deposit.Amount <= 0)
            ModelState.AddModelError(nameof(deposit.Amount), "Amount must be positive.");
        if (deposit.Amount.TwoDecimalPlacesCheck())
            ModelState.AddModelError(nameof(deposit.Amount), "Amount cannot have more than 2 decimal places.");

        if (!ModelState.IsValid)
        {
            var Account = await _context.Account.FindAsync(deposit.AccountNum);
            var viewModel = new DepositWithdrawViewModel
            {
                CurrentAccount = Account
            };
            return View(viewModel);
        }

        return View("DepositConfirmation", deposit);
    }

    [HttpPost]
    public async Task<IActionResult> DepositConfirmation(DepositWithdrawViewModel deposit)
    {
        var Account = await _context.Account.FindAsync(deposit.AccountNum);
        if (!ModelState.IsValid)
        {
            ViewBag.Amount = deposit.Amount;
            var viewModel = new DepositWithdrawViewModel
            {
                CurrentAccount = Account
            };
            return View(viewModel);
        }

        Account.Transactions.Add(
            new Transaction
            {
                TransactionType = Constants.Deposit,
                Amount = deposit.Amount,
                Comment = deposit.Comment,
                TransactionTimeUtc = DateTime.UtcNow
            });


        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Withdraw(int id)
    {
        var Account = await _context.Account.FindAsync(id);
        var viewModel = new DepositWithdrawViewModel
        {
            CurrentAccount = Account
        };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Withdraw(DepositWithdrawViewModel withdraw)
    {
        var Account = await _context.Account.FindAsync(withdraw.AccountNum);
        if (withdraw.Amount <= 0)
            ModelState.AddModelError(nameof(withdraw.Amount), "Amount must be positive.");
        if (withdraw.Amount.TwoDecimalPlacesCheck())
            ModelState.AddModelError(nameof(withdraw.Amount), "Amount cannot have more than 2 decimal places.");
        if (Account.Transactions.CalculateAccountBalance() - (withdraw.Amount + Constants.WithdrawFee) <
            Constants.SavingMinimumBalance &&
            Account.AccountType == Constants.SavingAccType)
            ModelState.AddModelError(nameof(withdraw.Amount), "Not enough funds for withdraw.");
        if (Account.Transactions.CalculateAccountBalance() - (withdraw.Amount + Constants.WithdrawFee) <
            Constants.CheckingMinimumBalance &&
            Account.AccountType == Constants.CheckingAccType)
            ModelState.AddModelError(nameof(withdraw.Amount), "Not enough funds for withdraw.");
        if (!ModelState.IsValid)
        {
            var viewModel = new DepositWithdrawViewModel
            {
                CurrentAccount = Account
            };
            return View(viewModel);
        }

        return View("WithdrawConfirmation", withdraw);
    }

    [HttpPost]
    public async Task<IActionResult> WithdrawConfirmation(DepositWithdrawViewModel withdraw)
    {
        var account = await _context.Account.FindAsync(withdraw.AccountNum);


        if (!ModelState.IsValid)
        {
            ViewBag.Amount = withdraw.Amount;
            var viewModel = new DepositWithdrawViewModel
            {
                CurrentAccount = account
            };
            return View(viewModel);
        }

        account.Transactions.Add(
            new Transaction
            {
                TransactionType = Constants.Withdraw,
                Amount = withdraw.Amount,
                Comment = withdraw.Comment,
                TransactionTimeUtc = DateTime.UtcNow
            });
        if (account.Transactions.MoreThanTwoTransactions())
            account.Transactions.Add(new Transaction
            {
                TransactionType = Constants.ServiceFee,
                Amount = Constants.WithdrawFee,
                TransactionTimeUtc = DateTime.UtcNow
            });


        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Transfer(int id)
    {
        var Account = await _context.Account.FindAsync(id);
        var viewModel = new TransferViewModel
        {
            CurrentAccount = Account
        };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Transfer(TransferViewModel transfer)
    {
        var Account = await _context.Account.FindAsync(transfer.AccountNum);
        var destionationAccount = await _context.Account.FindAsync(transfer.DestinationAccountNum);

        if (destionationAccount == null) ModelState.AddModelError(nameof(transfer.Amount), "Account does not exist.");

        if (transfer.Amount <= 0)
            ModelState.AddModelError(nameof(transfer.Amount), "Amount must be positive.");
        if (transfer.Amount.TwoDecimalPlacesCheck())
            ModelState.AddModelError(nameof(transfer.Amount), "Amount cannot have more than 2 decimal places.");
        if (Account.Transactions.CalculateAccountBalance() - (transfer.Amount + Constants.TransferFee) <
            Constants.SavingMinimumBalance &&
            Account.AccountType == Constants.SavingAccType)
            ModelState.AddModelError(nameof(transfer.Amount), "Not enough funds for Transfer.");
        if (Account.Transactions.CalculateAccountBalance() - (transfer.Amount + Constants.TransferFee) <
            Constants.CheckingMinimumBalance &&
            Account.AccountType == Constants.CheckingAccType)
            ModelState.AddModelError(nameof(transfer.Amount), "Not enough funds for Transfer.");

        if (!ModelState.IsValid)
        {
            var viewModel = new TransferViewModel
            {
                CurrentAccount = Account
            };
            return View(viewModel);
        }

        transfer.DestinationAccount = destionationAccount;
        return View("TransferConfirmation", transfer);
    }

    [HttpPost]
    public async Task<IActionResult> TransferConfirmation(TransferViewModel transfer)
    {
        var account = await _context.Account.FindAsync(transfer.AccountNum);
        var DestinationAccount = await _context.Account.FindAsync(transfer.DestinationAccountNum);


        account.Transactions.Add(
            new Transaction
            {
                TransactionType = Constants.Transfer,
                Amount = transfer.Amount,
                Comment = transfer.Comment,
                DestinationAccountNumber = transfer.DestinationAccountNum,
                TransactionTimeUtc = DateTime.UtcNow
            });
        DestinationAccount.Transactions.Add(new Transaction
        {
            TransactionType = Constants.Transfer,
            Amount = transfer.Amount,
            Comment = transfer.Comment,
            TransactionTimeUtc = DateTime.UtcNow
        });

        if (account.Transactions.MoreThanTwoTransactions())
            account.Transactions.Add(new Transaction
            {
                TransactionType = Constants.ServiceFee,
                Amount = Constants.TransferFee,
                TransactionTimeUtc = DateTime.UtcNow
            });

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

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
        if (account == null)
            return RedirectToAction(nameof(Index));

        ViewBag.Account = account;

        // Page the orders, maximum of 4 transaction per page.
        const int pageSize = 4;
        var pagedList = await _context.Transaction.Where(x => x.AccountNumber == account.AccountNumber)
            .OrderByDescending(x => x.TransactionTimeUtc).ToPagedListAsync(page, pageSize);

        return View(pagedList);
    }
}