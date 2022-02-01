using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankApplication.Service.Interfaces;
using BankApplication.Models.Models;
using AutoMapper;
using BankApplication.API.DTOs.BankStaff;

namespace BankApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankStaffController : ControllerBase
    {
        //Inject automapper and bankstaffservice
        private readonly IBankStaffServiceInterface bankStaffService;
        private readonly IMapper mapper;
        public BankStaffController(IBankStaffServiceInterface bankStaffService, IMapper mapper)
        {
            this.bankStaffService = bankStaffService;
            this.mapper = mapper;
        }
       /* [HttpGet("accountId")]
        public IActionResult GetBalance(string accountId)
        {
            var acc =bankStaffService.GetAccount(accountId);
            if (bankStaffService.GetAccount( accountId) == null)
                return BadRequest();
            var accDTO = mapper.Map<GetAccountBalanceDTO>(acc);
            return Ok(accDTO);
        }*/
        [HttpPost("register_new_account")]
        public IActionResult RegisterNewAccount(string bankId, [FromBody] CreateAccountDTO newAccount)
        {
            if (!ModelState.IsValid) return BadRequest(newAccount);
            //map to account
            var acc = mapper.Map<Account>(newAccount);
            return Ok(bankStaffService.CreateAccount(bankId, newAccount.AccountHolderName, newAccount.Password, newAccount.InitialBalance));

        }
        [HttpGet("get_account_by_number")]
        public IActionResult GetAccount(string AccountNumber)
        {
            var acc=bankStaffService.GetAccount(AccountNumber);
            var accDTO=mapper.Map<GetAccountDTO>(acc);
            return Ok(accDTO);
        }
        [HttpPost("update_account")]
        public IActionResult UpdateAccount([FromBody] UpdateAccountDTO dTO)
        {
            if (!ModelState.IsValid) return BadRequest(dTO);
            var acc=mapper.Map<Account>(dTO);
            return Ok(bankStaffService.UpdateAccountPassword(acc,dTO.AccountId,dTO.Password));
        }








        }


    }