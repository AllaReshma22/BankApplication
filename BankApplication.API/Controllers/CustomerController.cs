using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankApplication.Models.Models;
using BankApplication.Service.Interfaces;

namespace BankApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServiceInterface customerService;
        public CustomerController(ICustomerServiceInterface customerService)
        {
          this.customerService = customerService;
        }
        
    }
}
