using System;
using System.Collections.Generic;
using BankApplication.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Models.Exceptions;
using BankApplication.Models.Enums;
using BankApplication.Service;

namespace BankApplication
{
    public class CustomerOptions:IGetMenu
    {
        public void GetMenu( string bankId, string accountId, int password)
        {
            ICustomerServiceInterface serviceInterface = new CustomerService();
            int count = 0;
            while (count == 0)
            {
                StandardMessages.CustomerChoiceDisplay();
                switch ((CustomerEnum)Enum.Parse(typeof(CustomerEnum), Console.ReadLine()))
                {
                    case CustomerEnum.Deposit:
                        {
                            foreach(KeyValuePair<string,decimal> kvp in Datastore.Currency)
                            {
                                StandardMessages.PrintString($"Currency={kvp.Key}  MultiplierRate with INR={kvp.Value}");
                            }
                            string currencyType = StandardMessages.GetUserInput("Select the type of currency to be deposited");

                            decimal amount = StandardMessages.GetUserInputAsDecimal("Enter amount");                          
                             decimal effectiveAmount=serviceInterface.Deposit(bankId, amount,currencyType, accountId, password);
                             StandardMessages.PrintString($"Amount{effectiveAmount} deposited in accountnumber {accountId}");                           
                            break;
                        }
                    case CustomerEnum.WithDraw:
                        {
                            decimal amount = StandardMessages.GetUserInputAsDecimal("Enter amount");
                            try
                            {
                               serviceInterface.WithDraw(bankId, amount, accountId, password);
                               StandardMessages.PrintString($"Amount{amount}withdrawn from accountnumber{accountId}");
                            }
                            catch (AmountNotSufficient )
                            {
                                StandardMessages.PrintString("Amount not sufficient");
                            }
                            
                            break;
                        }

                    case CustomerEnum.CheckBalance:
                        {
                          StandardMessages.PrintString($"{serviceInterface.GetBalance(bankId, accountId, password)}");
                          break;
                        }
                    case CustomerEnum.TransferAmount:
                        {
                            ReceiverValidateAgain:
                            string receiverBankId = StandardMessages.GetUserInput("Enter the receiver bankid");
                            string receiverAccountId = StandardMessages.GetUserInput("enter receiver accountid");
                            decimal amount = StandardMessages.GetUserInputAsDecimal("Enter amount");
                            string paymentMode = StandardMessages.GetUserInput("Enter payment mode");
                            try
                            {
                                serviceInterface.TransferAmount(bankId, accountId, password, amount, receiverBankId, receiverAccountId, paymentMode);
                                StandardMessages.PrintString("Amount transferred succesfully");
                            }
                            catch (AmountNotSufficient)
                            {
                                StandardMessages.PrintString("Amount not sufficient");
                            }
                            catch (IncorrectBankIdException)
                            {
                                receiverBankId = StandardMessages.GetUserInput("Incorrect Bank Id entered\nPlease try to re enter bank id");
                                goto ReceiverValidateAgain;
                            }
                            catch (IncorrectPin )
                            {
                                accountId = StandardMessages.GetUserInput("Password you have entered is invalid \n Please try to reenter password");
                                goto ReceiverValidateAgain;
                            }
                            catch (IncorrectAccountNumberException )
                            {
                                accountId = StandardMessages.GetUserInput("AccountNumber you have entered is invalid \n Please try to reenter account number");
                                goto ReceiverValidateAgain;
                            }
                            break;
                        }

                    case CustomerEnum.Transactionhistory:
                        {
                                List<Transaction> transactions = serviceInterface.GetTransactionHistory(bankId, accountId);
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
       