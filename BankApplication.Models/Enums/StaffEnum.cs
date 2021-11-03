using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Models.Enums
{
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
        Exit
    }
}
