using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Models;
using BankApplication.Models.Exceptions;

namespace BankApplication.Service
{
    public class BankStaffActions:IServiceInterface
    {
        static int AccountNumber=1000;
        public string AddBank(string bankName)
        {
            if (Datastore.Banks.Any(m => m.BankName == bankName))
            {
                throw new DuplicateBankNameException();
            }
            Bank bank = new Bank
            {
                BankId = bankName.Substring(0, 3) + DateTime.UtcNow.ToString("ddMMyyyy"),
                BankName = bankName,
                StaffId = bankName.Substring(0, 3) + "staff"

            };
            Datastore.Banks.Add(bank);

            return bank.BankId;
        }
        public  string CreateAccount(string bankId, string accountName, int password, double initialBal)
        {
            Bank bank = Datastore.Banks.SingleOrDefault(m => m.BankId == bankId);
            string accountId = accountName.Substring(0, 3) + DateTime.UtcNow.ToString("ddMMyyyy");
            int accountNumber = AccountNumber;
            Account account = new Account(accountNumber,accountId, accountName, password, initialBal);
            bank.AccountsList.Add(account);
            AccountNumber++;
            return account.AccountId;
        }
        public  bool DeleteAccount(string bankId,string accountId)
        {
            Bank bank = Datastore.Banks.SingleOrDefault(m => m.BankId == bankId);
            var account = bank.AccountsList.SingleOrDefault(m => m.AccountId == accountId);
            bank.AccountsList.Remove(account);
            return true;
        }
        public  int UpdateAccountPassword(string bankId,string accountId,int newPassword)
        {
            Bank bank = Datastore.Banks.SingleOrDefault(m => m.BankId == bankId);
            var account = bank.AccountsList.SingleOrDefault(m => m.AccountId == accountId);
            account.Password = newPassword;
            return account.Password;
        }
        public  bool RevertTransaction(string transactionId ,string bankId,string accountId)
        {
            Bank bank = Datastore.Banks.SingleOrDefault(m => m.BankId ==bankId);
            var account = bank.AccountsList.SingleOrDefault(m => m.AccountId == accountId);
            var transaction = account.Transactions.SingleOrDefault(m => m.TransactionId == transactionId);
            if (transactionId.Substring(transactionId.Length-1) == "D")
                account.Balance -= transaction.Amount;
            else
                account.Balance += transaction.Amount;
            return true;
        }
        public  bool RevertTransfer(string senderTransactionId,string receiverTransactionId,string senderBankId,string senderAccountId,string receiverBankId,string receiverAccountId)
        {
            Bank senderbank= Datastore.Banks.SingleOrDefault(m => m.BankId == senderBankId);
            if (senderbank is null)
                throw new IncorrectBankIdException();
            var senderaccount = senderbank.AccountsList.SingleOrDefault(m => m.AccountId == senderAccountId);
            if (senderaccount is null)
                throw new IncorrectAccountNumberException();
            var transaction = senderaccount.Transactions.SingleOrDefault(m => m.TransactionId == senderTransactionId);
            senderaccount.Balance += transaction.Amount;
            Bank receiverbank = Datastore.Banks.SingleOrDefault(m => m.BankId ==receiverBankId);
            if (receiverbank is null)
                throw new IncorrectBankIdException();
            var receiveraccount = receiverbank.AccountsList.SingleOrDefault(m => m.AccountId == receiverAccountId);
            if (receiveraccount is null)
                throw new IncorrectAccountNumberException();
            var transaction1 = receiveraccount.Transactions.SingleOrDefault(m => m.TransactionId == receiverTransactionId);
            receiveraccount.Balance -= transaction1.Amount;
            return true;

        }
        public  bool UpdateSameBankRTGS(string bankId,int newRTGS)
        {
            Bank bank = Datastore.Banks.SingleOrDefault(m => m.BankId == bankId);
            bank.SameBankRTGSCharges = newRTGS;
            return true;
        }
        public  bool UpdateSameBankIMPS(string bankId, int newIMPS)
        {
            Bank bank = Datastore.Banks.SingleOrDefault(m => m.BankId == bankId);
            bank.SameBankIMPSCharges = newIMPS;
            return true;
        }
        public  bool UpdateOtherBankIMPS(string bankId, int newIMPS)
        {
            Bank bank = Datastore.Banks.SingleOrDefault(m => m.BankId == bankId);
            bank.OtherBankIMPSCharges = newIMPS;
            return true;
        }
        public  bool UpdateOtherBankRTGS(string bankId, int newRTGS)
        {
            Bank bank = Datastore.Banks.SingleOrDefault(m => m.BankId == bankId);
            if (bank is null)
                throw new IncorrectBankIdException();
            bank.OtherBankIMPSCharges = newRTGS;
            return true;
        }
        public  bool AddAcceptedCurrency(string bankId,string newAcceptedCurrency,double multiplier)
        {
            Bank bank = Datastore.Banks.SingleOrDefault(m => m.BankId == bankId);
            Datastore.Currency.Add(newAcceptedCurrency, multiplier);
            
            return true;

        }
        public  List<Transaction> GetTransactionHistory(string bankId, string accountId)
        {
            var bank = Datastore.Banks.SingleOrDefault(m => m.BankId == bankId);
            var account = bank.AccountsList.SingleOrDefault(m => m.AccountId == accountId);
            return account.Transactions;
        }

    }
}
