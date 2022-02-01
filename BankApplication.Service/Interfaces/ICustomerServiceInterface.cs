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
        public Account Authenticate(string accountId,int? password);
        
        public decimal? WithDraw(string accountId, decimal amount);
        public decimal? GetBalance(string accountId);
        public void TransferAmount(string senderAccountId, string receiverAccountId, decimal amount,string paymentMode);

        
    }
}
