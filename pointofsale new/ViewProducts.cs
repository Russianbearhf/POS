using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pointofsale_new
{
    public partial class ViewProducts : Form
    {
        public ViewProducts()
        {
            InitializeComponent();
            DisplayProducts();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            MainMenu Obj = new MainMenu();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

       SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\khodo\Documents\pos.mdf;Integrated Security=True;Connect Timeout=30");

        private void DisplayProducts()
        {
            Con.Open();
            string Query = "Select * from ProductTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select The Product");
            }
            else
            {
                try
                {
                    Con.Open();

                    SqlCommand cmd = new SqlCommand("delete from ProductTbl where PId=@PKey", Con);
                    cmd.Parameters.AddWithValue("@PKey", Key);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Product Deleted");

                    Con.Close();
                    DisplayProducts();
                    /////// Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private int Key = 0;

        private void ProductsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PnameTb.Text = ProductsDGV.SelectedRows[0].Cells[1].Value.ToString();
            PCatCb.SelectedItem = ProductsDGV.SelectedRows[0].Cells[2].Value.ToString();
            PriceTb.Text = ProductsDGV.SelectedRows[0].Cells[3].Value.ToString();
            QtyTb.Text = ProductsDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (PnameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ProductsDGV.SelectedRows[0].Cells[1].Value.ToString());
            }
        }

        private void Reset()
        {
            PnameTb.Text = "";
            QtyTb.Text = "";
            PriceTb.Text = "";
            PCatCb.SelectedIndex = -1;
            Key = 0;
        }

        private void button2_Click(object sender, EventArgs e)
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

                    SqlCommand cmd = new SqlCommand("Update  ProductTbl set Pname=@PN,PCat=@PC,Pprice=@PP,PQty=@PQ where PId=@PK", Con);
                    cmd.Parameters.AddWithValue("@PN", PnameTb.Text);
                    cmd.Parameters.AddWithValue("@PC", PCatCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PP", PriceTb.Text);
                    cmd.Parameters.AddWithValue("@PQ", QtyTb.Text);
                    cmd.Parameters.AddWithValue("@PKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Saved");

                    Con.Close();
                    DisplayProducts();
                    Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}