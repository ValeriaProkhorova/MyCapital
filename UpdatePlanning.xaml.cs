using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Primitives;
using MyCapital.Class;
//using MyCapital.Interface;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для UpdatePlanning.xaml
    /// </summary>
    public partial class UpdatePlanning : Window
    {
        PlanningClass planning { get; set; }
        List<string> month { get; set; }


        public UpdatePlanning()
        {
            InitializeComponent();

            ShowCategoryExpenses();
            ShowMonth();
        }

        //обработчик выбор категории
        private void CBCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBCategory.SelectedIndex == 0)
            {
                CBCategory.Text = string.Empty;
            }
        }

        //Обработчик выбор месяца
        private void CBMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBMonth.SelectedIndex == 0)
            {
                CBMonth.Text = string.Empty;
            }
        }


        private void CloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }



        //Просмотр названий категорий расходов
        private void ShowCategoryExpenses()
        {
            string sqlExpression = "SELECT * FROM CategoriesExpenses";
            using (var connection = new SqliteConnection("Data Source=MyCapital.db"))
            {
                connection.Open();

                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            var id = reader.GetValue(0);
                            var title = reader.GetValue(1).ToString();

                            CBCategory.Items.Add(title);

                        }

                    }
                }
            }
        }
       
        
        
        //Просмотр названий месяцев
        public void ShowMonth()
        {
            month = new List<string>()
            { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };

            for (int i = 0; i < month.Count; i++) {
                CBMonth.Items.Add(month[i]);
                     }
        }


        //Изменение данных в таблице Планирование
        private void AddPlanning(object sender, RoutedEventArgs e)
        {
            string MonthName = CBMonth.Text;

            // Получаем информацию о текущем годе
            int currentYear = DateTime.Now.Year;
           
            int monthNumber =0;
            for (int i = 0; i < month.Count; i++)
            {
                if(month[i].ToString() == MonthName)
                {  
                    monthNumber = i+1; 
                    break; }
              
            }
         

            // Получаем первый день месяца
            DateTime startDate = new DateTime(currentYear, monthNumber, 1);

        

            // Получаем последний день месяца
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);

            planning = new PlanningClass()
            {
                Categories = CBCategory.Text,
                StartDate = startDate.ToString(),
                EndDate = endDate.ToString(),
                Summ = Convert.ToInt32(SummTextBox.Text)

            };
            try
            {
                    string sqlExpression = $"UPDATE Planning SET Summ={planning.Summ} " +
                    $"WHERE StartDate='{planning.StartDate}' AND EndDate='{planning.EndDate}' " +
                    $"AND IdCategory=(SELECT Id FROM CategoriesExpenses WHERE Title='{planning.Categories}');";

                    using (var connection = new SqliteConnection("Data Source=MyCapital.db"))
                    {
                        connection.Open();
                        SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                        command.ExecuteNonQuery();
                    }

                    MyMessageBoxNotifications myMessageBoxNotifications = new MyMessageBoxNotifications();
                    myMessageBoxNotifications.Message = "Изменения успешно внесены";
                    myMessageBoxNotifications.ShowDialog();
               
                foreach (Window window in Application.Current.Windows)
                {
                    if (window is Planning)
                    {
                        window.Close();
                        break;
                    }
                }
                Planning planningShow = new Planning();
                planningShow.Show();

                this.Close();
                }
                catch (Exception ex)
                {
                    MyMessageBoxNotifications myMessageBoxNotifications = new MyMessageBoxNotifications();
                    myMessageBoxNotifications.Message = $"Ошибка: {ex.Message.ToString()}";
                    myMessageBoxNotifications.ShowDialog();

                }
            }


     
       
    }
    }

