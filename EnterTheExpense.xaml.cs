using System;
using System.Collections.Generic;
using System.Linq;
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
using MyCapital.Class;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using System.Windows.Data;
using Microsoft.VisualBasic.ApplicationServices;
//using MyCapital.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace MyCapital
{
    /// <summary>
    /// Логика взаимодействия для EnterTheExpense.xaml
    /// </summary>
    public partial class EnterTheExpense : Window
    {


        //Переменная для вывода суммы на экран. Данные приходят из Окна калькулятора
        public static string SummExpense { get; set; }

        //Переменная для обновленяи данных в БД
        public int Id {  get; set; }    

        public Expenses expenses { get; set; }



        //Событие для перегрузки главного окна, после обновления данных
        public event EventHandler ChildWindowClosed;



        //Загрузка окна для добавления Данных
        public EnterTheExpense(string summ)
        {
            InitializeComponent();
           
            SummTextBox.Text = summ;
            ShowCategoryScore();
            ShowCategoryExpenses();

            this.Closed += ChildWindow_Closed;
        }


        //Загрузка окна для обновления данных 
        public EnterTheExpense(Expenses _expenses)
        {
            InitializeComponent();

            expenses = _expenses;

            DataPicker.Text = expenses.Date;
            CBScore.Text = expenses.Score;
            CBCategory.Text = expenses.Categories;
            SummTextBox.Text = expenses.Summ.ToString();
            CommentTextBox.Text = expenses.Comment;

            CBCategory.SelectedIndex = expenses.Id_Category-1; //????
            CBScore.SelectedIndex = expenses.IdScore - 1;


            ShowCategoryScore();
            ShowCategoryExpenses();

            ButtonAddUpdate.Click -= AddExpense;
            ButtonAddUpdate.Click += UpdateExpense;


            this.Closed += ChildWindow_Closed;

        }

        //ОБРАБОТЧИКИ 

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
        private void  CBScore_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
        
        
        //вызов события для обновления главного окна, после добавления данных
        private void ChildWindow_Closed(object sender, EventArgs e)
        {
            ChildWindowClosed?.Invoke(this, EventArgs.Empty);
        }

        //ФУНКЦИИ

        //Добавить данные
        public async void AddExpense(object sender, RoutedEventArgs e)
        {
            //проверка на корректность ввода цифры
            if (int.TryParse(SummTextBox.Text, out var summ))
            {

                string date = DataPicker.Text;
                string category = CBCategory.Text;
                string score = CBScore.Text;
                string comment = CommentTextBox.Text;

                //проверяем все ли поля заполнены 
                if (!string.IsNullOrEmpty(date) && !string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(score))
                {

                    Expenses expenses = new Expenses()
            {
                Date = date,
                Categories = category,
                Score = score,
                Summ = summ,
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

                    commandAdd.CommandText = $"INSERT INTO Expenses (Date, Id_Category, IdScore, Summ, Comment) " +
                        $"VALUES ('{expenses.Date}', (SELECT Id FROM CategoriesExpenses WHERE Title='{titleCategories}'), " +
                        $"(SELECT Id FROM Score WHERE Title='{titleScore}'), {expenses.Summ}, '{expenses.Comment}')";

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
        public async void UpdateExpense(object sender, RoutedEventArgs e)
        {

         
            //вызов MessageBox
            MyMessageBoxYesNo messageBoxYesNo = new MyMessageBoxYesNo();
            messageBoxYesNo.Message = "Вы действительно хотите внести введенные изменения?";


            //проверка на выбор пользователя 
            bool? result = messageBoxYesNo.ShowDialog();
            if (result == true)
            {
                string date = DataPicker.Text;
                string categories = CBCategory.Text;
                string score = CBScore.Text;
                int summ = int.Parse(SummTextBox.Text);
                string comment = CommentTextBox.Text;
                try
                {
                    string sqlExpression = $"UPDATE Expenses " +
                    $"SET Date='{date}', Id_Category= (SELECT Id FROM CategoriesExpenses WHERE Title='{categories}'),\r\n" +
                        $"IdScore=(SELECT Id FROM Score WHERE Title='{score}')," +
                        $"Summ={summ}, Comment='{comment}'" +
                        $"WHERE Id = {expenses.Id};";

                  await  using (var connection = new SqliteConnection("Data Source=MyCapital.db"))
                    {
                      await connection.OpenAsync();
                        SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                        await command.ExecuteNonQueryAsync();
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

