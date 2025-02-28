using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCapital
{
  public class Income : IExpensesIncome
    {
        //Свойства
        public int Id { get; set; }
        public string Date { get; set; }
        public int Id_Category { get; set; }
        public string Categories { get; set; }
        public int IdScore { get; set; }
        public string Score { get; set; }
        public int Summ { get; set; }
        public string Comment { get; set; }


        //Методы
        public void Add() { }

        public void Update() { }

        public void Delete() { }
    }
}
