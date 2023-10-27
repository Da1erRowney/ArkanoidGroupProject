namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //fdf
            Form3 form3 = new Form3();
            this.Hide();
            form3.Show();
        }
    }
}