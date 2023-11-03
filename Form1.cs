using SQLite;
using Recordes;
using System.Diagnostics.Eventing.Reader;
using Microsoft.VisualBasic.ApplicationServices;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public static string databasePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "record.db");
        public static string Nick;
        public bool statusAccount = false;
        private DatabaseServiceRecords _databaseService;

        public SQLiteConnection CreateDatabase(string databasePath) // Create BD
        {
            SQLiteConnection connection = new SQLiteConnection(databasePath);
            connection.CreateTable<Record>();
            return connection;
        }

        public Form1()
        {
            InitializeComponent();
            _databaseService = new DatabaseServiceRecords(databasePath);
            SQLiteConnection connection = CreateDatabase(databasePath);

            List<Record> records = _databaseService.GetAllRecord();
            checkedListBox1.Items.Clear();
            string itemText = "Страница Рекордов";
            checkedListBox1.Items.Add(itemText);
            foreach (Record record in records)
            {
                itemText = "Ник: " + record.NickName + " | Рекорд: " + record.RecordUser;
                checkedListBox1.Items.Add(itemText);
            }

        }

        private void button1_Click(object sender, EventArgs e) // Game
        {

            if (statusAccount == false)
            {
                MessageBox.Show("Для игры внесите пользователя в систему");
            }

            else if (statusAccount == true)
            {
                Form2 form2 = new Form2();
                this.Hide();
                form2.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e) //Exit
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) //Setting
        {
            Form3 form3 = new Form3();
            this.Hide();
            form3.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Nick = textBox1.Text;

            if (Nick == null)
            {

                MessageBox.Show("Вы не ввели свой Nick\nСверху справа поле для ввода");
            }

            else
            {
                statusAccount = true;
                var user = new Record
                {
                    NickName = Nick,
                    RecordUser = 0
                };
                MessageBox.Show("Пользователь внесен");
                _databaseService.InsertRecord(user);
                // Получение всех записей из базы данных
                List<Record> records = _databaseService.GetAllRecord();


                checkedListBox1.Items.Clear();
                string itemText = "Страница Рекордов";
                checkedListBox1.Items.Add(itemText);
                foreach (Record record in records)
                {
                    itemText = "Ник: " + record.NickName + " | Рекорд: " + record.RecordUser;
                    checkedListBox1.Items.Add(itemText);
                }
                _databaseService.CloseConnection();
            }

        }
    }
}