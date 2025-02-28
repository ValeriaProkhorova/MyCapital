using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Controls;


namespace MyCapital
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //КАЛЬКУЛЯТОР
        private void Button_Click(object sender, RoutedEventArgs e)
        {
        
          
                Button b = sender as Button;
                string cont = b.Content.ToString();

                switch (cont)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                    case "0":
                        Calculator.EnterNumber(Int32.Parse(cont));
                        break;
                    case ".":
                        Calculator.EnterDot();
                        break;

                    case "⟵":
                        Calculator.EraseLast();
                        break;
                    case "C":
                        Calculator.Clear();
                        break;
                    case "CE":
                        Calculator.ClearEntry();
                        break;
                    case "+":
                        Calculator.ExecuteOperation(Calculator.CalculatorOperationType.Addition);
                        break;
                    case "-":
                        Calculator.ExecuteOperation(Calculator.CalculatorOperationType.Substraction);
                        break;
                    case "*":
                        Calculator.ExecuteOperation(Calculator.CalculatorOperationType.Multiplying);
                        break;
                    case "/":
                        Calculator.ExecuteOperation(Calculator.CalculatorOperationType.Dividing);
                        break;
                    case "=":
                        Calculator.Equal();
                        break;

                }
            }
    }

}
