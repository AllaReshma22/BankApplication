using System;
using System.Collections.Generic;
using System.Linq;
using BankApplication;
using BankApplication.Models;
using BankApplication.Models.Exceptions;

namespace BankApplication.Service
{
        public class BankService:IServiceInterface
        {
            static int TransactionCount = 1;
            public double Deposit(string bankId, double amount,string currencyType, string accountId, int pin)
            {
               Bank bank = Datastore.Banks.SingleOrDefault(m => m.BankId == bankId);
               var account = bank.AccountsList.SingleOrDefault(m => m.AccountId == accountId);
               double multiplier = Datastore.Currency[currencyType];
               account.Balance += amount*multiplier;
               TransactionType.transactionType t = (TransactionType.transactionType)1;
               string TransactionId = "Txn" + " " + bankId + " " + accountId + " " + DateTime.UtcNow.ToString("ddMMyyyy")+TransactionCount+"D";
               Transaction transaction = new Transaction(accountId, amount, t, TransactionId, DateTime.Now);
               TransactionCount++;
               account.Transactions.Add(transaction);
               return amount*multiplier;
            }
            public bool WithDraw(string bankId, double amount, string accountId, int pin)
            {
                Bank bank = Datastore.Banks.SingleOrDefault(m => m.BankId == bankId);
                var account = bank.AccountsList.SingleOrDefault(m => m.AccountId == accountId);               
                        if (account.Balance < amount)
                            throw new AmountNotSufficient();
                        else
                        {
                            account.Balance -= amount;
                            TransactionType.transactionType t = (TransactionType.transactionType)2;
                            string TransactionId = "Txn" + " " + bankId + " " + accountId + " " + DateTime.UtcNow.ToString("ddMMyyyy")+TransactionCount+"W";
                            Transaction transaction = new Transaction(accountId, amount, t, TransactionId, DateTime.Now);
                            account.Transactions.Add(transaction);
                            TransactionCount++;
                        }
                return true;
            }
            public bool TransferAmount(string senderBankId, string senderAccountId, int pin, double amount, string receiverBankId, string receiverAccountId,string paymentMode)
            {
                Bank senderBank = Datastore.Banks.SingleOrDefault(m => m.BankId == senderBankId);
                var senderAccount = senderBank.AccountsList.SingleOrDefault(m => m.AccountId == senderAccountId);
                    if (senderAccount.Balance > amount)
                    {
                        Bank receiverbank = Datastore.Banks.SingleOrDefault(m => m.BankId == receiverBankId);
                        if (receiverbank is null)
                            throw new IncorrectBankIdException();
                        var receiveraccount =receiverbank.AccountsList.SingleOrDefault(m => m.AccountId == receiverAccountId);
                        if (receiveraccount is null)
                            throw new Exception("Account invalid");
                        if (paymentMode == "RTGS")
                        {
                            if (senderBankId == receiverBankId)
                            senderAccount.Balance -= (amount + (amount * senderBank.SameBankRTGSCharges / 100));
                            else
                            senderAccount.Balance -= (amount + (amount * senderBank.OtherBankRTGSCharges / 100));
                        }
                        else
                        {
                            if (senderBankId == receiverBankId)
                            senderAccount.Balance -= (amount + (amount * senderBank.SameBankIMPSCharges / 100));
                            else
                            senderAccount.Balance -= (amount + (amount * senderBank.OtherBankIMPSCharges / 100));
                        }                   
                        receiveraccount.Balance += amount;
                        TransactionType.transactionType t = (TransactionType.transactionType)1;
                        string TransactionId = "Txn" + " " + senderBankId + " " + senderAccountId + " " + DateTime.UtcNow.ToString("ddMMyyyy")+TransactionCount+"T";
                        Transaction transaction = new Transaction(senderAccountId, amount, t, TransactionId, DateTime.Now);
                        senderAccount.Transactions.Add(transaction);
                        TransactionType.transactionType t1 = (TransactionType.transactionType)2;
                        string TransactionId1 = "Txn" + " " + receiverBankId + " " + receiverAccountId + " " + DateTime.UtcNow.ToString("ddMMyyyy")+TransactionCount+"T";
                        Transaction transaction1 = new Transaction(receiverAccountId, amount, t1, TransactionId1, DateTime.Now);
                        receiveraccount.Transactions.Add(transaction1);
                        TransactionCount++;
                    }
                    else
                        throw new AmountNotSufficient();
                return true;
            }
            public double GetBalance(string BankId, string accountId, int pin)
            {
                Bank bank = Datastore.Banks.SingleOrDefault(m => m.BankId == BankId);
                var account = bank.AccountsList.SingleOrDefault(m => m.AccountId == accountId);
                return account.Balance;                 
            }
            public List<Transaction> GetTransactionHistory(string bankId, string accountId)
            {
               var bank = Datastore.Banks.SingleOrDefault(m => m.BankId == bankId);
              var account = bank.AccountsList.SingleOrDefault(m => m.AccountId == accountId);
              return account.Transactions;
             }
    }

    }

