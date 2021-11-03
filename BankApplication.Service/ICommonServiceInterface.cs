using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Models;

namespace BankApplication.Service
{
    public interface ICommonServiceInterface
    {
        List<Transaction> GetTransactionHistory(string bankId, string accountId)
        {
            List<Transaction> Transactions = new List<Transaction>();
            return Transactions;
        }
    }
}
