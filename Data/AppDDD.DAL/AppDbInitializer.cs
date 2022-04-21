using AppDDD.DAL.Context;
using AppDDD.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AppDDD.DAL;

public class AppDbInitializer
{
    private readonly AppDB _db;
    private readonly ILogger<AppDbInitializer> _Logger;

    public AppDbInitializer(AppDB db, ILogger<AppDbInitializer> Logger)
    {
        _db = db;
        _Logger = Logger;
    }

    public async Task<bool> RemoveAsync(CancellationToken Cancel = default)
    {
        if (await _db.Database.EnsureDeletedAsync(Cancel).ConfigureAwait(false))
        {
            _Logger.LogInformation("База данных удалена");
            return true;
        }

        _Logger.LogInformation("База данных отсутствует");
        return false;
    }

    public async Task InitializeAsync(bool RemoveBefore = false,  CancellationToken Cancel = default)
    {
        if (RemoveBefore)
            await RemoveAsync(Cancel).ConfigureAwait(false);

        await _db.Database.MigrateAsync(Cancel).ConfigureAwait(false);
        _Logger.LogInformation("БД создана и находится в актуальном состоянии");

        await AddTestDataAsync(Cancel);
    }

    private async Task AddTestDataAsync(CancellationToken Cancel = default)
    {
        if (!await _db.Employees.AnyAsync(Cancel).ConfigureAwait(false))
        {
            var deps = Enumerable.Range(1, 10)
               .Select(i => new Departament
                {
                    Name = $"Dep-{i}"
                })
               .ToArray();

            var rnd = new Random();

            var employees = Enumerable.Range(1, 100)
               .Select(i => new Employee
                {
                    LastName = $"LastName-{i}",
                    Name = $"Name-{i}",
                    Patronymic = $"Patronymic-{i}",
                    Salary = rnd.Next(50000, 80000),
                    Departament = deps[rnd.Next(0, deps.Length)],
                });

            await _db.Employees.AddRangeAsync(employees, Cancel);
            await _db.Departaments.AddRangeAsync(deps, Cancel);
            await _db.SaveChangesAsync(Cancel);
        }

        if (!await _db.Products.AnyAsync(Cancel))
        {
            var products = Enumerable.Range(1, 10)
               .Select(i => new Product
                {
                    Name = $"Product-{i}"
                });
            await _db.AddRangeAsync(products, Cancel);
            await _db.SaveChangesAsync(Cancel);
        }
    }
}
