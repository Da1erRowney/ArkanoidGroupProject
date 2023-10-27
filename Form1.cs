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

            // Отображаем новую форму
            form2.Show();

            // Закрываем текущую форму (Form1), если необходимо
            //this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}