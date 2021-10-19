using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    class UserMenu
    {
        public enum Features
        {
            AddBank = 1,
            CreateAccount,
            Deposit,
            Withdraw,
            TransferAmount,
            CheckBalance,
            Transactionhistory,
            Exit,
        }
    }
}
