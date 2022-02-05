
using AssignmentClassLibrary.Data;
using AssignmentClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment2WebApi.Repositories;

public class TransactionsRepository
{
    private readonly ModelDbContext _dbContext;

    public TransactionsRepository(ModelDbContext dbContext) => _dbContext = dbContext;

    public async Task<List<Transaction>> GetAllTransactions() => await _dbContext.Transaction.ToListAsync();

    public List<Transaction> GetTransactionsByAccountNumber(int accountNumber, DateTime? start, DateTime? end)
    {
        var filterFunction = (Transaction x) =>
        {
            var matches = x.AccountNumber == accountNumber;

            if (matches)
            {
                if (start.HasValue)
                {
                    matches &= x.TransactionTimeUtc >= start.Value;
                }
                if (end.HasValue)
                {
                    matches &= x.TransactionTimeUtc <= end.Value;
                }
            }

            return matches;
        };

        return _dbContext.Transaction.Where(filterFunction).ToList();
    }
} 