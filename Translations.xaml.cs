using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
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
    /// Логика взаимодействия для Translations.xaml
    /// </summary>
    public partial class Translations : Window
    {
        public Translations()
        {
            InitializeComponent();

            ShowCategoryScore();
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
        //Обработчик выбор с какого счета
        private void CBScore_SelectionChangedTwo(object sender, SelectionChangedEventArgs e)
        {
            if (CBScore.SelectedIndex == 0)
            {
                CBScoreTwo.Text = string.Empty;
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
                            CBScoreTwo.Items.Add(title);

                        }
                    }
                }
            }

        }
      
        //Сделать перевод между счетами
        private async void AddTranclation(object sender, RoutedEventArgs e)
        {


            //проверка на корректность ввода цифры
            if (int.TryParse(SummTextBox.Text, out var Summ))
            {
                string TitleOneScore = CBScore.Text;
                string TitleTwoScore = CBScoreTwo.Text;



                //проверяем все ли поля заполнены 
                if (!string.IsNullOrEmpty(TitleOneScore) && !string.IsNullOrEmpty(TitleTwoScore))
                {



                    try
                    {
                        await using (var connection = new SqliteConnection("Data Source=MyCapital.db"))
                        {
                            await connection.OpenAsync();   // открываем подключение

                            SqliteCommand commandDelete = new SqliteCommand();
                            // определяем выполняемую команду
                            commandDelete.CommandText = $"UPDATE Score SET Summ=Summ-{Summ} WHERE Title='{TitleOneScore}';";
                            // определяем используемое подключение
                            commandDelete.Connection = connection;
                            // выполняем команду
                            await commandDelete.ExecuteNonQueryAsync();


                            SqliteCommand commandAdd = new SqliteCommand();
                            // определяем выполняемую команду
                            commandAdd.CommandText = $"UPDATE Score SET Summ=Summ+{Summ} WHERE Title='{TitleTwoScore}';";
                            // определяем используемое подключение
                            commandAdd.Connection = connection;
                            // выполняем команду
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
                        myMessageBoxNotifications.Message = "Данные успешно изменены";
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
        }
    }

