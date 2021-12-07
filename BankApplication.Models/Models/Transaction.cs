using System;
using System.Collections.Generic;

#nullable disable

namespace BankApplication.Models.Models
{
    public partial class Transaction
    {
        public Transaction(string transactionId, string senderAccountId, string receiverAccountId, decimal? amount, string transactionType)
        {
            TransactionId = transactionId;
            SenderAccountId = senderAccountId;
            ReceiverAccountId = receiverAccountId;
            Amount = amount;
            TransactionType = transactionType;
        }
        public string TransactionId { get; set; }
        public string SenderAccountId { get; set; }
        public string ReceiverAccountId { get; set; }
        public decimal? Amount { get; set; }
        public string TransactionType { get; set; }

        public virtual Account ReceiverAccount { get; set; }
        public virtual Account SenderAccount { get; set; }
    }
}
