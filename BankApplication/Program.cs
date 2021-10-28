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
            Validations validation = new Validations();
            while (true)
            {
                Displays.MainChoiceDisplay();
                //displaying options
                Enums.MainEnum choice = (Enums.MainEnum)Enum.Parse(typeof(Enums.MainEnum), Console.ReadLine());
                switch (choice)
                {
                    case Enums.MainEnum.AddBank:
                        {
                            string bankName = Displays.GetUserInput("Enter Bank name");
                            IServiceInterface serviceInterface = new BankStaffActions();
                            string bankId =serviceInterface.AddBank(bankName);
                            Displays.PrintString($"Bank is succesfully created with bankid {bankId}");
                            break;
                        }
                    case Enums.MainEnum.Bankstaff:
                        {
                            string bankId = Displays.GetUserInput("Enter bankid");
                            string staffId = Displays.GetUserInput("Enter staffid for authentication");
                            try
                            {
                                bool Validation = validation.StaffValidate(bankId, staffId);
                                if (Validation == true)
                                {
                                    IGetMenu getMenu = new BankStaffOptions();
                                    getMenu.GetMenu(bankId);
                                }
                                else
                                    Displays.PrintString("Incorrect Id entered try to relogin");
                            }
                            catch(IncorrectBankIdException ex)
                            {
                                Displays.PrintString(ex.Message);
                            }
                            catch(IncorrectStaffIdException ex)
                            {
                                Displays.PrintString(ex.Message);
                            }
                            break;
                        }

                    case Enums.MainEnum.Customer:
                        {
                            BankService bankService = new BankService();
                            string bankName = Displays.GetUserInput("Enter Bank name");
                            string bankId = Displays.GetUserInput("Enter the Bankid ");
                            string accountId= Displays.GetUserInput("enter AccountId");
                            int password = Displays.GetUserInputAsInt("Enter password");
                            try
                            {
                                bool validate = validation.CustomerValidate(bankId, accountId, password);
                                if (validate == true)
                                {
                                    IGetMenu getMenu = new CustomerOptions();
                                     getMenu.GetMenu( bankId, accountId, password);
                                }
                                else
                                    Displays.PrintString("Enter incorrect login credentials");
                              
                            }
                            catch (IncorrectBankIdException ex)
                            {
                                Displays.PrintString(ex.Message);
                            }
                            catch (IncorrectAccountNumberException ex)
                            {
                                Displays.PrintString(ex.Message);
                            }
                            catch(IncorrectPin ex)
                            {
                                Displays.PrintString(ex.Message);
                            }
                            break;
   
                        }
                    case Enums.MainEnum.Exit:
                        {
                            Environment.Exit(0);
                            break;
                        }
                    default:
                        Displays.PrintString("Enter valid number from 1-4");
                        break;
                }

            }
        
        }
    }
}

