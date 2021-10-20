using System;
using BankApplication.Service;
using BankApplication.Models;
using System.Linq;
using System.Collections.Generic;
using BankApplication.Models.Exceptions;
namespace BankApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            //Giving title to the console
            Console.Title = "Bank App";
            while (true)
            { 

               printString("Enter Your Choice");
               printString("1.Bank staff      ");
               printString("2.customer        ");
               printString("3.EXIT           ");
               //displaying options
                MainUsermenu.Choice choice = (MainUsermenu.Choice)Enum.Parse(typeof(MainUsermenu.Choice), Console.ReadLine());
                switch (choice)
                {
                    case MainUsermenu.Choice.Bankstaff:
                        {
                            BankStaffActions bankstaffactions = new BankStaffActions();
                            printString("________________________________");
                            printString("Enter Your Choice                ");
                            printString("1.CreateAccount                  ");
                            printString("2.AddNewAcceptedCurrency           ");
                            printString("3.AddServiceChargesForSameBank     ");
                            printString("4.AddServiceChargesForDifferentBank");
                            printString("5.ViewTransactionHistoryOfAccount  ");
                            printString("6.RevertTransaction                ");
                            printString("_________________________________");
                            BankStaffusermenu.StaffChoice staffChoice = (BankStaffusermenu.StaffChoice)Enum.Parse(typeof(BankStaffusermenu.StaffChoice), Console.ReadLine());
                            switch (staffChoice)
                            {
                                case BankStaffusermenu.StaffChoice.CreateAccount:
                                    {
                                        string bankid = GetUserInput("Enter the bankid ");
                                        string AccountHolderName = GetUserInput("Enter account holder name");
                                        int Password = GetUserInputAsInt("Enter password for setup:");
                                        double Balance = GetUserInputAsDouble("Enter initial balance");
                                        try
                                        {
                                            string AccountNumber = bankstaffactions.createAccount(bankid, AccountHolderName, Password, Balance);
                                            printString($"Account created succesfully in bank with accountnumber {AccountNumber} in bank with bankid {bankid}");
                                        }
                                        catch (IncorrectBankIdException ex)
                                        {
                                            printString(ex.Message);
                                        }

                                        break;
                                    }
                            }
                            break;

                        }

                    case MainUsermenu.Choice.Customer:
                        {
                            printString("______________________________");
                            printString("|ENTER YOUR CHOICE           |");
                            printString("|1.Add Bank                  |");
                            printString("|2.Create account            |");
                            printString("|3.Deposit Amount            |");
                            printString("|4.Withdraw amount           |");
                            printString("|5.transfer amount           |");
                            printString("|6.checkbalance              |");
                            printString("|7.Transaction history       |");
                            printString("|8.Exit                      |");
                            printString("______________________________");
                            BankService bankService = new BankService();
                            CustomerUsermenu userMenu = new CustomerUsermenu();
                            CustomerUsermenu.Features features = (CustomerUsermenu.Features)Enum.Parse(typeof(CustomerUsermenu.Features), Console.ReadLine());
                            switch (features)
                            {
                                /*  case CustomerUsermenu.Features.AddBank:
                                      {
                                          String Bankname = GetUserInput("Enter bank name");
                                          string bankid = bankService.addBank(Bankname);
                                          printString($"Bank added successfully with bank id {bankid} and bankname {Bankname}");
                                          break;
                                      }*/
                                case CustomerUsermenu.Features.Deposit:
                                    {
                                        string bankid = GetUserInput("Enter the bankid ");
                                        string AccountNumber = GetUserInput("Enter account number");
                                        double Amount = GetUserInputAsDouble("Enter amount");
                                        int Password = GetUserInputAsInt("Enter password");
                                        try
                                        {
                                            bankService.Deposit(bankid, Amount, AccountNumber, Password);
                                            printString($"Amount{Amount} deposited in accountnumber {AccountNumber}");
                                        }
                                        catch (AmountNotSufficient ex)
                                        {
                                            printString(ex.Message);
                                        }
                                        catch (IncorrectBankIdException ex)
                                        {
                                            printString(ex.Message);
                                        }
                                        catch (IncorrectPin ex)
                                        {
                                            printString(ex.Message);
                                        }
                                        catch (IncorrectAccountNumberException ex)
                                        {
                                            printString(ex.Message);
                                        }
                                        break;
                                    }
                                case CustomerUsermenu.Features.Withdraw:
                                    {
                                        string bankid = GetUserInput("Enter the bankid ");
                                        string AccountNumber = GetUserInput("Enter Ac number");
                                        double Amount = GetUserInputAsDouble("Enter amount");
                                        int Password = GetUserInputAsInt("Enter pin: ");
                                        try
                                        {
                                            bankService.WithDraw(bankid, Amount, AccountNumber, Password);
                                            printString($"Amount{Amount}withdrawn from accountnumber{AccountNumber}");
                                        }
                                        catch (AmountNotSufficient ex)
                                        {
                                            printString(ex.Message);
                                        }
                                        catch (IncorrectBankIdException ex)
                                        {
                                            printString(ex.Message);
                                        }
                                        catch (IncorrectPin ex)
                                        {
                                            printString(ex.Message);
                                        }
                                        catch (IncorrectAccountNumberException ex)
                                        {
                                            printString(ex.Message);
                                        }
                                        break;
                                    }

                                case CustomerUsermenu.Features.CheckBalance:
                                    {
                                        string bankid = GetUserInput("Enter the bankid ");
                                        string AccountNumber = GetUserInput("enter acnumber");
                                        int Password = GetUserInputAsInt("Enter pin");
                                        try
                                        {
                                            printString($"{bankService.GetBalance(bankid, AccountNumber, Password)}");
                                        }
                                        catch (AmountNotSufficient ex)
                                        {
                                            printString(ex.Message);
                                        }
                                        catch (IncorrectBankIdException ex)
                                        {
                                            printString(ex.Message);
                                        }
                                        catch (IncorrectPin ex)
                                        {
                                            printString(ex.Message);
                                        }
                                        catch (IncorrectAccountNumberException ex)
                                        {
                                            printString(ex.Message);
                                        }
                                        break;
                                    }
                                case CustomerUsermenu.Features.TransferAmount:
                                    {
                                        string senderbankid = GetUserInput("Enter the bankid ");
                                        string SenderAccountNumber = GetUserInput("entersender account number");
                                        int SenderPassword = GetUserInputAsInt("Enter pin");
                                        string receiverbankid = GetUserInput("Enter the receiver bankid");
                                        string ReceiverAccountNumber = GetUserInput("enter receiver account number");
                                        double Amount = GetUserInputAsDouble("Enter amount");
                                        try
                                        {
                                            bankService.transferAmount(senderbankid, SenderAccountNumber, SenderPassword, Amount, receiverbankid, ReceiverAccountNumber);
                                            printString("Amount transferred succesfully");
                                        }
                                        catch (AmountNotSufficient ex)
                                        {
                                            printString(ex.Message);
                                        }
                                        catch (IncorrectBankIdException ex)
                                        {
                                            printString(ex.Message);
                                        }
                                        catch (IncorrectPin ex)
                                        {
                                            printString(ex.Message);
                                        }
                                        catch (IncorrectAccountNumberException ex)
                                        {
                                            printString(ex.Message);
                                        }
                                        break;
                                    }

                                case CustomerUsermenu.Features.Transactionhistory:
                                    {
                                        string bankid = GetUserInput("Enter the bankid ");
                                        string AccountNumber = GetUserInput("enter acnumber");
                                        int Password = GetUserInputAsInt("Enter Password");
                                        try
                                        {
                                            List<Transaction> transactions = bankService.GetTransactionhistory(bankid, AccountNumber, Password);
                                            foreach (Transaction transaction in transactions)
                                            {
                                                printString(transaction.TransactionId + " " + transaction.Type);
                                                if (transaction.Type == TransactionType.transactionType.Deposit)
                                                    printString("credited+" + transaction.Amount);
                                                else
                                                    printString("debited- " + transaction.Amount);
                                                printString("");
                                            }
                                        }
                                        catch (IncorrectBankIdException ex)
                                        {
                                            printString(ex.Message);
                                        }
                                        catch (IncorrectPin ex)
                                        {
                                            printString(ex.Message);
                                        }
                                        catch (IncorrectAccountNumberException ex)
                                        {
                                            printString(ex.Message);
                                        }


                                        break;
                                    }


                                case CustomerUsermenu.Features.Exit:
                                    {
                                        Environment.Exit(0);
                                        break;
                                    }

                            }
                            break;

                        }
                }

            }
            static string GetUserInput(string message)
            {
                Console.WriteLine(message);
                return Console.ReadLine();
            }
            static int GetUserInputAsInt(string message)
            {
                return int.Parse(GetUserInput(message));
            }

            static double GetUserInputAsDouble(string message)
            {
                Console.WriteLine(message);
                return double.Parse(Console.ReadLine());
            }
            static int printString(string str)
            {
                Console.WriteLine(str);
                return 1;
            }


        }
    }
}
