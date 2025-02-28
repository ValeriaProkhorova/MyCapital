using Microsoft.VisualBasic.ApplicationServices;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MyCapital.Class;
using Microsoft.Data.Sqlite;
using MyCapital.User_Controls;
using LiveCharts.Wpf;
using LiveCharts;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Bibliography;
using System.Collections.Specialized;

//using MyCapital.Style;

namespace MyCapital
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    ///          //Проверка на получение данных
   
    /// 


    public partial class MainWindow : Window
    {

        //История вычислений для калькулятора 
        private Collection<string> computationHistory;

        //Коллекция всех расходов из БД
        private Collection<Expenses> expensesList;


        //Коллекция всех доходов из БД
        private Collection <Income> incomeList;


        //коллекция серий данных для отображения для Диаграммы
        public SeriesCollection SeriesCollection { get; set; }
        //Подписи для оси Х
        public string[] Labels { get; set; }

         //для хранения данных для Диаграммы
        public List <int> DataDiagramm {  get; set; }     

        //Переменные для вывода 
        public  int SummExpensesDay { get; set; }
        public  int SummExpensesWeek { get; set; }
        public  int SummExpensesMonth { get; set; }
        public int SummExpensesYear { get; set; }

        public  int SummIncomeDay { get; set; }
        public  int SummIncomeWeek { get; set; }
        public  int SummIncomeMonth { get; set; }
        public  int SummIncomeYear { get; set; }
              
        public  int SummScore { get; set; }

        //Переменные, которые отслеживают сколько раз нажимался обработчик событий День, Неделя, Месяц, Год
        //Чтобы данные повторно не суммировались
        private bool isCalculatedDay = false;
        private bool isCalculatedWeek = false;
        private bool isCalculatedMonth = false;
        private bool isCalculatedYear = false;
       
        public MainWindow()
      
        {
      
            
             InitializeComponent();

            
            ReceiningData();  //Получение данных из БД
            ShowScore();   //Вывод капитала


            //КАЛЬКУЛЯТОР
            computationHistory = new Collection<string>();
            // Ограничение ввода
            Calculator.InputRestriction = 12;

            //вызов события если вычесление заверено
            Calculator.ComputationEnded += (s, b) =>
            {
                //получение последнего вычесления
                computationHistory.Add(Calculator.getTheLastComputation());

            };



            AddingDataChart();  //метод для заполенение коллекции для диаграммы 

            //присвоение диаграмме серии данных
            ChartValues<int> values = new ChartValues<int>(DataDiagramm);   

            SeriesCollection = new SeriesCollection()
            {
                new LineSeries
                {
                    Title = "",
                    Values = values
                },
            };
           
          

            cartesianChart.Series = SeriesCollection; 


            //Установка подписей к Оси Х
            Labels = new[] { "Пн", "Вт", "Ср", "Чт", "Пт", "Сб","Вск" };
            DataContext = this;

          

            Loaded += btnDay;  //Вывод данных за день по умолчанию

            

        }


        //ОБРАБОТЧИКИ СОБЫТИЙ

        //Чтобы окно перемещать
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        //Вызов окна ВНЕСТИ доходы
        private void Button_Click_EnterIncome(object sender, RoutedEventArgs e)
        {
            //Получение данных с окна от кнопки 
            object tag = ((Button)e.OriginalSource).Tag;
            string summ = (string)tag;

            EnterIncome enterIncome = new EnterIncome(summ) ;
            //добавления обработчика событий 
            enterIncome.ChildWindowClosed += ChildWindow_ChildWindowClosed;
            enterIncome.Show();

        }


        //Вызов окна ВНЕСТИ расходы
        private void Button_Click_EnterTheExpenses(object sender, RoutedEventArgs e)
        {
            //Получение данных с окна от кнопки 
            object tag = ((Button)e.OriginalSource).Tag;
            string summ = (string)tag;

            EnterTheExpense enterTheExpense = new EnterTheExpense(summ);
            enterTheExpense.ChildWindowClosed += ChildWindow_ChildWindowClosed;
            enterTheExpense.Show();

        }
        //Вызов окна ВНЕСТИ Накопления
        private void ButtonClickEnterSavings(object sender, RoutedEventArgs e)
        {
            //Получение данных с окна от кнопки 
            object tag = ((Button)e.OriginalSource).Tag;
            string summ = (string)tag;

            EnterSavings enterTheSavings = new EnterSavings(summ);
            enterTheSavings.ChildWindowClosed += ChildWindow_ChildWindowClosed;
            enterTheSavings.Show();
        }

        //Вызов окна ПРОСМОТР Расходов по всем категориям
        private void ClickViewExpensesByCategory(object sender, RoutedEventArgs e)
        {
            ViewExpensesByCategory viewExpensesByCategory = new ViewExpensesByCategory();
            viewExpensesByCategory.Show();
        }

        //Вызов окна ПРОСМОТР Доходов по все категориям
        private void ClickVieIconByCategory(object sender, RoutedEventArgs e)
        {
            ViewIncomeByCategory viewIncomeByCategory = new ViewIncomeByCategory();
            viewIncomeByCategory.Show();
        }

        //Вызов окна Просмотр Капитала 
        private void ClickScore(object sender, RoutedEventArgs e)
        {
            Capital capital = new Capital();
            capital.Show();
        }

        //вызов окна накопления
        private void SavingsClick(object sender, RoutedEventArgs e)
        {
           ViewSavingsByCategory viewSavingsBy = new ViewSavingsByCategory();
            viewSavingsBy.Show();
        }

        //Вызов окна Категории
        private void ClickCategories(object sender, RoutedEventArgs e)
        {
            Categories categories = new Categories();
            categories.Show();
        }

        //Вызовы окна планирование
        private void PlanningClick(object sender, RoutedEventArgs e)
        {
            Planning planning = new Planning();
            planning.Show();
        }

        //Вызов окна Диаграмма Расходов
        private void ShowDiagramm(object sender, RoutedEventArgs e)
        {
            Diagramma diagramma = new Diagramma();
            diagramma.Show();

        }
        
        //Вызов окна Диаграма Доходов
        private void ShowDiagrammIncome(object sender, RoutedEventArgs e)
        {
            DiagrammaIncome diagrammaIncome = new DiagrammaIncome();
            diagrammaIncome.Show();
        }
        //Обрабтчик на закрытие всх окон, после закрытия главного
        private void CloseMain(object sender, RoutedEventArgs e)
        {

            // Получаем список всех открытых окон.
            var windows = Application.Current.Windows;

            // Закрываем все окна, кроме главного.
            for (int i = windows.Count - 1; i >= 0; i--)
            {
                if (windows[i] != this)
                {
                    windows[i].Close();
                }
            }



            Close();
        }



        

        //Методы для Перезагрузки главного окна
        private void ChildWindow_ChildWindowClosed(object sender, EventArgs e)
        {
            RestartMainWindow();
        }

        private void RestartMainWindow()
        {
            MainWindow newMainWindow = new MainWindow();

            newMainWindow.Left = this.Left;
            newMainWindow.Top = this.Top;
            newMainWindow.Width = this.Width;
            newMainWindow.Height = this.Height;

            Application.Current.MainWindow = newMainWindow;
            newMainWindow.Show();
            this.Close();
        }




        //ФУНКЦИИ 

        //Получение данных в коллекции
        private async Task ReceiningData()
        {
            expensesList = new Collection<Expenses>();
            incomeList = new Collection<Income>();
          

            string sqlExpression = "SELECT * FROM Expenses";
            string sqlExpressionIncome = "SELECT * FROM Income";
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
                            Expenses expenses = new Expenses()
                            {
                                Id = reader.GetInt32(0),
                                Date = reader.GetString(1),
                                Summ = reader.GetInt32(4)
                            };

                            expensesList.Add(expenses);
                     

                        }
                    }

                    //Доходы
                    SqliteCommand commandIncome = new SqliteCommand(sqlExpressionIncome, connection);
                    await using (SqliteDataReader readerI = commandIncome.ExecuteReader())
                    {
                        if ( readerI.HasRows) // если есть данные
                        {
                            while (await readerI.ReadAsync())   // построчно считываем данные
                            {
                                Income income = new Income()
                                {
                                    Id = readerI.GetInt32(0),
                                    Date = readerI.GetString(1),
                                    Summ = readerI.GetInt32(4)
                                };

                                incomeList.Add(income);

                            }}}}}}
     


        //Вывод данных Расход, Доход, Капитал За День, Неделю, Месяц, Год
        //ДЕНЬ
             private void btnDay(object sender, RoutedEventArgs e)
        { 
            //Расходы

            Collection<Expenses> expensesShow = new Collection<Expenses>(); 

            foreach (Expenses expenses in expensesList) 
            {
                DateTime date = DateTime.Parse(expenses.Date);

                if(date== DateTime.Today)
                {
                    expensesShow.Add(expenses); 

                }
            }

            //
            foreach (Expenses expenses2 in expensesShow)
            {
                if (!isCalculatedDay)
                    SummExpensesDay += expenses2.Summ;
            }
        


            //Доходы
            Collection<Income> incomeShow = new Collection<Income>();

            foreach (Income income in incomeList)
            {
                DateTime date = DateTime.Parse(income.Date);

                if (date == DateTime.Today)
                {
                    incomeShow.Add(income);

                }
            }
            foreach (Income income2 in incomeShow)
            {
                if (!isCalculatedDay)
                SummIncomeDay += income2.Summ;
            }

            isCalculatedDay = true;

            InfoCardExpenses.Number = SummExpensesDay;
            InfoCardIncome.Number = SummIncomeDay;  
        }

        
        //НЕДЕЛЯ
        private void btnWeek(object sender, RoutedEventArgs e)
        {
           //получаем текущую дату
            DateTime today = DateTime.Now;
            //получаем начало текущей недели - понедельник
            DateTime startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);

            //Расходы
            Collection<Expenses> expensesShow = new Collection<Expenses>();

      

            foreach (Expenses expenses in expensesList)
            {
                DateTime date = DateTime.Parse(expenses.Date);

                //если дата входит в эту неделю
                if (date >= startOfWeek)
                {                
                    expensesShow.Add(expenses);
                  

                }
            }

            foreach (Expenses expenses2 in expensesShow)
            {
                if (!isCalculatedWeek)
                SummExpensesWeek += expenses2.Summ;
            }

            //Доходы
            Collection<Income> incomeShow = new Collection<Income>();
             foreach (Income income in incomeList)
            {
                DateTime date = DateTime.Parse(income.Date);
                if (date >= startOfWeek)
                {
                   incomeShow.Add(income);

                }
            }
            foreach (Income income in incomeShow)
            {
                if (!isCalculatedWeek)
                    SummIncomeWeek += income.Summ;
            }

            isCalculatedWeek = true;

          

            InfoCardExpenses.Number = SummExpensesWeek;
            InfoCardIncome.Number = SummIncomeWeek; 
        }

        //МЕСЯЦ
        private void btnMonth(object sender, RoutedEventArgs e)
        {
           
            DateTime myDateTime = DateTime.Now;

            //Расходы
            Collection<Expenses> expensesShow = new Collection<Expenses>();

            foreach (Expenses expenses in expensesList)
            {
                DateTime date = DateTime.Parse(expenses.Date);

                if (date.Month == myDateTime.Month)
                {
                    expensesShow.Add(expenses);

                }
            }

            foreach (Expenses expenses2 in expensesShow)
            {
                if (!isCalculatedMonth)
                    SummExpensesMonth += expenses2.Summ;
            }

            //Доходы
            Collection<Income> incomeShow = new Collection<Income>();

            foreach (Income income in incomeList)
            {
                DateTime date = DateTime.Parse(income.Date);

                if (date.Month == myDateTime.Month)
                {
                    incomeShow.Add(income);

                }
            }

            foreach (Income income2 in incomeShow)
            {
                if (!isCalculatedMonth)
                    SummIncomeMonth += income2.Summ;
            }


            isCalculatedMonth = true;

            InfoCardExpenses.Number = SummExpensesMonth;
            InfoCardIncome.Number = SummIncomeMonth;

        }

        //ГОД
        private void btnYear(object sender, RoutedEventArgs e)
        {
          
            DateTime myDateTime = DateTime.Now;
            
            //Расходы
            Collection<Expenses> expensesShow = new Collection<Expenses>();

            foreach (Expenses expenses in expensesList)
            {
                DateTime date = DateTime.Parse(expenses.Date);

                //если расход за текущей год
                if (date.Year == myDateTime.Year)
                {
                    expensesShow.Add(expenses);

                }
            }

            foreach (Expenses expenses2 in expensesShow)
            {
                if (!isCalculatedYear)
                    SummExpensesYear += expenses2.Summ;
            }

            //Доходы
            Collection<Income> incomeShow = new Collection<Income>();

            foreach (Income income in incomeList)
            {
                DateTime date = DateTime.Parse(income.Date);

                if (date.Year == myDateTime.Year)
                {
                   incomeShow.Add(income);

                }
            }

            foreach (Income income2 in incomeShow)
            {
                if (!isCalculatedYear)
                    SummIncomeYear += income2.Summ;
            }
            isCalculatedYear = true;

            InfoCardExpenses.Number = SummExpensesYear;
            InfoCardIncome.Number=SummIncomeYear;
        }


        //вывод всего КАПИТАЛА 
        private async Task ShowScore()
        {
            
            string sqlExpression = $"SELECT Score.Summ FROM Score";

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
                                Summ = reader.GetInt32(0),
                            };

                            SummScore += score.Summ;
                        }
                    }

                }
               
            }
            InfoCardScore.Number = SummScore;
        }


        //Добавление данных в диаграмму за текущую неделю
        public void AddingDataChart()
        {
            DataDiagramm = new List<int>(7);

            // Получаем текущую дату и определяем начало недели (понедельник)
            DateTime today = DateTime.Today;
            int currentDayOfWeek = (int)today.DayOfWeek;
            if (currentDayOfWeek == 0) // Воскресенье
            {
                currentDayOfWeek = 7;
            }

            DateTime startOfWeek = today.AddDays(1 - currentDayOfWeek);
            DateTime endOfWeek = startOfWeek.AddDays(6);


            // Словарь для хранения сумм по дням недели
            Dictionary<DateTime, int> dailyAmounts = new Dictionary<DateTime, int>();

            // Инициализируем словарь нулями для каждого дня недели
            for (DateTime date = startOfWeek; date <= endOfWeek; date = date.AddDays(1))
            {
                dailyAmounts[date] = 0;
            }


            // Обрабатываем расходы
            foreach (Expenses exp in expensesList)
            {
                DateTime expenseDate;
                if (DateTime.TryParse(exp.Date, out expenseDate))
                {
                    // Проверяем, входит ли расход в текущую неделю
                    if (expenseDate >= startOfWeek && expenseDate <= endOfWeek)
                    {
                        dailyAmounts[expenseDate] += exp.Summ;

                    }
                }
               
            }


            //Добавляeм данные в коллекцию
            int i = 0;
            for (DateTime date = startOfWeek; date <= endOfWeek; date = date.AddDays(1))
            {
                DataDiagramm.Add(dailyAmounts[date]);
            }
        }

      
    }
}