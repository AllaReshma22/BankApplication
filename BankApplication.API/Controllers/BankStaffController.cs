using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankApplication.Service.Interfaces;
using BankApplication.Models.Models;
using AutoMapper;
using BankApplication.API.DTOs.Bank;
using BankApplication.API.DTOs.Account;

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
        [HttpGet("stafflogin")]
        public IActionResult AuthenticateStaff(AuthenticateStaffDTO authenticateStaffDTO)
        {
            if (bankStaffService.AuthenticateStaff(authenticateStaffDTO.BankId, authenticateStaffDTO.StaffId))
                return Ok("Logged in succesfully");
            else
                return BadRequest("Enter a valid staffId");
        }
        [HttpPost("register_new_account")]
        public IActionResult RegisterNewAccount(string bankId, [FromBody] CreateAccountDTO newAccount)
        {
            if (!ModelState.IsValid) return BadRequest(newAccount);
            //map to account
            string accId = bankStaffService.CreateAccount(bankId, newAccount.AccountHolderName, newAccount.Password, newAccount.InitialBalance);
            Account account = bankStaffService.GetAccount(accId);
            var accDTO = mapper.Map<GetAccountDTO>(account);
            return Ok(accDTO);
        }
        [HttpGet("get_account_by_number")]
        public IActionResult GetAccount(string AccountId)
        {
            var account = bankStaffService.GetAccount(AccountId);
            var accDTO = mapper.Map<GetAccountDTO>(account);
            return Ok(accDTO);
        }
        [HttpPost("update_account")]
        public IActionResult UpdateAccount([FromBody] UpdateAccountDTO dTO)
        {
            if (!ModelState.IsValid) return BadRequest(dTO);
                bankStaffService.UpdateAccountPassword(dTO.AccountId, dTO.Password);
                var account = bankStaffService.GetAccount(dTO.AccountId);
                var accDTO = mapper.Map<GetAccountDTO>(account);
                return Ok(accDTO);
       
        }
        [HttpPut("Update same bank RTGS")]
        public IActionResult UpdateSameBankRTGS(string bankId, UpdateSameBankRTGSDTO updateBankDTO)
        {
            if (!ModelState.IsValid) return BadRequest(updateBankDTO);
            bankStaffService.UpdateSameBankRtgs(bankId, updateBankDTO.SameBankRtgsCharges);
            var bank = bankStaffService.GetBank(bankId);
            return Ok(mapper.Map<GetBankDTO>(bank));

        }
        [HttpPut("Update same bank IMPS")]
        public IActionResult UpdateSameBankIMPS(string bankId, UpdateSameBankIMPSDTO updateBankDTO)
        {
            if (!ModelState.IsValid) return BadRequest(updateBankDTO);
            bankStaffService.UpdateSameBankImps(bankId, updateBankDTO.SameBankImpsCharges);
            var bank = bankStaffService.GetBank(bankId);
            return Ok(mapper.Map<GetBankDTO>(bank));
        }
        [HttpPut("Update other bank IMPS")]
        public IActionResult UpdateOtherBankIMPS(string bankId, UpdateOtherBankIMPSDTO updateBankDTO)
        {
            if (!ModelState.IsValid) return BadRequest(updateBankDTO);
            bankStaffService.UpdateOtherBankImps(bankId, updateBankDTO.OtherBankImpsCharges);
            var bank = bankStaffService.GetBank(bankId);
            return Ok(mapper.Map<GetBankDTO>(bank));
        }
        [HttpPut("Update other bank RTGS")]
        public IActionResult UpdateOtherBankRTGS(string bankId, UpdateOtherBankRTGSDTO updateBankDTO)
        {
            if (!ModelState.IsValid) return BadRequest(updateBankDTO);
            bankStaffService.UpdateOtherBankRtgs(bankId, updateBankDTO.OtherBankRtgsCharges);
            var bank = bankStaffService.GetBank(bankId);
            return Ok(mapper.Map<GetBankDTO>(bank));
        }
        [HttpPut("Revert transaction")]
        public IActionResult RevertTransaction(string transactionId, [FromBody] AuthenticateStaffDTO authenticateStaffDTO)
        {
            if (bankStaffService.RevertTransaction(transactionId))
                return Ok("Transaction is reverted successfully");
            else
                return BadRequest("Enter a valid TransactionId");
        }


    }
}