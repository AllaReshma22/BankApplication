using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Models;
using BankApplication.Models.Models;

namespace BankApplication.Service.Interfaces
{
   public  interface ICustomerServiceInterface
   {
        public decimal? Deposit(string accountId, decimal amount);
        public bool Authenticate(string accountId,int? password);
        public  Account GetAccount(string accountId);
        
        public decimal? WithDraw(string accountId, decimal amount);
        public decimal? GetBalance(string accountId);
        public string TransferAmount(string senderAccountId, string receiverAccountId, decimal amount,string paymentMode);
        public Transaction GetTransaction(string transactionId);
        public List<string> TransactionHistory(string accountId);
    }
}
