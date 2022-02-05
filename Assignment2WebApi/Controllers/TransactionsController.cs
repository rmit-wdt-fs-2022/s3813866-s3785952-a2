using Assignment2WebApi.Repositories;
using AssignmentClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly TransactionsRepository _transactionsRepository;

    
    public TransactionsController(TransactionsRepository transactionsRepository)
    {
        _transactionsRepository = transactionsRepository;
    }

    /*
     * Gets all the transaction from the DB
     */
    [HttpGet]
    public async Task<List<Transaction>> GetAllTransactions() => await _transactionsRepository.GetAllTransactions();

    
    /*
     * Gets all the transaction made by the account owner with accountNumber
     */
    [HttpGet]
    [Route("{accountNumber:int}")]
    public List<Transaction> GetTransactionsByAccountNumber(int accountNumber, DateTime? start, DateTime? end) =>
        _transactionsRepository.GetTransactionsByAccountNumber(accountNumber, start, end);
}
