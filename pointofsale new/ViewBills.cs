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

namespace pointofsale_new
{
    public partial class ViewBills : Form
    {
        public ViewBills()
        {
            InitializeComponent();
            DisplayBill();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\khodo\Documents\pos.mdf;Integrated Security=True;Connect Timeout=30");

        private void DisplayBill()
        {
            Con.Open();
            string Query = "Select * from BillTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            SellsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void ViewBills_Load(object sender, EventArgs e)
        {

        }
    }
}
