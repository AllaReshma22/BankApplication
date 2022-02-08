using System;
using System.Collections.Generic;
using System.Linq;
using BankApplication.Service;
using System.Threading.Tasks;
using BankApplication.Models;
using BankApplication.Models.Exceptions;
using BankApplication.Models.Models;
using BankApplication.Service.Interfaces;

namespace BankApplication.Service
{
    public class BankStaffService :IBankStaffServiceInterface,ICommonServiceInterface
    {
        private readonly BankAppContext bankAppContext;

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
                BankId = bankName.Substring(0, 3) + DateTime.UtcNow.ToddMMyyyy(),
                BankName = bankName,
                StaffId = bankName.Substring(0, 3) + "staff",
            };
            bankAppContext.Banks.Add(bank);
            bankAppContext.SaveChanges();
            return bank.BankId;
        }
        public bool AuthenticateStaff(string bankId, string staffId)
        {
            var bank = bankAppContext.Banks.FirstOrDefault(m => m.BankId == bankId);
            if (bank == null)
            {
                throw new IncorrectBankIdException();
            }
            if (bank.StaffId == staffId)
                return true;
            else
                throw new IncorrectStaffIdException();
        }
        public string CreateAccount(string bankId,string accountHolderName,int password,decimal initialBal)
        {

            string accountId = accountHolderName[..3] + DateTime.UtcNow.ToddMMyyyy();
            Account account = new(accountId, bankId, accountHolderName, password, initialBal);
            bankAppContext.Accounts.Add(account);
            bankAppContext.SaveChanges();

            return accountId;
        }
        public Account GetAccount(string accountId)
        {
            if (accountId == null) throw new IncorrectAccountIdException();
            return bankAppContext.Accounts.FirstOrDefault(m => (m.AccountId == accountId));
        }
        public Bank GetBank(string bankId)
        {
            if(bankId==null) throw new IncorrectAccountIdException();
            return bankAppContext.Banks.FirstOrDefault(m => m.BankId == bankId);
        }
        public IEnumerable<Account> GetAllAccounts()
        {
            return bankAppContext.Accounts.ToList();
        }
        public Account UpdateAccount(Account account,string accountId, int newPassword)
        {
            account = GetAccount(accountId);
            if (account == null)
                throw new IncorrectAccountIdException();
            account.Password = newPassword;
            bankAppContext.Accounts.Update(account);
            bankAppContext.SaveChanges();
            return account;
        }
        public bool DeleteAccount(string accountId)
        {
            var account = GetAccount(accountId);
            if (account == null)
                throw new IncorrectAccountIdException();
            bankAppContext.Accounts.Remove(account);
            bankAppContext.SaveChanges();
            return true;
        }
        public void UpdateSameBankRtgs(string bankId, decimal newRtgs)
        {
            var bank = GetBank(bankId);
            bank.SameBankRtgsCharges = newRtgs;
            bankAppContext.Banks.Update(bank);
            bankAppContext.SaveChanges();
        }
        public void UpdateSameBankImps(string bankId, decimal newImps)
        {
            var bank = GetBank  (bankId);
            bank.SameBankImpsCharges = newImps;
            bankAppContext.Banks.Update(bank);
            bankAppContext.SaveChanges();
        }
        public void UpdateOtherBankRtgs(string bankId, decimal newRtgs)
        {
            var bank =GetBank(bankId);
            bank.OtherBankRtgsCharges = newRtgs;
            bankAppContext.Banks.Update(bank);
            bankAppContext.SaveChanges();
        }
        public void UpdateOtherBankImps(string bankId, decimal newImps)
        {
            var bank = GetBank(bankId);
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
                account.Balance+= transaction.Amount;
                bankAppContext.Accounts.Update(account);
                bankAppContext.SaveChanges();
                account1.Balance-= transaction.Amount;
                bankAppContext.Accounts.Update(account1);
                bankAppContext.SaveChanges();
                bankAppContext.Transactions.Remove(transaction);
                bankAppContext.SaveChanges();
                return true;
            }
            if (transaction.TransactionType == "Deposit")
            {
                var account = bankAppContext.Accounts.FirstOrDefault(m => m.AccountId == transaction.ReceiverAccountId);
                account.Balance -=transaction.Amount;
                bankAppContext.Accounts.Update(account);
                bankAppContext.SaveChanges();
                bankAppContext.Transactions.Remove(transaction);
                bankAppContext.SaveChanges();
                return true;
            }
            if (transaction.TransactionType == "WithDraw")
            {
                var account = bankAppContext.Accounts.FirstOrDefault(m => m.AccountId == transaction.SenderAccountId);
                account.Balance += transaction.Amount;
                bankAppContext.Accounts.Update(account);
                bankAppContext.SaveChanges();
                bankAppContext.Transactions.Remove(transaction);
                bankAppContext.SaveChanges();
                return true;
            }
            return false;
        }
        public List<string> TransactionHistory(string accountId)
        {
            List<string> TransactionList = new();
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
            var account = GetAccount(accountId);
            return account.Balance;
        }
        public bool UpdateAccountPassword(string accountId,int? newPassword)
        {
            var account = GetAccount(accountId);
            account.Password = newPassword;
            bankAppContext.Accounts.Update(account);
            bankAppContext.SaveChanges();
            return true;
        }


    }
}
