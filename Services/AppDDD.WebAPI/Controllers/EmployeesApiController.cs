using AppDDD.DAL.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppDDD.WebAPI.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeesApiController : ControllerBase
    {
        public EmployeesApiController(AppDB db)
        {
            
        }
    }
}
