using System.Data.SqlClient;

namespace pointofsale_new
{
    public partial class AddProducts : Form
    {
        public AddProducts()
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
            PnameTb.Text = "";
            QtyTb.Text = "";
            PriceTb.Text = "";
            PCatCb.SelectedIndex = -1;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (PnameTb.Text == "" | PCatCb.SelectedIndex == -1 || PriceTb.Text == "" || QtyTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    Reset();
                    SqlCommand cmd = new SqlCommand("insert into ProductTbl(PName,PCat,Pprice,PQty)values(@PN,@PC,@PP,@PQ)", Con);
                    cmd.Parameters.AddWithValue("@PN", PnameTb.Text);
                    cmd.Parameters.AddWithValue("@PC", PCatCb.SelectedItem?.ToString());
                    cmd.Parameters.AddWithValue("@PP", PriceTb.Text);
                    cmd.Parameters.AddWithValue("@PQ", QtyTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Saved");

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

        private void AddProducts_Load(object sender, EventArgs e)
        {
        }
    }
}