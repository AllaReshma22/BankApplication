using System;
using System.Collections.Generic;

namespace BankApplication.Models
{
    public class Bank
    {
        public List<Account> AccountsList { get; set; }
        public string BankId { get; set; }
        public string BankName { get; set; }
        public String staffId{ get; set; }
        public string AcceptedCurrency { get; set; }
        public int SameBankRTGS { get; set; }
        public int SameBankIMPS { get; set; }
        public int OtherBankRTGS { get; set; }
        public int OtherBankIMPS { get; set; }
        public Bank()
        {
            AccountsList = new List<Account>();
            SameBankIMPS = 5;
            SameBankRTGS = 0;
            OtherBankIMPS = 6;
            OtherBankRTGS = 2;
            AcceptedCurrency = "INR";
        }
    }
}
