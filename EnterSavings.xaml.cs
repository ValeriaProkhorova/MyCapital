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
using Microsoft.Data.Sqlite;
using MyCapital;
using MyCapital.Class;
namespace MyCapital
{
    /// <summary>
    /// Логика взаимодействия для EnterSavings.xaml
    /// </summary>
    public partial class EnterSavings : Window
    {
        //Переменная для вывода суммы на экран. Данные приходят из Окна калькулятора
        public static string SummSavings { get; set; }

        //Переменная для обновленяи данных в БД
        public int Id { get; set; }

        public Savings savings { get; set; }

        //Событие для перегрузки главного окна, после обновления данных
        public event EventHandler ChildWindowClosed;

        //Загрузка окна для добавления Данных
        public EnterSavings(string summ)
        {
            InitializeComponent();

            SummTextBox.Text = summ;
            ShowCategoryScore();
            ShowCategorySavings();
            this.Closed += ChildWindow_Closed;
        }

        //Загрузка окна для обновления данных 
        public EnterSavings(Savings sav)
        {
            InitializeComponent();

            savings = sav;

            DataPicker.Text = savings.Date;
            CBScore.Text = savings.Score;
            CBCategory.Text = savings.Categories;
            SummTextBox.Text = savings.Summ.ToString();
            CommentTextBox.Text = savings.Comment;

            CBCategory.SelectedIndex = savings.Id_Category - 1; //????
            CBScore.SelectedIndex = savings.IdScore - 1;


            ShowCategoryScore();
            ShowCategorySavings();

            ButtonAddUpdate.Click -= AddSavings;
            ButtonAddUpdate.Click += UpdateSavings;
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


        //Просмотр названий категорий накопления
        private void ShowCategorySavings()
        {
            string sqlExpression = "SELECT * FROM CategoriesSavings";
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
        public async void AddSavings(object sender, RoutedEventArgs e)
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

                    Savings savings = new Savings()
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

                            commandAdd.CommandText = $"INSERT INTO Savings (Date, Id_Category, IdScore, Summ, Comment) " +
                                $"VALUES ('{savings.Date}', (SELECT Id FROM CategoriesSavings WHERE Title='{titleCategories}'), " +
                                $"(SELECT Id FROM Score WHERE Title='{titleScore}'), {savings.Summ}, '{savings.Comment}')";

                            await commandAdd.ExecuteNonQueryAsync();

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
        public async void UpdateSavings(object sender, RoutedEventArgs e)
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
                   string sqlExpression = $"UPDATE Savings " +
                    $"SET Date='{date}', Id_Category= (SELECT Id FROM CategoriesSavings WHERE Title='{categories}'),\r\n" +
                        $"IdScore=(SELECT Id FROM Score WHERE Title='{score}')," +
                        $"Summ={summ}, Comment='{comment}'" +
                        $"WHERE Id = {savings.Id};";

                   await using (var connection = new SqliteConnection("Data Source=MyCapital.db"))
                    {
                       await connection.OpenAsync();
                        SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                      await  command.ExecuteNonQueryAsync();
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