using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;

namespace WinFormsApp1
{
    public partial class Form3 : Form
    {
        Form1 form1;
        private ResourceManager resourceManager;
        public Form3()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = new Bitmap(pictureBox1.Image);
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            form1 = new Form1();

            resourceManager = new ResourceManager("WinFormsApp1.Properties.Resources", typeof(Form1).Assembly);

            if (button2.Text == "Светлая тема")
            {
                //Загрузка светлой темы в зависимости что написано на кнопке
                Image image = (Image)resourceManager.GetObject("15 Twerking Gifs That Didn't Quite Twerk It"); //Загрузка из реурсов gif изображение по названию (что бы изменить картинку нужно изменить название в ковычках)
                pictureBox1.Image = image;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Size = new Size(1000, 600); // Размеры формы для картинки
            }
            else
            {
                //Загрузка темной темы в зависимости что написано на кнопке
                Image image = (Image)resourceManager.GetObject("998e055aba57c24138220937cc5166ab");
                pictureBox1.Image = image;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Size = new Size(1000, 600); // Размеры формы для картинки

                //я ебал git

            }
        }// что за хуйня

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Close();
            form1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Темная тема")
            {
                button2.Text = "Светлая тема";
                //Загрузка светлой темы в зависимости что написано на кнопке
                Image image = (Image)resourceManager.GetObject("15 Twerking Gifs That Didn't Quite Twerk It"); //Загрузка из реурсов gif изображение по названию (что бы изменить картинку нужно изменить название в ковычках)
                pictureBox1.Image = image;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Size = new Size(1000, 600); // Размеры формы для картинки
            }
            else
            {
                button2.Text = "Темная тема";
                //Загрузка темной темы в зависимости что написано на кнопке
                Image image = (Image)resourceManager.GetObject("998e055aba57c24138220937cc5166ab");
                pictureBox1.Image = image;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Size = new Size(1000, 600); // Размеры формы для картинки
            }
        }
    }
}
