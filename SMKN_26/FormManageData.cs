using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMKN_26
{
    public partial class FormManageData : Form
    {
        private DataBaseDataContext db = new DataBaseDataContext();

        private int selectedId;
        public FormManageData()
        {
            InitializeComponent();
        }
        private void showData()
        {

            DGVData.Columns.Clear();
            var data = db.SIJA_ITCs.Where(a => a.Username.Contains(tbSearch.Text))
                .Select(a => new
                {
                    a.Username,
                    a.Password,
                    a.id
                });
            DGVData.DataSource = data;
            DGVData.Columns["id"].Visible = false;
        }

        private void FormManageData_Load(object sender, EventArgs e)
        {
            showData();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            var newData = new SIJA_ITC();
            newData.Username = tbUsername.Text;
            newData.Password = tbPassword.Text;

            db.SIJA_ITCs.InsertOnSubmit(newData);
            db.SubmitChanges();

            MessageBox.Show("Insert data successfully!");

            tbUsername.Clear(); tbPassword.Clear();
            showData();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            new MainForm().Show();
            this.Hide();
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            showData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var data = db.SIJA_ITCs.Where(x => x.id == selectedId).FirstOrDefault();

            if (data == null)
            {
                MessageBox.Show("Data Not Found!");
                return;
            }

            data.Username = tbUsername.Text;
            data.Password = tbPassword.Text;
            db.SubmitChanges();

            tbUsername.Clear();
            tbPassword.Clear();
            showData();

            MessageBox.Show("Update Data Successfully");


        }

        private void DGVData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedId = Convert.ToInt32(DGVData.Rows[e.RowIndex].Cells["id"].Value);
                tbUsername.Text = DGVData.Rows[e.RowIndex].Cells["Username"].Value.ToString();
                tbPassword.Text = DGVData.Rows[e.RowIndex].Cells["Password"].Value.ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var data = db.SIJA_ITCs.Where(x => x.id == selectedId).FirstOrDefault();

            if (data == null)
            {
                MessageBox.Show("Data Not Found!");
                return;
            }

            db.SIJA_ITCs.DeleteOnSubmit(data);
            db.SubmitChanges();
            MessageBox.Show("Delete Data Successfully!");
            showData();

            tbUsername.Clear();
            tbPassword.Clear();
        }
    }
}
