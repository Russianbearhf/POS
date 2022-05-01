using System.Data;
using System.Data.SqlClient;

namespace pointofsale_new
{
    public partial class ViewCustomers : Form
    {
        public ViewCustomers()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {
        }

        private void label13_Click(object sender, EventArgs e)
        {
            MainMenu Obj = new MainMenu();
            Obj.Show();
            this.Hide();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\khodo\Documents\pos.mdf;Integrated Security=True;Connect Timeout=30");

        private void DisplayCust()
        {
            Con.Open();
            string Query = "Select * from CustomerTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CustomersDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void Reset()
        {
            CNameTb.Text = "";
            CAddressTb.Text = "";
            CPhoneTb.Text = "";
            Key = 0;
        }
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select The Customer");
            }
            else
            {
                try
                {
                    Con.Open();

                    SqlCommand cmd = new SqlCommand("delete from CustomersTbl where CustID=@CKey", Con);
                    cmd.Parameters.AddWithValue("@CKey", Key);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Customer Deleted");

                    Con.Close();
                    DisplayCust();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int Key = 0;
        private void CustomersDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CNameTb.Text = CustomersDGV.SelectedRows[0].Cells[1].Value.ToString();
            CAddressTb.Text = CustomersDGV.SelectedRows[0].Cells[2].Value.ToString();
            CPhoneTb.Text = CustomersDGV.SelectedRows[0].Cells[3].Value.ToString();
            if (CNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(CustomersDGV.SelectedRows[0].Cells[1].Value.ToString());
            }
        }
/// <summary>
/// this is the edit button for customer ///////
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (CNameTb.Text == "" || CAddressTb.Text == "" || CPhoneTb.Text == "" || Key == 0 )
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    Reset();
                    SqlCommand cmd = new SqlCommand("Update into CustomerTbl set CustName=@CN,CustAd=@CA,CustPhone=@CP  where CustId=@CKey" , Con);
                    cmd.Parameters.AddWithValue("@CN", CNameTb.Text);
                    cmd.Parameters.AddWithValue("@CA", CAddressTb.Text);
                    cmd.Parameters.AddWithValue("@CP", CPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@CKey", Key);


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Updated");

                    Con.Close();
                    DisplayCust();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}


