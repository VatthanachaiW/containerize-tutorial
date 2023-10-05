using Demo.API.Contexts;
using Demo.API.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Serilog;

namespace Demo.API.BackgroundServices;

public class DatabaseMigrationService : IHostedService, IDisposable
{
    private bool _disposed = false;
    private readonly IDemoDbContext _context;

    public DatabaseMigrationService(IDemoDbContext context)
    {
        _context = context;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            await _context.Database.MigrateAsync(cancellationToken);
            await DefaultDataSeederAsync();
        }
        catch (PostgresException ex)
        {
            Log.Error(ex, "Error on migration: [{ex}]");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error on migration: [{ex}]");
        }
    }

    private async Task DefaultDataSeederAsync()
    {
        try
        {
            await _context.Set<Province>().AddRangeAsync(new List<Province>
            {
                new()
                {
                    ProvinceName = "กรุงเทพมหานคร",
                    IsActivated = true
                },
                new()
                {
                    ProvinceName = "ปทุมธานี",
                    IsActivated = true
                },
            });

            await _context.SaveChangesAsync();
        }
        catch (PostgresException ex)
        {
            Log.Error(ex, "Error on database seeder: [{ex}]");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error on database seeder: [{ex}]");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Log.Information("Background service successful");
        return Task.CompletedTask;
    }


    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context?.Dispose();
            }

            _disposed = true;
        }
    }

    ~DatabaseMigrationService()
    {
        Dispose(false);
    }
}