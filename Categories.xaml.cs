using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.Data.Sqlite;
using MyCapital.Class;
//using MyCapital.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для Categories.xaml
    /// </summary>
    /// 

 

    
  public partial class Categories : Window
    {
        
        ObservableCollection <MyCategories> categories { get; set; }    
        
        int Choice {  get; set; }

  
        public Categories()
        {
            InitializeComponent();

            

            Loaded += ExpensesCategoriesClick;
        }

        //Обработчики событий
    
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

  
        //Просмотр категорий расходов
        private void ExpensesCategoriesClick(object sender, RoutedEventArgs e)
        {

             Choice = 1;
            ListViewCategories.Items.Clear();

          
            categories = new ObservableCollection<MyCategories>();   

            string sqlExpression = $"SELECT Id, Title FROM CategoriesExpenses";

            using (var connection = new SqliteConnection("Data Source=MyCapital.db"))
            {
                connection.Open();

                //Расходы
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {
                           

                            MyCategories myCategories = new MyCategories()
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                //TitleTable = "Expenses"
                            };

                            categories.Add(myCategories);


                        }
                    }
                }

                
            }

            foreach (var category in categories)
            {
                //Добавление данных в элемент 
                ListViewCategories.Items.Add(category);
            }

           
        }
        //Просмотр категорий доходов
        private void IncomeCategoriesClick(object sender, RoutedEventArgs e)
        {
            Choice = 2;
            ListViewCategories.Items.Clear();

            categories = new ObservableCollection<MyCategories>();

            string sqlExpression = $"SELECT Id, Title FROM CategoriesIncome";

            using (var connection = new SqliteConnection("Data Source=MyCapital.db"))
            {
                connection.Open();

                //Расходы
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (reader.Read())   // построчно считываем данные
                        {

                            MyCategories myCategories = new MyCategories()
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                //TitleTable = "Income"
                            };

                            categories.Add(myCategories);
                        }
                    }
                }
            }


            foreach (var category in categories)
            {
                //Добавление данных в элемент 
                ListViewCategories.Items.Add(category);
            }
        }
        //Просмотр категорий накоплений
        private void SavingsCategoriesClick(object sender, RoutedEventArgs e)
        {
            Choice = 3;
            ListViewCategories.Items.Clear();
            categories = new ObservableCollection<MyCategories>();


            string sqlExpression = $"SELECT Id, Title FROM CategoriesSavings";

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
                            MyCategories myCategories = new MyCategories()
                            {
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                //TitleTable = "Income"
                            };

                            categories.Add(myCategories);

                        }
                    }
                }
            }

            foreach (var category in categories)
            {
                //Добавление данных в элемент 
                ListViewCategories.Items.Add(category);
            }

        }

        //Изменение категории
        private void UpdateCategoriesClick(object sender, RoutedEventArgs e)
        {
            //Получение данных с окна от кнопки 
            object tag = ((Button)e.OriginalSource).Tag;
            int Id = (int)tag;
            string TitleTable = " ";


            

            if (Choice == 1)
            {
                TitleTable = "CategoriesExpenses";
            }
            else if (Choice == 2)
            {
                TitleTable = "CategoriesIncome";
            }
            else if (Choice == 3)
            {
                TitleTable = "CategoriesSavings";
            }


            UpdateCategories categories = new UpdateCategories(Id, TitleTable);
            categories.Show();
            Close();
          
        }
        //Удаление категории
        private void DeleteCategoriesClick(object sender, RoutedEventArgs e)
        {
            //Получение данных с окна от кнопки 
            object tag = ((Button)e.OriginalSource).Tag;
            int Id = (int)tag;

        
            string TitleTable = " ";
          
            if (Choice == 1)
            {
                TitleTable = "CategoriesExpenses";
            }
            else if (Choice == 2)
            {
                TitleTable = "CategoriesIncome";
            }
            else if (Choice == 3)
            {
                TitleTable = "CategoriesSavings";
            }


            //вызов MessageBox
            MyMessageBoxYesNo messageBoxYesNo = new MyMessageBoxYesNo();
            messageBoxYesNo.Message = "Вы действительно хотите удалить эту категорию? При ее удалении все связанные записи" +
                " будут безвозвратно утеряны";
            //messageBoxYesNo.ShowDialog();

            //проверка на выбор пользователя 
            bool? result = messageBoxYesNo.ShowDialog();
            if (result == true)
            {
                try
                {
                    string sqlExpression = $"DELETE FROM {TitleTable} WHERE Id={Id}";

                    using (var connection = new SqliteConnection("Data Source=MyCapital.db"))
                    {
                        connection.Open();
                        SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                        command.ExecuteNonQuery();
                    }

                    MyMessageBoxNotifications myMessageBoxNotifications = new MyMessageBoxNotifications();
                    myMessageBoxNotifications.Message = "Категория успешно удалена";
                    myMessageBoxNotifications.ShowDialog();

                    Close();



                }
                catch (Exception ex)
                {
                    MyMessageBoxNotifications myMessageBoxNotifications = new MyMessageBoxNotifications();
                    myMessageBoxNotifications.Message = $"Ошибка: {ex.Message.ToString()}";
                    myMessageBoxNotifications.ShowDialog();

                }
            }
            else
            {
                Close();
            }


        }
        //Добавление категории
        private void AddCategoriesClick(object sender, RoutedEventArgs e)
        {
            string TitleTable = " ";
            if (Choice == 1)
            {
                TitleTable = "CategoriesExpenses";
            }
            else if (Choice == 2)
            {
                TitleTable = "CategoriesIncome";
            }
            else if (Choice == 3)
            {
                TitleTable = "CategoriesSavings";
            }

            UpdateCategories updateCategories = new UpdateCategories(TitleTable);
            updateCategories.Show();

            Close();
        }
    }
}
