using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApplication.Models;

namespace BankApplication.Service
{
    public class Datastore
    {
        public static List<Bank> Banks = new List<Bank>();
        public static Dictionary<string,decimal> Currency = new Dictionary<string, decimal>() { { "INR", 1 } } ;
    }
}

