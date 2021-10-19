using System;
using System.Collections.Generic;
using System.Linq;
using BankApplication;
using BankApplication.Models;
using BankApplication.Models.Exceptions;

namespace BankApplication.Service
{
        public class BankService
        {
            private List<Bank> banks;
            public BankService()
            {
                this.banks = new List<Bank>();
            }
            public string addBank(string bankname)
            {
                Bank bank = new Bank
                {
                    BankId = bankname.Substring(0, 3) + DateTime.UtcNow.ToString("dd-MM-yyyy"),
                    BankName = bankname
                };
                this.banks.Add(bank);
                //       BId++;
                return bank.BankId;
            }
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
            public bool Deposit(string bankId, double amount, string accountnumber, int pin)
            {
                Bank bank = this.banks.SingleOrDefault(m => m.BankId == bankId);
                if (bank is null)
                    throw new IncorrectBankIdException();
                var account = bank.AccountsList.SingleOrDefault(m => m.AccountNumber == accountnumber);
                if (account is null)
                    throw new IncorrectAccountNumberException();
                if (account.Password == pin)
                {
                    account.Balance += amount;
                    TransactionType.transactionType t = (TransactionType.transactionType)1;
                    string TransactionId = "Txn" + " " + bankId + " " + accountnumber + " " + DateTime.UtcNow.ToString("dd-MM-yyyy");
                    Transaction transaction = new Transaction(accountnumber, amount, t, TransactionId, DateTime.Now);
                    account.Transactions.Add(transaction);
                }
                else
                    throw new IncorrectPin();
                return true;
            }
            public bool WithDraw(string bankId, double amount, string accountnumber, int pin)
            {
                Bank bank = this.banks.SingleOrDefault(m => m.BankId == bankId);
                if (bank is null)
                    throw new IncorrectBankIdException();
                var account = bank.AccountsList.SingleOrDefault(m => m.AccountNumber == accountnumber);
                if (account is null)
                    throw new IncorrectAccountNumberException();
                else
                {
                    if (account.Password == pin)
                    {
                        if (account.Balance < amount)
                            throw new AmountNotSufficient();
                        else
                        {

                            account.Balance -= amount;
                            TransactionType.transactionType t = (TransactionType.transactionType)2;
                            string TransactionId = "Txn" + " " + bankId + " " + accountnumber + " " + DateTime.UtcNow.ToString("dd-MM-yyyy");
                            Transaction transaction = new Transaction(accountnumber, amount, t, TransactionId, DateTime.Now);
                            account.Transactions.Add(transaction);
                        }
                    }
                    else
                        throw new IncorrectPin();
                }
                return true;
            }
            public bool transferAmount(string senderbankId, string senderaccnumber, int pin, double amount, string receiverbankid, string receiveraccnumber)
            {
                Bank senderbank = this.banks.SingleOrDefault(m => m.BankId == senderbankId);
                if (senderbank is null)
                    throw new IncorrectBankIdException();
                var senderaccount = senderbank.AccountsList.SingleOrDefault(m => m.AccountNumber == senderaccnumber);
                if (senderaccount is null)
                    throw new Exception("Account invalid");
                if (senderaccount.Password == pin)
                {
                    if (senderaccount.Balance > amount)
                    {
                        Bank receiverbank = this.banks.SingleOrDefault(m => m.BankId == receiverbankid);
                        if (receiverbank is null)
                            throw new IncorrectBankIdException();
                        var receiveraccount = receiverbank.AccountsList.SingleOrDefault(m => m.AccountNumber == receiveraccnumber);
                        if (receiveraccount is null)
                            throw new Exception("Account invalid");
                        senderaccount.Balance -= amount;
                        receiveraccount.Balance += amount;
                        TransactionType.transactionType t = (TransactionType.transactionType)1;
                        string TransactionId = "Txn" + " " + senderbankId + " " + senderaccnumber + " " + DateTime.UtcNow.ToString("dd-MM-yyyy");
                        Transaction transaction = new Transaction(senderaccnumber, amount, t, TransactionId, DateTime.Now);
                        senderaccount.Transactions.Add(transaction);
                        TransactionType.transactionType t1 = (TransactionType.transactionType)2;
                        string TransactionId1 = "Txn" + " " + receiverbankid + " " + receiveraccnumber + " " + DateTime.UtcNow.ToString("dd-MM-yyyy");
                        Transaction transaction1 = new Transaction(receiveraccnumber, amount, t1, TransactionId1, DateTime.Now);
                        receiveraccount.Transactions.Add(transaction1);
                    }
                    else
                        throw new AmountNotSufficient();
                }
                else
                    throw new IncorrectPin();
                return true;
            }
            public double GetBalance(string BankId, string accountnumber, int pin)
            {
                Bank bank = this.banks.SingleOrDefault(m => m.BankId == BankId);
                if (bank is null)
                    throw new IncorrectBankIdException();
                var account = bank.AccountsList.SingleOrDefault(m => m.AccountNumber == accountnumber);
                if (account is null)
                    throw new IncorrectAccountNumberException();
                else
                {
                    if (account.Password == pin)
                        return account.Balance;

                    else
                        throw new Exception("Invalid pin number");
                }
            }
            public List<Transaction> GetTransactionhistory(string bankId, string acnumber, int password)
            {
                var bank = this.banks.SingleOrDefault(m => m.BankId == bankId);
                if (bank is null)
                    throw new IncorrectBankIdException();
                var account = bank.AccountsList.SingleOrDefault(m => m.AccountNumber == acnumber);
                if (account is null)
                    throw new IncorrectAccountNumberException();
                if (account.Password == password)
                    return account.Transactions;
                else
                    throw new IncorrectPin();
            }
        }

    }

