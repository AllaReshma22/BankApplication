using System;
using System.Collections.Generic;
using System.Linq;
using BankApplication;
using BankApplication.Models;
using BankApplication.Models.Exceptions;
using BankApplication.Models.Enums;
using BankApplication.Models.Models;
using BankApplication.Service.Interfaces;

namespace BankApplication.Service
{
    public class CustomerService : ICommonServiceInterface, ICustomerServiceInterface
    {
        private readonly BankAppContext bankAppContext;
        public CustomerService(BankAppContext bankAppContext)
        {
            this.bankAppContext = bankAppContext;
        }
        static int Transactioncount = 1;
        private Account GetAccount(string accountId)
        {
            return bankAppContext.Accounts.FirstOrDefault(m => m.AccountId == accountId);
        }
        public Account Authenticate(string accountId,int? password)
        {
            var account = bankAppContext.Accounts.FirstOrDefault(m => m.AccountId == accountId);
            if (account == null)
            {
                throw new IncorrectAccountIdException();
            }
            if (account.Password == password)
            {
                bankAppContext.Accounts.Attach(account);
                return account;
            }

            else
                throw new IncorrectPin();
        }
        public decimal? Deposit(string accountId, decimal amount)
        {
            var account = GetAccount(accountId);
            account.Balance += amount;
            bankAppContext.Update(account);
            string TransactionId = "Txn" + account.BankId + accountId + DateTime.UtcNow.ToddMMyyyy() + Transactioncount;
            string TransactionType = "Deposit";
            Transaction transaction = new Transaction(TransactionId, accountId, accountId, amount, TransactionType);
            Transactioncount++;
            bankAppContext.Transactions.Add(transaction);
            bankAppContext.SaveChanges();
            return account.Balance;
        }
        public decimal? WithDraw(string accountId, decimal amount)
        {
            var account =GetAccount(accountId);
            if (account.Balance < amount)
                throw new AmountNotSufficient();
            else
            {
                account.Balance -= amount;
                bankAppContext.Accounts.Update(account);
                bankAppContext.SaveChanges();
                string TransactionId = "Txn" + account.BankId + accountId + DateTime.UtcNow.ToddMMyyyy() + Transactioncount;
                string TransactionType = "WithDraw";
                Transaction transaction = new(TransactionId, accountId, accountId, amount, TransactionType);
                Transactioncount++;
                bankAppContext.Transactions.Add(transaction);
                bankAppContext.SaveChanges();
            }
            return account.Balance;
        }
        public void TransferAmount(string senderAccountId, string receiverAccountId, decimal amount,string paymentMode)
        {  
            var senderAccount =GetAccount(senderAccountId);
            var senderBank = bankAppContext.Banks.FirstOrDefault(m => m.BankId == senderAccount.BankId);
            var receiverAccount = GetAccount(receiverAccountId);
            var receiverBank = bankAppContext.Banks.FirstOrDefault(m => m.BankId == receiverAccount.BankId);
            if (senderAccount.Balance > amount)
            {
                if (paymentMode == "RTGS")
                {
                    if (senderAccount.BankId == receiverAccount.BankId)
                        senderAccount.Balance -= (amount + (amount * senderBank.SameBankRtgsCharges / 100));
                    else
                        senderAccount.Balance -= (amount + (amount * senderBank.OtherBankRtgsCharges / 100));
                }
                else
                {
                    if (senderAccount.BankId == receiverAccount.BankId)
                        senderAccount.Balance -= (amount + (amount * senderBank.SameBankImpsCharges / 100));
                    else
                        senderAccount.Balance -= (amount + (amount * senderBank.OtherBankImpsCharges / 100));

                }
                bankAppContext.Accounts.Update(senderAccount);
                receiverAccount.Balance += amount;
                bankAppContext.Accounts.Update(receiverAccount);
                bankAppContext.SaveChanges();
                string TransactionId = "Txn" + senderAccount.BankId + senderAccountId + DateTime.UtcNow.ToddMMyyyy() + Transactioncount;
                string TransactionType = "Transfer";
                Transaction transaction = new(TransactionId, senderAccountId, receiverAccountId, amount, TransactionType);
                Transactioncount++;
                bankAppContext.Transactions.Add(transaction);
                bankAppContext.SaveChanges();
            }
            else
            {
                throw new AmountNotSufficient();
            }
  
        }
        public decimal? GetBalance(string accountId)
        {
            var account =GetAccount(accountId);
            return account.Balance;
        }
        public List<string> TransactionHistory(string accountId)
        {
            List<string> TransactionList = new();
            foreach (Transaction transaction in bankAppContext.Transactions)
            {
                if (transaction.SenderAccountId == accountId || transaction.ReceiverAccountId == accountId)
                {
                    string st = transaction.TransactionId + " Type:" + transaction.TransactionType + " Amount:" + transaction.Amount;
                    TransactionList.Add(st);
                }
            }
            return TransactionList;

        }
    }

}



