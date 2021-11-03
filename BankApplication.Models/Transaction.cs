using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  BankApplication.Models.Enums;

namespace BankApplication.Models
{
    public class Transaction
    {
        public Transaction(string TargetAccount, decimal Amount,TransactionType type, string Transactionid, DateTime datetime)
        {
            this.TargetAccount = TargetAccount;

            this.Amount = Amount;
            Type = type;
            TransactionId = Transactionid;
            dateTime = datetime;
        }

        public string TargetAccount { get; set; }
        public decimal Amount { get; set; }
        public DateTime dateTime { get; set; }
        public string TransactionId { get; set; }
        public TransactionType Type { get; set; }

    }
}
