using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMKN_26
{
    public partial class Form1 : Form
    {
        private DataBaseDataContext db = new DataBaseDataContext();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var id = db.SIJA_ITCs.Where(x => x.Username == tbUsername.Text 
            && x.Password == tbPassword.Text).FirstOrDefault();
           

            if (id != null)
            {
                new MainForm().Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Username Dan Password Tidak Terdaftar");
            }
        }
    }
}
