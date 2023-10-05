using Demo.API.BackgroundServices;
using Demo.API.Contexts;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration as IConfiguration;

// Add services to the container.
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddDbContext<DemoDbContext>(opt =>
{
    opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), options =>
    {
        options.EnableRetryOnFailure(5);
        options.CommandTimeout(200);
    });
});

builder.Services.AddScoped<IDemoDbContext, DemoDbContext>();

builder.Services.AddHostedService<DatabaseMigrationService>();

builder.Services.AddControllers();
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