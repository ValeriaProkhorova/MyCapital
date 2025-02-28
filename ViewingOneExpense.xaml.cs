using Microsoft.Data.Sqlite;
//using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
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
    /// Логика взаимодействия для ViewingOneExpense.xaml
    /// </summary>
    public partial class ViewingOneExpense : Window
    {
        //переменная для поиска нужной записи в БД
        int Id { get; set; } = 0; 
        
        //Коллекция для получения данных
        Expenses expenses { get; set; }

        //Событие для перегрузки главного окна, после обновления данных
        public event EventHandler ChildWindowClosed;

        public ViewingOneExpense(int id)
        {
            InitializeComponent();
            Id = id;
            GettingData();
            this.Closed += ChildWindow_Closed;
        }



        //ОБРАБОТЧИКИ СОБЫТИЙ 
        //Для перемещения окна 
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        //Удаление записи 
        private void DeleteExpensesClick(object sender, RoutedEventArgs e)
        {
            //вызов MessageBox
            MyMessageBoxYesNo messageBoxYesNo = new MyMessageBoxYesNo();
            messageBoxYesNo.Message = "Вы действительно хотите удалить данную запись?";
            //messageBoxYesNo.ShowDialog();

            //проверка на выбор пользователя 
            bool? result = messageBoxYesNo.ShowDialog();
            if (result == true)
            {
                try
                {
                    string sqlExpression = $"DELETE FROM Expenses WHERE Expenses.Id={Id}";

                    using (var connection = new SqliteConnection("Data Source=MyCapital.db"))
                    {
                        connection.Open();
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
                    myMessageBoxNotifications.Message = "Запись успешно удалена";
                    myMessageBoxNotifications.ShowDialog();

                    ViewExpensesByCategory viewingExpensesneOneCategory = new ViewExpensesByCategory();
                    viewingExpensesneOneCategory.Show();

                    this.Close();
                }
            }
            else
            {
                Close();
            }

        }


        //Редаетирование записи
        private void UpdateBtn(object sender, RoutedEventArgs e)
        {

            EnterTheExpense enterTheExpense = new EnterTheExpense(expenses);
            enterTheExpense.Show();
            Close();

        }


        //вызов события для обновления главного окна, после добавления данных
        private void ChildWindow_Closed(object sender, EventArgs e)
        {
            ChildWindowClosed?.Invoke(this, EventArgs.Empty);
        }


        private void Closebtn(object sender, RoutedEventArgs e)
        {
            Close();
        }



        //ФУНКЦИИ

        //Получение данных 
        public async Task GettingData()
        {
           
            string sqlExpression = $"SELECT Expenses.Id, Expenses.Date, CategoriesExpenses.Title, Score.Title, " +
                $"Expenses.Summ, Expenses.Comment, CategoriesExpenses.Id, Score.Id \r\nFROM Expenses\r\n" +
                $"INNER JOIN CategoriesExpenses ON Expenses.Id_Category = CategoriesExpenses.Id\r\n" +
                $"INNER JOIN Score ON Expenses.IdScore = Score.Id\r\n" +
                $"WHERE Expenses.Id = {Id}";

           await using (var connection = new SqliteConnection("Data Source=MyCapital.db"))
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
                            string CommentStr = " ";
                            if (!reader.IsDBNull(reader.GetOrdinal("Comment")))
                            {
                                CommentStr = reader.GetString(reader.GetOrdinal("Comment"));
                            }


                            expenses = new Expenses()
                            {
                                Id = reader.GetInt32(0),
                                Date = reader.GetString(1),
                                Categories = reader.GetString(2),
                                Score = reader.GetString(3),
                                Summ = reader.GetInt32(4),                            
                                Comment = CommentStr,
                                Id_Category = reader.GetInt32(6),
                                IdScore = reader.GetInt32(7)                                 

                            };

                       
                            DateTextBox.Text = expenses.Date;
                            CategoriesTextBox.Text = expenses.Categories;
                            ScoreTextBox.Text = expenses.Score;
                            SummTextBox.Text = expenses.Summ.ToString();
                            CommentTextBox.Text = expenses.Comment;
                         
                                   
                        }
                    }
                }
            }
        }

      

       
    }
}
