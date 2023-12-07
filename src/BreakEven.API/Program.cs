using BreakEven.API.Data;
using BreakEven.API.Interfaces.Repositories;
using BreakEven.API.Interfaces.Services;
using BreakEven.API.Repositories;
using BreakEven.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var env = builder.Environment.EnvironmentName;
builder.Configuration 
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange:false)
    .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: false)
    .AddEnvironmentVariables();

// Run migrations
var connStr = builder.Configuration.GetConnectionString("AppDbContextConnectionString");
DbContextOptions options = new DbContextOptionsBuilder()
    .UseMySql(connStr, ServerVersion.AutoDetect(connStr))
    .Options;  

using (var dbContext = new AppDbContext(options))
{
    try
    {
        dbContext.Database.EnsureCreated();
        dbContext.Database.Migrate();
        Console.WriteLine("Database migrations completed.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error applying migrations: {ex.Message}");
    }
}

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseMySql(connStr, ServerVersion.AutoDetect(connStr)));
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IIRRFService, IRRFService>();
builder.Services.AddScoped<IINSSService, INSSService>();
builder.Services.AddScoped<IFGTSService,FGTSService>();
builder.Services.AddScoped<ITransportationAllowanceService, TransportationAllowanceService>();
builder.Services.AddScoped<IPaycheckService, PaycheckService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();