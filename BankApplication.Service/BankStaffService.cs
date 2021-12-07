﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Models;
using BankApplication.Models.Exceptions;
using BankApplication.Models.Models;
using BankApplication.Service.Interfaces;

namespace BankApplication.Service
{
    public class BankStaffService :IBankStaffServiceInterface
    {
        private BankAppContext bankAppContext;
        public BankStaffService(BankAppContext bankAppContext)
        {
            this.bankAppContext = bankAppContext;
        }
        public string AddBank(string bankName)
        {
            if (bankAppContext.Banks.Any(m => m.BankName == bankName))
                throw new DuplicateBankNameException();
            var bank = new Bank
            {
                BankId = bankName.Substring(0, 3) + DateTime.UtcNow.ToString("ddMMyyyy"),
                BankName = bankName,
                StaffId = bankName.Substring(0, 3) + "staff",
            };
            bankAppContext.Banks.Add(bank);
            bankAppContext.SaveChanges();
            return bank.BankId;
        }
        public string CreateAccount(string bankId, string accountHolderName, int password, decimal initialBal)
        {
            string accountId = accountHolderName.Substring(0, 3) + DateTime.UtcNow.ToString("ddMMyyyy");
            Account account = new Account(accountId, bankId, accountHolderName, password, initialBal);
            bankAppContext.Accounts.Add(account);
            bankAppContext.SaveChanges();
            return account.AccountId;
        }
        public bool UpdateAccount(string accountId, int newPassword)
        {
            var account = bankAppContext.Accounts.FirstOrDefault(m => m.AccountId == accountId);
            account.Password = newPassword;
            bankAppContext.Accounts.Update(account);
            bankAppContext.SaveChanges();
            return true;
        }
        public IEnumerable<Account> GetAllAccounts(string bankId)
        {
            return bankAppContext.Accounts.Where(m => m.BankId == bankId).ToList();
        }
        public bool DeleteAccount(string accountId)
        {
            var account = bankAppContext.Accounts.FirstOrDefault(m => m.AccountId == accountId);
            bankAppContext.Accounts.Remove(account);
            return true;
        }
        public void UpdateSameBankRtgs(string bankId, decimal newRtgs)
        {
            var bank = bankAppContext.Banks.FirstOrDefault(m => m.BankId == bankId);
            bank.SameBankRtgsCharges = newRtgs;
            bankAppContext.Banks.Update(bank);
            bankAppContext.SaveChanges();
        }
        public void UpdateSameBankImps(string bankId, decimal newImps)
        {
            var bank = bankAppContext.Banks.FirstOrDefault(m => m.BankId == bankId);
            bank.SameBankImpsCharges = newImps;
            bankAppContext.Banks.Update(bank);
            bankAppContext.SaveChanges();
        }
        public void UpdateOtherBankRtgs(string bankId, decimal newRtgs)
        {
            var bank = bankAppContext.Banks.FirstOrDefault(m => m.BankId == bankId);
            bank.OtherBankRtgsCharges = newRtgs;
            bankAppContext.Banks.Update(bank);
            bankAppContext.SaveChanges();
        }
        public void UpdateOtherBankImps(string bankId, decimal newImps)
        {
            var bank = bankAppContext.Banks.FirstOrDefault(m => m.BankId == bankId);
            bank.OtherBankRtgsCharges = newImps;
            bankAppContext.Banks.Update(bank);
            bankAppContext.SaveChanges();
        }
        public bool RevertTransaction(string transactionId)
        {
            var transaction = bankAppContext.Transactions.FirstOrDefault(m => m.TransactionId == transactionId);
            if (transaction.TransactionType == "Transfer")
            {
                var account = bankAppContext.Accounts.FirstOrDefault(m => m.AccountId == transaction.SenderAccountId);
                var account1 = bankAppContext.Accounts.FirstOrDefault(m => m.AccountId == transaction.ReceiverAccountId);
                account.Balance = account.Balance + transaction.Amount;
                bankAppContext.Accounts.Update(account);
                bankAppContext.SaveChanges();
                account1.Balance = account1.Balance - transaction.Amount;
                bankAppContext.Accounts.Update(account1);
                bankAppContext.SaveChanges();
                return true;
            }
            if (transaction.TransactionType == "Deposit")
            {
                var account = bankAppContext.Accounts.FirstOrDefault(m => m.AccountId == transaction.ReceiverAccountId);
                account.Balance = account.Balance - transaction.Amount;
                bankAppContext.Accounts.Update(account);
                bankAppContext.SaveChanges();
                return true;
            }
            if (transaction.TransactionType == "WithDraw")
            {
                var account = bankAppContext.Accounts.FirstOrDefault(m => m.AccountId == transaction.SenderAccountId);
                account.Balance = account.Balance + transaction.Amount;
                bankAppContext.Accounts.Update(account);
                bankAppContext.SaveChanges();
                return true;
            }
            return false;
        }
        public List<string> TransactionHistory(string accountId)
        {
            List<string> TransactionList = new List<string>();
            foreach (Transaction transaction in bankAppContext.Transactions)
            {
                if (transaction.SenderAccountId == accountId || transaction.ReceiverAccountId == accountId)
                {
                    string st = transaction.TransactionId + "Type:" + transaction.TransactionType + "Amount:" + transaction.Amount;
                    TransactionList.Add(st);
                }
            }
            return TransactionList;
        }
        public decimal? GetBalance(string accountId)
        {
            var account = bankAppContext.Accounts.FirstOrDefault(m => m.AccountId == accountId);
            return account.Balance;
        }
        public bool UpdateAccountPassword(string accountId,int newPassword)
        {
            var account = bankAppContext.Accounts.FirstOrDefault(m => m.AccountId == accountId);
            account.Password = newPassword;
            bankAppContext.Accounts.Update(account);
            bankAppContext.SaveChanges();
            return true;
        }


    }
}
