using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Decanat
{
    public partial class DisciplineForm : Form
    {
        public DisciplineForm()
        {
            InitializeComponent();
        }

        private void subjectBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.subjectBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.decanatDataSet);

        }

        private void DisciplineForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'decanatDataSet.Subject' table. You can move, or remove it, as needed.
            this.subjectTableAdapter.Fill(this.decanatDataSet.Subject);

        }

        private void filterButton_Click(object sender, EventArgs e)
        {
            subjectBindingSource.Filter = "semester='" + semesterComboBox.Text + "'";
        }

        private void showAllButton_Click(object sender, EventArgs e)
        {
            subjectBindingSource.Filter = "";
        }
    }
}
