using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCapital
{
    interface IExpensesIncome
    {
       int Id { get; set; }
        string Date { get; set; }
        int Id_Category { get; set; }   
        string Categories { get; set; } 
        int IdScore { get; set; }   
        string Score { get; set; } 
        int Summ { get; set; }
        string Comment { get; set; }

        //Свойства

        

    }
}
