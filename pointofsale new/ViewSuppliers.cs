using System.Data.SqlClient;

namespace pointofsale_new
{
    public partial class ViewSuppliers : Form
    {
        public ViewSuppliers()
        {
            InitializeComponent();
        }

        private void label9_Click(object sender, EventArgs e)
        {
        }

        private void label13_Click(object sender, EventArgs e)
        {
            MainMenu Obj = new MainMenu();
            Obj.Show();
            this.Hide();
        }

        private int key = 0;

        public int Key { get; private set; }

        private void ProductsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            {
                SNameTb.Text = SuppliersDGV.SelectedRows[0].Cells[1].Value.ToString();
                SAddressTb.Text = SuppliersDGV.SelectedRows[0].Cells[2].Value.ToString();
                SPhoneTb.Text = SuppliersDGV.SelectedRows[0].Cells[3].Value.ToString();
                SRemarks.Text = SuppliersDGV.SelectedRows[0].Cells[4].Value.ToString();
                if (SNameTb.Text == "")
                {
                    Key = 0;
                }
                else
                {
                    Key = Convert.ToInt32(SuppliersDGV.SelectedRows[0].Cells[1].Value.ToString());
                }
            }
        }
        private SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\khodo\Documents\pos.mdf;Integrated Security=True;Connect Timeout=30");

        private void Reset()
        {
            SNameTb.Text = "";
            SPhoneTb.Text = "";
            SAddressTb.Text = "";
            SRemarks.Text = "";
            Key = 0;
        }
        /// <summary>
        /// delete button//
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select The Supplier");
            }
            else
            {
                try
                {
                    Con.Open();

                    SqlCommand cmd = new SqlCommand("delete from SupplierTbl where SupId=@SKey", Con);
                    cmd.Parameters.AddWithValue("@SKey", Key);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Supplier Deleted");

                    Con.Close();
                    
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (SNameTb.Text == "" || SAddressTb.Text == "" || SPhoneTb.Text == "" || SRemarks.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    Reset();
                    SqlCommand cmd = new SqlCommand("Update   SupplierTbl  set SupName==@SN,SupAddress==@SA,SupPhone==SP,SupRem==@SR WHERE SupId=@SKey", Con);
                    cmd.Parameters.AddWithValue("@SN", SNameTb.Text);
                    cmd.Parameters.AddWithValue("@SA", SAddressTb.Text);
                    cmd.Parameters.AddWithValue("@SP", SPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@SR", SRemarks.Text);
                    cmd.Parameters.AddWithValue("@SKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Supplier Updated");

                    Con.Close();
                    
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
    }
}