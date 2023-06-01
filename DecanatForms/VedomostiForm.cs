using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Decanat.DecanatForms
{
    public partial class VedomostiForm : Form
    {
        private DataGridViewColumn _col;
        public VedomostiForm()
        {
            InitializeComponent();
        }

        private void VedomostiForm_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Connection.connection))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT [dbo].[Student].[fullname], [dbo].[Groups].[name], [dbo].[Subject].[name], [dbo].[Mark].[mark], [dbo].[Mark].[type_of_control]" +
                        "FROM [dbo].[Student], [dbo].[Groups], [dbo].[Subject], [dbo].[Mark]" +
                        "WHERE ([dbo].[Student].[id]=[dbo].[Groups].[id]) AND" +
                        "([dbo].[Mark].[id_student]=[dbo].[Student].[id]) AND" +
                        "([dbo].[Mark].[id_subject]=[dbo].[Subject].[id]);";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader sqlDataReader = command.ExecuteReader();

                    if (sqlDataReader.HasRows == false)
                    {
                        MessageBox.Show("Data has not found");
                    }
                    else
                    {
                        while (sqlDataReader.Read())
                        {
                            dataGridStudent.Rows.Add(sqlDataReader[0], sqlDataReader[1], sqlDataReader[2], sqlDataReader[3], sqlDataReader[4]);
                        }
                    }
                    sqlDataReader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex}");
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            _col = new DataGridViewColumn();
            switch (listBox1.SelectedIndex)
            {
                case 0:
                    _col = dataGridStudent.Columns[0];
                    break;
                case 1:
                    _col = dataGridStudent.Columns[1];
                    break;
                case 2:
                    _col = dataGridStudent.Columns[2];
                    break;
                case 3:
                    _col = dataGridStudent.Columns[3];
                    break;
                case 4:
                    _col = dataGridStudent.Columns[4];
                    break;
            }
            if (radioButton1.Checked)
                dataGridStudent.Sort(_col, System.ComponentModel.ListSortDirection.Ascending);
            else
                dataGridStudent.Sort(_col, System.ComponentModel.ListSortDirection.Descending);

        }

        private void buttonExportExcel_Click(object sender, EventArgs e)
        {
            Excel.Application excel = new Excel.Application();
            excel.SheetsInNewWorkbook = 1;
            Excel.Workbook workbook = excel.Workbooks.Add(Type.Missing);
            Excel.Worksheet worksheet = excel.Worksheets.Item[1];
            worksheet.Name = "Vedomisti";

            worksheet.Cells[1, 1] = dataGridStudent.Columns[0].HeaderCell.Value;
            worksheet.Cells[1, 2] = dataGridStudent.Columns[1].HeaderCell.Value;
            worksheet.Cells[1, 3] = dataGridStudent.Columns[2].HeaderCell.Value;
            worksheet.Cells[1, 4] = dataGridStudent.Columns[3].HeaderCell.Value;
            worksheet.Cells[1, 5] = dataGridStudent.Columns[4].HeaderCell.Value;


            for (int i = 2; i < dataGridStudent.RowCount + 1; i++)
            {
                worksheet.Cells[i, 1] = dataGridStudent[0, i - 2].Value;
                worksheet.Cells[i, 2] = dataGridStudent[1, i - 2].Value;
                worksheet.Cells[i, 3] = dataGridStudent[2, i - 2].Value;
                worksheet.Cells[i, 4] = dataGridStudent[3, i - 2].Value;
                worksheet.Cells[i, 5] = dataGridStudent[4, i - 2].Value;

            }

            excel.Visible = true;
        }
    }
}
