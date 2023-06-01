using System;
using System.Windows.Forms;

namespace Decanat
{
    public partial class GroupForm : Form
    {
        public GroupForm()
        {
            InitializeComponent();
        }

        private void groupsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.groupsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.decanatDataSet);

        }

        private void GroupForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'decanatDataSet.Groups' table. You can move, or remove it, as needed.
            this.groupsTableAdapter.Fill(this.decanatDataSet.Groups);

        }
    }
}
