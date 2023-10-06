using Autofac;
using Autofac.Extensions.DependencyInjection;
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

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.Register((context, config) =>
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var dbContextOption = new DbContextOptionsBuilder<DemoDbContext>();
        return dbContextOption.UseNpgsql(connectionString, optionsBuilder =>
        {
            optionsBuilder.EnableRetryOnFailure(20);
            optionsBuilder.MigrationsAssembly(typeof(Program).Assembly.GetName().Name);
            optionsBuilder.CommandTimeout(9000);
        }).Options;
    }).SingleInstance();

    builder.Register((com, _) =>
        {
            var contextOptions = com.Resolve<DbContextOptions<DemoDbContext>>();

            return new DemoDbContext(contextOptions);
        }).AsImplementedInterfaces()
        .AsSelf()
        .InstancePerLifetimeScope();
});


builder.Services.AddDbContext<DemoDbContext>(opt =>
{
    opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), options =>
    {
        options.EnableRetryOnFailure(5);
        options.CommandTimeout(200);
    });
});

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

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();