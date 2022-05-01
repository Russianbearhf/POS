using System.Data.SqlClient;

namespace pointofsale_new
{
    public partial class Addsuppliers : Form
    {
        public Addsuppliers()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\khodo\Documents\pos.mdf;Integrated Security=True;Connect Timeout=30");

        private void Reset()
        {
            SNameTb.Text = "";
            SPhoneTb.Text = "";
            SAddressTb.Text = "";
            SRemarksTb.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SNameTb.Text == "" || SAddressTb.Text == "" || SPhoneTb.Text == "" || SRemarksTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    Reset();
                    SqlCommand cmd = new SqlCommand("insert into SupplierTbl(SupName,SupAddress,SupPhone,SupRem)values(@SN,@SA,@SP,@SR)", Con);
                    cmd.Parameters.AddWithValue("@SN", SNameTb.Text);
                    cmd.Parameters.AddWithValue("@SA", SAddressTb.Text);
                    cmd.Parameters.AddWithValue("@SP", SPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@SR", SRemarksTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Supplier Saved");

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