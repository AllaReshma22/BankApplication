using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Service
{
    public interface IBankStaffServiceInterface
    {
        string CreateAccount(string bankId, string accountName, int password, decimal initialbal)
        {
            return accountName;
        }
        bool DeleteAccount(string bankId, string accountId)
        {
            return true;
        }
        string AddBank(string bankName)
        {
            return bankName;
        }
        void  UpdateAccountPassword(string bankId, string accountId, int newPassword)
        {

        }
        bool RevertTransfer(string senderTransactionId, string receiverTransactionId, string senderBankId, string senderAccountId, string receiverBankId, string receiverAccountId)
        {
            return true;

        }
        bool RevertTransaction(string transactionId, string bankId, string accounNumber)
        {
            return true;
        }
        void UpdateSameBankRTGS(string bankId, decimal newRTGS)
        {

        }
        void UpdateSameBankIMPS(string bankId, decimal newIMPS)
        {

        }
        void UpdateOtherBankRTGS(string bankId, decimal newRTGS)
        {

        }
        void UpdateOtherBankIMPS(string bankId, decimal newIMPS)
        {

        }
        void AddAcceptedCurrency(string bankId, string addNewAcceptedCurrency, decimal multiplier)
        {

        }
    }
}
