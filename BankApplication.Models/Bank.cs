using System;
using System.Collections.Generic;

namespace BankApplication.Models
{
    public class Bank
    {
        public List<Account> AccountsList { get; set; }
        public string BankId { get; set; }
        public string BankName { get; set; }
        public string StaffId{ get; set; }
        public string AcceptedCurrency { get; set; }
        public int SameBankRTGSCharges { get; set; }
        public int SameBankIMPSCharges { get; set; }
        public int OtherBankRTGSCharges { get; set; }
        public int OtherBankIMPSCharges{ get; set; }
        public Bank()
        {
            AccountsList = new List<Account>();
            SameBankIMPSCharges = 5;
            SameBankRTGSCharges = 0;
            OtherBankIMPSCharges = 6;
            OtherBankRTGSCharges = 2;
            AcceptedCurrency = "INR";
        }
    }
}
