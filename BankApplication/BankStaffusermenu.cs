using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    class BankStaffusermenu
    {
        public enum StaffChoice
        {
            CreateAccount=1,
            AddNewAcceptedCurrency,
            AddServiceChargesForSameBank,
            AddServiceChargesForDifferentBank,
            ViewTransactionHistoryOfAccount,
            RevertTransaction,
        }
    }
}
