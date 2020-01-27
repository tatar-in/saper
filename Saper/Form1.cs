using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        int buttonInWidth; //количество кнопок по Х
        int buttonInHeigth; //количество кнопок по У
        int bombInGame; //количество бомб в игре
        NewButton[,] allButtons;//массив кнопок

    //новый класс кнопок     
    public class NewButton : Button
    {
        public string Param { get; set; }
            
    }
        
    //вид кнопок (звездочки, кружки)
    private void CreateButtonInTable(int j, int i)
    {
            
        NewButton button = new NewButton();
        button.Location = new Point(j*30, i*30 + 100);
        button.Size = new Size(30, 30);
        button.BackColor = Color.White;
        button.Name = j + "-" + i;
        Controls.Add(button);
        button.Click += new EventHandler(Button_Click);
        allButtons[j, i] = button;

        GraphicsPath path = new GraphicsPath();
        //звездочка вместо кнопки
        //path.AddPolygon(new PointF[]{
        //            new PointF(15.4658477444273f, 2.3f),
        //            new PointF(19.5803445104746f, 10.6368810393754f),
        //            new PointF(28.7806389725595f, 11.9737620787507f),
        //            new PointF(22.1232433584934f, 18.4631189606246f),
        //            new PointF(23.6948412765219f, 27.6262379212493f),
        //            new PointF(15.4658477444273f, 23.3f),
        //            new PointF(7.23685421233268f, 27.6262379212493f),
        //            new PointF(8.80845213036122f, 18.4631189606246f),
        //            new PointF(2.15105651629515f, 11.9737620787507f),
        //            new PointF(11.35135097838f, 10.6368810393754f)});

        //кружок вместо кнопки
        path.AddEllipse(3, 3, 22, 22);
        Region rgn = new Region(path);
        button.Region = rgn;

    }

    //вид бомбы в кнопке
    private void SetBomb(NewButton button)
    {
        button.Param = "bomb";
    }

    //проверка кнопки на наличие бомбы
    private bool IsBomb(NewButton button)
    {
        if(button.Param == "bomb")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //старт игры
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
                ClientSize = new Size(buttonInWidth * 30, buttonInHeigth * 30 + 100);
}
            else
            {
                ClientSize = new Size(500, buttonInHeigth * 30 + 100);
            }
            timer1.Start();
            allButtons = new NewButton[buttonInWidth, buttonInHeigth];

            CreateTable();
            CreateBomb();
            CountAroundBomb();
        }   
    }
    
    //создание поля кнопок для игры (пока долго грузится при больших количествах)
    private void CreateTable()
    {
        for (int i = 0; i < buttonInHeigth; i++)
        {
            for (int j = 0; j < buttonInWidth; j++)
            {
                CreateButtonInTable(j, i);
            }
        }
    }

    //расстановка бомб
    private void CreateBomb()
    {
        Random bombRandom = new Random();
        if (bombInGame > buttonInWidth * buttonInHeigth)
        {
            Message("mistake_limitofbomb");
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
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
    }

    //рассчет значений ячеек (количество бомб вокруг)
    private void CountAroundBomb()
    {
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
                    allButtons[j, i].Param = countBomb.ToString();
                }
            }

        }
    }
        
    //клик на кнопку
    private void Button_Click(object sender, EventArgs e)
    {
        NewButton button = (NewButton)sender;
        OpenButton(button);
        if (IsBomb(button))
        {
            OpenTableButton();
            Message("lose_on_bomb");

        }
        else if (button.Param == "0")
        {
            OpenButton(button);
            GetNullArea(button);
        }
        else if (WinInGame())
        {
            Message("win");
        }
    }

    //открывает кнопку и подкрашивает в соответствующий цвет
    private void OpenButton(NewButton button)
    {
        if (IsBomb(button))
        {

            ////делаем обозначение бомбы в виде текста
            //button.BackColor = Color.Red;
            //button.ForeColor = Color.White;
            //button.Text = "A";

            //делаем обозначение бомбы в виде иконки 
            button.BackgroundImage = Image.FromFile(@"C:\Users\User\source\repos\Saper\Saper\Resources\picture_bomb.jpg", false);
            button.BackgroundImageLayout = ImageLayout.Zoom;
        }
        else if (button.Param == "0")
        {
            button.BackColor = Color.Gray;
        }
        else if (button.Param == "1")
        {
            button.ForeColor = Color.Blue;
            button.Text = button.Param;
        }
        else if (button.Param == "2")
        {
            button.ForeColor = Color.Green;
            button.Text = button.Param;
        }
        else if (button.Param == "3")
        {
            button.ForeColor = Color.DarkOrange;
            button.Text = button.Param;
        }
        else
        {
            button.ForeColor = Color.Red;
            button.Text = button.Param;
        }
    }

    //открывает всю таблицу
    private void OpenTableButton()
    {
        for (int i = 0; i < buttonInHeigth; i++)
        {
            for (int j = 0; j < buttonInWidth; j++)
            {
                OpenButton(allButtons[j, i]);

            }
        }
    }

    //проверка победы (открытие всех кнопок)
    private bool WinInGame()
    {
        for (int i = 0; i < buttonInHeigth; i++)
        {
            for (int j = 0; j < buttonInWidth; j++)
            {
                if (allButtons[j, i].Text == "" && allButtons[j, i].Param != "bomb" && allButtons[j, i].Param != "0")
                {
                    return false;
                }
            }
        }
        return true;
    }
        
    //открытие смежных ячеек
    private void OpenNearButton(int j, int i)
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

    //открывает пустые области (пока не оптимально)
    private void GetNullArea(NewButton button)
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
                if (allButtons[j, i].Param == "0")
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
                if (allButtons[j, i].Param == "0")
                {
                    OpenButton(allButtons[j, i]);
                    OpenNearButton(j, i);
                }
                else
                {
                    break;
                }
            }
            if (i < buttonInHeigth - 1)
            {
                if (allButtons[x, i + 1].Param != "0") break;
            }
                
        }
        for (int i = y; i >= 0; i--)
        {
            for (int j = x; j < buttonInWidth; j++)
            {
                if (allButtons[j, i].Param == "0")
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
                if (allButtons[j, i].Param == "0")
                {
                    OpenButton(allButtons[j, i]);
                    OpenNearButton(j, i);
                }
                else
                {
                    break;
                }
            }
            if (i > 0) 
            {
                if (allButtons[x, i - 1].Param != "0") break;
            } 
        }
        if (WinInGame())
        {
            Message("win");
        }
    }

    //таймер
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
            Message("lose_on_timeout");
        }

    }

    //блок с выводимыми сообщениями
    private void Message(string message)
    {
        //сообщение о победе
        if (message == "win")
        {
            MessageBox.Show("Поздравляем с победой", "Вы выграли!");
        }
        //сообщение о проигрыше (попадение в бомбу)
        else if (message == "lose_on_bomb")
        {
            MessageBox.Show("Вы попали в бомбу", "Вы проиграли!");
        }
        //сообщение о проигрыше (истечение времени)
        else if (message == "lose_on_timeout")
        {
            MessageBox.Show("Время вышло", "Вы проиграли!");
        }
        //сообщение об ошибке (неверное количество бомб)
        else if (message == "mistake_limitofbomb")
        {
            MessageBox.Show("Количество бомб не может быть больше, чем количество клеток", "Ошибка!");
        }
        Application.Restart();
    }
    
    
    }
}
