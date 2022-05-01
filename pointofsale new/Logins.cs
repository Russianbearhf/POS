namespace pointofsale_new
{
    public partial class Logins : Form
    {
        public Logins()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Billings Obj = new Billings();
            Obj.Show();
            this.Hide();
        }
    }
}