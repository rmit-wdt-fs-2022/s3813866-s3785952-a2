using Assignment2WebApi.Repositories;
using AssignmentClassLibrary.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ModelDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Main"));
    options.UseLazyLoadingProxies();
});



builder.Services.AddScoped<CustomersRepository>();
builder.Services.AddScoped<TransactionsRepository>();


builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    // Configure JSON serialiser settings.
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
