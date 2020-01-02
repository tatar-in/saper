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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int coordinateX; //начальная позиция таблица кнопок по оси Х
        int coordinateY = 100; //начальная позиция таблица кнопок по оси У
        int buttonInWidth; //количество кнопок по Х
        int buttonInHeigth; //количество кнопок по У
        int bombInGame; //количество бомб в игре
        Button[,] allButtons;//массив кнопок
        private void CreateButtonInTable(int j, int i, int coordinateX, int coordinateY)
        {
            
            Button button = new Button();
            button.Location = new Point(coordinateX, coordinateY);
            button.Size = new Size(30, 30);
            //button.BackColor = Color.White;
            //button.ForeColor = Color.White;
            //button.Name = j + "-" + i;
            //button.FlatStyle = FlatStyle.Standard;
            //button.Font = new Font("Arial", 15F, FontStyle.Bold);
            Controls.Add(button);
            button.Click += new EventHandler(Button_Click);
            //coordinateX += 30;
            allButtons[j, i] = button;
            
        }
        private void SetBomb(Button button)//установка бомбы
        {
            //button.Text = "A";
            button.BackgroundImage = Image.FromFile(@"C:\Users\User\source\repos\Saper\Saper\Resources\picture_bomb.jpg", false);
            button.BackgroundImageLayout = ImageLayout.Zoom;
            button.Tag = "bomb";
            
        }
        private bool IsBomb(Button button)
        {
            if(button.Tag == "bomb")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            if (buttonStartGame.Text == "Сброс")
            {
                buttonStartGame.Text = "Старт";
                Application.Restart();
            }
            else
            {
                buttonInWidth = (int)countButtonX.Value;
                countButtonX.Enabled = false;
                buttonInHeigth = (int)countButtonY.Value;
                countButtonY.Enabled = false;
                bombInGame = (int)countMine.Value;
                countMine.Enabled = false;
                timerOfGame.Enabled = false;
                buttonStartGame.Text = "Сброс";
                if (buttonInWidth * 30 + 20 > 500)
                {
                    ClientSize = new Size(buttonInWidth * 30 + 20, buttonInHeigth * 30 + coordinateY + 10);
}
                else
                {
                    ClientSize = new Size(500, buttonInHeigth * 30 + coordinateY + 10);
                }
                timer1.Start();
                allButtons = new Button[buttonInWidth, buttonInHeigth];
            Random bombRandom = new Random();

            //создание поля кнопок для игры
            for (int i = 0; i < buttonInHeigth; i++)
            {
                coordinateX = 10;
                for (int j = 0; j < buttonInWidth; j++)
                {
                    CreateButtonInTable(j, i, coordinateX, coordinateY);
                    coordinateX += 30;
                    
                }
                coordinateY += 30;
            }

            //расстановка бомб
            if (bombInGame > buttonInWidth * buttonInHeigth)
            {
                MessageBox.Show("Количество бомб не может быть больше, " +
                    "чем количество клеток", "Ошибка!");
                Application.Restart();
            }
            else
            {
                for (int i = 0; i < bombInGame; i++)
                {
                    while (true) // чтобы рандом не попадал в ту же ячейку
                    {
                        int x = bombRandom.Next(buttonInWidth);
                        int y = bombRandom.Next(buttonInHeigth);
                        if (!IsBomb(allButtons[x, y]))
                        {
                            SetBomb(allButtons[x, y]);
                            //allButtons[x, y].BackColor = Color.Red;
                            //allButtons[x, y].ForeColor = Color.White;

                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            //рассчет значений ячеек 
            for (int i = 0; i < buttonInHeigth; i++)
            {
                for (int j = 0; j < buttonInWidth; j++)
                {
                    int countBomb = 0;
                    if (!IsBomb(allButtons[j, i]))
                    {
                        if (i > 0 && j > 0 && i < buttonInHeigth - 1 && j < buttonInWidth - 1)
                        {
                            countBomb = IsBomb(allButtons[j - 1, i - 1]) ? countBomb + 1 : countBomb;
                            countBomb = IsBomb(allButtons[j, i - 1]) ? countBomb + 1 : countBomb;
                            countBomb = IsBomb(allButtons[j + 1, i - 1]) ? countBomb + 1 : countBomb;
                            countBomb = IsBomb(allButtons[j - 1, i + 1]) ? countBomb + 1 : countBomb;
                            countBomb = IsBomb(allButtons[j, i + 1]) ? countBomb + 1 : countBomb;
                            countBomb = IsBomb(allButtons[j + 1, i + 1]) ? countBomb + 1 : countBomb;
                            countBomb = IsBomb(allButtons[j - 1, i]) ? countBomb + 1 : countBomb;
                            countBomb = IsBomb(allButtons[j + 1, i]) ? countBomb + 1 : countBomb;
                        }
                        else if (i == 0 && j > 0 && j < buttonInWidth - 1)
                        {
                            countBomb = IsBomb(allButtons[j - 1, i + 1]) ? countBomb + 1 : countBomb;
                            countBomb = IsBomb(allButtons[j, i + 1]) ? countBomb + 1 : countBomb;
                            countBomb = IsBomb(allButtons[j + 1, i + 1]) ? countBomb + 1 : countBomb;
                            countBomb = IsBomb(allButtons[j - 1, i]) ? countBomb + 1 : countBomb;
                            countBomb = IsBomb(allButtons[j + 1, i]) ? countBomb + 1 : countBomb;
                        }
                        else if (i > 0 && j == 0 && i < buttonInHeigth - 1)
                        {
                            countBomb = IsBomb(allButtons[j, i - 1]) ? countBomb + 1 : countBomb;
                            countBomb = IsBomb(allButtons[j + 1, i - 1]) ? countBomb + 1 : countBomb;
                            countBomb = IsBomb(allButtons[j, i + 1]) ? countBomb + 1 : countBomb;
                            countBomb = IsBomb(allButtons[j + 1, i + 1]) ? countBomb + 1 : countBomb;
                            countBomb = IsBomb(allButtons[j + 1, i]) ? countBomb + 1 : countBomb;
                        }
                        else if (j > 0 && i == buttonInHeigth - 1 && j < buttonInWidth - 1)
                        {
                            countBomb = IsBomb(allButtons[j - 1, i - 1]) ? countBomb + 1 : countBomb;
                            countBomb = IsBomb(allButtons[j, i - 1]) ? countBomb + 1 : countBomb;
                            countBomb = IsBomb(allButtons[j + 1, i - 1]) ? countBomb + 1 : countBomb;
                            countBomb = IsBomb(allButtons[j - 1, i]) ? countBomb + 1 : countBomb;
                            countBomb = IsBomb(allButtons[j + 1, i]) ? countBomb + 1 : countBomb;
                        }
                        else if (i > 0 && i < buttonInHeigth - 1 && j == buttonInWidth - 1)
                        {
                            countBomb = IsBomb(allButtons[j - 1, i - 1]) ? countBomb + 1 : countBomb;
                            countBomb = IsBomb(allButtons[j, i - 1]) ? countBomb + 1 : countBomb;
                            countBomb = IsBomb(allButtons[j - 1, i + 1]) ? countBomb + 1 : countBomb;
                            countBomb = IsBomb(allButtons[j, i + 1]) ? countBomb + 1 : countBomb;
                            countBomb = IsBomb(allButtons[j - 1, i]) ? countBomb + 1 : countBomb;
                        }
                        else if (i == 0 && j == 0)
                        {
                            countBomb = IsBomb(allButtons[j, i + 1]) ? countBomb + 1 : countBomb;
                            countBomb = IsBomb(allButtons[j + 1, i + 1]) ? countBomb + 1 : countBomb;
                            countBomb = IsBomb(allButtons[j + 1, i]) ? countBomb + 1 : countBomb;
                        }
                        else if (i == 0 && j == buttonInWidth - 1)
                        {
                            countBomb = IsBomb(allButtons[j - 1, i + 1]) ? countBomb + 1 : countBomb;
                            countBomb = IsBomb(allButtons[j, i + 1]) ? countBomb + 1 : countBomb;
                            countBomb = IsBomb(allButtons[j - 1, i]) ? countBomb + 1 : countBomb;
                        }
                        else if (i == buttonInHeigth - 1 && j == buttonInWidth - 1)
                        {
                            countBomb = IsBomb(allButtons[j - 1, i - 1]) ? countBomb + 1 : countBomb;
                            countBomb = IsBomb(allButtons[j, i - 1]) ? countBomb + 1 : countBomb;
                            countBomb = IsBomb(allButtons[j - 1, i]) ? countBomb + 1 : countBomb;
                        }
                        else if (j == 0 && i == buttonInHeigth - 1)
                        {
                            countBomb = IsBomb(allButtons[j, i - 1]) ? countBomb + 1 : countBomb;
                            countBomb = IsBomb(allButtons[j + 1, i - 1]) ? countBomb + 1 : countBomb;
                            countBomb = IsBomb(allButtons[j + 1, i]) ? countBomb + 1 : countBomb;
                        }
                    }
                    if (countBomb > 0)
                    {
                        allButtons[j, i].Text = countBomb.ToString();
                    }
                }

            }
        }
    }
        private void Button_Click(object sender, EventArgs e)//клик на кнопку
        {
            Button button = (Button)sender;
            OpenButton(button);
            if (IsBomb(button))
            {
                OpenTableButton();
                MessageBox.Show("Вы попали в бомбу", "Вы проиграли!");
                Application.Restart();

            }
            else if (button.Text == "")
            {
                OpenButton(button);
                GetNullArea(button);
            }
            else if (WinInGame())
            {
                MessageBox.Show("Поздравляем с победой", "Вы выграли!");
                Application.Restart();
            }
        }
        private void OpenButton(Button button)//открывает кнопку и подкрашивает в соответствующий цвет
        {
            if (IsBomb(button))
            {
                button.BackColor = Color.Red;
                button.ForeColor = Color.White;
            }
            else if (button.Text == "")
            {
                button.BackColor = Color.Gray;

            }
            else if (button.Text == "1")
            {
                button.ForeColor = Color.Blue;
            }
            else if (button.Text == "2")
            {
                button.ForeColor = Color.Green;
            }
            else if (button.Text == "3")
            {
                button.ForeColor = Color.DarkOrange;
            }
            else
            {
                button.ForeColor = Color.Red;
            }
        }
        private void OpenTableButton()//открывает всю таблицу
        {
            for (int i = 0; i < buttonInHeigth; i++)
            {
                for (int j = 0; j < buttonInWidth; j++)
                {
                    OpenButton(allButtons[j, i]);

                }
            }
        }
        private bool WinInGame()//проверка победы (открытие всех кнопок)
        {
            for (int i = 0; i < buttonInHeigth; i++)
            {
                for (int j = 0; j < buttonInWidth; j++)
                {
                    if (allButtons[j, i].ForeColor == Color.White &&
                        allButtons[j, i].Text != "X" && allButtons[j, i].Text != "")
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private void OpenNearButton(int j, int i)//открытие смежных ячеек
        {
            if (j - 1 >= 0 && j + 1 < buttonInWidth &&
                        i - 1 >= 0 && i + 1 < buttonInHeigth)
            {
                OpenButton(allButtons[j - 1, i]);
                OpenButton(allButtons[j + 1, i]);
                OpenButton(allButtons[j - 1, i - 1]);
                OpenButton(allButtons[j + 1, i - 1]);
                OpenButton(allButtons[j, i - 1]);
                OpenButton(allButtons[j - 1, i + 1]);
                OpenButton(allButtons[j + 1, i + 1]);
                OpenButton(allButtons[j, i + 1]);

            }
            else if (j - 1 >= 0 && j + 1 < buttonInWidth &&
                   i == 0)
            {
                OpenButton(allButtons[j - 1, i]);
                OpenButton(allButtons[j + 1, i]);
                OpenButton(allButtons[j - 1, i + 1]);
                OpenButton(allButtons[j + 1, i + 1]);
                OpenButton(allButtons[j, i + 1]);
            }
            else if (j - 1 >= 0 && j + 1 < buttonInWidth &&
                    i == buttonInHeigth - 1)
            {

                OpenButton(allButtons[j - 1, i]);
                OpenButton(allButtons[j + 1, i]);
                OpenButton(allButtons[j - 1, i - 1]);
                OpenButton(allButtons[j + 1, i - 1]);
                OpenButton(allButtons[j, i - 1]);

            }
            else if (j == 0 && i - 1 >= 0 && i + 1 < buttonInHeigth)
            {
                OpenButton(allButtons[j + 1, i]);
                OpenButton(allButtons[j + 1, i - 1]);
                OpenButton(allButtons[j, i - 1]);
                OpenButton(allButtons[j + 1, i + 1]);
                OpenButton(allButtons[j, i + 1]);
            }
            else if (j == buttonInWidth - 1 && i - 1 >= 0 && i + 1 < buttonInHeigth)
            {
                OpenButton(allButtons[j - 1, i]);
                OpenButton(allButtons[j - 1, i - 1]);
                OpenButton(allButtons[j, i - 1]);
                OpenButton(allButtons[j - 1, i + 1]);
                OpenButton(allButtons[j, i + 1]);
            }
            else if (j == 0 && i == 0)
            {
                OpenButton(allButtons[j + 1, i]);
                OpenButton(allButtons[j + 1, i + 1]);
                OpenButton(allButtons[j, i + 1]);
            }
            else if (j == 0 && i == buttonInHeigth - 1)
            {
                OpenButton(allButtons[j + 1, i]);
                OpenButton(allButtons[j + 1, i - 1]);
                OpenButton(allButtons[j, i - 1]);
            }
            else if (j == buttonInWidth - 1 && i == 0)
            {
                OpenButton(allButtons[j - 1, i]);
                OpenButton(allButtons[j - 1, i + 1]);
                OpenButton(allButtons[j, i + 1]);
            }
            else if (j == buttonInWidth - 1 && i == buttonInHeigth - 1)
            {
                OpenButton(allButtons[j - 1, i]);
                OpenButton(allButtons[j - 1, i - 1]);
                OpenButton(allButtons[j, i - 1]);
            }
        }
        private void GetNullArea(Button button)//открывает пустые области (пока не оптимально)
        {
            int x = 0;//координаты кнопки по оси Х
            int y = 0;//координаты кнопки по оси У

            //определение координат кнопки по осям Х и У
            for (int i = 0; i < buttonInHeigth; i++)
            {
                for (int j = 0; j < buttonInWidth; j++)
                {
                    if (allButtons[j, i].Name == button.Name)
                    {
                        x = j;
                        y = i;
                    }
                }
            }

            //поиск прилегающих пустых ячеек

            for (int i = y; i < buttonInHeigth; i++)
            {
                for (int j = x; j < buttonInWidth; j++)
                {
                    if (allButtons[j, i].Text == "")
                    {
                        OpenButton(allButtons[j, i]);
                        OpenNearButton(j, i);
                    }
                    else
                    {
                        break;
                    }
                }
                for (int j = x; j >= 0; j--)
                {
                    if (allButtons[j, i].Text == "")
                    {
                        OpenButton(allButtons[j, i]);
                        OpenNearButton(j, i);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            for (int i = y; i >= 0; i--)
            {
                for (int j = x; j < buttonInWidth; j++)
                {
                    if (allButtons[j, i].Text == "")
                    {
                        OpenButton(allButtons[j, i]);
                        OpenNearButton(j, i);
                    }
                    else
                    {
                        break;
                    }
                }
                for (int j = x; j >= 0; j--)
                {
                    if (allButtons[j, i].Text == "")
                    {
                        OpenButton(allButtons[j, i]);
                        OpenNearButton(j, i);
                    }
                    else
                    {
                        break;
                    }
                }

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int time = (int)timerOfGame.Value;
            if (time > 0)
            {
                time--;
                timerOfGame.Value = time;
            }
            else
            {
                timer1.Stop();
                OpenTableButton();
                MessageBox.Show("Время вышло", "Вы проиграли!");
                Application.Restart();
            }

        }


    }
}
