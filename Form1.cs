using System;
using System.Windows.Forms;

namespace Decanat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void groupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GroupForm groupForm = new GroupForm();
            groupForm.ShowDialog();
        }

        private void disciplinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisciplineForm disciplineForm = new DisciplineForm();
            disciplineForm.ShowDialog();
        }

        private void studentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StudentForm studentForm = new StudentForm();
            studentForm.ShowDialog();
        }

        private void vedomostiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DecanatForms.VedomostiForm vedomosti = new DecanatForms.VedomostiForm();
            vedomosti.ShowDialog();
        }

        private void labelAuthor_Click(object sender, EventArgs e)
        {
            DecanatForms.AuthForm authForm = new DecanatForms.AuthForm();
            authForm.ShowDialog();

            if (Personal.UserIsAuth == true)
            {
                if (Personal.UserStatus == "admin") handbookToolStripMenuItem.Visible = true;
                labelAuthor.Text = Personal.UserFullname;
                labelStatus.Text = Personal.UserStatus;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void aboutProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("by @dobrodelete");
        }
    }
}
