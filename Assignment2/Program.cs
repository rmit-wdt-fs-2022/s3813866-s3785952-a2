using Assignment2.BackgroundServices;
using Assignment2.Data;
using AssignmentClassLibrary.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//establish tables for model
builder.Services.AddDbContext<ModelDbContext>(options =>
{
    
    
    
    options.UseSqlServer(builder.Configuration.GetConnectionString("Main"),
        x => x.MigrationsAssembly(nameof(AssignmentClassLibrary)));
    options.UseLazyLoadingProxies();
});

builder.Services.AddHostedService<BillPayBackGroundService>();

//Store sessions
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => { options.Cookie.IsEssential = true; });


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        SeedData.SaveCustomerInDb(services);
        Console.WriteLine("done");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();