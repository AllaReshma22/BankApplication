using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Models;

namespace BankApplication.Service
{
   public  interface IServiceInterface
   {
        string CreateAccount(string bankId,string accountName,int password,double initialbal )
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
        void UpdateAccountPassword(string bankId,string accountId,int newPassword)
        {

        }
        bool RevertTransfer(string senderTransactionId,string receiverTransactionId,string senderBankId,string senderAccountId,string receiverBankId,string receiverAccountId)
        {
            return true;

        }
        bool RevertTransaction(string transactionId,string bankId,string accounNumber)
        {
            return true;
        }
        void UpdateSameBankRTGS(string bankId, int newRTGS)
        {
            
        }
        void UpdateSameBankIMPS(string bankId, int newIMPS)
        {

        }
        void UpdateOtherBankRTGS(string bankId, int newRTGS)
        {

        }
        void UpdateOtherBankIMPS(string bankId,int newIMPS)
        {

        }
        void AddAcceptedCurrency(string bankId,string addNewAcceptedCurrency,double multiplier)
        {

        }
        List<Transaction> GetTransactionHistory(string bankId,string accountId)
        {
            List<Transaction> Transactions = new List<Transaction>();
            return Transactions;
        }
        double Deposit(string bankId,double amount,string currencyType,string accountId,int pin)
        {
            return amount;
        }
        bool WithDraw(string bankId, double amount, string accountId, int pin)
        {
            return true;
        }
        double GetBalance(string bankId,string accountId, int pin)
        {
            double amount=0;
            return amount;
        }
        bool TransferAmount(string bankId, string accountId, int pin,double amount,string receiverBankId,string receiverAccountId,string paymentMode)
        {
            return true;
        }

    }
}
