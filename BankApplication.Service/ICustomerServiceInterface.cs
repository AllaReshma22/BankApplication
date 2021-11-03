using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Models;

namespace BankApplication.Service
{
   public  interface ICustomerServiceInterface
   {
        List<Transaction> GetTransactionHistory(string bankId,string accountId)
        {
            List<Transaction> Transactions = new List<Transaction>();
            return Transactions;
        }
        decimal Deposit(string bankId,decimal amount,string currencyType,string accountId,int pin)
        {
            return amount;
        }
        bool WithDraw(string bankId, decimal amount, string accountId, int pin)
        {
            return true;
        }
        decimal GetBalance(string bankId,string accountId, int pin)
        {
            decimal amount=0;
            return amount;
        }
        bool TransferAmount(string bankId, string accountId, int pin,decimal amount,string receiverBankId,string receiverAccountId,string paymentMode)
        {
            return true;
        }

    }
}
