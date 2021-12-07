using System;
using System.Collections.Generic;
using BankApplication.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Models.Exceptions;
using BankApplication.Models.Enums;
using BankApplication.Models.Models;
using BankApplication.Service;
using BankApplication.Service.Interfaces;

namespace BankApplication
{
    public class CustomerOptions:IGetMenu
    {
        public void GetMenu(string accountId, int password)
        {
            var bankAppContext = new BankAppContext();
            ICustomerServiceInterface serviceInterface = new CustomerService(bankAppContext);
            int count = 0;
            while (count == 0)
            {
                StandardMessages.CustomerChoiceDisplay();
                switch ((CustomerEnum)Enum.Parse(typeof(CustomerEnum), Console.ReadLine()))
                {
                    case CustomerEnum.Deposit:
                        {
                            decimal amount = StandardMessages.GetUserInputAsDecimal("Enter amount");
                            serviceInterface.Deposit(accountId, amount);
                            StandardMessages.PrintString($"Amount{amount} deposited in accountnumber {accountId}");
                            break;
                        }
                    case CustomerEnum.WithDraw:
                        {
                            decimal amount = StandardMessages.GetUserInputAsDecimal("Enter amount");
                            try
                            {
                                serviceInterface.WithDraw(accountId, amount);
                                StandardMessages.PrintString($"Amount{amount}withdrawn from accountnumber{accountId}");
                            }
                            catch (AmountNotSufficient)
                            {
                                StandardMessages.PrintString("Amount not sufficient");
                            }

                            break;
                        }

                    case CustomerEnum.CheckBalance:
                        {
                            StandardMessages.PrintString($"{serviceInterface.GetBalance(accountId)}");
                            break;
                        }
                    case CustomerEnum.TransferAmount:
                        {
                        ReceiverValidateAgain:
                            string receiverAccountId = StandardMessages.GetUserInput("enter receiver accountid");
                            decimal amount = StandardMessages.GetUserInputAsDecimal("Enter amount");
                            string paymentMode = StandardMessages.GetUserInput("Enter the payment mode");
                            try
                            {
                                serviceInterface.TransferAmount(accountId, receiverAccountId, amount,paymentMode);
                                StandardMessages.PrintString("Amount transferred succesfully");
                            }
                            catch (AmountNotSufficient)
                            {
                                StandardMessages.PrintString("Amount not sufficient");
                            }
                            catch (IncorrectAccountNumberException)
                            {
                                accountId = StandardMessages.GetUserInput("AccountNumber you have entered is invalid \n Please try to reenter account number");
                                goto ReceiverValidateAgain;
                            }
                            break;
                        }

                    case CustomerEnum.Transactionhistory:
                        {
                            List<string> Transactions = serviceInterface.TransactionHistory(accountId);
                            break;
                        }
                    case CustomerEnum.Exit:
                        {
                            count = 1;
                            break;
                        }

                    default:
                        StandardMessages.PrintString("Enter valid number from 1-6");
                        break;
                }
            }
        }
    }
}
