using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pointofsale_new
{
    public partial class Billings : Form
    {
        public Billings()
        {
            InitializeComponent();
            DisplayProducts();
            GetCustomer();
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            Logins Obj = new Logins();
            Obj.Show();
            this.Hide();
        }

        private SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\khodo\Documents\pos.mdf;Integrated Security=True;Connect Timeout=30");

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

        private void GetCustName()
        {
            Con.Open();
            string Query = "select * from CustomerTbl where CustId= " + CustIdCb.SelectedValue.ToString() + "";
            SqlCommand CMD = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(CMD);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CustNameTb.Text = dr["CuatName"].ToString();
            }

            Con.Close();
        }
        
        private void SearchProducts()
        {
            Con.Open();
            string Query = "Select * from ProductTbl where PName='" + SearchTb.Text + "'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ProductsDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SearchProducts();
            SearchTb.Text = "";
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            DisplayProducts();
            SearchTb.Text = "";
        }

        private void GetCustomer()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select CUstId from CustomerTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustId", typeof(int));
            dt.Load(rdr);
            CustIdCb.ValueMember = "CustId";
            CustIdCb.DataSource = dt;
            Con.Close();
        }
        private void Reset()
        {
            Pname = "";
            QtyTb.Text = "";
            Key = 0;
        }

        private void UpdateQty()

        {
            int newQty = PStock - Convert.ToInt32(QtyTb.Text);
            try
            {
                Con.Open();

                SqlCommand cmd = new SqlCommand("Update  ProductTbl set PQty=@PQ where PId=@PK", Con);
               
                cmd.Parameters.AddWithValue("@PQ", newQty);
                cmd.Parameters.AddWithValue("@PKey", Key);

                cmd.ExecuteNonQuery();
              //  MessageBox.Show("Product Saved");

                Con.Close();
                DisplayProducts();
               // Reset();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

         int Key = 0;
         string Pname;
         int Pprice, PStock;
         int n = 1, total = 0;

        private void AddToBill_Click(object sender, EventArgs e)

        {
            if (Key == 0)
            {
                MessageBox.Show("Select A Product");
            }
            else if (QtyTb.Text == "")
            {
                MessageBox.Show("Enter The Quantity");

            }
            else
            {
                int Subtotal = Convert.ToInt32(QtyTb.Text) * Pprice;
                total = total + Subtotal;
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                newRow.Cells[0].Value = n;
                newRow.Cells[1].Value = Pname;
                newRow.Cells[2].Value = QtyTb.Text;
                newRow.Cells[3].Value = Pprice;
                newRow.Cells[4].Value = Subtotal;
                BillDGV.Rows.Add(newRow);
                n++;
                SubTotalTb.Text = "" + total;
                UpdateQty();   
                Reset();





            }
        
    }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void VATTb_TextChanged(object sender, EventArgs e)
        {
        }

        private void VATTb_MouseLeave(object sender, EventArgs e)
        {

        }

        private void VATTb_KeyDown(object sender, KeyEventArgs e)
        {
           

        }

        private void VATTb_KeyUp(object sender, KeyEventArgs e)
        {
            if (VATTb.Text == "")
            {

            }else if (SubTotalTb.Text == "")
            {
                MessageBox.Show("Add Products ToCard ");
                VATTb.Text = "";
            }
            else
            {
                try
                {
                    double VAT = (Convert.ToDouble(VATTb.Text) / 100) * Convert.ToInt32(SubTotalTb.Text);
                    TotTaxTb.Text = "" + VAT;
                    GrdTotalTb.Text = ""+ (Convert.ToInt32(SubTotalTb.Text) + Convert.ToDouble(TotTaxTb.Text));
                }
                catch (Exception Exception)
                {
                    MessageBox.Show(Exception.Message);
                }

            }
        }

        private void InsertBill()
        {
            if (CustIdCb.SelectedIndex == -1 | PaymentMtdCb.SelectedIndex == -1 || GrdTotalTb.Text == "" )
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    Reset();
                    SqlCommand cmd = new SqlCommand("insert into BillTbl(BDate,CustId,CustName,PMethod,Amt)values(@BD,@CI,@CN,@PM,@Am)", Con);
                    cmd.Parameters.AddWithValue("@BD", BDate.Value.Date);
                    cmd.Parameters.AddWithValue("@CI", CustIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@CN", CustNameTb.Text);
                    cmd.Parameters.AddWithValue("@PM", PaymentMtdCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Am", Convert.ToDouble(GrdTotalTb.Text));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bill Saved");

                    Con.Close();
                  //  Reset();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int flag = 0;

        private void EditBtn_Click(object sender, EventArgs e)
        {
            InsertBill();
            if (flag == 1)
            {
                printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 285, 600);
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();

                }
            }
        }


        private void textBox4_KeyUp(object sender, KeyEventArgs e)
        {
            if (DiscountTb.Text == "")
            {

            }
            else if (SubTotalTb.Text == "")
            {
                MessageBox.Show("Add Products ToCard ");
                DiscountTb.Text = "";
            }
            else
            {
                try
                {
                    double Disc = (Convert.ToDouble(DiscountTb.Text) / 100) * Convert.ToInt32(SubTotalTb.Text);
                    DiscTotTb.Text = "" + Disc;
                    GrdTotalTb.Text = "" + (Convert.ToInt32(SubTotalTb.Text) + Convert.ToDouble(TotTaxTb.Text)-Convert.ToDouble(DiscTotTb.Text));
                }
                catch (Exception Exception)
                {
                    MessageBox.Show(Exception.Message);
                }

            }
        }

        private void PrintBtn_Click(object sender, EventArgs e)
        {
            InsertBill();
        }






        int prodid, prodqty, prodprice, tottal, pos = 60;

        private void BillDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            ViewBills Obj = new ViewBills();
            Obj.Show();
        }

        string prodname;

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Khodor POS", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Red, new Point(80));
            e.Graphics.DrawString("ID PRODUCT PRICE QUANTITY TOTAL", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Red, new Point(26, 40));
            foreach (DataGridView row in BillDGV.Rows)
            {









                prodid = Convert.ToInt32(row.Cells["Column1"].Value);
                prodname = "" + row.Cells["Column2"].Value;
                prodprice = Convert.ToInt32(row.Cells[" Column3 "].Value);
                prodqty = Convert.ToInt32(row.Cells["Column4"].Value);
                tottal = Convert.ToInt32(row.Cells["Column5"].Value);
                e.Graphics.DrawString("" + prodid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26, pos));
                e.Graphics.DrawString("" + prodname, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                e.Graphics.DrawString("" + prodprice, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                e.Graphics.DrawString("" + prodqty, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                e.Graphics.DrawString("" + tottal, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(235, pos));
                pos = pos + 20;
            }
            e.Graphics.DrawString("Grand Total: Rs" + GrdTotalTb.Text, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Crimson, new Point(50, pos + +50));
            e.Graphics.DrawString("* ****** Khodor,pos *******", new Font ("Century Gothic ", 12, FontStyle.Bold), Brushes.Crimson, new Point(10, pos + 85));
            BillDGV.Rows.Clear();
            BillDGV.Refresh();
            pos = 100;
        }

        private void CustIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetCustName();
        }
        

        private void ProductsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Pname = ProductsDGV.SelectedRows[0].Cells[1].Value.ToString();
            //PCatCb.SelectedItem = ProductsDGV.SelectedRows[0].Cells[2].Value.ToString();
            Pprice = (int)Convert.ToUInt32(ProductsDGV.SelectedRows[0].Cells[3].Value.ToString());
            PStock = (int)Convert.ToUInt32(ProductsDGV.SelectedRows[0].Cells[4].Value.ToString());
            if (Pname == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ProductsDGV.SelectedRows[0].Cells[1].Value.ToString());
            }
        }
    }
}