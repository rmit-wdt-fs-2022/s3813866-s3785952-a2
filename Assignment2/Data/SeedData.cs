﻿using Assignment2.DTO;
using Assignment2.Models;
using Newtonsoft.Json;

namespace Assignment2.Data;

public class SeedData
{
    public static void SaveCustomerInDb(IServiceProvider serviceProvider)
    {
        
        var context = serviceProvider.GetRequiredService<ModelDbContext>();
    
        
        Console.WriteLine("here");
        if(context.Customer.Any())
            return; // DB has already been seeded.
        Console.WriteLine("here1");
        const string Url = "https://coreteaching01.csit.rmit.edu.au/~e103884/wdt/services/customers/";
    
        
        using var client = new HttpClient();
        var json = client.GetStringAsync(Url).Result;
        // Convert JSON into objects.
        var customersDTO = JsonConvert.DeserializeObject<List<CustomerDTO>>(json, new JsonSerializerSettings
        {
            DateFormatString = "dd/MM/yyyy hh:mm:ss tt"
        });


        //using DTOs to seed into DB
        var customers = customersDTO.Select(customersDTOs => new Customer
        {
            CustomerId = customersDTOs.CustomerId,
            Name = customersDTOs.Name,
            Address = customersDTOs.address,
            Suburb = customersDTOs.City,
            PostCode = customersDTOs.Postcode,
            Accounts = customersDTOs.Accounts.Select(accountDTO => new Account
            {
                AccountNumber = accountDTO.AccountNumber,
                AccountType = accountDTO.AccountType,
                CustomerId = accountDTO.CustomerId,
                Transactions = accountDTO.Transactions.Select(transactionDTO => new Transaction
                {
                    TransactionType = 'D',
                    AccountNumber = accountDTO.AccountNumber,
                    Amount = transactionDTO.Amount,
                    Comment = transactionDTO.Comment,
                    TransactionTimeUtc = transactionDTO.TransactionTimeUtc
                }).ToList()
            }).ToList(),
            Login = new Login
            {
                LoginId = customersDTOs.Login.LoginId,
                CustomerId = customersDTOs.CustomerId,
                PasswordHash = customersDTOs.Login.PasswordHash
            }
        });

        foreach (var customer in customers)
        {
            context.Customer.Add(customer);
            context.Login.Add(customer.Login);
            foreach (var account in customer.Accounts)
            {
                context.Account.Add(account);
                foreach (var transaction in account.Transactions)
                {
                    context.Transaction.Add(transaction);
                }
            }
        
        }

        context.SaveChanges();
        //seed other tables I.E BillPay and Payee

    }
}