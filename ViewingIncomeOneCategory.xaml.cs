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
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using Microsoft.Data.Sqlite;
namespace MyCapital
{
    /// <summary>
    /// Логика взаимодействия для ViewingIncomeOneCategory.xaml
    /// </summary>
    public partial class ViewingIncomeOneCategory : Window
    {
        //название категории которые передается из предыдущего окна
        public static string CategoriesTitle { get; set; }


        //коллекция для получения данных
        List<Income> Incomes { get; set; }

        //коллекция для вывода данных
        List<Income> IncomesShow { get; set; }



        //Даты для устновки временного периода
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        //Начальная загрузка окна. По умолчанию даные выводятся за текущий месяц
        public ViewingIncomeOneCategory(string categories, DateTime startDate, DateTime endDate)
        {
            InitializeComponent();

            CategoriesTitle = categories;
            StartDate = startDate;
            EndDate = endDate;

            TitleCategories.Content = CategoriesTitle;

            GettingData();

            DateSelection(StartDate, EndDate);
        }

        //Загрузка окна когда пользователь выбирает временной период 
        public ViewingIncomeOneCategory(DateTime startDate, DateTime endDate)
        {
            InitializeComponent();


            StartDate = startDate;
            EndDate = endDate;

            TitleCategories.Content = CategoriesTitle;

            GettingData();

            DateSelection(StartDate, EndDate);
        }



        //ОБРАБОТЧИКИ СОБЫТИЙ

        //выбор временного периода
        private void DateSelectionClick(object sender, RoutedEventArgs e)
        {
            DateSelection dateSelection = new DateSelection(this);
            dateSelection.Show();
        }

        //скачать отчет
        private void DowloadWorld(object sender, RoutedEventArgs e)
        {
            //Создаем название файла 
            string fileName = $"Доход в виде {CategoriesTitle}_{StartDate.ToString("yyyyMMddHHmmss")}-{EndDate.ToString("yyyyMMddHHmmss")}.docx";
            //Путь к папке мои Документы 
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = System.IO.Path.Combine(documentsPath, fileName);

            try
            {
                // Создаем документ Word
                using (WordprocessingDocument document = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document))
                {
                    // Создаем основной текст документа
                    MainDocumentPart mainPart = document.AddMainDocumentPart();
                    mainPart.Document = new Document();
                    DocumentFormat.OpenXml.Wordprocessing.Body body = mainPart.Document.AppendChild(new DocumentFormat.OpenXml.Wordprocessing.Body());

                    //Создаем примечание об периоде отчетности 
                    DocumentFormat.OpenXml.Wordprocessing.Paragraph paragraph = new DocumentFormat.OpenXml.Wordprocessing.Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run
                         (new Text($"Отчетный период {StartDate.ToString()}-{EndDate.ToString()}")));
                    body.AppendChild(paragraph);

                    // Создаем таблицу
                    DocumentFormat.OpenXml.Wordprocessing.Table table = new DocumentFormat.OpenXml.Wordprocessing.Table();
                    body.AppendChild(table);



                    // Создаем заголовок таблицы
                    DocumentFormat.OpenXml.Wordprocessing.TableRow headerRow = new DocumentFormat.OpenXml.Wordprocessing.TableRow();
                    table.AppendChild(headerRow);


                    DocumentFormat.OpenXml.Wordprocessing.TableCell headerCell1 = new DocumentFormat.OpenXml.Wordprocessing.TableCell(new TableCellProperties(new TableCellWidth { Width = "3000", Type = TableWidthUnitValues.Auto }), new DocumentFormat.OpenXml.Wordprocessing.Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new Text("Дата"))));
                    DocumentFormat.OpenXml.Wordprocessing.TableCell headerCell2 = new DocumentFormat.OpenXml.Wordprocessing.TableCell(new TableCellProperties(new TableCellWidth { Width = "2400", Type = TableWidthUnitValues.Auto }), new DocumentFormat.OpenXml.Wordprocessing.Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new Text("Сумма"))));
                    //DocumentFormat.OpenXml.Wordprocessing.TableCell headerCell3 = new DocumentFormat.OpenXml.Wordprocessing.TableCell(new TableCellProperties(new TableCellWidth { Width = "2400", Type = TableWidthUnitValues.Auto }), new DocumentFormat.OpenXml.Wordprocessing.Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new Text("Начальная дата"))));
                    //DocumentFormat.OpenXml.Wordprocessing.TableCell headerCell4 = new DocumentFormat.OpenXml.Wordprocessing.TableCell(new TableCellProperties(new TableCellWidth { Width = "2400", Type = TableWidthUnitValues.Auto }), new DocumentFormat.OpenXml.Wordprocessing.Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new Text("Конечная дата"))));

                    headerRow.AppendChild(headerCell1);
                    headerRow.AppendChild(headerCell2);
                    //headerRow.AppendChild(headerCell3);
                    //headerRow.AppendChild(headerCell4);

                    // Заполняем таблицу данными
                    foreach (Income exp in IncomesShow)
                    {
                        DocumentFormat.OpenXml.Wordprocessing.TableRow row = new DocumentFormat.OpenXml.Wordprocessing.TableRow();
                        table.AppendChild(row);

                        DocumentFormat.OpenXml.Wordprocessing.TableCell cell1 = new DocumentFormat.OpenXml.Wordprocessing.TableCell(new TableCellProperties(new TableCellWidth { Width = "2400", Type = TableWidthUnitValues.Auto }), new DocumentFormat.OpenXml.Wordprocessing.Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new Text(exp.Date))));
                        DocumentFormat.OpenXml.Wordprocessing.TableCell cell2 = new DocumentFormat.OpenXml.Wordprocessing.TableCell(new TableCellProperties(new TableCellWidth { Width = "2400", Type = TableWidthUnitValues.Auto }), new DocumentFormat.OpenXml.Wordprocessing.Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new Text(exp.Summ.ToString()))));
                        //DocumentFormat.OpenXml.Wordprocessing.TableCell cell3 = new DocumentFormat.OpenXml.Wordprocessing.TableCell(new TableCellProperties(new TableCellWidth { Width = "2400", Type = TableWidthUnitValues.Auto }), new DocumentFormat.OpenXml.Wordprocessing.Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(StartDate.ToString())));
                        //DocumentFormat.OpenXml.Wordprocessing.TableCell cell4 = new DocumentFormat.OpenXml.Wordprocessing.TableCell(new TableCellProperties(new TableCellWidth { Width = "2400", Type = TableWidthUnitValues.Auto }), new DocumentFormat.OpenXml.Wordprocessing.Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(StartDate.ToString())));
                        row.AppendChild(cell1);
                        row.AppendChild(cell2);
                        //row.AppendChild(cell3);
                        //row.AppendChild(cell4);



                    }
                }

            }
            catch (Exception ex)
            {
                MyMessageBoxNotifications myMessageBox = new MyMessageBoxNotifications();
                myMessageBox.Message = $"Произошла ошибка {ex.Message.ToString()}";
                myMessageBox.ShowDialog();
            }
            finally
            {
                MyMessageBoxNotifications myMessageBoxTrue = new MyMessageBoxNotifications();
                myMessageBoxTrue.Message = "Отчет успешно создан! \n Отчеты хранятся в папке Мои Документы";
                myMessageBoxTrue.ShowDialog();
            }
        }

        //Вызов окна посмотреть один отдельный доход

        private void OneIncome_Click(object sender, RoutedEventArgs e)
        {
            //Получение данных с окна от кнопки 
            object tag = ((System.Windows.Controls.Button)e.OriginalSource).Tag;
            int Id = (Int32)tag;

            ViewingOneIncome viewingOneIncome = new ViewingOneIncome(Id);
            viewingOneIncome.ChildWindowClosed += ChildWindow_ChildWindowClosed;
            viewingOneIncome.Show();
            Close();
        }

        //Для перемещения окна 
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        //Методы для Перезагрузки главного окна
        private void ChildWindow_ChildWindowClosed(object sender, EventArgs e)
        {
            RestartMainWindow();
        }

        private void RestartMainWindow()
        {
            MainWindow newMainWindow = new MainWindow();

           
            newMainWindow.Show();
            this.Close();
        }

        private void BtnCloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        //ФУНКЦИИ
        //Получение данных 
        public async Task GettingData()
        {
            Incomes = new List<Income>();

            string sqlExpression = $"SELECT Income.Id, Income.Date, Income.Summ\r\nFROM Income\r\n" +
                $"INNER JOIN CategoriesIncome ON Income.Id_Category = CategoriesIncome.Id\r\n" +
                $"WHERE CategoriesIncome.Title = '{CategoriesTitle}'\r\n" +
                $"ORDER BY Income.Date;";

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
                            Income inc = new Income()
                            {
                                Id = reader.GetInt32(0),
                                Date = reader.GetString(1),
                                Summ = reader.GetInt32(2),

                            };

                            Incomes.Add(inc);

                        }
                    }
                }
            }
        }
        //добавление данный за выбранный временной период 
        public void DateSelection(DateTime startDate, DateTime endDate)
        {
           IncomesShow = new List<Income>();

            foreach (Income inc in Incomes)
            {
                DateTime dateTime = DateTime.Parse(inc.Date);

                if (dateTime >= startDate && dateTime <= EndDate)
                {

                  IncomesShow.Add(inc);

                }

            }

            foreach (Income inc in IncomesShow)
            {
                //Добавление данных в элемент 
                ListViewIncomeOneCategory.Items.Add(inc);

            }

        }

    }
}
