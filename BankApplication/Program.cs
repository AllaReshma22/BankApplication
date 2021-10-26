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
            BankStaffActions bankstaffactions = new BankStaffActions();
            bankstaffactions.addBank("SBI");
            bankstaffactions.addBank("Union");
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
                        string bankid = GetUserInput("Enter bankid");
                        string staffid = GetUserInput("Enter staffid for authentication");
                        
                        bool Validation = bankstaffactions.StaffValidate(bankid, staffid);
                        if (Validation == true)
                        {
                            while (true)
                            {
                                printString("___________________________________");
                                printString("Enter Your Choice                  ");
                                printString("1.CreateAccount                    ");
                                printString("2.AddNewAcceptedCurrency           ");
                                printString("3.AddRTGSServiceChargesForSameBank ");
                                printString("4.AddRTGSServiceChargesForOtherBank");
                                printString("5.AddIMPSServiceChargesForSameBank ");
                                printString("6.AddIMPSServiceChargesForOtherBank");
                                printString("7.ViewTransactionHistoryOfAccount  ");
                                printString("8.RevertTransaction                ");
                                printString("9.Revert Transfer                  ");
                                printString("10.Updateaccountpassword           ");
                                printString("11.Delete Account                  ");
                                printString("12.Exit                             ");
                                printString("___________________________________");
                                BankStaffusermenu.StaffChoice staffChoice = (BankStaffusermenu.StaffChoice)Enum.Parse(typeof(BankStaffusermenu.StaffChoice), Console.ReadLine());
                                switch (staffChoice)
                                {
                                    case BankStaffusermenu.StaffChoice.CreateAccount:
                                        {
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
                                    case BankStaffusermenu.StaffChoice.AddNewAcceptedCurrency:
                                        {
                                            string newAcceptedcurrency = GetUserInput("Enter new accepted currency");
                                            bankstaffactions.UpdateAcceptedCurrency(bankid, newAcceptedcurrency);
                                            printString($"New accepted currency is{newAcceptedcurrency}");
                                            break;
                                        }
                                    case BankStaffusermenu.StaffChoice.RevertTransaction:
                                        {
                                            string TransactionId = GetUserInput("Enter the transactionid");
                                            bankstaffactions.revertTransaction(TransactionId);
                                            printString($"Transaction with transactionid {TransactionId} is reverted");
                                            break;
                                        }
                                    case BankStaffusermenu.StaffChoice.UpdateSameBankRTGS:
                                        {
                                            int newRTGS = GetUserInputAsInt("Enter newRTGS");
                                            bankstaffactions.UpdateSameBankRTGS(bankid, newRTGS);
                                            printString($"Bank RTGS service charge for same bank updated to {newRTGS} ");
                                            break;
                                        }
                                    case BankStaffusermenu.StaffChoice.UpdateOtherBankRTGS:
                                        {
                                            int newRTGS = GetUserInputAsInt("Enter newRTGS");
                                            bankstaffactions.UpdateOtherBankRTGS(bankid, newRTGS);
                                            printString($"Bank RTGS service charge for other bank updated to {newRTGS} ");
                                            break;
                                        }
                                    case BankStaffusermenu.StaffChoice.UpdateOtherBankIMPS:
                                        {
                                            int newIMPS = GetUserInputAsInt("Enter newIMPS");
                                            bankstaffactions.UpdateOtherBankIMPS(bankid, newIMPS);
                                            printString($"Bank IMPS service charge for other bank updated to {newIMPS} ");
                                            break;
                                        }
                                    case BankStaffusermenu.StaffChoice.UpdateSameBankIMPS:
                                        {
                                            int newIMPS = GetUserInputAsInt("Enter newIMPS");
                                            bankstaffactions.UpdateSameBankIMPS(bankid, newIMPS);
                                            printString($"Bank IMPS service charge for same bank updated to {newIMPS} ");
                                            break;
                                        }
                                    case BankStaffusermenu.StaffChoice.RevertTransfer:
                                        {
                                            string SenderTransactionId = GetUserInput("Enter Sendertransactionid");
                                            string ReceiverTransactionId = GetUserInput("Enter Receivertransactionid");
                                            bankstaffactions.revertTransfer(SenderTransactionId, ReceiverTransactionId);
                                            break;
                                        }
                                    case BankStaffusermenu.StaffChoice.ViewTransactionHistoryOfAccount:
                                        {
                                            string accountnumber = GetUserInput("Enter account number");
                                            List<Transaction> transactions = bankstaffactions.GetTransactionhistory(bankid, accountnumber);
                                            foreach (Transaction transaction in transactions)
                                            {
                                                printString(transaction.TransactionId + " " + transaction.Type);
                                                if (transaction.Type == TransactionType.transactionType.Deposit)
                                                    printString("credited+" + transaction.Amount);
                                                else
                                                    printString("debited- " + transaction.Amount);
                                                printString("");
                                            }

                                            break;
                                        }
                                    case BankStaffusermenu.StaffChoice.UpdateAccount:
                                        {
                                            string accountnumber = GetUserInput("Enter account number");
                                            int newpassword = GetUserInputAsInt("Enter new password for update");
                                            bankstaffactions.updateAccountPassword(bankid, accountnumber, newpassword);
                                            break;
                                        }
                                    case BankStaffusermenu.StaffChoice.DeleteAccount:
                                        {
                                            string accountnumber = GetUserInput("Enter account number");
                                            bankstaffactions.deleteAccount(bankid, accountnumber);
                                            break;
                                        }
                                    case BankStaffusermenu.StaffChoice.Exit:
                                        {
                                            Environment.Exit(0);
                                            break;
                                        }
                                }
                            }
                        }
                        else
                            printString("Incorrect Id entered try to relogin");
                        break;
                    }

                case MainUsermenu.Choice.Customer:
                    {
                        int Password = GetUserInputAsInt("Enter password");
                        string bankid = GetUserInput("Enter the bankid ");
                        string AccountNumber = GetUserInput("enter acnumber");
                        BankService bankService = new BankService();
                        bool validate = bankService.CustomerValidate(bankid, AccountNumber, Password);
                        if (validate)
                        {
                            while (true)
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
                                CustomerUsermenu userMenu = new CustomerUsermenu();
                                CustomerUsermenu.Features features = (CustomerUsermenu.Features)Enum.Parse(typeof(CustomerUsermenu.Features), Console.ReadLine());
                                switch (features)
                                {
                                    case CustomerUsermenu.Features.Deposit:
                                        {
                                            double Amount = GetUserInputAsDouble("Enter amount");
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
                                            double Amount = GetUserInputAsDouble("Enter amount");
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
                                            string receiverbankid = GetUserInput("Enter the receiver bankid");
                                            string ReceiverAccountNumber = GetUserInput("enter receiver account number");
                                            double Amount = GetUserInputAsDouble("Enter amount");
                                            string paymentmode = GetUserInput("Enter payment mode");
                                            try
                                            {
                                                bankService.transferAmount(bankid, AccountNumber, Password, Amount, receiverbankid, ReceiverAccountNumber,paymentmode);
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
                            }
                        }
                        else
                            printString("Enter incorrect login credentials");
                        break;
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

