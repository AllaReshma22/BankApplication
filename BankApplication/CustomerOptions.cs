using System;
using System.Collections.Generic;
using BankApplication.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Models.Exceptions;
using BankApplication.Service;

namespace BankApplication
{
    public class CustomerOptions:IGetMenu
    {
        public void GetMenu( string bankId, string accountId, int password)
        {
            IServiceInterface serviceInterface = new BankService();
            int count = 0;
            while (count == 0)
            {
                Displays.CustomerChoiceDisplay();
                Enums.CustomerEnum Choice = (Enums.CustomerEnum)Enum.Parse(typeof(Enums.CustomerEnum), Console.ReadLine());

                switch (Choice)
                {
                    case Enums.CustomerEnum.Deposit:
                        {
                            foreach(KeyValuePair<string,double> kvp in Datastore.Currency)
                            {
                                Displays.PrintString($"Currency={kvp.Key}  MultiplierRate with INR={kvp.Value}");
                            }
                            string currencyType = Displays.GetUserInput("Select the type of currency to be deposited");

                            double amount = Displays.GetUserInputAsDouble("Enter amount");                          
                             double effectiveAmount=serviceInterface.Deposit(bankId, amount,currencyType, accountId, password);
                             Displays.PrintString($"Amount{effectiveAmount} deposited in accountnumber {accountId}");                           
                            break;
                        }
                    case Enums.CustomerEnum.WithDraw:
                        {
                            double amount = Displays.GetUserInputAsDouble("Enter amount");
                            try
                            {
                               serviceInterface.WithDraw(bankId, amount, accountId, password);
                                Displays.PrintString($"Amount{amount}withdrawn from accountnumber{accountId}");
                            }
                            catch (AmountNotSufficient ex)
                            {
                                Displays.PrintString(ex.Message);
                            }
                            
                            break;
                        }

                    case Enums.CustomerEnum.CheckBalance:
                        {
                          Displays.PrintString($"{serviceInterface.GetBalance(bankId, accountId, password)}");
                          break;
                        }
                    case Enums.CustomerEnum.TransferAmount:
                        {
                            string receiverBankId = Displays.GetUserInput("Enter the receiver bankid");
                            string receiverAccountId = Displays.GetUserInput("enter receiver accountid");
                            double amount = Displays.GetUserInputAsDouble("Enter amount");
                            string paymentMode = Displays.GetUserInput("Enter payment mode");
                            try
                            {
                                serviceInterface.TransferAmount(bankId, accountId, password, amount, receiverBankId, receiverAccountId, paymentMode);
                                Displays.PrintString("Amount transferred succesfully");
                            }
                            catch (AmountNotSufficient ex)
                            {
                                Displays.PrintString(ex.Message);
                            }
                            catch (IncorrectBankIdException ex)
                            {
                                Displays.PrintString(ex.Message);
                            }
                            catch (IncorrectPin ex)
                            {
                                Displays.PrintString(ex.Message);
                            }
                            catch (IncorrectAccountNumberException ex)
                            {
                                Displays.PrintString(ex.Message);
                            }
                            break;
                        }

                    case Enums.CustomerEnum.Transactionhistory:
                        {
                                List<Transaction> transactions = serviceInterface.GetTransactionHistory(bankId, accountId);
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
                    case Enums.CustomerEnum.Exit:
                        {
                            count = 1;
                            break;
                        }

                    default:
                        Displays.PrintString("Enter valid number from 1-6");
                        break;
                }
            }
        }
    }
}
       