using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCapital.Class
{
   public class Savings : IExpensesIncome
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public int Id_Category { get; set; }
        public string Categories { get; set; }
        public int IdScore { get; set; }
        public string Score { get; set; }
        public int Summ { get; set; }
        public string Comment { get; set; }
    }
}
