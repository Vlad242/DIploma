using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DIploma_repair.Admin
{
    public partial class DetailOrderList : Form
    {
        private string Login;
        public MySqlConnection conn;
        private string Search = "";

        public DetailOrderList(string login)
        {
            InitializeComponent();
            Login = login;
            DataBase.DataBaseInfo dataBase = new DataBase.DataBaseInfo();
            conn = new MySqlConnection(dataBase.GetConnectInfo());
            conn.Open();

            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyy:MM:dd";
            dateTimePicker1.Visible = false;
        }

        private void DetailOrderList_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Columns.Clear();
                MySqlDataAdapter mda = new MySqlDataAdapter("SELECT Detail_order.D_Order_id, Detail.Detail_name, Detail.Prod_country, Detail.Price, Detail_order.D_Order_date, Detail_order.D_Count, Worker.Worker_surname, Worker.Worker_name, Status.Status_name FROM Detail_order INNER JOIN Detail on(Detail_order.Detail_id=Detail.Detail_id) INNER JOIN Status on(Detail_order.Status_id=Status.Status_id) INNER JOIN Worker on(Worker.Worker_id=Detail_order.Worker_id);", conn);
                DataSet ds = new DataSet();
                mda.Fill(ds, "Detail_order");
                dataGridView1.DataSource = ds.Tables["Detail_order"];
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    dataGridView1.Rows[i].Cells[0].ReadOnly = true;
                /////columns names
                dataGridView1.Columns[0].HeaderText = "Ідентифікатор";
                dataGridView1.Columns[1].HeaderText = "Назва деталі";
                dataGridView1.Columns[2].HeaderText = "Країна-виробник";
                dataGridView1.Columns[3].HeaderText = "Ціна";
                dataGridView1.Columns[4].HeaderText = "Дата замовлення";
                dataGridView1.Columns[5].HeaderText = "Кількість деталей";
                dataGridView1.Columns[6].HeaderText = "Прізвище майстра";
                dataGridView1.Columns[7].HeaderText = "Ім'я майстра";
                dataGridView1.Columns[8].HeaderText = "Статус замовлення";

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                ReColorGrid();
            }
            catch (Exception)
            {

            }
        }

        private void ReColorGrid()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                switch (dataGridView1.Rows[i].Cells[8].Value)
                {
                    case "Order processing":
                        {
                            dataGridView1.Rows[i].Cells[8].Style.BackColor = System.Drawing.Color.Orange;
                            break;
                        }
                    case "Complete":
                        {
                            dataGridView1.Rows[i].Cells[8].Style.BackColor = System.Drawing.Color.Green;
                            break;
                        }
                    case "In the process of repair":
                        {
                            dataGridView1.Rows[i].Cells[8].Style.BackColor = System.Drawing.Color.Cyan;
                            break;
                        }
                    case "Purchase":
                        {
                            dataGridView1.Rows[i].Cells[8].Style.BackColor = System.Drawing.Color.Yellow;
                            break;
                        }
                }
            }
        }

        private void DetailOrderList_FormClosing(object sender, FormClosingEventArgs e)
        {
            AdminRoom room = new AdminRoom(Login);
            room.Show();
            this.Dispose();
        }

        private void DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = dateTimePicker1.Text;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString().Contains("Дата"))
            {
                dateTimePicker1.Visible = true;
            }
            else
            {
                dateTimePicker1.Visible = false;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                ReSearch();

                dataGridView1.Columns.Clear();
                MySqlDataAdapter mda = new MySqlDataAdapter("SELECT Detail_order.D_Order_id, Detail.Detail_name, Detail.Prod_country, Detail.Price, Detail_order.D_Order_date, Detail_order.D_Count, Worker.Worker_surname, Worker.Worker_name, Status.Status_name FROM Detail_order INNER JOIN Detail on(Detail_order.Detail_id=Detail.Detail_id) INNER JOIN Status on(Detail_order.Status_id=Status.Status_id) INNER JOIN Worker on(Worker.Worker_id=Detail_order.Worker_id) WHERE" + Search + ";", conn);
                DataSet ds = new DataSet();
                mda.Fill(ds, "Detail_order");
                dataGridView1.DataSource = ds.Tables["Detail_order"];
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    dataGridView1.Rows[i].Cells[0].ReadOnly = true;
                /////columns names
                dataGridView1.Columns[0].HeaderText = "Ідентифікатор";
                dataGridView1.Columns[1].HeaderText = "Назва деталі";
                dataGridView1.Columns[2].HeaderText = "Країна-виробник";
                dataGridView1.Columns[3].HeaderText = "Ціна";
                dataGridView1.Columns[4].HeaderText = "Дата замовлення";
                dataGridView1.Columns[5].HeaderText = "Кількість деталей";
                dataGridView1.Columns[6].HeaderText = "Прізвище майстра";
                dataGridView1.Columns[7].HeaderText = "Ім'я майстра";
                dataGridView1.Columns[8].HeaderText = "Статус замовлення";

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                ReColorGrid();
            }
            catch (Exception)
            {

            }
        }

        public void ReSearch()
        {
            if (comboBox1.Text != "" && textBox1.Text != "")
            {
                string operation = "";
                switch (comboBox2.Text)
                {
                    case ">":
                        operation = ">";
                        break;
                    case "<":
                        operation = "<";
                        break;
                    case "=":
                        operation = "=";
                        break;
                    case ">=":
                        operation = ">=";
                        break;
                    case "<=":
                        operation = "<=";
                        break;
                    case "!=":
                        operation = "!=";
                        break;
                    default:
                        operation = " LIKE ";
                        break;
                }
                switch (comboBox1.Text)
                {
                    case "Ідентифікатор":
                        {
                            if(Search == "")
                            {
                                Search += " Detail_order.D_Order_id" + operation + "'" + textBox1.Text + "' ";
                            }
                            else
                            {
                                Search += " and Detail_order.D_Order_id" + operation + "'" + textBox1.Text + "' ";
                            }
                           
                            break;
                        }
                    case "Назва деталі":
                        {
                            if(Search == "")
                            {
                                Search += " Detail.Detail_name" + operation + "'" + textBox1.Text + "' ";
                            }
                            else
                            {
                                Search += " and Detail.Detail_name" + operation + "'" + textBox1.Text + "' ";
                            }
                            break;
                        }
                    case "Країна-виробник":
                        {
                            if (Search == "")
                            {
                                Search += " Detail.Prod_country" + operation + "'" + textBox1.Text + "' ";
                            }
                            else
                            {
                                Search += " and Detail.Prod_country" + operation + "'" + textBox1.Text + "' ";
                            }
                            break;
                        }
                    case "Ціна":
                        {
                            if (Search == "")
                            {
                                Search += " Detail.Price" + operation + "'" + textBox1.Text + "' ";
                            }
                            else
                            {
                                Search += " and Detail.Price" + operation + "'" + textBox1.Text + "' ";
                            }
                            break;
                        }
                    case "Дата замовлення":
                        {
                            if (Search == "")
                            {
                                Search += " Detail_order.D_Order_date" + operation + "'" + textBox1.Text + "' ";
                            }
                            else
                            {
                                Search += " and Detail_order.D_Order_date" + operation + "'" + textBox1.Text + "' ";
                            }
                            break;
                        }
                    case "Кількість деталей":
                        {
                            if (Search == "")
                            {
                                Search += " Detail_order.D_Count" + operation + "'" + textBox1.Text + "' ";
                            }
                            else
                            {
                                Search += " and Detail_order.D_Count" + operation + "'" + textBox1.Text + "' ";
                            }
                            break;
                        }
                    case "Прізвище майстра":
                        {
                            if (Search == "")
                            {
                                Search += " Worker.Worker_surname" + operation + "'" + textBox1.Text + "' ";
                            }
                            else
                            {
                                Search += " and Worker.Worker_surname" + operation + "'" + textBox1.Text + "' ";
                            }
                            break;
                        }
                    case "Ім'я майстра":
                        {
                            if (Search == "")
                            {
                                Search += " Worker.Worker_name" + operation + "'" + textBox1.Text + "' ";
                            }
                            else
                            {
                                Search += " and Worker.Worker_name" + operation + "'" + textBox1.Text + "' ";
                            }
                            break;
                        }
                    case "Статус замовлення":
                        {
                            if (Search == "")
                            {
                                Search += " Status.Status_name" + operation + "'" + textBox1.Text + "' ";
                            }
                            else
                            {
                                Search += " and Status.Status_name" + operation + "'" + textBox1.Text + "' ";
                            }
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            DetailOrderList_Load(null, null);
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentCell.Value.ToString();
        }

        private void DataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ReColorGrid();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog
            {
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
            };
            string path = "";

            if (s.ShowDialog() == DialogResult.OK)
            {
                path = s.FileName;
            }

            try
            {
                FileStream fs = new FileStream(@path, FileMode.Create);
                StreamWriter streamWriter = new StreamWriter(fs);


                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    streamWriter.Write(dataGridView1.Columns[j].HeaderText + " ");
                }
                streamWriter.WriteLine();

                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    for (int i = 0; i < dataGridView1.Rows[j].Cells.Count; i++)
                    {
                        streamWriter.Write(dataGridView1.Rows[j].Cells[i].Value + " ");
                    }

                    streamWriter.WriteLine();
                }

                streamWriter.Close();
                fs.Close();

                MessageBox.Show("Файл успішно збережено");
            }
            catch
            {
                MessageBox.Show("Помилка при збереженні файлу!");
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog
            {
                Filter = "pdf files (*.pdf)|*.pdf|All files (*.*)|*.*"
            };
            string path = "";

            if (s.ShowDialog() == DialogResult.OK)
            {
                path = s.FileName;
            }

            try
            {
                string sql = "";
                if(Search == "")
                {
                    sql = "SELECT Detail_order.D_Order_id, Detail.Detail_name, Detail.Prod_country, Detail.Price, Detail_order.D_Order_date, Detail_order.D_Count, Worker.Worker_surname, Worker.Worker_name, Status.Status_name FROM Detail_order INNER JOIN Detail on(Detail_order.Detail_id=Detail.Detail_id) INNER JOIN Status on(Detail_order.Status_id=Status.Status_id) INNER JOIN Worker on(Worker.Worker_id=Detail_order.Worker_id);";
                }
                else
                {
                    sql = "SELECT Detail_order.D_Order_id, Detail.Detail_name, Detail.Prod_country, Detail.Price, Detail_order.D_Order_date, Detail_order.D_Count, Worker.Worker_surname, Worker.Worker_name, Status.Status_name FROM Detail_order INNER JOIN Detail on(Detail_order.Detail_id=Detail.Detail_id) INNER JOIN Status on(Detail_order.Status_id=Status.Status_id) INNER JOIN Worker on(Worker.Worker_id=Detail_order.Worker_id) WHERE" + Search + ";";
                }
                MySqlDataAdapter mda = new MySqlDataAdapter(sql , conn);
                DataSet ds = new DataSet();
                mda.Fill(ds, "Detail_order");

                iTextSharp.text.Document doc = new iTextSharp.text.Document();

                PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));

                doc.Open();

                BaseFont baseFont = BaseFont.CreateFont("C:\\Windows\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);

                PdfPTable table = new PdfPTable(ds.Tables["Detail_order"].Columns.Count);

                PdfPCell cell = new PdfPCell(new Phrase(" " + this.Text + " ", font))
                {
                    Colspan = ds.Tables["Detail_order"].Columns.Count,
                    HorizontalAlignment = PdfPCell.ALIGN_CENTER,
                    Border = 0
                };
                table.AddCell(cell);
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    cell = new PdfPCell(new Phrase(new Phrase(dataGridView1.Columns[j].HeaderText, font)))
                    {
                        BackgroundColor = iTextSharp.text.BaseColor.LIGHT_GRAY
                    };
                    table.AddCell(cell);
                }

                for (int j = 0; j < ds.Tables["Detail_order"].Rows.Count; j++)
                {
                    for (int k = 0; k < ds.Tables["Detail_order"].Columns.Count; k++)
                    {
                        table.AddCell(new Phrase(ds.Tables["Detail_order"].Rows[j][k].ToString(), font));
                    }
                }
                doc.Add(table);

                doc.Close();

                MessageBox.Show("Pdf-документ збережено!");
            }
            catch (Exception)
            {
                MessageBox.Show("Проблеми зі збереженням в PDF формат!");
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog
            {
                Filter = "xlsx files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
            };
            string path = "";

            if (s.ShowDialog() == DialogResult.OK)
            {
                path = s.FileName;
            }
            try
            {
                SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(path, SpreadsheetDocumentType.Workbook);

                WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
                workbookpart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet(new SheetData());

                Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.
                    AppendChild<Sheets>(new Sheets());

                Sheet sheet = new Sheet()
                {
                    Id = spreadsheetDocument.WorkbookPart.
                    GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "Detail_order"
                };
                sheets.Append(sheet);
                spreadsheetDocument.Close();

                Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                ExcelApp.Application.Workbooks.Open(path);
                ExcelApp.Columns.ColumnWidth = 15;

                ExcelApp.Cells[1, 1] = "Ідентифікатор";
                ExcelApp.Cells[1, 2] = "Назва деталі";
                ExcelApp.Cells[1, 3] = "Країна-виробник";
                ExcelApp.Cells[1, 4] = "Ціна";
                ExcelApp.Cells[1, 5] = "Дата замовлення";
                ExcelApp.Cells[1, 6] = "Кількість деталей";
                ExcelApp.Cells[1, 7] = "Прізвище майстра";
                ExcelApp.Cells[1, 8] = "Ім'я майстра";
                ExcelApp.Cells[1, 9] = "Статус замовлення";

                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    for (int j = 0; j < dataGridView1.RowCount - 1; j++)
                    {
                        ExcelApp.Cells[j + 2, i + 1] = (dataGridView1.Rows[j].Cells[i].Value).ToString();
                    }
                }
                ExcelApp.Quit();
            }
            catch (Exception)
            {

            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            try
            {
                bmp = new Bitmap(dataGridView1.Size.Width + 10, dataGridView1.Size.Height + 10);
                dataGridView1.DrawToBitmap(bmp, dataGridView1.Bounds);
                if ((dataGridView1.Size.Width + 10) > 210)
                {
                    printDocument1.DefaultPageSettings.Landscape = true;
                }
                else
                {
                    printDocument1.DefaultPageSettings.Landscape = false;
                }
                printDocument1.DefaultPageSettings.Color = false;

                printPreviewDialog1.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Printing Error!");
            }
        }

        Bitmap bmp;
        private void PrintDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bmp, 0, 0);
        }
    }
}
