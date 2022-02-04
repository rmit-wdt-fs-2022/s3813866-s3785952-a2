using Assignment2.Data;
using Assignment2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2.Controllers;

public class BillPayController : Controller
{
    private readonly ModelDbContext _context;
    
    public BillPayController(ModelDbContext context) => _context = context;
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var payee = _context.Payee.ToList();
        var billpay = _context.BillPay.ToList();

        var viewModel = new BillPayTableViewModel
        {
            Payees = payee,
            BillPays = billpay
        };
        return View(viewModel);
    }
    
    
    
    
    
    
    
    
}