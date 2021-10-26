using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Models;
using BankApplication.Models.Exceptions;

namespace BankApplication.Service
{
    public class BankStaffActions:Validations
    {   
        public string createAccount(string bankId, string AccountName, int password, double initialbal)
        {
            Bank bank = this.banks.SingleOrDefault(m => m.BankId == bankId);
            if (bank is null)
                throw new IncorrectBankIdException();
            string AccountNumber = AccountName.Substring(0, 3) + DateTime.UtcNow.ToString("dd-MM-yyyy");
            Account account = new Account(AccountNumber, AccountName, password, initialbal);
            bank.AccountsList.Add(account);
            return account.AccountNumber;
        }
        public bool deleteAccount(string bankId,string accountnumber)
        {
            Bank bank = this.banks.SingleOrDefault(m => m.BankId == bankId);
            if (bank is null)
                throw new IncorrectBankIdException();
            var account = bank.AccountsList.SingleOrDefault(m => m.AccountNumber == accountnumber);
            if (account is null)
                throw new IncorrectAccountNumberException();
            bank.AccountsList.Remove(account);
            return true;
        }
        public int updateAccountPassword(string bankId,string accountnumber,int newpassword)
        {
            Bank bank = this.banks.SingleOrDefault(m => m.BankId == bankId);
            if (bank is null)
                throw new IncorrectBankIdException();
            var account = bank.AccountsList.SingleOrDefault(m => m.AccountNumber == accountnumber);
            if (account is null)
                throw new IncorrectAccountNumberException();

            account.Password = newpassword;
            return account.Password;
        }
        public bool revertTransaction(string Transactionid)
        {
            Bank bank = this.banks.SingleOrDefault(m => m.BankId == Transactionid.Substring(2,13));
            if (bank is null)
                throw new IncorrectBankIdException();
            var account = bank.AccountsList.SingleOrDefault(m => m.AccountNumber == Transactionid.Substring(16,13));
            if (account is null)
                throw new IncorrectAccountNumberException();
            var Transaction = account.Transactions.SingleOrDefault(m => m.TransactionId == Transactionid);
            if (Transactionid.Substring(0, 1) == "D")
                account.Balance -= Transaction.Amount;
            else
                account.Balance += Transaction.Amount;
            return true;
        }
        public bool revertTransfer(string senderTransactionId,string receiverTransactionId)
        {
            Bank senderbank= this.banks.SingleOrDefault(m => m.BankId == senderTransactionId.Substring(2, 13));
            if (senderbank is null)
                throw new IncorrectBankIdException();
            var senderaccount = senderbank.AccountsList.SingleOrDefault(m => m.AccountNumber == senderTransactionId.Substring(16, 13));
            if (senderaccount is null)
                throw new IncorrectAccountNumberException();
            var Transaction = senderaccount.Transactions.SingleOrDefault(m => m.TransactionId == senderTransactionId);
            senderaccount.Balance += Transaction.Amount;
            Bank receiverbank = this.banks.SingleOrDefault(m => m.BankId == receiverTransactionId.Substring(2, 13));
            if (receiverbank is null)
                throw new IncorrectBankIdException();
            var receiveraccount = receiverbank.AccountsList.SingleOrDefault(m => m.AccountNumber == receiverTransactionId.Substring(16, 13));
            if (receiveraccount is null)
                throw new IncorrectAccountNumberException();
            var Transaction1 = receiveraccount.Transactions.SingleOrDefault(m => m.TransactionId == receiverTransactionId);
            receiveraccount.Balance -= Transaction.Amount;
            return true;

        }
        public bool UpdateSameBankRTGS(string bankid,int newRTGS)
        {
            Bank bank = this.banks.SingleOrDefault(m => m.BankId == bankid);
            if (bank is null)
                throw new IncorrectBankIdException();
            bank.SameBankRTGS = newRTGS;
            return true;
        }
        public bool UpdateSameBankIMPS(string bankid, int newIMPS)
        {
            Bank bank = this.banks.SingleOrDefault(m => m.BankId == bankid);
            if (bank is null)
                throw new IncorrectBankIdException();
            bank.SameBankIMPS = newIMPS;
            return true;
        }
        public bool UpdateOtherBankIMPS(string bankid, int newIMPS)
        {
            Bank bank = this.banks.SingleOrDefault(m => m.BankId == bankid);
            if (bank is null)
                throw new IncorrectBankIdException();
            bank.OtherBankIMPS = newIMPS;
            return true;
        }
        public bool UpdateOtherBankRTGS(string bankid, int newRTGS)
        {
            Bank bank = this.banks.SingleOrDefault(m => m.BankId == bankid);
            if (bank is null)
                throw new IncorrectBankIdException();
            bank.OtherBankIMPS = newRTGS;
            return true;
        }
        public bool UpdateAcceptedCurrency(string bankid,string NewAcceptedCurrency)
        {
            Bank bank = this.banks.SingleOrDefault(m => m.BankId == bankid);
            if (bank is null)
                throw new IncorrectBankIdException();
            bank.AcceptedCurrency = NewAcceptedCurrency;
            return true;

        }
        public List<Transaction> GetTransactionhistory(string bankId, string acnumber)
        {
            var bank = this.banks.SingleOrDefault(m => m.BankId == bankId);
            if (bank is null)
                throw new IncorrectBankIdException();
            var account = bank.AccountsList.SingleOrDefault(m => m.AccountNumber == acnumber);
            if (account is null)
                throw new IncorrectAccountNumberException();
            return account.Transactions;
        }

    }
}
