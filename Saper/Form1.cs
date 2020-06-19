using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //добавление меню игры
            MenuStrip menu = new MenuStrip();
            ToolStripMenuItem param = new ToolStripMenuItem();
            param.Text = "Параметры";
            param.Click += new EventHandler(param_Click);
            ToolStripMenuItem about = new ToolStripMenuItem();
            about.Text = "Об игре";
            about.Click += new EventHandler(about_Click);
            menu.Items.AddRange(new ToolStripItem[] { param, about});
            Controls.Add(menu);

            Load += new EventHandler(Form_Load);

            //добавляем поле игры
            gameBox = new PictureBox();
            gameBox.Size = new Size(Game.CountX * Game.ButtonWidthHeight, Game.CountY * Game.ButtonWidthHeight);
            gameBox.Location = Game.StartPointGame;
            gameBox.Paint += new PaintEventHandler(GameBox_Paint);
            gameBox.MouseClick += new MouseEventHandler(Mouse_Click);
            Controls.Add(gameBox);
        }

        //создаем поле игры
        public PictureBox gameBox;

        //старт игры
        private void Form_Load(object sender, EventArgs e)
        {
            Logic.StartGame();
        }

        //перерисовка
        private void GameBox_Paint(object sender, PaintEventArgs e)
        {
            //рисуем поле игры
            View.DrawGame(e.Graphics);
            //рисуем вид кнопок
            for (int y = 0; y < Game.CountY; y++)
            {
                for (int x = 0; x < Game.CountX; x++)
                {
                    //рисуем открытую непустую кнопку и ее значение
                    if (Data.ButtonStatus[x, y] == 1 && Data.ButtonValue[x, y] != "bomb" && Data.ButtonValue[x, y] != "0")
                    {
                        View.OpenButton(e.Graphics, x, y);
                    }
                    //рисуем открытую пустую кнопку 
                    else if (Data.ButtonStatus[x, y] == 1 && Data.ButtonValue[x, y] == "0")
                    {
                        View.NullButton(e.Graphics, x, y);
                    }
                    //рисуем бомбу
                    else if (Data.ButtonStatus[x, y] == 1 && Data.ButtonValue[x, y] == "bomb")
                    {
                        View.Bomb(e.Graphics, x, y);
                    }
                    //рисуем флаг
                    else if (Data.ButtonStatus[x, y] == 2)
                    {
                        View.Flag(e.Graphics, x, y);
                    }
                }
            }
        }

        //клик мышки
        private void Mouse_Click(object sender, MouseEventArgs e)
        {
            //по координатам клика (с коррекцией на начальную точку игры) получаем положение кнопки в массиве
            int x = e.X / Game.ButtonWidthHeight;
            int y = e.Y / Game.ButtonWidthHeight;

            //прямоугольник для последующего обновления области кнопки
            Rectangle rect_button = new Rectangle(x * Game.ButtonWidthHeight, y * Game.ButtonWidthHeight, Game.ButtonWidthHeight, Game.ButtonWidthHeight);
            //прямоугольник для последующего обновления всей области игры
            Rectangle rect_game = new Rectangle(0, 0, Game.CountX * Game.ButtonWidthHeight, Game.CountY * Game.ButtonWidthHeight);
            
            //если клик в игровое поле
            if (x < Game.CountX && y < Game.CountY)
            {
                //открытие кнопки (клик левой мыши)
                if (e.Button == MouseButtons.Left)
                {
                    //если кнопка еще не открыта 
                    if (Data.ButtonStatus[x, y] == 0)
                    {
                        Data.ButtonStatus[x, y] = 1;
                        //попал в бомбу - проиграл
                        if (Data.ButtonValue[x, y] == "bomb")
                        {
                            Logic.OpenTableButton();
                            gameBox.Invalidate(rect_game);
                            DialogResult result = MessageBox.Show("Вы попали в бомбу\n\nСыграть еще?", "Вы проиграли!", MessageBoxButtons.YesNo);
                            if (result == DialogResult.Yes)
                            {
                                Application.Restart();
                            }
                            else if (result == DialogResult.No)
                            {
                                Application.Exit();
                            }
                        }
                        //попал в пустую область - открытие области
                        else if (Data.ButtonValue[x, y] == "0")
                        {
                            Logic.GetNullArea(x, y);
                            gameBox.Invalidate(rect_game);
                        }
                        //открытие кнопки
                        else
                        {
                            gameBox.Invalidate(rect_button);
                        }
                    }
                }
                //флаг (клик правой мыши)
                else if (e.Button == MouseButtons.Right)
                {
                    //установка флага
                    if (Data.ButtonStatus[x, y] == 0)
                    {
                        Data.ButtonStatus[x, y] = 2;
                        gameBox.Invalidate(rect_button);
                    }
                    //снятие флага (повторное нажатие)
                    else if (Data.ButtonStatus[x, y] == 2)
                    {
                        Data.ButtonStatus[x, y] = 0;
                        gameBox.Invalidate(rect_button);
                    }
                }
                //проверка условия победы, остановка игры и вывод сообщения
                if (Logic.WinInGame())
                {
                    DialogResult result = MessageBox.Show("Поздравляем с победой\n\nСыграть еще?", "Вы выграли!", MessageBoxButtons.YesNo);
                    if(result == DialogResult.Yes)
                    {
                        Application.Restart();
                    }
                    else if(result == DialogResult.No)
                    {
                        Application.Exit();
                    }
                }
            }
        }

        //открытие новой формы при клике пункта меню "Параметры" 
        private void param_Click(object sender, EventArgs e)
        {
            Form f = new Form2();
            f.FormClosed += new FormClosedEventHandler(Form2_Close);
            f.Show();
        }

        //закрытие формы "Параметры", запуск рассчета обновленных параметров игры и обновление области
        private void Form2_Close(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Logic.StartGame();
                gameBox.Size = new Size(Game.CountX * Game.ButtonWidthHeight, Game.CountY * Game.ButtonWidthHeight);
                gameBox.Invalidate();
            }
        }

        //открытие новой формы при клике пункта меню "Об игре" 
        private void about_Click(object sender, EventArgs e)
        {
            Form f = new Form3();
            f.Show();
        }
    }
}
