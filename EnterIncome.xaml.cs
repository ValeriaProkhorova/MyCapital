using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.Data.Sqlite;
using MyCapital.Class;
using System;
using System.Collections.Generic;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyCapital
{
    /// <summary>
    /// Логика взаимодействия для EnterIncome.xaml
    /// </summary>
    public partial class EnterIncome : Window
    {
        //Переменная для вывода суммы на экран. Данные приходят из Окна калькулятора
        public static string SummIncome { get; set; }

        //Переменная для обновленяи данных в БД
        public int Id { get; set; }

        Income incomes{ get; set; }


        //Событие для перегрузки главного окна, после обновления данных
        public event EventHandler ChildWindowClosed;


        //Загрузка окна для добавления Данных
        public EnterIncome(string summ)
        {
            InitializeComponent();

            //присвоение сумме Расходов значения из калькулятора
            SummIncome = summ;
            DataContext = this; //Связывание  DataContext с текущим объектом 

    
            ShowCategoryIncome();
            ShowCategoryScore();

            this.Closed += ChildWindow_Closed; 

        }

        //Загрузка окна для обновления данных 
        public EnterIncome(Income inc)
        {
            InitializeComponent();

            incomes = inc;

            DatePicker.Text = incomes.Date;
            CBScore.Text = incomes.Score;
            CBCategory.Text = incomes.Categories;
            SummTextBox.Text = incomes.Summ.ToString();
            CommentTextBox.Text = incomes.Comment; 

            CBCategory.SelectedIndex = incomes.Id_Category - 1; //????
            CBScore.SelectedIndex = incomes.IdScore - 1;


            ShowCategoryScore();
            ShowCategoryIncome();

            ButtonAddUpdate.Click -= AddIncome;
            ButtonAddUpdate.Click += UpdateIncome;

            this.Closed += ChildWindow_Closed;
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

  

        //Обработчик выбор с какого счета
        private void CBScore_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBScore.SelectedIndex == 0)
            {
                CBScore.Text = string.Empty;
            }
        }

        //обработчик выбор категории
        private void CBCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBCategory.SelectedIndex == 0)
            {
                CBCategory.Text = string.Empty;
            }
        }

        //Просмотр названий счетов
        private void ShowCategoryScore()
        {
            string sqlExpression = "SELECT * FROM Score";
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

                            CBScore.Items.Add(title);

                        }
                    }
                }
            }

        }

        //Просмотр названий категорий доходов
        private void ShowCategoryIncome()

        {
            string sqlExpression = "SELECT * FROM CategoriesIncome";
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

        //вызов события для обновления главного окна, после добавления данных
        private void ChildWindow_Closed(object sender, EventArgs e)
        {
            ChildWindowClosed?.Invoke(this, EventArgs.Empty);
        }


        //ФУНКЦИИ

        //Добавить данные
        public async void AddIncome(object sender, RoutedEventArgs e)
        {

         //проверка на корректность ввода цифры
            if (int.TryParse(SummTextBox.Text, out var summ))
            {

                string date = DatePicker.Text;
                string category = CBCategory.Text;
                string score = CBScore.Text;
                string comment = CommentTextBox.Text;

                //проверяем все ли поля заполнены 
             if (!string.IsNullOrEmpty(date) && !string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(score))
                {


                    Income income = new Income()
                {
                    Date = date,
                    Categories = category,
                    Score = score,
                    Summ = summ,
                    //Summ = Int32.Parse(SummTextBox.Text),
                    Comment = comment
                };
        
       

            try
            {
                await using (var connection = new SqliteConnection("Data Source=MyCapital.db"))
                {
                   await connection.OpenAsync();
                    string titleCategories = CBCategory.Text;
                    string titleScore = CBScore.Text;


                    SqliteCommand commandAdd = new SqliteCommand();
                    commandAdd.Connection = connection;

                    commandAdd.CommandText = $"INSERT INTO Income (Date, Id_Category, IdScore, Summ, Comment) " +
                        $"VALUES ('{income.Date}', (SELECT Id FROM CategoriesIncome WHERE Title='{titleCategories}'), " +
                        $"(SELECT Id FROM Score WHERE Title='{titleScore}'), {income.Summ}, '{income.Comment}')";

                           
                  await  commandAdd.ExecuteNonQueryAsync();

                }
            }
            catch (Exception ex)
            {
                MyMessageBoxNotifications myMessageBoxNotifications = new MyMessageBoxNotifications();
                myMessageBoxNotifications.Message = $"Ошибка: {ex.Message.ToString()}";
                myMessageBoxNotifications.ShowDialog();

            }
            finally
            {
                      

                MyMessageBoxNotifications myMessageBoxNotifications = new MyMessageBoxNotifications();
                myMessageBoxNotifications.Message = "Данные успешно внесены";
                myMessageBoxNotifications.ShowDialog();
                this.Close();
            }

            }
                else
                {
                    MyMessageBoxNotifications myMessageBox = new MyMessageBoxNotifications();
                    myMessageBox.Message = "Пожалуйста, проверьте заполненные данные.Возможно, некоторые поля остались незаполненными или содержат ошибки.";
                    myMessageBox.ShowDialog();
                }

            }


            else
            {
                MyMessageBoxNotifications myMessageBox = new MyMessageBoxNotifications();
                myMessageBox.Message = "Пожалуйста, проверьте заполненные данные.Возможно, некоторые поля остались незаполненными или содержат ошибки.";
                myMessageBox.ShowDialog();
            }






        }


        //Изменения данных
        public async void UpdateIncome(object sender, RoutedEventArgs e)
        {
            Income income = new Income();
            //вызов MessageBox
            MyMessageBoxYesNo messageBoxYesNo = new MyMessageBoxYesNo();
            messageBoxYesNo.Message = "Вы действительно хотите внести введенные изменения?";


            //проверка на выбор пользователя 
            bool? result = messageBoxYesNo.ShowDialog();
            if (result == true)
            {
                income.Date = DatePicker.Text;
                income.Categories = CBCategory.Text;
                income.Score = CBScore.Text;
                income.Summ= int.Parse(SummTextBox.Text);
                income.Comment = CommentTextBox.Text;
                try
                {
                    string sqlExpression = $"UPDATE Income " +
                    $"SET Date='{income.Date}', Id_Category= (SELECT Id FROM CategoriesIncome WHERE Title='{income.Categories}'),\r\n" +
                        $"IdScore=(SELECT Id FROM Score WHERE Title='{income.Score}')," +
                        $"Summ={income.Summ}, Comment='{income.Comment}'" +
                        $"WHERE Id = {incomes.Id};";

                   await using (var connection = new SqliteConnection("Data Source=MyCapital.db"))
                    {
                        await connection.OpenAsync();
                        SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                        command.ExecuteNonQuery();
                    }




                }
                catch (Exception ex)
                {
                    MyMessageBoxNotifications myMessageBoxNotifications = new MyMessageBoxNotifications();
                    myMessageBoxNotifications.Message = $"Ошибка: {ex.Message.ToString()}";
                    myMessageBoxNotifications.ShowDialog();

                }

                finally
                {
                  
                    MyMessageBoxNotifications myMessageBoxNotifications = new MyMessageBoxNotifications();
                    myMessageBoxNotifications.Message = "Изменения успешно внесены";
                    myMessageBoxNotifications.ShowDialog();

                    this.Close();

                }
            }
            else
            {
                Close();
            }
        }




    }
}
