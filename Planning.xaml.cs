using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Data.Sqlite;
using MyCapital.Class;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
//using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyCapital
{
    /// <summary>
    /// Логика взаимодействия для Planning.xaml
    /// </summary>



    //Класс для вывода в DataGrid
    public class AddingData
    {
        public string Categories { get; set; } 
       public string SummJanuary { get; set; } = "0";
        public string SummFebruary { get; set; } = "0";
        public string SummMarch { get; set; } = "0";
       public string SummApril { get; set; } = "0";
        public string SummMay { get; set; } = "0";
        public string SummJune { get; set; } = "0";
        public string SummJuly { get; set; } = "0";
        public string SummAugust { get; set; } = "0";
        public string SummSeptember{ get; set; } = "0";
        public string SummOctober { get; set; } = "0";
        public string SummNovember { get; set; } = "0";
        public string SummDecember { get; set; } = "0";

    }
  
    public partial class Planning : Window
    {
       
        List<MyCategories> categories { get; set; }

        ObservableCollection<PlanningClass> planning {  get; set; } 

        ObservableCollection<AddingData> addingDatas { get; set; }
     

        public Planning()
        {
            InitializeComponent();
          

             GettingData();
             GettingDataCategories();

        
            ShowDate();

            DataGridPlanning.ItemsSource = addingDatas;
      
        }

        //ОБРАБОТЧИКИ СОБЫТИЙ
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

        //Вызов окна внести измение в планирование
        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            UpdatePlanning updatePlanning = new UpdatePlanning();
            updatePlanning.Show();

        }

        //Вызов окна Отчеты
        private void ReportClick(object sender, RoutedEventArgs e)
        {
            PlanningReport planningReport = new PlanningReport();
            planningReport.Show();
        }

        //ФУНКЦИИ
        //ПОЛУЧЕНИЕ ДАННЫХ о категориях 
        public async Task GettingDataCategories()
        {
            categories = new List<MyCategories>();

            string sqlExpressionCateg = $"SELECT * FROM CategoriesExpenses";

            using (var connectionCateg = new SqliteConnection("Data Source=MyCapital.db"))
            {
                connectionCateg.Open();


                SqliteCommand commandCateg = new SqliteCommand(sqlExpressionCateg, connectionCateg);
                await using (SqliteDataReader reader = commandCateg.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                            MyCategories _categories = new MyCategories()
                            {
                                Id = reader.GetInt32(0),    
                                Title = reader.GetString(1) 
                            };



                            categories.Add(_categories);

                        }
                    }
                }



            }
        }

     


        //Получение данных из таблицы Планирование
        public async Task GettingData()
        {
            planning = new ObservableCollection<PlanningClass>();
           

            string sqlExpression = $"SELECT Planning.Id, Planning.StartDate, Planning.EndDate, CategoriesExpenses.Title, " +
                $"Planning.Summ, CategoriesExpenses.Id FROM Planning INNER JOIN CategoriesExpenses ON Planning.IdCategory = CategoriesExpenses.Id";

            await using (var connection = new SqliteConnection("Data Source=MyCapital.db"))
            {
               await connection.OpenAsync();


                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = await command.ExecuteReaderAsync())
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

        //Заполение данными для правильного выводы в DataGrid
        public async Task ShowDate()
        {
            addingDatas = new ObservableCollection<AddingData>();

            // Получаем текущий год
            int currentYear = DateTime.Now.Year;


            foreach (MyCategories cat in categories)
            {
                AddingData addingData2 = new AddingData()
                {
                    Categories = cat.Title
                };
                foreach (PlanningClass plan in planning)
                {
                    if (cat.Title == plan.Categories)
                    {
                      
                       
                            // Создаем объект DateTime для первого дня текущего месяца
                            DateTime January = new DateTime(currentYear, 1, 1);
                            DateTime February = new DateTime(currentYear, 2, 1);
                            DateTime March = new DateTime(currentYear, 3, 1);
                            DateTime April = new DateTime(currentYear, 4, 1);
                            DateTime May = new DateTime(currentYear, 5, 1);
                            DateTime June = new DateTime(currentYear, 6, 1);
                            DateTime July = new DateTime(currentYear, 7, 1);
                            DateTime August = new DateTime(currentYear, 8, 1);
                            DateTime September = new DateTime(currentYear, 9, 1);
                            DateTime October = new DateTime(currentYear, 10, 1);
                            DateTime November = new DateTime(currentYear, 11, 1);
                            DateTime December = new DateTime(currentYear, 12, 1);
                            if (plan.StartDate == January.ToString())
                            {
                                addingData2.SummJanuary = plan.Summ.ToString();
                               
                            }

                            if (plan.StartDate == February.ToString())
                            {
                                addingData2.SummFebruary = plan.Summ.ToString();


                            }
                            if (plan.StartDate == March.ToString())
                            {
                                addingData2.SummMarch = plan.Summ.ToString();


                            }
                            if (plan.StartDate == April.ToString())
                            {
                                addingData2.SummApril= plan.Summ.ToString();


                            }
                            if (plan.StartDate == May.ToString())
                            {
                                addingData2.SummMay = plan.Summ.ToString();


                            }
                            if (plan.StartDate == June.ToString())
                            {
                                addingData2.SummJune = plan.Summ.ToString();


                            }
                            if (plan.StartDate == July.ToString())
                            {
                                addingData2.SummJuly = plan.Summ.ToString();


                            }
                            if (plan.StartDate == August.ToString())
                            {
                                addingData2.SummAugust = plan.Summ.ToString();


                            }
                            if (plan.StartDate == September.ToString())
                            {
                                addingData2.SummSeptember = plan.Summ.ToString();


                            }
                            if (plan.StartDate == October.ToString())
                            {
                                addingData2.SummOctober = plan.Summ.ToString();


                            }
                            if (plan.StartDate == November.ToString())
                            {
                                addingData2.SummNovember = plan.Summ.ToString();


                            }
                            if (plan.StartDate == December.ToString())
                            {
                                addingData2.SummDecember = plan.Summ.ToString();


                            }
                      
                        //}
                        
                    }
                   
                }
                     if (addingData2.SummJanuary!=null)
                    addingDatas.Add(addingData2);
              
             
            }
          
          
        }




    }



}

