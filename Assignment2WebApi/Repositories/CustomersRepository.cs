using AssignmentClassLibrary.Data;
using AssignmentClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment2WebApi.Repositories;

public class CustomersRepository
{
    private readonly ModelDbContext _dbContext;

    public CustomersRepository(ModelDbContext dbContext) => _dbContext = dbContext;

    public async Task<Customer> GetCustomerByCustomerId(int customerId) => await _dbContext.Customer.SingleAsync(x => x.CustomerId == customerId);

    public async Task UpdateCustomer(Customer customer)
    {
        _dbContext.Customer.Update(customer);
        await _dbContext.SaveChangesAsync();
    }

}

