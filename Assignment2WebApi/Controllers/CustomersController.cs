using Assignment2WebApi.Repositories;
using AssignmentClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment2WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly CustomersRepository _customersRepository;

    public CustomersController(CustomersRepository customersRepository) => _customersRepository = customersRepository;

    [HttpGet]
    [Route("{customerId:int}")]
    public async Task<Customer> GetCustomerByCustomerId(int customerId) => await _customersRepository.GetCustomerByCustomerId(customerId);


    [HttpPut]
    public async Task UpdateCustomer([FromBody] Customer customer) => await _customersRepository.UpdateCustomer(customer);
}