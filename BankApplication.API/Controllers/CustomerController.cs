using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankApplication.Models.Models;
using BankApplication.Service.Interfaces;
using AutoMapper;
using BankApplication.API.DTOs.Account;
using BankApplication.Models.Exceptions;
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
        [HttpGet("{accountId}")]
        public IActionResult GetAccount(string accountId,int? password)
        {
            try
            {
                customerService.Authenticate(accountId, password);
                Account account = customerService.GetAccount(accountId);
                return Ok(mapper.Map<GetAccountDTO>(account));
            }
            catch (IncorrectPin) 
            {
                return BadRequest("Enter a valid password");
            }
            catch (IncorrectAccountIdException) 
            {
                return BadRequest("Enter correct account id");
            }
        }
        [HttpPut("Deposit/{id}")]
        public IActionResult Deposit(DepositorWithdrawAmountDTO depositorWithdrawAmountDTO)
        {
            decimal? balance=customerService.Deposit(depositorWithdrawAmountDTO.AccountId,depositorWithdrawAmountDTO.Amount);
            Account account = customerService.GetAccount(depositorWithdrawAmountDTO.AccountId);
            return Ok(mapper.Map<GetAccountDTO>(account));

        }
        [HttpPut("Withdraw/{id}")]
        public IActionResult withdraw(DepositorWithdrawAmountDTO depositorWithdrawAmountDTO)
        {
            try
            {
                decimal? balance = customerService.WithDraw(depositorWithdrawAmountDTO.AccountId, depositorWithdrawAmountDTO.Amount);
                Account account = customerService.GetAccount(depositorWithdrawAmountDTO.AccountId);
                return Ok(mapper.Map<GetAccountDTO>(account));
            }
            catch (AmountNotSufficient)
            {
                return BadRequest("Amount not sufficient");
            }

        }
        [HttpPost("Transfer/{id}")]
        public IActionResult Transfer(TransferAmountDTO transferAmountDTO)
        {
            string transactionId = customerService.TransferAmount(transferAmountDTO.senderAccountId, transferAmountDTO.receiverAccountId, transferAmountDTO.amount, transferAmountDTO.paymentMode);
            return Ok(customerService.GetTransaction(transactionId));
        }
        [HttpGet("Get Balance/{accountId}")]
        public IActionResult GetBalance(string accountId)
        {
            return Ok(customerService.GetBalance(accountId));
        }
        [HttpGet("GetTransactions/{accountId}")]
        public IActionResult GetTransactions(string accountId)
        {
            var transactionsList=customerService.TransactionHistory(accountId);
            return Ok(transactionsList);
        }


    }
}
