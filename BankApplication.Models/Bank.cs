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
        public string DefaultCurrency { get; set; }
        public decimal SameBankRTGSCharges { get; set; }
        public decimal SameBankIMPSCharges { get; set; }
        public decimal OtherBankRTGSCharges { get; set; }
        public decimal OtherBankIMPSCharges{ get; set; }
        public Bank()
        {
            AccountsList = new List<Account>();
            SameBankIMPSCharges = 5;
            SameBankRTGSCharges = 0;
            OtherBankIMPSCharges = 6;
            OtherBankRTGSCharges = 2;
            DefaultCurrency = "INR";
        }
    }
}
