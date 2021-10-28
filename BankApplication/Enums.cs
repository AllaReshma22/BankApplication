using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    class Enums
    {
        public enum MainEnum
        {
            AddBank=1,
            Bankstaff,
            Customer,
            Exit,
    
        }
        public enum CustomerEnum
        {
            Deposit=1,
            WithDraw,
            TransferAmount,
            CheckBalance,
            Transactionhistory,
            Exit,
        }
        public enum StaffEnum
        {
            
            CreateAccount = 1,
            AddNewAcceptedCurrency,
            UpdateAccount,
            DeleteAccount,
            ViewTransactionHistoryOfAccount,
            UpdateSameBankRTGS,
            UpdateOtherBankRTGS,
            UpdateSameBankIMPS,
            UpdateOtherBankIMPS,
            RevertTransaction,
            RevertTransfer,
            Exit,
        }
    }
}
