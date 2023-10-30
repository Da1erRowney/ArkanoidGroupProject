using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using Timer = System.Windows.Forms.Timer;


namespace WinFormsApp1
{
    public partial class Form2 : Form
    {
        private PictureBox paddle; // Платформа
        private PictureBox ball; // Шарик
        private PictureBox[] blocks; // Блоки
        private Timer gameTimer;
        private int ballSpeedX = 4; // Скорость шарика по горизонтали
        private int ballSpeedY = 4; // Скорость шарика по вертикали
        public Form2()
        {
            InitializeComponent();
            InitializeGame();
        }
        private void InitializeGame()
        {
            // Создаем платформу

            paddle = new PictureBox();
            paddle.Image = Image.FromFile("../../../Resources/platform.png");
            //paddle.Image = Properties.Resources.platform; // Замените "platform.png" на имя вашего изображения платформы в папке "Resources"
            paddle.SizeMode = PictureBoxSizeMode.StretchImage;
            paddle.Size = new Size(100, 40);
            paddle.Location = new Point((ClientSize.Width - paddle.Width) / 2, ClientSize.Height - paddle.Height - 50);
            // Устанавливаем прозрачный фон
            paddle.BackColor = Color.Transparent;
            Controls.Add(paddle);
            paddle.BringToFront();

            // Создаем шарик

            ball = new PictureBox();
            ball.Image = Image.FromFile("../../../Resources/ball.png"); // Замените "ball.png" на путь к вашей картинке шарика
            ball.SizeMode = PictureBoxSizeMode.StretchImage;
            ball.Size = new Size(30, 30);
            ball.Location = new Point((ClientSize.Width - ball.Width) / 2, paddle.Top - ball.Height);
            ball.BackColor = Color.Transparent; // Устанавливаем прозрачный фон
            Controls.Add(ball);
            ball.BringToFront();

            // Создаем блоки
            blocks = new PictureBox[30];
            int blockIndex = 0;
            int initialTop = 50;
            int initialLeft = 30;
            int blockWidth = 70;
            int blockHeight = 30;
            for (int row = 0; row < 3; row++)
            {
                for (int column = 0; column < 10; column++)
                {
                    blocks[blockIndex] = new PictureBox();
                    blocks[blockIndex].Image = Image.FromFile("../../../Resources/block2.png"); // Замените "block.png" на путь к вашей картинке блока
                    blocks[blockIndex].SizeMode = PictureBoxSizeMode.StretchImage;
                    blocks[blockIndex].Size = new Size(blockWidth, blockHeight);
                    blocks[blockIndex].Location = new Point(initialLeft + column * (blockWidth + 10),
                        initialTop + row * (blockHeight + 10));
                    blocks[blockIndex].BackColor = Color.Transparent; // Устанавливаем прозрачный фон
                    Controls.Add(blocks[blockIndex]);
                    blocks[blockIndex].BringToFront();
                    blockIndex++;
                }
            }


            // Создаем таймер для обновления игровых состояний
            gameTimer = new Timer();
            gameTimer.Interval = 1;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
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
            for (int i = 0; i < blocks.Length; i++)
            {
                if (ball.Bounds.IntersectsWith(blocks[i].Bounds) && blocks[i].Visible)
                {
                    blocks[i].Visible = false;
                    ballSpeedY = -ballSpeedY;
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
            if (ball.Top >= ClientSize.Height)
            {
                gameTimer.Stop();
                MessageBox.Show("Игра окончена!");
                Close();
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            // Перемещение платформы за курсором мыши
            if (e.X >= paddle.Width / 2 && e.X <= ClientSize.Width - paddle.Width / 2)
            {
                paddle.Left = e.X - paddle.Width / 2;
            }

            base.OnMouseMove(e);
        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Close();
            form1.Show();

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X >= paddle.Width / 2 && e.X <= ClientSize.Width - paddle.Width / 2)
            {
                paddle.Left = e.X - paddle.Width / 2;
            }

            base.OnMouseMove(e);
        }
    }
}
