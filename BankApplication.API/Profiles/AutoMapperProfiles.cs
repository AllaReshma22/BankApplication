using AutoMapper;
using BankApplication.API.DTOs.Bank;
using BankApplication.API.DTOs.Account;
using BankApplication.Models.Models;

namespace BankApplication.API.Profiles
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CreateBankDTO, Bank>();
            CreateMap<UpdateSameBankRTGSDTO, Bank>();
            CreateMap<UpdateSameBankIMPSDTO, Bank>();
            CreateMap<UpdateOtherBankIMPSDTO, Bank>();
            CreateMap<UpdateOtherBankRTGSDTO, Bank>();
            CreateMap<AuthenticateStaffDTO, Bank>();
            CreateMap<Bank,GetBankDTO>();
            CreateMap<CreateAccountDTO, Account>();
            CreateMap<UpdateAccountDTO, Account>(); 
            CreateMap<AuthenticateAccountDTO, Account>();
            CreateMap<UpdateAccountBalanceDTO, Account>();
            CreateMap< Account,GetAccountBalanceDTO>();
            CreateMap<Account,GetAccountDTO>();


        }

    }
}
