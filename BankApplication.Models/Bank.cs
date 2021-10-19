using System;
using System.Collections.Generic;

namespace BankApplication.Models
{
    public class Bank
    {
        public List<Account> AccountsList { get; set; }
        public string BankId { get; set; }
        public string BankName { get; set; }
        public Bank()
        {
            AccountsList = new List<Account>();
        }
    }
}
