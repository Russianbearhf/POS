namespace pointofsale_new
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
            AddProducts Obj = new AddProducts();
            Obj.Show();
            Obj.TopMost = true;
        }

        private void label6_Click(object sender, EventArgs e)
        { Addsuppliers Obj = new Addsuppliers();
             Obj.Show();
               Obj.TopMost = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            AddCustomer Obj = new AddCustomer();
            Obj.Show();
            Obj.TopMost = true;
        }

        private void label5_Click(object sender, EventArgs e)
        {
            ViewCustomers Obj = new ViewCustomers();
            Obj.Show();
            Obj.TopMost = true;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            ViewSuppliers Obj = new ViewSuppliers();
            Obj.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            ViewProducts obj = new ViewProducts();
            obj.Show();
            this.Hide();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            loopTimer.Enabled = true;
        }

        private int secondsActive = 1;

        private void loopTimer_Tick(object sender, EventArgs e)
        {
            secondsActive++;
            if (secondsActive % 3600 == 1)
            {
                MessageBox.Show("It's been 1 hour. Are you still here?");
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }
    }
}