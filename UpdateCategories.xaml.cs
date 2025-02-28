using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using MyCapital.Class;
//using MyCapital.Interface;
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
    /// Логика взаимодействия для UpdateCategories.xaml
    /// </summary>
    /// 

    //В этом окне прописывается важныя логика для таблицы Планирование!!!!!!
    public partial class UpdateCategories : Window
    {
        int Id { get; set; } = 0;
        //Название таблицы в которую добавляется или изменяется запись
        string TitleTable { get; set; }

        //название категории которое надо добавить или исправить
        string categories { get; set; } = "";
        public UpdateCategories(string titleTable)
        {
            InitializeComponent();
            TitleTable= titleTable; 
        }
        public UpdateCategories(int id, string titleTable)
        {
            InitializeComponent();
            Id = id;
            TitleTable = titleTable;    
        }
        private void CloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //Добавление или измение категории
        private void AddCategories(object sender, RoutedEventArgs e)
        {
            //ДОБАВЛЕНИЕ НОВОЙ КАТЕГОРИИ 
            categories = TextBoxTitleCategories.Text;
            string sqlExpression = "";



        
            MyMessageBoxYesNo messageBoxYesNo = new MyMessageBoxYesNo();
            //если Id не передается то добавляем новую запись
            if (Id == 0)
            {

                sqlExpression = $"INSERT INTO {TitleTable}(Title) VALUES ('{categories}');";
                messageBoxYesNo.Message = "Вы действительно хотите добавить новую категорию?";
            }
            else
            {
                sqlExpression = $"UPDATE {TitleTable} SET Title='{categories}' WHERE Id = {Id};";
                //вызов MessageBox
            
                messageBoxYesNo.Message = "Вы действительно хотите внести введенные изменения?";

            }

           
            //проверка на выбор пользователя 
            bool? result = messageBoxYesNo.ShowDialog();
            if (result == true)
            {



                try
                {


                    using (var connection = new SqliteConnection("Data Source=MyCapital.db"))
                    {
                        connection.Open();
                        SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                        command.ExecuteNonQuery();
                    }

               

                    MyMessageBoxNotifications myMessageBoxNotifications = new MyMessageBoxNotifications();
                    myMessageBoxNotifications.Message = "Изменения успешно внесены";
                    myMessageBoxNotifications.ShowDialog();


                    if (TitleTable == "CategoriesExpenses")
                    {
                        //Метод Для ДОБАВЛЕНИЯ записей в таблицу Планирование при создание новой категории
                        if (Id == 0)
                        {
                            InsertPlanning();
                        }
                    }

                    this.Close();
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


        //Метод Для ДОБАВЛЕНИЯ записей в таблицу Планирование при создание новой категории
        public void InsertPlanning()
        {
            //Получаем текщий год
            int curentYear = DateTime.Now.Year;

            for (int month = 1; month <= 12; month++)
            {
                DateTime StartDate = new DateTime(curentYear, month, 1);
                DateTime EndDate = StartDate.AddMonths(1).AddDays(-1); 

                string sqlInsertPlanning = "INSERT INTO Planning(StartDate,EndDate,IdCategory,Summ,IdScore) " +
                    $"VALUES ('{StartDate.ToString()}','{EndDate.ToString()}', " +
                    $"(SELECT Id FROM CategoriesExpenses WHERE Title='{categories}')," +
                    $"0,3);";

                using (var connection = new SqliteConnection("Data Source=MyCapital.db"))
                {
                    connection.Open();
                    SqliteCommand command = new SqliteCommand(sqlInsertPlanning, connection);
                    command.ExecuteNonQuery();
                }

            }
        }

    }
}
