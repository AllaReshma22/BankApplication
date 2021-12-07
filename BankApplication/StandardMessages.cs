using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    public class StandardMessages
    {
        public static void MainChoiceDisplay()
        {
            PrintString("Enter Your Choice");
            PrintString("1.Add bank          ");
            PrintString("2.Bank staff login      ");
            PrintString("3.customer login        ");
            PrintString("4.EXIT            ");
        }
        public static void BankStaffChoiceDisplay()
        {
            PrintString("___________________________________");
            PrintString("Enter Your Choice                  ");
            PrintString("1.CreateAccount                    ");
            PrintString("2.ViewAccountBalance               ");
            PrintString("3.Updateaccountpassword           ");
            PrintString("4.Delete Account                  ");
            PrintString("5.ViewTransactionHistoryOfAccount  ");
            PrintString("6.AddRTGSServiceChargesForSameBank ");
            PrintString("7.AddRTGSServiceChargesForOtherBank");
            PrintString("8.AddIMPSServiceChargesForSameBank ");
            PrintString("9.AddIMPSServiceChargesForOtherBank");
            PrintString("10.RevertTransaction                ");
            PrintString("11.Get All Accounts in the bank     ");
            PrintString("12.Exit                             ");
            PrintString("___________________________________");

        }
        public static void CustomerChoiceDisplay()
        {
            PrintString("______________________________");
            PrintString("|ENTER YOUR CHOICE           |");
            PrintString("|1.Deposit Amount            |");
            PrintString("|2.Withdraw amount           |");
            PrintString("|3.transfer amount           |");
            PrintString("|4.checkbalance              |");
            PrintString("|5.Transaction history       |");
            PrintString("|6.Exit                      |");
            PrintString("______________________________");
        }
        public static string GetUserInput(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }
        public static int GetUserInputAsInt(string message)
        {
            Console.WriteLine(message);
            Start:
            string input = Console.ReadLine();
            bool IsAllDigits = int.TryParse(input, out int i);
            if (IsAllDigits == true)
                return int.Parse(input);
            else
            {
                Console.WriteLine("You have entered in incorrect format\nEnter in numbers only");
                goto Start;
            }
        }

       public  static decimal GetUserInputAsDecimal(string message)
        {
            Console.WriteLine(message);
            Start:
            string input = Console.ReadLine();
            bool IsAllDigits = decimal.TryParse(input, out decimal i);
            if (IsAllDigits == true)
                return decimal.Parse(input);
            else
            {
                Console.WriteLine("You have entered in incorrect format\nEnter in numbers only");
                goto Start;
            }
        }
        public static int PrintString(string str)
        {
            Console.WriteLine(str);
            return 1;
        }
    }
}
