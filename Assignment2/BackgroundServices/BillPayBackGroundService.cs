using Assignment2.Data;
using Assignment2.Models;
using Assignment2.Utility;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.BackgroundServices;

public class BillPayBackGroundService : BackgroundService
{
    private readonly IServiceProvider _services;

    public BillPayBackGroundService(IServiceProvider services)
    {
        _services = services;
    }
        
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {

        while(!cancellationToken.IsCancellationRequested)
        {
            await DoWork(cancellationToken);


            await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
        }
    }

    private async Task DoWork(CancellationToken cancellationToken)
    {

        using var scope = _services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ModelDbContext>();
        
         foreach (var billPay  in context.BillPay.ToList())
        {
            var account = await context.Account.SingleAsync(bp => bp.AccountNumber==billPay.AccountNumber, cancellationToken);
            if (billPay.ScheduleTimeUtc < DateTime.UtcNow)
            {
                var balance = account.Transactions.CalculateAccountBalance();
                if (account.AccountType.Equals(Constants.CheckingAccType))
                {
                    if ((balance-300) >= billPay.Amount)
                    {
                        account.Transactions.Add(new Transaction
                        {
                            TransactionType = Constants.BillPay,
                            Amount = billPay.Amount,
                            TransactionTimeUtc = DateTime.UtcNow
                        });
                        if (billPay.Period == Period.Monthly)
                        {
                            billPay.ScheduleTimeUtc = billPay.ScheduleTimeUtc.AddMonths(1);
                        }
                        else
                        {
                            context.BillPay.Remove(billPay);
                        }
                    } else
                    {

                        account.Transactions.Add(new Transaction
                        {
                            TransactionType = Constants.Failed,
                            TransactionTimeUtc = DateTime.UtcNow,
                            Comment = "Failed Transaction"

                        });
                        context.BillPay.Remove(billPay);
                    }  
                    await context.SaveChangesAsync(cancellationToken);

                    
                } else if (account.AccountType.Equals(Constants.SavingAccType))
                {
                    if ((balance) >= billPay.Amount)
                    {
                        account.Transactions.Add(new Transaction
                        {
                            TransactionType = Constants.BillPay,
                            Amount = billPay.Amount,
                            TransactionTimeUtc = DateTime.UtcNow
                        });
                        if (billPay.Period == Period.Monthly)
                        {
                            billPay.ScheduleTimeUtc = billPay.ScheduleTimeUtc.AddMonths(1);
                        }
                        else
                        {
                            context.BillPay.Remove(billPay);
                        }
                        await context.SaveChangesAsync(cancellationToken);
                    } 
                    else
                    {

                        account.Transactions.Add(new Transaction
                        {
                            TransactionType = Constants.Failed,
                            TransactionTimeUtc = DateTime.UtcNow,
                            Comment = "Failed Transaction"

                        });
                        context.BillPay.Remove(billPay);
                    }  
                    await context.SaveChangesAsync(cancellationToken);
                    
                }
                
            } 
        }
        
        Console.WriteLine("I Have Ran");
    }
}