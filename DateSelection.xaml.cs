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
    /// Логика взаимодействия для DateSelection.xaml
    /// </summary>
    public partial class DateSelection : Window
    {
        DateTime startDate { get; set; }
        DateTime endDate { get; set; }

        //переменная, которая указывает из какого окна вызывают календарь 
        public static int Check { get; set; } = 0;
        public DateSelection()
        {
            InitializeComponent();
        }
        public DateSelection(ViewExpensesByCategory viewExpensesByCategory)
        {
            InitializeComponent();

            Check = 1;
        }
        public DateSelection(ViewingExpensesneOneCategory viewExpensesByCategory)
        {
            InitializeComponent();

            Check = 2;
        }

        public DateSelection(ViewIncomeByCategory viewIncomeByCategory) 
        {
            InitializeComponent();

            Check = 3;


        }
        public DateSelection(ViewingIncomeOneCategory viewIncomeByCategory)
        {
            InitializeComponent();

            Check = 4;

        }
        public DateSelection(ViewSavingsByCategory viewSavingsByCategory)
        {
            InitializeComponent();

            Check = 5;

        }
        public DateSelection(ViewingSavingsOneCategory viewSavingsByCategory)
        {
            InitializeComponent();

            Check = 6;

        }

        public DateSelection(Diagramma diagramma)
        {
            InitializeComponent();
            Check = 7;
        }

        public DateSelection(DiagrammaIncome diagramma)
        {
            InitializeComponent();
            Check = 8;
        }
        private void DateSelectionClick(object sender, RoutedEventArgs e)
        {
             startDate = StartDatePicker.SelectedDate.Value;
             endDate = EndDatePicker.SelectedDate.Value;
           

            // Проверка корректности введенных дат
            if (startDate > endDate)
            {
                MyMessageBoxNotifications myMessageBox = new MyMessageBoxNotifications();
                myMessageBox.Message = "Начальная дата не может быть больше конечной даты";
                myMessageBox.ShowDialog();
              
                return;
            }
            if (Check == 1) 
            { 
            ViewExpensesByCategory viewExpensesByCategoryy = new ViewExpensesByCategory(startDate, endDate);
            viewExpensesByCategoryy.Show();
            }

            if (Check == 2)
            {
                ViewingExpensesneOneCategory viewExpensesByCategory = new ViewingExpensesneOneCategory(startDate, endDate); 
                viewExpensesByCategory.Show();
            }

            if (Check == 3)
            {
                ViewIncomeByCategory viewIncomeByCategory = new ViewIncomeByCategory(startDate,endDate);
                viewIncomeByCategory.Show();
            }
            if (Check == 4)
            {
                ViewingIncomeOneCategory viewingIncomeOneCategory = new ViewingIncomeOneCategory(startDate, endDate);
                viewingIncomeOneCategory.Show();
            }
            if (Check == 5) { 
            ViewSavingsByCategory viewSavingsByCategory = new ViewSavingsByCategory(startDate, endDate);    
                viewSavingsByCategory.Show();

            }
            if (Check == 6)
            {
                ViewingSavingsOneCategory viewingSavingsOne = new ViewingSavingsOneCategory(startDate, endDate);
                viewingSavingsOne.Show();
            }
            if (Check == 7)
            {
                Diagramma diagramma = new Diagramma(startDate, endDate);
                diagramma.Show();
            }
            if (Check == 8) 
            {
                DiagrammaIncome diagramma = new DiagrammaIncome(startDate, endDate);
                diagramma.Show();
            }

            // Закрываем окно
            this.Close();

        }

        private void CloseDate(object sender, RoutedEventArgs e)
        {
            // Закрываем окно
            this.Close();
        }
    }
}
