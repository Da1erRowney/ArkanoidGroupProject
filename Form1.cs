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
            checkedListBox1.ItemCheck += checkedListBox1_ItemCheck;
            List<Record> records = _databaseService.GetAllRecord();
            checkedListBox1.Items.Clear();
            string itemText = "�������� ��������";
            checkedListBox1.Items.Add(itemText);
            foreach (Record record in records)
            {
                itemText = "���: " + record.NickName + " | ������: " + record.RecordUser;
                checkedListBox1.Items.Add(itemText);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string inputNick = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(inputNick))
            {
                MessageBox.Show("�� �� ����� ���");
                return;
            }

            // ��������� ������� ���� � ���� ������
            Record existingRecord = _databaseService.GetNickName(inputNick);
            if (existingRecord != null)
            {
                Nick = textBox1.Text;
                Form2 form2 = new Form2();
                this.Hide();
                form2.Show();
            }
            else
            {
                MessageBox.Show("��� �� ���������� � ���� ������");
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
        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                // �������� ��������� �����
                string selectedItem = checkedListBox1.Items[e.Index].ToString();

                // ��������� ��� ������������ �� ������
                string[] parts = selectedItem.Split('|');
                string nick = parts[0].Trim().Substring(5);

                // ����������� ��� ������������ � textBox1
                textBox1.Text = nick;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            Nick = textBox1.Text;
            if (string.IsNullOrEmpty(Nick))
            {
                MessageBox.Show("�� �� ����� ���� Nick\n������ ������ ���� ��� �����");
            }
            else
            {

                if (_databaseService.GetNickName(Nick) != null)
                {
                    MessageBox.Show("��� ��� ����������. ����������, �������� ������ ���.");
                    return;
                }

               
                var user = new Record
                {
                    NickName = Nick,
                    RecordUser = 0
                };
                MessageBox.Show("������������ ������");
                _databaseService.InsertRecord(user);

                List<Record> records = _databaseService.GetAllRecord();

                checkedListBox1.Items.Clear();
                string itemText = "�������� ��������";
                checkedListBox1.Items.Add(itemText);
                foreach (Record record in records)
                {
                    itemText = "���: " + record.NickName + " | ������: " + record.RecordUser;
                    checkedListBox1.Items.Add(itemText);
                }
              
                textBox1.Text="";
            }
        }
    }
}