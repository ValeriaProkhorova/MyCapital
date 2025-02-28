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
using MyCapital.Class;
namespace MyCapital
{
    /// <summary>
    /// Логика взаимодействия для ViewingSavingsOneCategory.xaml
    /// </summary>
    public partial class ViewingSavingsOneCategory : Window
    {
        //название категории которые передается из предыдущего окна
        public static string CategoriesTitle { get; set; }


        //коллекция для получения данных
        List<Savings> Saving{ get; set; }

        //коллекция для вывода данных
        List<Savings> SavingsShow { get; set; }



        //Даты для устновки временного периода
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        //Начальная загрузка окна. По умолчанию даные выводятся за текущий месяц

        public ViewingSavingsOneCategory(string categories, DateTime startDate, DateTime endDate)
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
        public ViewingSavingsOneCategory(DateTime startDate, DateTime endDate)
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
            string fileName = $"Накопления на{CategoriesTitle}_{StartDate.ToString("yyyyMMddHHmmss")}-{EndDate.ToString("yyyyMMddHHmmss")}.docx";
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
                    foreach (Savings exp in SavingsShow)
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
                myMessageBoxTrue.Message = "Отчет успешно создан! \nОтчеты хранятся в папке Мои Документы";
                myMessageBoxTrue.ShowDialog();
            }
        }


        //Вызов окна посмотреть одну запись

        private void OneSavings_Click(object sender, RoutedEventArgs e)
        {
            //Получение данных с окна от кнопки 
            object tag = ((System.Windows.Controls.Button)e.OriginalSource).Tag;
            int Id = (Int32)tag;

            ViewingOneSaving viewingOneSaving = new ViewingOneSaving(Id);
            viewingOneSaving.ChildWindowClosed += ChildWindow_ChildWindowClosed;
            viewingOneSaving.Show();
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

        //Для перемещения окна 
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void BtnCloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //ФУНКЦИИ
        //Получение данных 
        public async Task GettingData()
        {
            Saving = new List<Savings>();

            string sqlExpression = $"SELECT Savings.Id, Savings.Date, Savings.Summ\r\nFROM Savings\r\n" +
                $"INNER JOIN CategoriesSavings ON Savings.Id_Category = CategoriesSavings.Id\r\n" +
                $"WHERE CategoriesSavings.Title = '{CategoriesTitle}'\r\n" +
                $"ORDER BY Savings.Date;";

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
                            Savings sav = new Savings()
                            {
                                Id = reader.GetInt32(0),
                                Date = reader.GetString(1),
                                Summ = reader.GetInt32(2),

                            };

                            Saving.Add(sav);

                        }
                    }
                }
            }
        }
            //добавление данный за выбранный временной период 
            public void DateSelection(DateTime startDate, DateTime endDate)
            {
               SavingsShow = new List<Savings>();

                foreach (Savings sav in Saving)
                {
                    DateTime dateTime = DateTime.Parse(sav.Date);

                    if (dateTime >= startDate && dateTime <= EndDate)
                    {

                        SavingsShow.Add(sav);

                    }

                }

                foreach (Savings sav in SavingsShow)
                {
                    //Добавление данных в элемент 
                    ListViewSavingsOneCategory.Items.Add(sav);

                }

            }
        
    }
}
