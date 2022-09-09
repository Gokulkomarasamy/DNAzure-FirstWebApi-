using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pension_Management_Portal.Models
{
    public class Pensioner
    {
        public string Name { get; set; }
        public DateTime Dateofbirth { get; set; }
        public string Pan { get; set; }
        public int SalaryEarned { get; set; }
        public int Allowances { get; set; }
        public string AadharNumber { get; set; }
        public PensionTypeValue PensionType { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public BankType BankType { get; set; }


    }
}
