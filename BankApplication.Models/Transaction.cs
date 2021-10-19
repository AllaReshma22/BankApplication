using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Models
{
    public class Transaction
    {
        public Transaction(string TargetAccount, double Amount, TransactionType.transactionType type, string Transactionid, DateTime datetime)
        {
            this.TargetAccount = TargetAccount;

            this.Amount = Amount;
            Type = type;
            TransactionId = Transactionid;
            dateTime = datetime;
        }

        public string TargetAccount { get; set; }
        public double Amount { get; set; }
        public DateTime dateTime { get; set; }
        public string TransactionId { get; set; }
        public TransactionType.transactionType Type { get; set; }

    }
}
