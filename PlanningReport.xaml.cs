using Microsoft.Data.Sqlite;
using MyCapital.Class;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyCapital
{
    /// <summary>
    /// Логика взаимодействия для PlanningReport.xaml
    /// </summary>
    /// 
      //класс для сравнения коллекция для отчета 
    public class Overspend
    {
        public DateTime Month { get; set; }
        public string TitleMonth { get; set; }  
        public string Category { get; set; }
        public int OverspendSumm { get; set; }
    }

    public partial class PlanningReport : Window
    {
       static List<Expenses> expenses { get; set; }
       static List <PlanningClass> planning { get; set; }

        public PlanningReport()
        {
            InitializeComponent();

            GettingData();
            GettingDataExpenses();

           

            var overspends = CompareExpenses();

            //сортировка по дате с помощью компаратора
            //КомапараторЖ лябда выражение которое определяет логику сравнения двух элементов
            overspends.Sort((item1,item2)=>item1.Month.CompareTo(item2.Month)); 
            foreach (Overspend over in overspends)
            {
                over.TitleMonth = over.Month.ToString("MMMM", CultureInfo.CreateSpecificCulture("ru-RU"));

                //Добавление данных в элемент 
                ListViewPlanningReport.Items.Add(over);
            }

           

        }

        //Обработчик для кнопки свернуть
        private void BtnMinimizeClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        //Обработчик для кнопки Развернуть
        private void BtnMaximizeClick(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else
                this.WindowState = WindowState.Maximized;
        }

        //Обработчик для кнопки Закрыть 
        private void BtnCloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


       //ФУНКЦИИ

        //Получение данных 
        //Добавить только за текущий год о расходах
        public async Task GettingDataExpenses() 
        {
            expenses = new List<Expenses>();

            string sqlExpression = $"SELECT Expenses.Date, CategoriesExpenses.Title, Expenses.Id_Category, " +
                $"Expenses.Summ FROM Expenses\r\nINNER JOIN  CategoriesExpenses " +
                $"ON Expenses.Id_Category = CategoriesExpenses.Id";

          await  using (var connection = new SqliteConnection("Data Source=MyCapital.db"))
            {
               await connection.OpenAsync();

                //Расходы
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
               await using (SqliteDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (await reader.ReadAsync())   // построчно считываем данные
                        {
                            Expenses exp = new Expenses()
                            {
                                Date = reader.GetString(0),
                                Categories = reader.GetString(1),
                                Id_Category = reader.GetInt32(2),
                                Summ = reader.GetInt32(3)
                            };


                            expenses.Add(exp);

                        }
                    }
                }
            }
        }

        //Получение данных о Планировании 
        public async Task GettingData()
        {
            planning = new List<PlanningClass>();


            string sqlExpression = $"SELECT Planning.Id, Planning.StartDate, Planning.EndDate, CategoriesExpenses.Title, " +
                $"Planning.Summ, CategoriesExpenses.Id FROM Planning INNER JOIN CategoriesExpenses ON Planning.IdCategory = CategoriesExpenses.Id";

            await using (var connection = new SqliteConnection("Data Source=MyCapital.db"))
            {
               await connection.OpenAsync();


                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
               await using (SqliteDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (await reader.ReadAsync())   // построчно считываем данные
                        {
                            PlanningClass planningClass = new PlanningClass()
                            {
                                Id = reader.GetInt32(0),
                                StartDate = reader.GetString(1),
                                EndDate = reader.GetString(2),
                                Categories = reader.GetString(3),
                                Summ = reader.GetInt32(4),
                                Id_Category = reader.GetInt32(5)
                            };




                            planning.Add(planningClass);

                        }
                    }
                }


            }
        }
       
        
        //Сравнение данных
        public static List<Overspend> CompareExpenses()
        {
            var overspends = new List<Overspend>();
            //CultureInfo culture = new CultureInfo("en-US"); // Or "ru-RU" if your dates are in Russian format

            //  словарь для хранения расходов по месяцам и категориям
            Dictionary<string, Dictionary<string, int>> expensesByMonth = new Dictionary<string, Dictionary<string, int>>();

            // Populate the dictionary with expenses data
            foreach (Expenses expense in expenses)
            {
                DateTime date = DateTime.Parse(expense.Date);
                string month = $"{date.Year}-{date.Month:D2}"; // Format YYYY-MM

                if (!expensesByMonth.ContainsKey(month))
                {
                    expensesByMonth[month] = new Dictionary<string, int>();
                }

                if (!expensesByMonth[month].ContainsKey(expense.Categories))
                {
                    expensesByMonth[month][expense.Categories] = 0;
                }

                expensesByMonth[month][expense.Categories] += expense.Summ;
            }

            // Compare expenses with planned expenses
            foreach (PlanningClass plannedExpense in planning)
            {
                DateTime plannedDate = DateTime.Parse(plannedExpense.StartDate);
                string plannedMonth = $"{plannedDate.Year}-{plannedDate.Month:D2}";

                if (expensesByMonth.ContainsKey(plannedMonth) && expensesByMonth[plannedMonth].ContainsKey(plannedExpense.Categories))
                {
                   int actualExpenses = expensesByMonth[plannedMonth][plannedExpense.Categories];
                    //decimal overspend = actualExpenses - plannedExpense.Amount;
                   int overspend = plannedExpense.Summ - actualExpenses;
                    //if (overspend > 0)
                    //{
                    overspends.Add(new Overspend { Month = DateTime.Parse($"{plannedMonth}-01"), Category = plannedExpense.Categories, OverspendSumm = overspend });
                    //}
                }
            }

            return overspends;
        }
    }
}
