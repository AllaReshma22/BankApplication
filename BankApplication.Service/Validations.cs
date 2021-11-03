using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Models;
using BankApplication.Models.Exceptions;

namespace BankApplication.Service
{
   public class Validations
    {
        public static bool StaffValidate(string bankId, string staffId)
        {
            var bank = Datastore.Banks.SingleOrDefault(m => m.BankId == bankId);
            if (bank is null)
                throw new IncorrectBankIdException();
            if (bank.StaffId == staffId)
                return true;
            else
                return false;           
        }
        public  static bool CustomerValidate(string bankId, string accountId,int password)
        {
            var bank = Datastore.Banks.SingleOrDefault(m => m.BankId == bankId);
            if (bank is null)
                throw new IncorrectBankIdException();
            var account = bank.AccountsList.SingleOrDefault(m => m.AccountId == accountId);
            if (account is null)
                throw new IncorrectAccountNumberException();
            if (account.Password == password)
                return true;
            else
                throw new IncorrectPin();           
        }
    }
}

