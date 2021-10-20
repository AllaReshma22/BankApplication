using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Models;
using BankApplication.Models.Exceptions;

namespace BankApplication.Service
{
    public class BankStaffActions
    {
        /*    private List<Bank> banks;
            public BankStaffActions()
            {
                this.banks = new List<Bank>();
            }
            public string addBank(string bankname)
            {
                Bank bank = new Bank
                {
                    BankId = bankname.Substring(0, 3) + DateTime.UtcNow.ToString("dd-MM-yyyy"),
                    BankName = bankname
                };
                this.banks.Add(bank);

                return bank.BankId;
            }*/
        Bank bank = new Bank();
        public string createAccount(string bankId, string AccountName, int password, double initialbal)
        {
        /*    Bank bank = this.banks.SingleOrDefault(m => m.BankId == bankId);
            if (bank is null)
                throw new IncorrectBankIdException();*/
            string AccountNumber = AccountName.Substring(0, 3) + DateTime.UtcNow.ToString("dd-MM-yyyy");
            Account account = new Account(AccountNumber, AccountName, password, initialbal);
            bank.AccountsList.Add(account);
            return account.AccountNumber;
        }
    }
}
