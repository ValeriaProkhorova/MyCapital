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

namespace MyCapital
{
    /// <summary>
    /// Логика взаимодействия для Capital.xaml
    /// </summary>
    public partial class Capital : Window
    {
    

        public Capital()
        {
            InitializeComponent();

          GettingData();

        }
        //ОБРАБОТЧИКИ

        //Для перемещения окна 
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }

        }

        //вызов окна сделать перевод
            private void TranslationsClick(object sender, RoutedEventArgs e)
        {
            Translations translations = new Translations();
            translations.Show();
            Close();
        }

        //Обработчик для кнопки Закрыть
        private void BtnCloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        //ФУНКЦИИ


        //Получение данных 
        public async Task GettingData()
        {
            

            string sqlExpression = $"SELECT Score.Id, Score.Title, Score.Summ FROM Score";

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
                           Score score = new Score()
                            {                               
                                Id = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Summ = reader.GetInt32(2),
                            };

                            //capital.Add(score);
                            ListViewCapital.Items.Add(score);
                        }
                    }
                }
            }
        }

    }
}
