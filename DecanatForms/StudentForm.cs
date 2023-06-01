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
    public partial class StudentForm : Form
    {
        public StudentForm()
        {
            InitializeComponent();
        }

        private void studentBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.studentBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.decanatDataSet);

        }

        private void StudentForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'decanatDataSet.Groups' table. You can move, or remove it, as needed.
            this.groupsTableAdapter.Fill(this.decanatDataSet.Groups);
            // TODO: This line of code loads data into the 'decanatDataSet.Student' table. You can move, or remove it, as needed.
            this.studentTableAdapter.Fill(this.decanatDataSet.Student);

        }
    }
}
