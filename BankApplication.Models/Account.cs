using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Models
{
    public class Account
    {
        public Account(int accountNumber,string accountid, string accountHolderName, int password, decimal balance)
        {
            AccountNumber = accountNumber;
            AccountId = accountid;
            AccountHolderName = accountHolderName;
            Password = password;
            Balance = balance;
            Transactions = new List<Transaction>();
        }

        public int AccountNumber { get; set; }
        public string AccountHolderName { get; set; }

        public int Password { get; set; }
        public decimal Balance { get; set; }
        public string AccountId { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}
