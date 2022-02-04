using Assignment2.Data;
using Assignment2.Models;
using Assignment2.Models.ViewModels;
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
        
            _context.Add(new BillPay
            {
                AccountNumber = viewModel.SelectedAccountNumber,
                PayeeId = viewModel.PayeeId,
                Amount = viewModel.Amount,
                ScheduleTimeUtc = viewModel.ScheduleTimeUtc,
                Period = (Period) viewModel.Period
            });

            await _context.SaveChangesAsync();

            ModelState.Clear();

        return RedirectToAction("Index", "Customer");
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
            BillPay = billPay
        };

        return View("EditBillPay", model);
    }

    [HttpPost]
    public async Task<IActionResult> EditBillPay(BillPay model)
    {
        var billPay = await _context.BillPay.FindAsync(model.BillPayId);

        if (ModelState.IsValid)
        {
            billPay.AccountNumber = model.AccountNumber;
            billPay.PayeeId = model.PayeeId;
            billPay.Amount = model.Amount;
            billPay.ScheduleTimeUtc = model.ScheduleTimeUtc;
            billPay.Period = model.Period;

            await _context.SaveChangesAsync();

            ModelState.Clear();

            return View("EditBillPay");
        }

        return View("EditBillPay");
    }
    public async Task<IActionResult> Home()
    {
        return RedirectToAction("Index", "Customer");
    }
}