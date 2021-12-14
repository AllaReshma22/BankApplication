using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Models;
using BankApplication.Service;
using BankApplication.Models.Models;
using BankApplication.Service.Interfaces;
using BankApplication.Models.Exceptions;
using BankApplication.Models.Enums;

namespace BankApplication
{
    public class BankStaffOptions:IGetMenu
    {
        public void GetMenu(string bankId)
        {
            var bankAppContext = new BankAppContext();
            IBankStaffServiceInterface serviceInterface = new BankStaffService(bankAppContext);
            ICommonServiceInterface commonService = new BankStaffService(bankAppContext);
            int count = 0;
            while (count == 0)
            {

                StandardMessages.BankStaffChoiceDisplay();
                switch ((StaffEnum)Enum.Parse(typeof(StaffEnum), Console.ReadLine()))
                {
                    case StaffEnum.CreateAccount:
                        {
                            string accountHolderName = StandardMessages.GetUserInput("Enter account holder name");
                            PasswordReenter:
                            int password = StandardMessages.GetUserInputAsInt("Enter 4 digit password for setup:");
                            if (Validations.PasswordValidate(password) == false)
                            { 
                               StandardMessages.PrintString("Password you have entered is not valid,try to reenter password");
                                goto PasswordReenter;
                            }                              
                            decimal balance = StandardMessages.GetUserInputAsDecimal("Enter initial balance");
                            string accountId = serviceInterface.CreateAccount(bankId, accountHolderName, password, balance);
                            StandardMessages.PrintString($"Account created succesfully in bank with accountnumber {accountId} in bank with bankid {bankId}");
                            break;
                        }
                    case StaffEnum.RevertTransaction:
                        {
                            string transactionId = StandardMessages.GetUserInput("Enter the transactionid");
                            serviceInterface.RevertTransaction(transactionId);
                            StandardMessages.PrintString($"Transaction with transactionid {transactionId} is reverted");
                            break;
                        }
                    case StaffEnum.UpdateSameBankRTGS:
                        {
                            decimal newRTGS = StandardMessages.GetUserInputAsDecimal("Enter newRTGS");
                            serviceInterface.UpdateSameBankRtgs(bankId, newRTGS);
                            StandardMessages.PrintString($"Bank RTGS service charge for same bank updated to {newRTGS} ");
                            break;
                        }
                    case StaffEnum.UpdateOtherBankRTGS:
                        {
                            decimal newRTGS = StandardMessages.GetUserInputAsDecimal("Enter newRTGS");
                            serviceInterface.UpdateOtherBankRtgs(bankId, newRTGS);
                            StandardMessages.PrintString($"Bank RTGS service charge for other bank updated to {newRTGS} ");
                            break;
                        }
                    case StaffEnum.UpdateOtherBankIMPS:
                        {
                            decimal newIMPS = StandardMessages.GetUserInputAsDecimal("Enter newIMPS");
                            serviceInterface.UpdateOtherBankImps(bankId, newIMPS);
                            StandardMessages.PrintString($"Bank IMPS service charge for other bank updated to {newIMPS} ");
                            break;
                        }
                    case StaffEnum.UpdateSameBankIMPS:
                        {
                            decimal newIMPS = StandardMessages.GetUserInputAsDecimal("Enter newIMPS");
                            serviceInterface.UpdateSameBankImps(bankId, newIMPS);
                            StandardMessages.PrintString($"Bank IMPS service charge for same bank updated to {newIMPS} ");
                            break;
                        }
                    case StaffEnum.UpdateAccount:
                        {
                            string accountId = StandardMessages.GetUserInput("Enter account number");
                            int newpassword = StandardMessages.GetUserInputAsInt("Enter new 4 digit password for update");
                            try 
                            {
                                serviceInterface.UpdateAccountPassword(accountId, newpassword);
                            }
                            catch (IncorrectAccountIdException)
                            {
                                StandardMessages.PrintString("AccountId you have entered is incorrect");
                            }
                            break;
                        }
                    case StaffEnum.DeleteAccount:
                        {
                            string accountId = StandardMessages.GetUserInput("Enter accountid");
                            bool result=serviceInterface.DeleteAccount(accountId);
                            try
                            {
                                if (result == true)
                                    StandardMessages.PrintString($"Account with accountId {accountId} has been deleted succesfully ");
                            }
                            catch (IncorrectAccountIdException)
                            {
                                StandardMessages.PrintString("AccountId you have entered is incorrect");
                            }
                            break;
                        }
                    case StaffEnum.GetBalance:
                        {
                            string accountId = StandardMessages.GetUserInput("Enter accountid");
                            StandardMessages.PrintString($"{serviceInterface.GetBalance(accountId)}");
                            break;
                        }
                    case StaffEnum.ViewTransactionHistoryOfAccount:
                        {
                            string accountId = StandardMessages.GetUserInput("Enter account number");
                            List<string> Transactions = commonService.TransactionHistory(accountId);
                            for (int i = 0; i < Transactions.Count; i++)
                            {
                                StandardMessages.PrintString(Transactions[i]);
                            }
                            break;
                        }
                    case StaffEnum.Exit:
                        {
                            count = 1;
                            break;
                        }

                    default:
                        StandardMessages.PrintString("Enter valid number from 1-11");
                        break;
                }
            }
        }
    }
}

