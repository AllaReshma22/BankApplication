using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Models;
using BankApplication.Models.Exceptions;
using BankApplication.Models.Models;

namespace BankApplication.Service
{
    public class Validations
    {
        private readonly BankAppContext bankAppContext;
        public Validations(BankAppContext bankAppContext)
        {
            this.bankAppContext = bankAppContext;
        }
        public bool StaffValidate(string bankId, string staffId)
        {
            var bank = bankAppContext.Banks.FirstOrDefault(m => m.BankId == bankId);
            if (bank == null)
            {
                throw new IncorrectBankIdException();
            }
            if (bank.StaffId == staffId)
                return true;
            else
                throw new IncorrectStaffIdException();
        }
        public bool CustomerValidate(string accountId, int password)
        {
            var account = bankAppContext.Accounts.FirstOrDefault(m => m.AccountId == accountId);
            if (account == null)
            {
                throw new IncorrectAccountNumberException();
            }
            if (account.Password == password)
            {
                bankAppContext.Accounts.Attach(account);
                return true;
            }

            else
                throw new IncorrectPin();
        }
        public static bool PasswordValidate(int password)
        {
            if (password > 999 && password < 10000)
                return true;
            else
                return false;
        }
    }
}
