using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Models;

namespace BankApplication.Service.Interfaces
{
   public  interface ICustomerServiceInterface
   {
        public decimal? Deposit(string accountId, decimal amount);
        
        public decimal? WithDraw(string accountId, decimal amount);
        public decimal? GetBalance(string accountId);
        public void TransferAmount(string senderAccountId, string receiverAccountId, decimal amount,string paymentMode);

        
    }
}
