using Microsoft.Data.Sqlite;
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

namespace MyCapital
{
    /// <summary>
    /// Логика взаимодействия для ViewingOneIncome.xaml
    /// </summary>
    public partial class ViewingOneIncome : Window
    {
        //переменная для поиска нужной записи в БД
        int Id { get; set; } = 0;

        //Переменная для получения данных
        Income income{ get; set; }

        //Событие для перегрузки главного окна, после обновления данных
        public event EventHandler ChildWindowClosed;

        public ViewingOneIncome(int id)
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
        private void DeleteIncomeClick(object sender, RoutedEventArgs e)
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
                    string sqlExpression = $"DELETE FROM Income WHERE Income.Id={Id}";

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

                    ViewIncomeByCategory viewingOneCategory = new ViewIncomeByCategory();
                    viewingOneCategory.Show();

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

            EnterIncome enterTheIncome = new EnterIncome(income);
            enterTheIncome.Show();
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

            string sqlExpression = $"SELECT Income.Id, Income.Date, CategoriesIncome.Title, Score.Title, " +
                $"Income.Summ, Income.Comment, CategoriesIncome.Id, Score.Id \r\nFROM Income\r\n" +
                $"INNER JOIN CategoriesIncome ON Income.Id_Category = CategoriesIncome.Id\r\n" +
                $"INNER JOIN Score ON Income.IdScore = Score.Id\r\n" +
                $"WHERE Income.Id = {Id}";

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


                           income= new Income()
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


                            DateTextBox.Text = income.Date;
                            CategoriesTextBox.Text = income.Categories;
                            ScoreTextBox.Text = income.Score;
                            SummTextBox.Text = income.Summ.ToString();
                            CommentTextBox.Text = income.Comment;


                        }
                    }
                }
            }
        }



    }
}
