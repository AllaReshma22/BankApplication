using AutoMapper;
using BankApplication.API.DTOs.BankStaff;
using BankApplication.API.DTOs.Customer;
using BankApplication.Models.Models;

namespace BankApplication.API.Profiles
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CreateBankDTO, Bank>();
            CreateMap<UpdateBankDTO, Bank>();
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
