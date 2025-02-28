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
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using MyCapital.Class;
namespace MyCapital
{
    /// <summary>
    /// Логика взаимодействия для ViewSavingsByCategory.xaml
    /// </summary>
    public partial class ViewSavingsByCategory : Window
    {
        //коллекция для получения данных
        List<Savings> Saving { get; set; }

        //словарь для вывода данных
        Dictionary<string, int> SavingsShow = new Dictionary<string, int>();

        //Даты для устновки временного периода
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        //Первый вызов окна 

        public ViewSavingsByCategory()
        {
            InitializeComponent();
            //Получает первый и последний дни, текущего месяца 
            DateTime today = DateTime.Today;
            StartDate = new DateTime(today.Year, today.Month, 1);
            EndDate = StartDate.AddMonths(1).AddDays(-1);

            GettingData();
            DateSelection(StartDate, EndDate);
        }

        //Вызов окна при установке пользователем временного диапазона
        public ViewSavingsByCategory(DateTime startDate, DateTime endDate)
        {
            InitializeComponent();

            StartDate = startDate;
            EndDate = endDate;

            GettingData();
            DateSelection(StartDate, EndDate);
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

        //вызов окна просмотр все расходов отдельной категории 
        private void SelectCategory_Click(object sender, RoutedEventArgs e)
        {
            //Получение данных с окна от кнопки 
            object tag = ((Button)e.OriginalSource).Tag;
            string categories = (string)tag;

            //Получает первый и последний дни, текущего месяца 
            DateTime today = DateTime.Today;
            DateTime startDate = new DateTime(today.Year, today.Month, 1);
            DateTime endDate = StartDate.AddMonths(1).AddDays(-1);

            ViewingSavingsOneCategory viewingSavingOneCategory = new ViewingSavingsOneCategory(categories, startDate, endDate);
            viewingSavingOneCategory.Show();

            Close();
        }

        //вызов окна выбор даты
        private void DateSelectionClick(object sender, RoutedEventArgs e)
        {
            //this чтобы передавать информацию, о том из какого окна вызывается окно  DateSelection
            DateSelection dateSelection = new DateSelection(this);
            dateSelection.Show();
        }

        //Скачать отчет 
        private void DowloadWorld(object sender, RoutedEventArgs e)
        {
            //Создаем название файла 
            string fileName = $"Накопления_{StartDate.ToString("yyyyMMddHHmmss")}-{EndDate.ToString("yyyyMMddHHmmss")}.docx";
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


                    DocumentFormat.OpenXml.Wordprocessing.TableCell headerCell1 = new DocumentFormat.OpenXml.Wordprocessing.TableCell(new TableCellProperties(new TableCellWidth { Width = "3000", Type = TableWidthUnitValues.Auto }), new DocumentFormat.OpenXml.Wordprocessing.Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new Text("Категория"))));
                    DocumentFormat.OpenXml.Wordprocessing.TableCell headerCell2 = new DocumentFormat.OpenXml.Wordprocessing.TableCell(new TableCellProperties(new TableCellWidth { Width = "2400", Type = TableWidthUnitValues.Auto }), new DocumentFormat.OpenXml.Wordprocessing.Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new Text("Сумма"))));
                    //DocumentFormat.OpenXml.Wordprocessing.TableCell headerCell3 = new DocumentFormat.OpenXml.Wordprocessing.TableCell(new TableCellProperties(new TableCellWidth { Width = "2400", Type = TableWidthUnitValues.Auto }), new DocumentFormat.OpenXml.Wordprocessing.Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new Text("Начальная дата"))));
                    //DocumentFormat.OpenXml.Wordprocessing.TableCell headerCell4 = new DocumentFormat.OpenXml.Wordprocessing.TableCell(new TableCellProperties(new TableCellWidth { Width = "2400", Type = TableWidthUnitValues.Auto }), new DocumentFormat.OpenXml.Wordprocessing.Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new Text("Конечная дата"))));

                    headerRow.AppendChild(headerCell1);
                    headerRow.AppendChild(headerCell2);
                    //headerRow.AppendChild(headerCell3);
                    //headerRow.AppendChild(headerCell4);

                    // Заполняем таблицу данными
                    foreach (var item in SavingsShow)
                    {
                        DocumentFormat.OpenXml.Wordprocessing.TableRow row = new DocumentFormat.OpenXml.Wordprocessing.TableRow();
                        table.AppendChild(row);

                        DocumentFormat.OpenXml.Wordprocessing.TableCell cell1 = new DocumentFormat.OpenXml.Wordprocessing.TableCell(new TableCellProperties(new TableCellWidth { Width = "2400", Type = TableWidthUnitValues.Auto }), new DocumentFormat.OpenXml.Wordprocessing.Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new Text(item.Key))));
                        DocumentFormat.OpenXml.Wordprocessing.TableCell cell2 = new DocumentFormat.OpenXml.Wordprocessing.TableCell(new TableCellProperties(new TableCellWidth { Width = "2400", Type = TableWidthUnitValues.Auto }), new DocumentFormat.OpenXml.Wordprocessing.Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new Text(item.Value.ToString()))));
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
        //Обработчик для кнопки Закрыть
        private void BtnCloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        //ФУНКЦИИ
        //Получение данных 
        public async Task GettingData()
        {
            Saving= new List<Savings>();

            string sqlExpression = $"SELECT CategoriesSavings.Title, Savings.Summ, Savings.Date " +
                $"FROM Savings  \r\nJOIN CategoriesSavings ON CategoriesSavings.Id = Savings.Id_Category";

            await using (var connection = new SqliteConnection("Data Source=MyCapital.db"))
            {
              await  connection.OpenAsync();

                //Расходы
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);
                await using (SqliteDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        while (await reader.ReadAsync())   // построчно считываем данные
                        {
                            Savings saving = new Savings
                            {
                                Categories = reader.GetString(0),
                                Summ = reader.GetInt32(1),
                                Date = reader.GetString(2)
                            };

                            Saving.Add(saving);

                        }
                    }
                }
            }
        }


        //добавление данныХ в словарь за выбранный временной период 
        public void DateSelection(DateTime startDate, DateTime endDate)
        {
            foreach (Savings sav in Saving)
            {
                DateTime dateTime = DateTime.Parse(sav.Date);

                if (dateTime >= startDate && dateTime <= EndDate)
                {
                    if (SavingsShow.ContainsKey(sav.Categories))
                    {
                        SavingsShow[sav.Categories] += sav.Summ;
                    }
                    else
                    {
                        SavingsShow[sav.Categories] = sav.Summ;
                    }

                }


            }

            foreach (var item in SavingsShow)
            {
                Savings saving = new Savings()
                {
                    Categories = item.Key,
                    Summ = item.Value
                };

                //Добавление данных в элемент 
                ListViewSavingsCategory.Items.Add(saving);

            }

        }


    }
}
