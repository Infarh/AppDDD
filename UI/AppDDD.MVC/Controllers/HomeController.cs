using System.Diagnostics;
using AppDDD.Domain.Entities;
using AppDDD.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AppDDD.MVC.Models;

namespace AppDDD.MVC.Controllers;

public class HomeController : Controller
{
    private readonly IRepositoryAsync<Employee> _Employees;
    private readonly ILogger<HomeController> _logger;

    public HomeController(IRepositoryAsync<Employee> Employees, ILogger<HomeController> logger)
    {
        _Employees = Employees;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var employees = await _Employees.GetAllAsync();
        return View(employees);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
