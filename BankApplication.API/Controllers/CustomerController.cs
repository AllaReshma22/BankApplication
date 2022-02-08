using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankApplication.Models.Models;
using BankApplication.Service.Interfaces;
using AutoMapper;
using BankApplication.API.DTOs.Account;
namespace BankApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServiceInterface customerService;
        private readonly IMapper mapper;
        public CustomerController(ICustomerServiceInterface customerService, IMapper mapper)
        {
            this.customerService = customerService;
            this.mapper = mapper;
        }
        [HttpGet("Id")]
        public IActionResult GetAccount(string accountId,int? password)
        {
            if (customerService.Authenticate(accountId, password))
            {
                Account account = customerService.GetAccount(accountId);
                return Ok(mapper.Map<GetAccountDTO>(account));
            }
            else
                return BadRequest("Enter a valid password");
        }
        [HttpPut("Deposit")]
        public IActionResult Deposit(DepositorWithdrawAmountDTO depositorWithdrawAmountDTO)
        {
            decimal? balance=customerService.Deposit(depositorWithdrawAmountDTO.AccountId,depositorWithdrawAmountDTO.Amount);
            Account account = customerService.GetAccount(depositorWithdrawAmountDTO.AccountId);
            return Ok(mapper.Map<GetAccountDTO>(account));

        }
        [HttpPut("Withdraw")]
        public IActionResult withdraw(DepositorWithdrawAmountDTO depositorWithdrawAmountDTO)
        {
            decimal? balance = customerService.WithDraw(depositorWithdrawAmountDTO.AccountId, depositorWithdrawAmountDTO.Amount);
            Account account = customerService.GetAccount(depositorWithdrawAmountDTO.AccountId);
            return Ok(mapper.Map<GetAccountDTO>(account));

        }
        [HttpPost("Transfer")]
        public IActionResult Transfer(TransferAmountDTO transferAmountDTO)
        {
            string transactionId = customerService.TransferAmount(transferAmountDTO.senderAccountId, transferAmountDTO.receiverAccountId, transferAmountDTO.amount, transferAmountDTO.paymentMode);
            return Ok(customerService.GetTransaction(transactionId));
        }
        [HttpGet("Get Balance")]
        public IActionResult GetBalance(string accountId)
        {
            return Ok(customerService.GetBalance(accountId));
        }
        [HttpGet("Get Transactions")]
        public IActionResult GetTransactions(string accountId)
        {
            var transactionsList=customerService.TransactionHistory(accountId);
            return Ok(transactionsList);
        }


    }
}
