using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankApplication.Service.Interfaces;
using BankApplication.Models.Models;
using AutoMapper;
using BankApplication.API.DTOs.Bank;
using BankApplication.API.DTOs.Account;
using BankApplication.Models.Exceptions;

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
        [HttpGet("auth/{bankId}")]
        public IActionResult AuthenticateStaff(AuthenticateStaffDTO authenticateStaffDTO)
        {
            try
            {
                bankStaffService.AuthenticateStaff(authenticateStaffDTO.BankId, authenticateStaffDTO.StaffId);
                return Ok("Logged in succesfully");
            }
            catch (IncorrectBankIdException)
            {
                return BadRequest("Enter a valid bankId");
            }
            catch (IncorrectStaffIdException)
            {
                return BadRequest("Enter a valid staffId");
            }
        }
        [HttpPost("newaccount/{bankId}")]
        public IActionResult RegisterNewAccount(string bankId, [FromBody] CreateAccountDTO newAccount)
        {
            if (!ModelState.IsValid) return BadRequest(newAccount);
            //map to account
            string accId = bankStaffService.CreateAccount(bankId, newAccount.AccountHolderName, newAccount.Password, newAccount.InitialBalance);
            Account account = bankStaffService.GetAccount(accId);
            var accDTO = mapper.Map<GetAccountDTO>(account);
            return Ok(accDTO);
        }
        [HttpGet("GetBank/{bankId}")]
        public IActionResult GetBank(string BankId)
        {
            try
            {
                var bank = bankStaffService.GetBank(BankId);
                var bankDTO = mapper.Map<GetBankDTO>(bank);
                return Ok(bankDTO);
            }
            catch (IncorrectBankIdException)
            {
                return BadRequest("Enter a valid bankId");
            }
        }



        [HttpGet("{bankId}/{id}")]
        public IActionResult GetAccount(string AccountId)
        {
            try
            {
                var account = bankStaffService.GetAccount(AccountId);
                var accDTO = mapper.Map<GetAccountDTO>(account);
                return Ok(accDTO);
            }
            catch(IncorrectAccountIdException)
            {
                return BadRequest("Enter a valid Account Id");
            }
        }
        [HttpPost("UpdateAccount/{id}")]
        public IActionResult UpdateAccount([FromBody] UpdateAccountDTO dTO)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(dTO);
                bankStaffService.UpdateAccountPassword(dTO.AccountId, dTO.Password);
                var account = bankStaffService.GetAccount(dTO.AccountId);
                var accDTO = mapper.Map<GetAccountDTO>(account);
                return Ok(accDTO);
            }
            catch (IncorrectAccountIdException)
            {
                return BadRequest("Enter a valid Account Id");
            }
       
        }
        [HttpPut("UpdateSameBankRTGS/{bankId}")]
        public IActionResult UpdateSameBankRTGS(string bankId, UpdateSameBankRTGSDTO updateBankDTO)
        {
            if (!ModelState.IsValid) return BadRequest(updateBankDTO);
            bankStaffService.UpdateSameBankRtgs(bankId, updateBankDTO.SameBankRtgsCharges);
            var bank = bankStaffService.GetBank(bankId);
            return Ok(mapper.Map<GetBankDTO>(bank));

        }
        [HttpPut("UpdateSameBankIMPS/{bankId}")]
        public IActionResult UpdateSameBankIMPS(string bankId, UpdateSameBankIMPSDTO updateBankDTO)
        {
            if (!ModelState.IsValid) return BadRequest(updateBankDTO);
            bankStaffService.UpdateSameBankImps(bankId, updateBankDTO.SameBankImpsCharges);
            var bank = bankStaffService.GetBank(bankId);
            return Ok(mapper.Map<GetBankDTO>(bank));
        }
        [HttpPut("UpdateOtherBankIMPS/{bankId}")]
        public IActionResult UpdateOtherBankIMPS(string bankId, UpdateOtherBankIMPSDTO updateBankDTO)
        {
            if (!ModelState.IsValid) return BadRequest(updateBankDTO);
            bankStaffService.UpdateOtherBankImps(bankId, updateBankDTO.OtherBankImpsCharges);
            var bank = bankStaffService.GetBank(bankId);
            return Ok(mapper.Map<GetBankDTO>(bank));
        }
        [HttpPut("UpdateOtherBankRTGS/{bankId}")]
        public IActionResult UpdateOtherBankRTGS(string bankId, UpdateOtherBankRTGSDTO updateBankDTO)
        {
            if (!ModelState.IsValid) return BadRequest(updateBankDTO);
            bankStaffService.UpdateOtherBankRtgs(bankId, updateBankDTO.OtherBankRtgsCharges);
            var bank = bankStaffService.GetBank(bankId);
            return Ok(mapper.Map<GetBankDTO>(bank));
        }
        [HttpPut("RevertTransaction")]
        public IActionResult RevertTransaction(string transactionId, [FromBody] AuthenticateStaffDTO authenticateStaffDTO)
        {
            if (bankStaffService.RevertTransaction(transactionId))
                return Ok("Transaction is reverted successfully");
            else
                return BadRequest("Enter a valid TransactionId");
        }


    }
}