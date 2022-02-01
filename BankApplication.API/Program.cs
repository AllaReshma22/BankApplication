using Microsoft.EntityFrameworkCore;
using BankApplication.Models.Models;
using BankApplication.Service.Interfaces;
using BankApplication.Service;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BankAppContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BankApp")));
builder.Services.AddScoped<IBankStaffServiceInterface, BankStaffService>();
builder.Services.AddScoped<ICustomerServiceInterface, CustomerService>();
builder.Services.AddScoped<ICommonServiceInterface,BankStaffService>();
builder.Services.AddScoped<ICommonServiceInterface,CustomerService>();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
