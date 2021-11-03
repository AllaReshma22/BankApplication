using System;
using BankApplication.Service;
using BankApplication.Models;
using System.Linq;
using System.Collections.Generic;
using BankApplication.Models.Exceptions;
using BankApplication.Models.Enums;
namespace BankApplication
{
     class Program
    {
        static void Main()
        {
            //Giving title to the console
            Console.Title = "Bank App";
            while (true)
            {
                StandardMessages.MainChoiceDisplay();
                //displaying options              
                switch ((LoginType)Enum.Parse(typeof(LoginType), Console.ReadLine()))
                {
                    case LoginType.AddBank:
                        {
                            string bankName = StandardMessages.GetUserInput("Enter Bank name");
                            IBankStaffServiceInterface serviceInterface = new BankStaffService();
                            string bankId =serviceInterface.AddBank(bankName);
                            StandardMessages.PrintString($"Bank is succesfully created with bankid {bankId}");
                            break;
                        }
                    case LoginType.Bankstaff:
                        {
                            string bankId = StandardMessages.GetUserInput("Enter bankid");
                            string staffId = StandardMessages.GetUserInput("Enter staffid for authentication");
                            IGetMenu getMenu = new BankStaffOptions();
                            StaffValidateAgain:
                            try
                            {
                                
                                bool Validation = Validations.StaffValidate(bankId, staffId);
                                if (Validation == true)
                                    getMenu.GetMenu(bankId);
                                else
                                    StandardMessages.PrintString("Incorrect Id entered try to relogin");
                            }
                            catch(IncorrectBankIdException)
                            {
                                bankId = StandardMessages.GetUserInput("Incorrect Bank Id entered\nPlease try to re enter bank id");
                                goto StaffValidateAgain;
                            }
                            catch(IncorrectStaffIdException)
                            {
                              staffId= StandardMessages.GetUserInput("StaffId you have entered is incorrect\nso,try to re enter staff id");
                              goto StaffValidateAgain;
                            }
                            break;
                        }

                    case LoginType.Customer:
                        {
                            string bankId = StandardMessages.GetUserInput("Enter the Bankid ");
                            string accountId= StandardMessages.GetUserInput("enter AccountId");
                            int password = StandardMessages.GetUserInputAsInt("Enter password");
                            IGetMenu getMenu = new CustomerOptions();
                            CustomerValidateAgain:
                            try
                            {
                                bool validate = Validations.CustomerValidate(bankId, accountId, password);
                                if (validate == true)     
                                     getMenu.GetMenu( bankId, accountId, password);                               
                                else
                                    StandardMessages.PrintString("Enter incorrect login credentials");                             
                            }
                            catch (IncorrectBankIdException)
                            {

                                bankId = StandardMessages.GetUserInput("Incorrect Bank Id entered\nPlease try to re enter bank id");
                                goto CustomerValidateAgain;

                            }
                            catch (IncorrectAccountNumberException)
                            {
                                accountId=StandardMessages.GetUserInput("AccountNumber you have entered is invalid \n Please try to reenter account number");
                                goto CustomerValidateAgain;
                            }
                            catch(IncorrectPin)
                            {
                                password=StandardMessages.GetUserInputAsInt("Password you have entered is incorrect\nTry to renter password");
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

