using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saper
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            Text = "Параметры";
            Width = 400;
            Height = 200;
            Load += new EventHandler(Form2_Load);
        }
        
        //параметры игры
        Label text_bomb;
        NumericUpDown bomb;
        Label text_horizont;
        NumericUpDown horizont;
        Label text_vertical;
        NumericUpDown vertical;
        Button restart;

        //отображение параметров игры
        private void Form2_Load(object sender, EventArgs e)
        {
            text_bomb = new Label
            {
                Text = "Количество бомб",
                Location = new Point(10, 10)
            };
            bomb = new NumericUpDown
            {
                Minimum = 1,
                Maximum = 100000,
                Value = Game.CountBomb,
                Location = new Point(250, 10)
            };
            text_horizont = new Label
            {
                Text = "Количество клеток по горизонтали",
                Width = 200,
                Location = new Point(10, 40)
            };
            horizont = new NumericUpDown
            {
                Minimum = 1,
                Maximum = 1000,
                Value = Game.CountX,
                Location = new Point(250, 40)
            };
            text_vertical = new Label
            {
                Text = "Количество клеток по вертикали",
                Width = 200,
                Location = new Point(10, 70)
            };
            vertical = new NumericUpDown
            {
                Minimum = 1,
                Maximum = 1000,
                Value = Game.CountY,
                Location = new Point(250, 70)
            };
            restart = new Button
            {
                Text = "Перезапустить",
                Width = 140,
                Location = new Point(130, 120)
            };
            restart.Click += new EventHandler(restart_Click);
            Controls.Add(text_bomb);
            Controls.Add(text_horizont);
            Controls.Add(text_vertical);
            Controls.Add(bomb);
            Controls.Add(horizont);
            Controls.Add(vertical);
            Controls.Add(restart);
        }

        //нажатие "Перезапустить"
        private void restart_Click(object sender, EventArgs e)
        {
            //проверка на превышение количества бомб над количеством кнопок
            if (bomb.Value > horizont.Value * vertical.Value)
            {
                MessageBox.Show("Количество бомб не может быть больше, чем количество клеток", "Ошибка!");
            }
            //проверка на минимальный порог бомб
            if (bomb.Value < horizont.Value * vertical.Value / 10)
            {
                MessageBox.Show("Количество бомб не должно быть меньше 10% от общего количества клеток", "Ошибка!");
                bomb.Value = horizont.Value * vertical.Value / 10;
            }
            //установка новых параметров игры
            else
            {
                Game.CountX = (int)horizont.Value;
                Game.CountY = (int)vertical.Value;
                Game.CountBomb = (int)bomb.Value;
                Close();
            }
        }
    }
}
