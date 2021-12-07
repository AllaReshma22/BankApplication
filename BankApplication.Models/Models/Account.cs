using System;
using System.Collections.Generic;

#nullable disable

namespace BankApplication.Models.Models
{
    public partial class Account
    {
        public Account(string accountId, string bankId, string accountHolderName, int? password, decimal? balance)
        {
            AccountId = accountId;
            AccountHolderName = accountHolderName;
            BankId = bankId;
            Password = password;
            Balance = balance;
            TransactionReceiverAccounts = new HashSet<Transaction>();
            TransactionSenderAccounts = new HashSet<Transaction>();
        }

        public string AccountId { get; set; }
        public string AccountHolderName { get; set; }
        public string BankId { get; set; }
        public int? Password { get; set; }
        public decimal? Balance { get; set; }

        public virtual Bank Bank { get; set; }
        public virtual ICollection<Transaction> TransactionReceiverAccounts { get; set; }
        public virtual ICollection<Transaction> TransactionSenderAccounts { get; set; }
    }
}
