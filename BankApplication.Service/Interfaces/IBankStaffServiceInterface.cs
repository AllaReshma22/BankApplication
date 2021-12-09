using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Models.Models;

namespace BankApplication.Service.Interfaces
{
    public interface IBankStaffServiceInterface
    {
        public string CreateAccount(string bankId, string accountHolderName, int password, decimal initialbal);
        public bool DeleteAccount(string accountId);
        public string AddBank(string bankName);
        public bool UpdateAccountPassword(string accountId, int newPassword);
        public bool RevertTransaction(string transactionId);
        public void UpdateSameBankRtgs(string bankId, decimal newRtgs);
        public void UpdateSameBankImps(string bankId, decimal newImps);
        public void UpdateOtherBankRtgs(string bankId, decimal newRtgs);
        public void UpdateOtherBankImps(string bankId, decimal newImps);
        public decimal? GetBalance(string accountId);
    }
}
