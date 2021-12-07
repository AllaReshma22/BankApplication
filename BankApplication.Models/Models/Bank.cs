using System;
using System.Collections.Generic;

#nullable disable

namespace BankApplication.Models.Models
{
    public partial class Bank
    {
        public Bank()
        {
            SameBankRtgsCharges = 0;
            SameBankImpsCharges = 5;
            OtherBankImpsCharges = 6;
            OtherBankRtgsCharges = 2;
            DefaultCurrency = "INR";
            Accounts = new HashSet<Account>();
        }

        public string BankId { get; set; }
        public string BankName { get; set; }
        public string StaffId { get; set; }
        public string DefaultCurrency { get; set; }
        public decimal? SameBankRtgsCharges { get; set; }
        public decimal? SameBankImpsCharges { get; set; }
        public decimal? OtherBankRtgsCharges { get; set; }
        public decimal? OtherBankImpsCharges { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
