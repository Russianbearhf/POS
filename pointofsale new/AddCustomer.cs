using System.Data.SqlClient;

namespace pointofsale_new
{
    public partial class AddCustomer : Form
    {
        public AddCustomer()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\khodo\Documents\pos.mdf;Integrated Security=True;Connect Timeout=30");

        private void Reset()
        {
            CNameTb.Text = "";
            CAddressTb.Text = "";
            CPhoneTb.Text = "";
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (CNameTb.Text == "" || CAddressTb.Text == "" || CPhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    Reset();
                    SqlCommand cmd = new SqlCommand("insert into CustomerTbl (CustomerName,CustAddress,CustPhone)values(@CN,@CA,@CP)", Con);
                    cmd.Parameters.AddWithValue("@CN", CNameTb.Text);
                    cmd.Parameters.AddWithValue("@CA", CAddressTb.Text);
                    cmd.Parameters.AddWithValue("@CP", CPhoneTb.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Saved");

                    Con.Close();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }
    }
}