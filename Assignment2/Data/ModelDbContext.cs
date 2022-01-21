using Assignment2.Models;
using Microsoft.EntityFrameworkCore;
namespace Assignment2.Data;


public class ModelDbContext : DbContext
{
    public ModelDbContext(DbContextOptions<ModelDbContext> options) : base(options)
    { }
    
    public DbSet<Customer> Customer { get; set; }
    public DbSet<Login> Login { get; set; }
    public DbSet<Account> Account { get; set; }
    public DbSet<Transaction> Transaction { get; set; }
    public DbSet<BillPay> BillPay { get; set; }
    public DbSet<Payee> Payee { get; set; }

}