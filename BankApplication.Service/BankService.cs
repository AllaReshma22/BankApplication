using System;
using System.Collections.Generic;
using System.Linq;
using BankApplication;
using BankApplication.Models;
using BankApplication.Models.Exceptions;

namespace BankApplication.Service
{
        public class BankService:Validations
        {
            public bool Deposit(string bankId, double amount, string accountnumber, int pin)
            {
               Bank bank = this.banks.SingleOrDefault(m => m.BankId == bankId);
                var account = bank.AccountsList.SingleOrDefault(m => m.AccountNumber == accountnumber);
                    account.Balance += amount;
                    TransactionType.transactionType t = (TransactionType.transactionType)1;
                    string TransactionId = "D" + " " + bankId + " " + accountnumber + " " + DateTime.UtcNow.ToString("dd-MM-yyyy");
                    Transaction transaction = new Transaction(accountnumber, amount, t, TransactionId, DateTime.Now);
                return true;
            }
            public bool WithDraw(string bankId, double amount, string accountnumber, int pin)
            {
                Bank bank = this.banks.SingleOrDefault(m => m.BankId == bankId);
                var account = bank.AccountsList.SingleOrDefault(m => m.AccountNumber == accountnumber);               
                        if (account.Balance < amount)
                            throw new AmountNotSufficient();
                        else
                        {
                            account.Balance -= amount;
                            TransactionType.transactionType t = (TransactionType.transactionType)2;
                            string TransactionId = "W" + " " + bankId + " " + accountnumber + " " + DateTime.UtcNow.ToString("dd-MM-yyyy");
                            Transaction transaction = new Transaction(accountnumber, amount, t, TransactionId, DateTime.Now);
                            account.Transactions.Add(transaction);
                        }
                return true;
            }
            public bool transferAmount(string senderbankId, string senderaccnumber, int pin, double amount, string receiverbankid, string receiveraccnumber,string paymentmode)
            {
                Bank senderbank = this.banks.SingleOrDefault(m => m.BankId == senderbankId);
                var senderaccount = senderbank.AccountsList.SingleOrDefault(m => m.AccountNumber == senderaccnumber);
                    if (senderaccount.Balance > amount)
                    {
                        Bank receiverbank = this.banks.SingleOrDefault(m => m.BankId == receiverbankid);
                        if (receiverbank is null)
                            throw new IncorrectBankIdException();
                        var receiveraccount =receiverbank.AccountsList.SingleOrDefault(m => m.AccountNumber == receiveraccnumber);
                        if (receiveraccount is null)
                            throw new Exception("Account invalid");
                     if (paymentmode == "RTGS")
                     {
                        if (senderbankId == receiverbankid)
                        senderaccount.Balance -= (amount + (amount * senderbank.SameBankRTGS / 100));
                        else
                        senderaccount.Balance -= (amount + (amount * senderbank.OtherBankRTGS / 100));
                     }
                     else
                     {
                        if (senderbankId == receiverbankid)
                        senderaccount.Balance -= (amount + (amount * senderbank.SameBankIMPS / 100));
                       else
                        senderaccount.Balance -= (amount + (amount * senderbank.OtherBankIMPS / 100));
                     } 

                    
                        receiveraccount.Balance += amount;
                        TransactionType.transactionType t = (TransactionType.transactionType)1;
                        string TransactionId = "T" + " " + senderbankId + " " + senderaccnumber + " " + DateTime.UtcNow.ToString("dd-MM-yyyy");
                        Transaction transaction = new Transaction(senderaccnumber, amount, t, TransactionId, DateTime.Now);
                        senderaccount.Transactions.Add(transaction);
                        TransactionType.transactionType t1 = (TransactionType.transactionType)2;
                        string TransactionId1 = "T" + " " + receiverbankid + " " + receiveraccnumber + " " + DateTime.UtcNow.ToString("dd-MM-yyyy");
                        Transaction transaction1 = new Transaction(receiveraccnumber, amount, t1, TransactionId1, DateTime.Now);
                        receiveraccount.Transactions.Add(transaction1);
                    }
                    else
                        throw new AmountNotSufficient();
                return true;
            }
            public double GetBalance(string BankId, string accountnumber, int pin)
            {
                Bank bank = this.banks.SingleOrDefault(m => m.BankId == BankId);
                var account = bank.AccountsList.SingleOrDefault(m => m.AccountNumber == accountnumber);
                return account.Balance;                 
            }
            public List<Transaction> GetTransactionhistory(string bankId, string acnumber, int password)
            {
               var bank = this.banks.SingleOrDefault(m => m.BankId == bankId);
               var account = bank.AccountsList.SingleOrDefault(m => m.AccountNumber == acnumber);
               return account.Transactions;
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

