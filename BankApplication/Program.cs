using System;
using BankApplication.Service;
using BankApplication.Models;
using System.Linq;
using System.Collections.Generic;
using BankApplication.Service.Interfaces;
using BankApplication.Models.Exceptions;
using BankApplication.Models.Enums;
using BankApplication.Models.Models;
namespace BankApplication
{
     class Program
    {
        static void Main()
        {
            Console.Title = "Bank App";
            while (true)
            {
                StandardMessages.MainChoiceDisplay();
                var bankAppContext = new BankAppContext();
                Validations validate = new Validations(bankAppContext);
                //displaying options              
                switch ((LoginType)Enum.Parse(typeof(LoginType), Console.ReadLine()))
                {
                    case LoginType.AddBank:
                        {
                            string bankName = StandardMessages.GetUserInput("Enter Bank name");
                            IBankStaffServiceInterface serviceInterface = new BankStaffService(bankAppContext);
                            try
                            {
                                string bankId = serviceInterface.AddBank(bankName);
                                StandardMessages.PrintString($"Bank is succesfully created with bankid {bankId}");
                            }
                            catch (DuplicateBankNameException)
                            {
                                StandardMessages.PrintString("Bank already exists with that name");
                            }                          
                            break;
                        }
                    case LoginType.Bankstaff:
                        {
                            string bankId = StandardMessages.GetUserInput("Enter bankid");
                            string staffId = StandardMessages.GetUserInput("Enter staffid for authentication");
                            IGetMenu getMenu = new BankStaffOptions();

                        StaffvalidateAgain:
                            try
                            {
                                bool Validation = validate.StaffValidate(bankId, staffId);
                                if (Validation == true)
                                    getMenu.GetMenu(bankId);
                                else
                                    StandardMessages.PrintString("IncorrectId try to relogin");
                            }
                            catch (IncorrectBankIdException)
                            {
                                bankId = StandardMessages.GetUserInput("Incorrect Bank Id entered\nPlease try to reenter bank id");
                                goto StaffvalidateAgain;
                            }
                            catch (IncorrectStaffIdException)
                            {
                                staffId = StandardMessages.GetUserInput("StaffId you have entered is incorrect\nSo,try to re enter staffid ");
                                goto StaffvalidateAgain;
                            }

                            break;
                        }

                    case LoginType.Customer:
                        {
                            string accountId = StandardMessages.GetUserInput("enter AccountId");
                            int password = StandardMessages.GetUserInputAsInt("Enter password");
                            IGetMenu getMenu = new CustomerOptions();
                        CustomerValidateAgain:
                            try
                            {
                                bool validation = validate.CustomerValidate(accountId, password);
                                StandardMessages.PrintString($"{validation}");
                                if (validation == true)
                                    getMenu.GetMenu(accountId, password);
                                else
                                    StandardMessages.PrintString("Enter incorrect login credentials");
                            }

                            catch (IncorrectAccountNumberException)
                            {
                                accountId = StandardMessages.GetUserInput("AccountNumber you have entered is invalid \n Please try to reenter account number");
                                goto CustomerValidateAgain;
                            }
                            catch (IncorrectPin)
                            {
                                password = StandardMessages.GetUserInputAsInt("Password you have entered is incorrect\nTry to renter password");
                                goto CustomerValidateAgain;
                            }

                            break;

                        }
                    case LoginType.Exit:
                        {
                            Environment.Exit(0);
                            break;
                        }
                    default:
                        StandardMessages.PrintString("Enter valid number from 1-4");
                        break;
                }

            }

        }
    }
}

