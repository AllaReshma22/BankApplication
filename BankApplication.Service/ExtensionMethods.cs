using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication.Service
{
    public static class ExtensionMethods
    {
        public static string DateId()
        {
            return DateTime.UtcNow.ToString("ddMMyyyy");
        }
    }
}
