using AppDDD.DAL.Context;
using AppDDD.Domain.Entities;
using AppDDD.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppDDD.WebAPI.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeesApiController : ControllerBase
    {
        private readonly IRepositoryAsync<Employee> _Employees;
        private readonly ILogger<EmployeesApiController> _Logger;

        public EmployeesApiController(IRepositoryAsync<Employee> Employees, ILogger<EmployeesApiController> Logger)
        {
            _Employees = Employees;
            _Logger = Logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _Employees.GetAllAsync().ConfigureAwait(true);

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _Employees.GetByIdAsync(id);
            if (employee is null)
                return NotFound();
            return Ok(employee);
        }

        [HttpGet("count")]
        public async Task<IActionResult> Count()
        {
            var result = await _Employees.CountAsync();
            return Ok(result);
        }
    }
}
