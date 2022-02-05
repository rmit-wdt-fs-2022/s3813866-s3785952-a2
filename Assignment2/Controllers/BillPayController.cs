using AssignmentClassLibrary;
using Assignment2.ViewModels;
using AssignmentClassLibrary.Data;
using AssignmentClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers;

public class BillPayController : Controller
{
    private readonly ModelDbContext _context;

    public BillPayController(ModelDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int id)
    {
        var payee = _context.Payee.ToList();
        var billpay = _context.BillPay.ToList();

        var viewModel = new BillPayViewModel
        {
            SelectedAccountNumber = id,
            Payees = payee,
            BillPays = billpay
        };
        return View(viewModel);
    }

    public async Task<IActionResult> CreateNewBillPay (BillPayViewModel viewModel)
    {
        var payee = await _context.Payee.FindAsync(viewModel.PayeeId);
        if (payee == null) ModelState.AddModelError(nameof(viewModel.PayeeId), "Payee does not exist.");
        if (viewModel.ScheduleTimeUtc < DateTime.UtcNow) ModelState.AddModelError(nameof(viewModel.ScheduleTimeUtc), "Cannot Set Date in the past");

        if (ModelState.IsValid)
        {
            _context.Add(new BillPay
            {
                AccountNumber = viewModel.SelectedAccountNumber,
                PayeeId = viewModel.PayeeId,
                Amount = viewModel.Amount,
                ScheduleTimeUtc = viewModel.ScheduleTimeUtc.ToUniversalTime(),
                Period = (Period) viewModel.Period
            });

            await _context.SaveChangesAsync();

            ModelState.Clear();

            return RedirectToAction("Index", "Customer");
        }

        return View("CreateBillPay", viewModel);

    }
    public async Task<IActionResult> CreateBillPay(BillPayViewModel viewModel)
    {
        return View(viewModel);
    }
    
    
    [HttpGet]
    public async Task<IActionResult> EditBillPay(int billPayId)
    {
        
        var billPay = await _context.BillPay.FindAsync(billPayId);

        var model = new BillPayViewModel
        {
            BillPayId = billPay.BillPayId,
            PayeeId = billPay.PayeeId,
            AccountNumber = billPay.AccountNumber,
            Amount = billPay.Amount,
            ScheduleTimeUtc = billPay.ScheduleTimeUtc.ToLocalTime(),
            Period = billPay.Period
        };

        return View("EditBillPay", model);
    }

    [HttpPost]
    public async Task<IActionResult> EditBillPay(BillPayViewModel model)
    {
        var billPay = await _context.BillPay.FindAsync(model.BillPayId);
        var destinationAccount = await _context.Account.FindAsync(model.AccountNumber);
        var payee = await _context.Payee.FindAsync(model.PayeeId);
        
        
        if (destinationAccount == null) ModelState.AddModelError(nameof(billPay.AccountNumber), "Account does not exist.");
        if (payee == null) ModelState.AddModelError(nameof(billPay.PayeeId), "Payee does not exist.");
        if (model.ScheduleTimeUtc < DateTime.UtcNow) ModelState.AddModelError(nameof(model.ScheduleTimeUtc), "Cannot Set Date in the past");


        if (ModelState.IsValid)
        {
            billPay.AccountNumber = model.AccountNumber;
            billPay.PayeeId = model.PayeeId;
            billPay.Amount = model.Amount;
            billPay.ScheduleTimeUtc = model.ScheduleTimeUtc.ToUniversalTime();
            billPay.Period = model.Period;

            await _context.SaveChangesAsync();
            Home();
        }

        return View("EditBillPay", model);
    }

    public async Task<RedirectToActionResult> CancelBillPay(int billPayId)
    {
        var billPay = await _context.BillPay.FindAsync(billPayId);

        _context.BillPay.Remove(billPay);

        await _context.SaveChangesAsync();
        
        return RedirectToAction("Index", "Customer");
    }
    public async Task<IActionResult> Home()
    {
        return RedirectToAction("Index", "Customer");
    }
}