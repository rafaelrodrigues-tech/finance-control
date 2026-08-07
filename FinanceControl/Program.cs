using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using FinanceControl.Data;
using FinanceControl.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<FinanceControlDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<ExpenseService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapSwaggerUI();
    app.MapSwagger();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
