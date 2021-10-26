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
        protected List<Bank> banks;
        public Validations()
        {
            this.banks = new List<Bank>();
        }
        public string addBank(string bankname)
        {
            Bank bank = new Bank
            {
                BankId = bankname.Substring(0, 3) + DateTime.UtcNow.ToString("dd-MM-yyyy"),
                BankName = bankname,
                staffId= bankname.Substring(0, 3)+"staff"

            };
            this.banks.Add(bank);

            return bank.BankId;
        }
        
        public bool StaffValidate(string bankid, string staffid)
        {
            var bank = this.banks.SingleOrDefault(m => m.BankId == bankid);
            if (bank is null)
                throw new IncorrectBankIdException();
            if (bank.staffId == staffid)
                return true;
            else
                return false;           
        }
        public bool CustomerValidate(string bankid, string accountnumber,int password)
        {
            var bank = this.banks.SingleOrDefault(m => m.BankId == bankid);
            if (bank is null)
                throw new IncorrectBankIdException();
            var account = bank.AccountsList.SingleOrDefault(m => m.AccountNumber == accountnumber);
            if (account is null)
                throw new IncorrectAccountNumberException();
            if (account.Password == password)
                return true;
            else
                throw new IncorrectPin();           
        }

    }
}

