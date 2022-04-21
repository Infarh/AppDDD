using System.Net.Http.Json;
using AppDDD.Domain.Entities;
using AppDDD.Interfaces;
using Microsoft.Extensions.Logging;

namespace AppDDD.WebAPI.Clients.Employees;

public class EmployeesClient : IRepositoryAsync<Employee>
{
    private readonly HttpClient _Client;
    private readonly ILogger<EmployeesClient> _Logger;

    public EmployeesClient(HttpClient Client, ILogger<EmployeesClient> Logger)
    {
        _Client = Client;
        _Logger = Logger;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync(CancellationToken Cancel = default)
    {
        var employees = await _Client
           .GetFromJsonAsync<IEnumerable<Employee>>("api/employees", Cancel)
           .ConfigureAwait(false);

        if (employees is null)
            throw new InvalidOperationException("Не удалось получить данные от сервиса");

        return employees;
    }

    public async Task<Employee?> GetByIdAsync(int id, CancellationToken Cancel = default)
    {
        var employee = await _Client
           .GetFromJsonAsync<Employee>($"api/employees/{id}", Cancel)
           .ConfigureAwait(false);
        return employee;
    }

    public async Task<int> CountAsync(CancellationToken Cancel = default)
    {
        var count = await _Client
           .GetFromJsonAsync<int>("api/employees/count", Cancel)
           .ConfigureAwait(false);
        return count;
    }

    public async Task<int> AddAsync(Employee item, CancellationToken Cancel = default) { throw new NotImplementedException(); }

    public async Task<bool> UpdateAsync(Employee item, CancellationToken Cancel = default) { throw new NotImplementedException(); }

    public async Task<Employee?> DeleteAsync(Employee item, CancellationToken Cancel = default) { throw new NotImplementedException(); }
}
