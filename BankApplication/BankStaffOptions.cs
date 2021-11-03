using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Models;
using BankApplication.Service;
using BankApplication.Models.Exceptions;
using BankApplication.Models.Enums;

namespace BankApplication
{
    public class BankStaffOptions:IGetMenu
    {
        public void GetMenu(string bankId)
        {
            IBankStaffServiceInterface serviceInterface = new BankStaffService();
            ICommonServiceInterface commonServiceInterface = new BankStaffService();
            int count = 0;
            while (count == 0)
            {

                StandardMessages.BankStaffChoiceDisplay();
                switch ((StaffEnum)Enum.Parse(typeof(StaffEnum), Console.ReadLine()))
                {
                    case StaffEnum.CreateAccount:
                        {
                            string accountHolderName = StandardMessages.GetUserInput("Enter account holder name");
                            int password = StandardMessages.GetUserInputAsInt("Enter 4 digit password for setup:");
                            decimal balance = StandardMessages.GetUserInputAsDecimal("Enter initial balance");
                            string accountId = serviceInterface.CreateAccount(bankId, accountHolderName, password, balance);
                            StandardMessages.PrintString($"Account created succesfully in bank with accountnumber {accountId} in bank with bankid {bankId}");
                            break;
                        }
                    case StaffEnum.AddNewAcceptedCurrency:
                        {
                            string newAcceptedCurrency = StandardMessages.GetUserInput("Enter new accepted currency ");
                            decimal multiplier = StandardMessages.GetUserInputAsDecimal("Enter its multiplier to convert to INR");
                            serviceInterface.AddAcceptedCurrency(bankId, newAcceptedCurrency, multiplier);
                            StandardMessages.PrintString($"New accepted currency is{newAcceptedCurrency}");
                            break;
                        }
                    case StaffEnum.RevertTransaction:
                        {
                            string transactionId = StandardMessages.GetUserInput("Enter the transactionid");
                            string accountId = StandardMessages.GetUserInput("Enter the accountid");
                            serviceInterface.RevertTransaction(transactionId,bankId,accountId);
                            StandardMessages.PrintString($"Transaction with transactionid {transactionId} is reverted");
                            break;
                        }
                    case StaffEnum.UpdateSameBankRTGS:
                        {
                            decimal newRTGS = StandardMessages.GetUserInputAsDecimal("Enter newRTGS");
                            serviceInterface.UpdateSameBankRTGS(bankId, newRTGS);
                            StandardMessages.PrintString($"Bank RTGS service charge for same bank updated to {newRTGS} ");
                            break;
                        }
                    case StaffEnum.UpdateOtherBankRTGS:
                        {
                            decimal newRTGS = StandardMessages.GetUserInputAsDecimal("Enter newRTGS");
                            serviceInterface.UpdateOtherBankRTGS(bankId, newRTGS);
                            StandardMessages.PrintString($"Bank RTGS service charge for other bank updated to {newRTGS} ");
                            break;
                        }
                    case StaffEnum.UpdateOtherBankIMPS:
                        {
                            decimal newIMPS = StandardMessages.GetUserInputAsDecimal("Enter newIMPS");
                            serviceInterface.UpdateOtherBankIMPS(bankId, newIMPS);
                            StandardMessages.PrintString($"Bank IMPS service charge for other bank updated to {newIMPS} ");
                            break;
                        }
                    case StaffEnum.UpdateSameBankIMPS:
                        {
                            decimal newIMPS = StandardMessages.GetUserInputAsDecimal("Enter newIMPS");
                            serviceInterface.UpdateSameBankIMPS(bankId, newIMPS);
                            StandardMessages.PrintString($"Bank IMPS service charge for same bank updated to {newIMPS} ");
                            break;
                        }
                    case StaffEnum.RevertTransfer:
                        {
                            string senderTransactionId = StandardMessages.GetUserInput("Enter Sendertransactionid");
                            string receiverTransactionId = StandardMessages.GetUserInput("Enter Receivertransactionid");
                            string senderBankId = StandardMessages.GetUserInput("Enter the receiver bankid");
                            string senderAccountId = StandardMessages.GetUserInput("enter receiver accountid");
                            string receiverBankId = StandardMessages.GetUserInput("Enter the receiver bankid");
                            string receiverAccountId= StandardMessages.GetUserInput("enter receiver accountid");
                            serviceInterface.RevertTransfer(senderTransactionId, receiverTransactionId,senderBankId,senderAccountId,receiverBankId,receiverAccountId);
                            break;
                        }
                    case StaffEnum.ViewTransactionHistoryOfAccount:
                        {
                            string accountId = StandardMessages.GetUserInput("Enter accountid");
                            List<Transaction> transactions =commonServiceInterface.GetTransactionHistory(bankId, accountId);
                            foreach (Transaction transaction in transactions)
                            {
                                StandardMessages.PrintString(transaction.TransactionId + " " + transaction.Type);
                                if (transaction.Type == TransactionType.Deposit)
                                    StandardMessages.PrintString("credited+" + transaction.Amount);
                                else
                                    StandardMessages.PrintString("debited- " + transaction.Amount);
                                StandardMessages.PrintString("");
                            }

                            break;
                        }
                    case StaffEnum.UpdateAccount:
                        {
                            string accountId = StandardMessages.GetUserInput("Enter account number");
                            int newpassword = StandardMessages.GetUserInputAsInt("Enter new 4 digit password for update");
                            serviceInterface.UpdateAccountPassword(bankId, accountId, newpassword);
                            break;
                        }
                    case StaffEnum.DeleteAccount:
                        {
                            string accountId = StandardMessages.GetUserInput("Enter accountid");
                            serviceInterface.DeleteAccount(bankId,accountId);
                            break;
                        }
                    case StaffEnum.Exit:
                        {
                            count = 1;
                            break;
                        }

                    default:
                        StandardMessages.PrintString("Enter valid number from 1-12");
                        break;
                }
            }
        }

    }
}

