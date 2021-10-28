using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Models;
using BankApplication.Service;
using BankApplication.Models.Exceptions;

namespace BankApplication
{
    public class BankStaffOptions:IGetMenu
    {
        public void GetMenu(string bankId)
        {
            IServiceInterface serviceInterface = new BankStaffActions();
            int count = 0;
            while (count == 0)
            {

                Displays.BankStaffChoiceDisplay();
                Enums.StaffEnum staffChoice = (Enums.StaffEnum)Enum.Parse(typeof(Enums.StaffEnum), Console.ReadLine());
                switch (staffChoice)
                {
                    case Enums.StaffEnum.CreateAccount:
                        {
                            string accountHolderName = Displays.GetUserInput("Enter account holder name");
                            int password = Displays.GetUserInputAsInt("Enter 4 digit password for setup:");
                            double balance = Displays.GetUserInputAsDouble("Enter initial balance");
                            string accountId = serviceInterface.CreateAccount(bankId, accountHolderName, password, balance);
                            Displays.PrintString($"Account created succesfully in bank with accountnumber {accountId} in bank with bankid {bankId}");
                            break;
                        }
                    case Enums.StaffEnum.AddNewAcceptedCurrency:
                        {
                            string newAcceptedCurrency = Displays.GetUserInput("Enter new accepted currency ");
                            double multiplier = Displays.GetUserInputAsDouble("Enter its multiplier to convert to INR");
                            serviceInterface.AddAcceptedCurrency(bankId, newAcceptedCurrency, multiplier);
                            Displays.PrintString($"New accepted currency is{newAcceptedCurrency}");
                            break;
                        }
                    case Enums.StaffEnum.RevertTransaction:
                        {
                            string transactionId = Displays.GetUserInput("Enter the transactionid");
                            string accountId = Displays.GetUserInput("Enter the accountid");
                            serviceInterface.RevertTransaction(transactionId,bankId,accountId);
                            Displays.PrintString($"Transaction with transactionid {transactionId} is reverted");
                            break;
                        }
                    case Enums.StaffEnum.UpdateSameBankRTGS:
                        {
                            int newRTGS = Displays.GetUserInputAsInt("Enter newRTGS");
                            serviceInterface.UpdateSameBankRTGS(bankId, newRTGS);
                            Displays.PrintString($"Bank RTGS service charge for same bank updated to {newRTGS} ");
                            break;
                        }
                    case Enums.StaffEnum.UpdateOtherBankRTGS:
                        {
                            int newRTGS = Displays.GetUserInputAsInt("Enter newRTGS");
                            serviceInterface.UpdateOtherBankRTGS(bankId, newRTGS);
                            Displays.PrintString($"Bank RTGS service charge for other bank updated to {newRTGS} ");
                            break;
                        }
                    case Enums.StaffEnum.UpdateOtherBankIMPS:
                        {
                            int newIMPS = Displays.GetUserInputAsInt("Enter newIMPS");
                            serviceInterface.UpdateOtherBankIMPS(bankId, newIMPS);
                            Displays.PrintString($"Bank IMPS service charge for other bank updated to {newIMPS} ");
                            break;
                        }
                    case Enums.StaffEnum.UpdateSameBankIMPS:
                        {
                            int newIMPS = Displays.GetUserInputAsInt("Enter newIMPS");
                            serviceInterface.UpdateSameBankIMPS(bankId, newIMPS);
                            Displays.PrintString($"Bank IMPS service charge for same bank updated to {newIMPS} ");
                            break;
                        }
                    case Enums.StaffEnum.RevertTransfer:
                        {
                            string senderTransactionId = Displays.GetUserInput("Enter Sendertransactionid");
                            string receiverTransactionId = Displays.GetUserInput("Enter Receivertransactionid");
                            string senderBankId = Displays.GetUserInput("Enter the receiver bankid");
                            string senderAccountId = Displays.GetUserInput("enter receiver accountid");
                            string receiverBankId = Displays.GetUserInput("Enter the receiver bankid");
                            string receiverAccountId= Displays.GetUserInput("enter receiver accountid");
                            serviceInterface.RevertTransfer(senderTransactionId, receiverTransactionId,senderBankId,senderAccountId,receiverBankId,receiverAccountId);
                            break;
                        }
                    case Enums.StaffEnum.ViewTransactionHistoryOfAccount:
                        {
                            string accountId = Displays.GetUserInput("Enter accountid");
                            List<Transaction> transactions =serviceInterface.GetTransactionHistory(bankId, accountId);
                            foreach (Transaction transaction in transactions)
                            {
                                Displays.PrintString(transaction.TransactionId + " " + transaction.Type);
                                if (transaction.Type == TransactionType.transactionType.Deposit)
                                    Displays.PrintString("credited+" + transaction.Amount);
                                else
                                    Displays.PrintString("debited- " + transaction.Amount);
                                Displays.PrintString("");
                            }

                            break;
                        }
                    case Enums.StaffEnum.UpdateAccount:
                        {
                            string accountId = Displays.GetUserInput("Enter account number");
                            int newpassword = Displays.GetUserInputAsInt("Enter new 4 digit password for update");
                            serviceInterface.UpdateAccountPassword(bankId, accountId, newpassword);
                            break;
                        }
                    case Enums.StaffEnum.DeleteAccount:
                        {
                            string accountId = Displays.GetUserInput("Enter accountid");
                            serviceInterface.DeleteAccount(bankId,accountId);
                            break;
                        }
                    case Enums.StaffEnum.Exit:
                        {
                            count = 1;
                            break;
                        }

                    default:
                        Displays.PrintString("Enter valid number from 1-12");
                        break;
                }
            }
        }

    }
}

