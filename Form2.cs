using Recordes;
using SQLite;
using System;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
        public int level = 0;
        public static bool statusGame = true; 
        public string nickUser = Form1.Nick;
        public string databasePath = Form1.databasePath;
        private DatabaseServiceRecords _databaseService;
        private Record _selectedData;
        private PictureBox paddle; // Платформа
        private PictureBox ball; // Шарик
        private PictureBox[] blocks; // Блоки
        private System.Windows.Forms.Timer gameTimer;
        private int countRecord = 0;
        private int ballSpeedX = 4; // Скорость шарика по горизонтали
        private int ballSpeedY = 4; // Скорость шарика по вертикали
       Form4 form4 = new Form4(statusGame);
        public Form2()
        {
            InitializeComponent();
            if (Form1.statusMusic == true)
            {
                Form1.sound_main.PlayLooping();
                Form1.sound_main.Play();
            }
            
            level = 1;
            InitializeGame();
            _databaseService = new DatabaseServiceRecords(databasePath);
            _selectedData = _databaseService.GetNickName(nickUser);
        }

        private void InitializeGame()
        {
            if (level == 1)
            {
                // Создаем платформу
                paddle = new PictureBox
                {
                    Image = Properties.Resources.platform, // Замените на ваше изображение платформы
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(100, 40),
                    Location = new Point((ClientSize.Width - 100) / 2, ClientSize.Height - 90),
                    BackColor = Color.Transparent
                };
                Controls.Add(paddle);
                paddle.BringToFront();

                // Создаем шарик
                ball = new PictureBox
                {
                    Image = Properties.Resources.ball, // Замените на ваше изображение шарика
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(30, 30),
                    Location = new Point((ClientSize.Width - 30) / 2, paddle.Top - 30),
                    BackColor = Color.Transparent
                };
                Controls.Add(ball);
                ball.BringToFront();

                // Создаем блоки
                blocks = new PictureBox[30];
                int blockIndex = 0;
                int initialTop = 50;
                int initialLeft = 175;
                int blockWidth = 70;
                int blockHeight = 30;

                for (int row = 0; row < 3; row++)
                {
                    for (int column = 0; column < 10; column++)
                    {
                        blocks[blockIndex] = new PictureBox
                        {
                            Image = Properties.Resources.block2, // Замените на ваше изображение блока
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            Size = new Size(blockWidth, blockHeight),
                            Location = new Point(initialLeft + column * (blockWidth + 10), initialTop + row * (blockHeight + 10)),
                            BackColor = Color.Transparent
                        };
                        Controls.Add(blocks[blockIndex]);
                        blocks[blockIndex].BringToFront();
                        blockIndex++;
                    }
                }

                // Создаем таймер для обновления игровых состояний
                gameTimer = new System.Windows.Forms.Timer { Interval = 1 };
                gameTimer.Tick += GameTimer_Tick;
                gameTimer.Start();
            }
            //Создаем блоки

            if (level == 2)
            {
                blocks = new PictureBox[30];
                int blockIndex = 0;
                int initialTop = 50;
                int initialLeft = 175;
                int blockWidth = 70;
                int blockHeight = 30;

                for (int row = 0; row < 3; row++)
                {
                    for (int column = 0; column < 10; column++)
                    {
                        blocks[blockIndex] = new PictureBox
                        {
                            Image = Properties.Resources.block2, // Замените на ваше изображение блока
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            Size = new Size(blockWidth, blockHeight),
                            Location = new Point(initialLeft + column * (blockWidth + 10), initialTop + row * (blockHeight + 10)),
                            BackColor = Color.Transparent
                        };
                        Controls.Add(blocks[blockIndex]);
                        blocks[blockIndex].BringToFront();
                        blockIndex++;
                    }
                }

                // Создаем таймер для обновления игровых состояний
                gameTimer = new System.Windows.Forms.Timer { Interval = 1 };
                gameTimer.Tick += GameTimer_Tick;
                gameTimer.Start();
            }

        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            // Двигаем шарик
            ball.Left += ballSpeedX;
            ball.Top += ballSpeedY;

            // Проверяем столкновение шарика с платформой
            if (ball.Bounds.IntersectsWith(paddle.Bounds))
            {
                ballSpeedY = -ballSpeedY;
            }

            // Проверяем столкновение шарика с блоками
            foreach (var block in blocks)
            {
                if (block.Visible && ball.Bounds.IntersectsWith(block.Bounds))
                {

                    block.Visible = false;
                    ballSpeedY = -ballSpeedY;
                    countRecord += 200;
                    label1.Text = "Игрок " + Form1.Nick + " || Ваш Счёт: " + countRecord;
                    if (countRecord == 6000 && level == 1)
                    {

                        for (int i = 1; i <= 49; i++)
                        {
                            string pictureBoxName = "pictureBox" + i;
                            PictureBox pictureBox = Controls.Find(pictureBoxName, true).FirstOrDefault() as PictureBox;

                            if (pictureBox != null)
                            {
                                pictureBox.Image = Properties.Resources.kaneki;
                            }
                        }

                        level = 2;
                        ballSpeedX += 2;
                        ballSpeedY += 2;
                        //ball.Left += ballSpeedX;
                        //ball.Top += ballSpeedY;

                        InitializeGame();
                    }
                    if (countRecord == 12000 && level == 2)
                    {
                        level = 2;
                        ballSpeedX += 1;
                        ballSpeedY += 1;
                        //ball.Left += ballSpeedX;
                        //ball.Top += ballSpeedY;

                        InitializeGame();
                    }
                    if (countRecord == 18000 && level == 2)
                    {
                        level = 2;
                        InitializeGame();
                    }
                    if (countRecord == 24000 && level == 2)
                    {
                        level = 2;
                        InitializeGame();
                    }
                    if (countRecord == 30000 && level == 2)
                    {
                        level = 2;
                        InitializeGame();
                    }
                    if (countRecord == 36000 && level == 2)
                    {
                        level = 2;
                        InitializeGame();
                    }
                    // Дополнительные действия при попадании шариком в блок
                }
            }

            // Проверяем столкновение шарика со стенками
            if (ball.Left <= 0 || ball.Right >= ClientSize.Width)
            {
                ballSpeedX = -ballSpeedX;
            }
            if (ball.Top <= 0)
            {
                ballSpeedY = -ballSpeedY;
            }

            // Проверяем, завершилась ли игра
            if (ball.Top >= ClientSize.Height) ////////////СДЕЛАТЬ НОРМАЛЬНОЕ ВОЗВРАЩЕНИЕ НАЗАД С ВЫБОРОМ ПРОДОЛЖИТЬ ИЛИ ВЫЙТИ!!!!
            {
                gameTimer.Stop();
                _selectedData.RecordUser = countRecord;
                _databaseService.UpdateRecord(_selectedData);
                gameTimer.Enabled = false;
                MessageBox.Show("Игра окончена!");
                ClearFiled();

                countRecord = 0;
                //////////////////////// ВСТАВИЛ ЭТО ПОКА ДЛЯ ВОЗВРАТА НА ГЛАВ МЕНЮ ПОСЛЕ ПОРАЖЕНИЯ
                statusGame = false;
                form4.Close();
                
                Form1 form1 = new Form1();
                this.Close();
                form1.Show();
                form1.statusAccount = true;
                form1.textBox1.Text = nickUser;
                ////////////////////////////
                //InitializeGame(); ПОКА ЗАКОММЕНТИРОВАЛ ЭТО
            }
        }
        private void ClearFiled()
        {
            Controls.Remove(paddle);
            Controls.Remove(ball);
            foreach (PictureBox block in blocks)
            {
                Controls.Remove(block);
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            paddle.Left = e.X - paddle.Width / 2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gameTimer.Enabled = false;
            _selectedData.RecordUser = countRecord;
            _databaseService.UpdateRecord(_selectedData);
            MessageBox.Show("Игра окончена!");
            statusGame = false;
            countRecord = 0;
            form4.Close();

            Form1 form1 = new Form1();
            this.Close();
            form1.Show();
            
            form1.statusAccount = true;
            form1.textBox1.Text = nickUser;
        }
    }
}