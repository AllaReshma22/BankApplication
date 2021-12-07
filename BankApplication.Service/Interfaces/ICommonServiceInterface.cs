using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Models;

namespace BankApplication.Service.Interfaces
{
    public interface ICommonServiceInterface
    {
        public List<string> TransactionHistory(string accountId);
       
    }
}
