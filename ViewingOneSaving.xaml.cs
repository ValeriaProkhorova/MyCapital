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
using MyCapital.Class;
namespace MyCapital
{
    /// <summary>
    /// Логика взаимодействия для ViewingOneSaving.xaml
    /// </summary>
    public partial class ViewingOneSaving : Window
    {
        //переменная для поиска нужной записи в БД
        int Id { get; set; } = 0;

        //Коллекция для получения данных
        Savings savings{ get; set; }


        //Событие для перегрузки главного окна, после обновления данных
        public event EventHandler ChildWindowClosed;

        public ViewingOneSaving(int id)
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
        private void DeleteSavingsClick(object sender, RoutedEventArgs e)
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
                    string sqlExpression = $"DELETE FROM Savings WHERE Savings.Id={Id}";

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

                    ViewSavingsByCategory viewingOneCategory = new ViewSavingsByCategory();
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

            EnterSavings enterTheSavings = new EnterSavings(savings);
            enterTheSavings.Show();
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

            string sqlExpression = $"SELECT Savings.Id, Savings.Date, CategoriesSavings.Title, Score.Title, " +
                $"Savings.Summ, Savings.Comment, CategoriesSavings.Id, Score.Id \r\nFROM Savings\r\n" +
                $"INNER JOIN CategoriesSavings ON Savings.Id_Category = CategoriesSavings.Id\r\n" +
                $"INNER JOIN Score ON Savings.IdScore = Score.Id\r\n" +
                $"WHERE Savings.Id = {Id}";

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


                           savings = new Savings()
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


                            DateTextBox.Text = savings.Date;
                            CategoriesTextBox.Text = savings.Categories;
                            ScoreTextBox.Text = savings.Score;
                            SummTextBox.Text = savings.Summ.ToString();
                            CommentTextBox.Text = savings.Comment;


                        }
                    }
                }
            }
        }

    }
}
