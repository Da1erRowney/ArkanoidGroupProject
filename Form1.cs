namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "Я пидарас";
            int a = 0;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "Я пидарас, а не русский";
        }
    }
}