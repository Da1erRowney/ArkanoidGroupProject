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
        private Rectangle playerRectangle;
        private Point playerSpeed;
        private Point ballPosition;
        private Point ballSpeed;
        private int ballRadius;
        private List<Rectangle> rectangles;
        private List<Brush> rectangleBrushes;
        private int rowCount; // Добавлено объявление переменной rowCount
        private int rowLength; // Добавлено объявление переменной rowLength
        private int rectangleWidth; // Добавлено объявление переменной rectangleWidth
        private int rectangleHeight; // Добавлено объявление переменной rectangleHeight
        private int spacing; // Добавлено объявление переменной spacing
        private int startX; // Добавлено объявление переменной startX
        private int startY; // Добавлено объявление переменной startY
        private int gameAreaX; // Добавлено объявление переменной gameAreaX

        public Form2()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            Size = new Size(1000, 900);
            StartPosition = FormStartPosition.CenterScreen;
            bool isGameOver = false;


            int gameAreaWidth = 600;
            int gameAreaHeight = 500;
            gameAreaX = (ClientSize.Width - gameAreaWidth) / 2;
            int gameAreaY = (ClientSize.Height - gameAreaHeight) / 2;

            int playerX = gameAreaX + (gameAreaWidth - 100) / 2;
            int playerY = gameAreaY + gameAreaHeight - 100;
            int ballX = gameAreaX + gameAreaWidth / 2;
            int ballY = playerY - 15;

            playerRectangle = new Rectangle(playerX, playerY, 100, 20);
            playerSpeed = new Point(15, 0);

            ballRadius = 10;
            ballPosition = new Point(ballX, ballY);
            ballSpeed = new Point(6, -6);

            rectangles = new List<Rectangle>();
            rectangleBrushes = new List<Brush>();

            rowCount = 3;
            rowLength = 5;
            rectangleWidth = 80;
            rectangleHeight = 60;
            spacing = 5;
            startX = gameAreaX + (gameAreaWidth - (rowLength * rectangleWidth + (rowLength - 1) * spacing)) / 2;
            startY = gameAreaY + 20;

            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < rowLength; col++)
                {
                    int x = startX + (rectangleWidth + spacing) * col;
                    int y = startY + (rectangleHeight + spacing) * row;

                    Rectangle newRectangle = new Rectangle(x, y, rectangleWidth, rectangleHeight);
                    Brush newBrush = Brushes.Red;

                    rectangles.Add(newRectangle);
                    rectangleBrushes.Add(newBrush);
                }
            }

            DoubleBuffered = true;
            KeyPreview = true;
            KeyDown += Form2_KeyDown;

            Timer timer = new Timer();
            timer.Interval = 20;
            timer.Tick += Timer_Tick;
            timer.Start();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Close();
            form1.Show();

        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            int gameAreaWidth = 600; // Объявляем и инициализируем переменную gameAreaWidth

            if (e.KeyCode == Keys.A)
            {
                if (playerRectangle.X - playerSpeed.X >= gameAreaX)
                {
                    playerRectangle.X -= playerSpeed.X;
                }
            }
            else if (e.KeyCode == Keys.D)
            {
                if (playerRectangle.X + playerSpeed.X + playerRectangle.Width <= gameAreaX + gameAreaWidth)
                {
                    playerRectangle.X += playerSpeed.X;
                }
            }
            else if (e.KeyCode == Keys.N)
            {
                Rectangle newRectangle = new Rectangle(150, 300, 80, 60);
                Brush newBrush = Brushes.Green;

                rectangles.Add(newRectangle);
                rectangleBrushes.Add(newBrush);
            }
        }

        private void ResetGame()
        {
            rectangles.Clear();
            rectangleBrushes.Clear();

            // Добавляем прямоугольники в список
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < rowLength; col++)
                {
                    int x = startX + (rectangleWidth + spacing) * col;
                    int y = startY + (rectangleHeight + spacing) * row;

                    Rectangle newRectangle = new Rectangle(x, y, rectangleWidth, rectangleHeight);
                    Brush newBrush = Brushes.Red;

                    rectangles.Add(newRectangle);
                    rectangleBrushes.Add(newBrush);
                }
            }

            ballPosition = new Point(playerRectangle.X + playerRectangle.Width / 2, playerRectangle.Y - ballRadius - 5); // Изменяем начальную позицию шарика
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            ballPosition.X += ballSpeed.X;
            ballPosition.Y += ballSpeed.Y;

            // Проверяем столкновение шарика с игроком
            if (ballPosition.Y + ballRadius >= playerRectangle.Y && ballPosition.Y - ballRadius <= playerRectangle.Y + playerRectangle.Height)
            {
                if (ballPosition.X + ballRadius >= playerRectangle.X && ballPosition.X - ballRadius <= playerRectangle.X + playerRectangle.Width)
                {
                    ballSpeed.Y = -ballSpeed.Y;
                }
            }

            // Проверяем столкновение шарика с прямоугольниками
            for (int i = 0; i < rectangles.Count; i++)
            {
                Rectangle rectangle = rectangles[i];
                if (ballPosition.Y + ballRadius >= rectangle.Y && ballPosition.Y - ballRadius <= rectangle.Y + rectangle.Height)
                {
                    if (ballPosition.X + ballRadius >= rectangle.X && ballPosition.X - ballRadius <= rectangle.X + rectangle.Width)
                    {
                        rectangles.RemoveAt(i);
                        rectangleBrushes.RemoveAt(i);

                        // Определяем, с какой стороны шарик столкнулся с прямоугольником
                        bool collidedFromLeft = ballPosition.X + ballRadius <= rectangle.X && ballSpeed.X > 0;
                        bool collidedFromRight = ballPosition.X - ballRadius >= rectangle.X + rectangle.Width && ballSpeed.X < 0;
                        bool collidedFromBottom = ballPosition.Y - ballRadius <= rectangle.Y + rectangle.Height && ballSpeed.Y < 0;
                        bool collidedFromTop = ballPosition.Y + ballRadius >= rectangle.Y && ballSpeed.Y > 0;

                        if (collidedFromLeft || collidedFromRight)
                        {
                            // Меняем только горизонтальное направление движения
                            ballSpeed.X = -ballSpeed.X;
                        }
                        else if (collidedFromBottom || collidedFromTop)
                        {
                            // Меняем только вертикальное направление движения
                            ballSpeed.Y = -ballSpeed.Y;
                        }
                        else
                        {
                            // Меняем оба направления при столкновении с вертикальными сторонами прямоугольника
                            ballSpeed.X = -ballSpeed.X;
                            ballSpeed.Y = -ballSpeed.Y;
                        }

                        break;
                    }
                }
            }

            // Проверяем столкновение шарика с границами окна
            if (ballPosition.X + ballRadius >= ClientSize.Width || ballPosition.X - ballRadius <= 0)
            {
                ballSpeed.X = -ballSpeed.X;
            }
            if (ballPosition.Y - ballRadius <= 0)
            {
                ballSpeed.Y = -ballSpeed.Y;
            }
            if (ballPosition.Y + ballRadius >= ClientSize.Height)
            {
                ResetGame();
            }

            Invalidate();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;


            // Рисуем фон области игры
            int gameAreaWidth = 600;
            int gameAreaHeight = 500;
            int gameAreaX = (ClientSize.Width - gameAreaWidth) / 2;
            int gameAreaY = (ClientSize.Height - gameAreaHeight) / 2;
            g.FillRectangle(Brushes.White, gameAreaX, gameAreaY, gameAreaWidth, gameAreaHeight);

            // Рисуем игрока
            g.FillRectangle(Brushes.Blue, playerRectangle.X - gameAreaX, playerRectangle.Y - gameAreaY, playerRectangle.Width, playerRectangle.Height);

            // Рисуем шарик
            g.FillEllipse(Brushes.Black, ballPosition.X - ballRadius - gameAreaX, ballPosition.Y - ballRadius - gameAreaY, ballRadius * 2, ballRadius * 2);

            // Рисуем прямоугольники
            for (int i = 0; i < rectangles.Count; i++)
            {
                Rectangle rectangle = rectangles[i];
                g.FillRectangle(rectangleBrushes[i], rectangle.X - gameAreaX, rectangle.Y - gameAreaY, rectangle.Width, rectangle.Height);
            }

            base.OnPaint(e);
        }
    }
}
