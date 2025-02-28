using LiveCharts.Wpf;
using LiveCharts;
using Microsoft.Data.Sqlite;
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
using System.Windows.Forms;
using LiveCharts.Definitions.Charts;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.Extensions.Primitives;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MyCapital
{
    /// <summary>
    /// Логика взаимодействия для Diagramma.xaml
    /// </summary>
    /// 


    public partial class Diagramma : Window
    {
        //коллекция для получения данных
        List<Expenses> ExpensesList { get; set; }

        //словарь для вывода данных
        Dictionary<string, int> ExpensesShow = new Dictionary<string, int>();

        //коллекция серий данных для отображения
        public SeriesCollection SeriesCollection = new SeriesCollection();

        //Подписи для оси Х
        public string[] Labels { get; set; }

        public Func<Int32, string> Formatter { get; set; }


        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }




        public Diagramma()
        {
            InitializeComponent();
            GettingData();

            //Получает первый и последний дни, текущего месяца 
            DateTime today = DateTime.Today;
            StartDate = new DateTime(today.Year, today.Month, 1);
            EndDate = StartDate.AddMonths(1).AddDays(-1);

            DateSelection(StartDate, EndDate);

            foreach (var item in ExpensesShow)
            {

                SeriesCollection.Add(new ColumnSeries
                {
                    Title = $"{item.Key}",
                    Values = new ChartValues<int> { item.Value }
                });

            }
            cartesianChart.Series = SeriesCollection; //присвоедин диаграмме серию данных
            cartesianChart.AxisX[0].LabelFormatter = value => { return $"{StartDate} - {EndDate}"; };

        }

        public Diagramma(DateTime startDate, DateTime endDate)
        {
            InitializeComponent();
            GettingData();

            StartDate = startDate;
            EndDate = endDate;

            DateSelection(StartDate, EndDate);

            foreach (var item in ExpensesShow)
            {


                SeriesCollection.Add(new ColumnSeries
                {
                    Title = $"{item.Key}",
                    Values = new ChartValues<int> { item.Value }
                });

            }
            cartesianChart.Series = SeriesCollection; //присвоедин диаграмме серию данных
            cartesianChart.AxisX[0].LabelFormatter = value => { return $"{StartDate} - {EndDate}"; };



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



        //Вызов окна выбор временное периода
        private void CalendClick(object sender, RoutedEventArgs e)
        {
            //this чтобы передавать информацию, о том из какого окна вызывается окно  DateSelection
            DateSelection dateSelection = new DateSelection(this);
            dateSelection.Show();
            Close();
        }




        //Получение данных 
        public async Task GettingData()
        {
            ExpensesList = new List<Expenses>();

            string sqlExpression = $"SELECT CategoriesExpenses.Title, Expenses.Summ, Expenses.Date " +
                $"FROM Expenses  \r\nJOIN CategoriesExpenses ON CategoriesExpenses.Id = Expenses.Id_Category";

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
                            Expenses exp = new Expenses()
                            {
                                Categories = reader.GetString(0),
                                Summ = reader.GetInt32(1),
                                Date = reader.GetString(2)
                            };

                            ExpensesList.Add(exp);

                        }
                    }
                }
            }
        }

        //добавление данныХ в словарь за выбранный временной период 
        public void DateSelection(DateTime startDate, DateTime endDate)
        {
            foreach (Expenses inc in ExpensesList)
            {
                DateTime dateTime = DateTime.Parse(inc.Date);

                if (dateTime >= startDate && dateTime <= EndDate)
                {
                    if (ExpensesShow.ContainsKey(inc.Categories))
                    {
                        ExpensesShow[inc.Categories] += inc.Summ;
                    }
                    else
                    {
                        ExpensesShow[inc.Categories] = inc.Summ;
                    }

                }


            }
        }
    }
}












