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
        //создаем переменные для игры
        private int countX; //количество кнопок по оси Х
        private int countY; //количество кнопок по оси У
        private int countBomb; //количество бомб в игре
        private int buttonWH = 30; //ширина и высота кнопки по осям Х и Y
        private int menuHeight = 100; //высота блока меню с установкой параметров игры

        //создаем массив значений кнопок (логика игры)
        //"" - граница игрового поля (чтобы не писать условия на проверку границ массива)
        //"0" - кнопка пустая 
        //"1"..."8" - вокруг кнопки указанное количество бомб 
        //"bomb" - кнопка содержит бомбу
        private string[,] buttonValue;

        //создаем массив статусов кнопок
        //0 - кнопка закрыта
        //1 - кнопка открыта
        //2 - поставлен флаг
        private int[,] buttonStatus;
        
        //создаем поле для рисования
        private Graphics g;

        public Form1()
        {
            InitializeComponent();
            //создание кнопки для закрытия игры по ESC
            Button buttonClose = new Button();
            buttonClose.Size = new Size(0, 0);
            buttonClose.Click += new EventHandler(buttonClose_Click);
            Controls.Add(buttonClose);
            
            //кнопки по умолчанию: ESC - выход, ENTER - старт
            CancelButton = buttonClose;
            AcceptButton = buttonStartGame;
        }

        //закрытие игры по ESC
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        //старт игры
        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            //забираем данные с формы для генерации игры (количество кнопок по Х и Y, количество бомб)
            //деактивируем кнопки и поля
            countX = (int)countButtonX.Value;
            countY = (int)countButtonY.Value;
            countBomb = (int)countMine.Value;
            countButtonX.Enabled = false;
            countButtonY.Enabled = false;
            countMine.Enabled = false;
            timerOfGame.Enabled = false;
            buttonStartGame.Enabled = false;

            //проверка на превышение количества бомб над количеством кнопок
            if (countBomb > countX * countY)
            {
                Message("mistake-limitofbomb");
            }

            //проверка на превышение ширины и высоты игрового поля 
            //над максимально возможной шириной и высотой окна формы
            //и генерация ширины и высоты формы в соотвествии с размерами игрового поля
            if ((countY + 2) * buttonWH + menuHeight < SystemInformation.PrimaryMonitorMaximizedWindowSize.Height
                && (countX + 2) * buttonWH < SystemInformation.PrimaryMonitorMaximizedWindowSize.Width)
            {
                //проверка превышения ширины формы, заданной по умолчанию = 500
                if ((countX + 2) * buttonWH < 500)
                {
                    ClientSize = new Size(500, (countY + 2) * buttonWH + menuHeight);
                }
                else if ((countX + 2) * buttonWH >= 500 && (countX + 2) * buttonWH < SystemInformation.PrimaryMonitorMaximizedWindowSize.Width)
                {
                    ClientSize = new Size((countX + 2) * buttonWH, (countY + 2) * buttonWH + menuHeight);
                }
            }
            else
            {
                string descr;
                string capt = "Ошибка!";
                MessageBoxButtons type = MessageBoxButtons.YesNo;
                //сообщение, что игровое поле выходит за границы максимально возможной ширины и высоты окна формы
                //ДА - установка максимального количества кнопок по оси Х и У
                //НЕТ - перезапуск игры
                if ((countY + 2) * buttonWH + menuHeight >= SystemInformation.PrimaryMonitorMaximizedWindowSize.Height
                    && (countX + 2) * buttonWH >= SystemInformation.PrimaryMonitorMaximizedWindowSize.Width)
                {
                    descr = "Количество кнопок не может быть больше, чем размер окна!\n\n" +
                        $"Максимальное количество кнопок по оси Х = {SystemInformation.PrimaryMonitorMaximizedWindowSize.Width / buttonWH - 2}" +
                        $", а по оси Y = {(SystemInformation.PrimaryMonitorMaximizedWindowSize.Height - menuHeight) / buttonWH - 2}\n\n" +
                        "Применить максимальные параметры к игре?";
                    DialogResult result = MessageBox.Show(descr, capt, type);
                    if (result == DialogResult.Yes)
                    {
                        countX = SystemInformation.PrimaryMonitorMaximizedWindowSize.Width / buttonWH - 2;
                        countY = (SystemInformation.PrimaryMonitorMaximizedWindowSize.Height - menuHeight) / buttonWH - 2;
                        ClientSize = new Size((countX + 2) * buttonWH, (countY + 2) * buttonWH + menuHeight);
                        countButtonX.Value = countX;
                        countButtonY.Value = countY;
                    }
                    else
                    {
                        Application.Restart();
                    }
                }
                //игровое поле выходит за границу максимально возможной высоты окна формы
                else if ((countY + 2) *buttonWH + menuHeight >= SystemInformation.PrimaryMonitorMaximizedWindowSize.Height)
                {
                    descr = "Количество кнопок не может быть больше, чем размер окна!\n\n" +
                        $"Максимальное количество кнопок по оси Y = {(SystemInformation.PrimaryMonitorMaximizedWindowSize.Height - menuHeight) / buttonWH - 2}\n\n" +
                        "Применить параметры к игре?";
                    DialogResult result = MessageBox.Show(descr, capt, type);
                    if (result == DialogResult.Yes)
                    {
                        countY = (SystemInformation.PrimaryMonitorMaximizedWindowSize.Height - menuHeight) / buttonWH - 2;
                        ClientSize = new Size((countX + 2) * buttonWH, (countY + 2) * buttonWH + menuHeight);
                        countButtonY.Value = countY;
                    }
                    else
                    {
                        Application.Restart();
                    }
                }
                //игровое поле выходит за границу максимально возможной ширины окна формы
                else
                {
                    descr = "Количество кнопок не может быть больше, чем размер окна!\n\n" +
                        $"Максимальное количество кнопок по оси Х = {SystemInformation.PrimaryMonitorMaximizedWindowSize.Width / buttonWH - 2}\n\n" +
                        "Применить максимальные параметры к игре?";
                    DialogResult result = MessageBox.Show(descr, capt, type);
                    if (result == DialogResult.Yes)
                    {
                        countX = SystemInformation.PrimaryMonitorMaximizedWindowSize.Width / buttonWH - 2;
                        ClientSize = new Size((countX + 2) * buttonWH, (countY + 2) * buttonWH + menuHeight);
                        countButtonX.Value = countX;
                    }
                    else
                    {
                        Application.Restart();
                    }
                }
            }
            //генерируем и запускаем игру
            Location = new Point(0, 0);
            g = CreateGraphics();
            buttonValue = new string[countX + 2, countY + 2];
            buttonStatus = new int[countX + 2, countY + 2];
            CreateBomb();
            CountAroundBomb();
            CreateTable();
            timer1.Start();
        }
        
        //вид кнопки
        private void CreateButton(int x, int y)
        {
            //рисуем закрытую кнопку
            if (buttonStatus[x, y] == 0)
            {
                g.FillRectangle(Brushes.Gray, x * buttonWH, y * buttonWH + menuHeight, buttonWH, buttonWH);
                g.DrawRectangle(Pens.White, x * buttonWH, y * buttonWH + menuHeight, buttonWH, buttonWH);
            }
            //рисуем открытую непустую кнопку и ее значение
            else if (buttonStatus[x, y] == 1 && buttonValue[x, y] != "bomb" && buttonValue[x, y] != "0")
            {
                g.FillRectangle(Brushes.White, x * buttonWH, y * buttonWH + menuHeight, buttonWH, buttonWH);
                g.DrawRectangle(Pens.Gray, x * buttonWH, y * buttonWH + menuHeight, buttonWH, buttonWH);
                if (buttonValue[x, y] == "1")
                {
                    g.DrawString(buttonValue[x, y], new Font("Arial", (int)(buttonWH * 0.3), FontStyle.Bold), Brushes.Blue, new Point(x * buttonWH + (int)(buttonWH * 0.3), y * buttonWH + menuHeight + (int)(buttonWH * 0.3)));
                }
                else if (buttonValue[x, y] == "2")
                {
                    g.DrawString(buttonValue[x, y], new Font("Arial", (int)(buttonWH * 0.3), FontStyle.Bold), Brushes.Green, new Point(x * buttonWH + (int)(buttonWH * 0.3), y * buttonWH + menuHeight + (int)(buttonWH * 0.3)));
                }
                else if (buttonValue[x, y] == "3")
                {
                    g.DrawString(buttonValue[x, y], new Font("Arial", (int)(buttonWH * 0.3), FontStyle.Bold), Brushes.DarkOrange, new Point(x * buttonWH + (int)(buttonWH * 0.3), y * buttonWH + menuHeight + (int)(buttonWH * 0.3)));
                }
                else 
                {
                    g.DrawString(buttonValue[x, y], new Font("Arial", (int)(buttonWH * 0.3), FontStyle.Bold), Brushes.Red, new Point(x * buttonWH + (int)(buttonWH * 0.3), y * buttonWH + menuHeight + (int)(buttonWH * 0.3)));
                }
            }
            //рисуем открытую пустую кнопку 
            else if (buttonStatus[x, y] == 1 && buttonValue[x, y] == "0")
            {
                g.FillRectangle(Brushes.LightGray, x * buttonWH, y * buttonWH + menuHeight, buttonWH, buttonWH);
            }
            //рисуем бомбу
            else if (buttonStatus[x, y] == 1 && buttonValue[x, y] == "bomb")
            {
                g.FillRectangle(Brushes.Red, 
                    x * buttonWH, 
                    y * buttonWH + menuHeight, 
                    buttonWH, 
                    buttonWH);
                g.FillEllipse(Brushes.Black,
                    x * buttonWH + (int)(buttonWH * 0.2),
                    y * buttonWH + menuHeight + (int)(buttonWH * 0.2),
                    (int)(buttonWH * 0.6),
                    (int)(buttonWH * 0.6));
                g.DrawLine(Pens.Black,
                    x * buttonWH + (int)(buttonWH * 0.5), 
                    y * buttonWH + menuHeight + (int)(buttonWH * 0.1), 
                    x * buttonWH + (int)(buttonWH * 0.5), 
                    y * buttonWH + menuHeight + (int)(buttonWH * 0.9));
                g.DrawLine(Pens.Black,
                    x * buttonWH + (int)(buttonWH * 0.1),
                    y * buttonWH + menuHeight + (int)(buttonWH * 0.5),
                    x * buttonWH + (int)(buttonWH * 0.9),
                    y * buttonWH + menuHeight + (int)(buttonWH * 0.5));
                g.DrawLine(Pens.Black,
                    x * buttonWH + (int)(buttonWH * 0.15),
                    y * buttonWH + menuHeight + (int)(buttonWH * 0.15),
                    x * buttonWH + (int)(buttonWH * 0.85),
                    y * buttonWH + menuHeight + (int)(buttonWH * 0.85));
                g.DrawLine(Pens.Black,
                    x * buttonWH + (int)(buttonWH * 0.85),
                    y * buttonWH + menuHeight + (int)(buttonWH * 0.15),
                    x * buttonWH + (int)(buttonWH * 0.15),
                    y * buttonWH + menuHeight + (int)(buttonWH * 0.85));
            }
            //рисуем флаг
            else if (buttonStatus[x, y] == 2)
            {
                g.FillRectangle(Brushes.Yellow, x * buttonWH, y * buttonWH + menuHeight, buttonWH, buttonWH);
                g.FillPolygon(Brushes.Red, new Point[] {
                    new Point(x * buttonWH + (int)(buttonWH * 0.2), y * buttonWH + menuHeight + (int)(buttonWH * 0.1)),
                    new Point(x * buttonWH + (int)(buttonWH * 0.8), y * buttonWH + menuHeight + (int)(buttonWH * 0.3)), 
                    new Point(x * buttonWH + (int)(buttonWH * 0.2), y * buttonWH + menuHeight + (int)(buttonWH * 0.5))});
                g.DrawLine(Pens.Black, 
                    x * buttonWH + (int)(buttonWH * 0.2), 
                    y * buttonWH + menuHeight + (int)(buttonWH * 0.5), 
                    x * buttonWH + (int)(buttonWH * 0.2), 
                    y * buttonWH + menuHeight + (int)(buttonWH * 0.9));
            }
        }

        //создание поля кнопок для игры
        private void CreateTable()
        {
            for (int i = 1; i < countY + 1; i++)
            {
                for (int j = 1; j < countX + 1; j++)
                {
                    CreateButton(j, i);
                }
            }
        }

        //расстановка бомб И запись в массив 
        private void CreateBomb()
        {
            Random bombRandom = new Random();
            for (int i = 0; i < countBomb; i++)
            {
                //чтобы рандом не попадал в ту же ячейку
                while (true) 
                {
                    int x = bombRandom.Next(1, countX + 1);
                    int y = bombRandom.Next(1, countY + 1);
                    if (buttonValue[x, y] != "bomb")
                    {
                        buttonValue[x, y] = "bomb";
                        break;
                    }
                }
            }
        }

        //рассчет значений кнопок, находящихся рядом с бомбой (количество бомб вокруг)
        //запись в массив
        private void CountAroundBomb()
        {
            for (int i = 1; i < countY + 1; i++)
            {
                for (int j = 1; j < countX + 1; j++)
                {
                    int countBomb = 0;
                    if (buttonValue[j, i] != "bomb")
                    {
                        if (buttonValue[j - 1, i - 1] == "bomb") countBomb++;
                        if (buttonValue[j, i - 1] == "bomb") countBomb++;
                        if (buttonValue[j + 1, i - 1] == "bomb") countBomb++;
                        if (buttonValue[j - 1, i + 1] == "bomb") countBomb++;
                        if (buttonValue[j, i + 1] == "bomb") countBomb++;
                        if (buttonValue[j + 1, i + 1] == "bomb") countBomb++;
                        if (buttonValue[j - 1, i] == "bomb") countBomb++;
                        if (buttonValue[j + 1, i] == "bomb") countBomb++;
                        buttonValue[j, i] = countBomb.ToString();
                    }
                }
            }
        }

        //клик на кнопку
        private void Mouse_Click(object sender, MouseEventArgs e)
        {
            //по координатам клика получаем координаты кнопки
            int x = e.X / buttonWH;
            int y = (e.Y - menuHeight) / buttonWH;
            //проверяем, чтобы клик был в игровое поле
            if (x >= 1 && x <= countX && y >= 1 && y <= countY)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (buttonStatus[x, y] == 0)
                    {
                        buttonStatus[x, y] = 1;
                        CreateButton(x, y);
                        //попал в бомбу - проиграл
                        if (buttonValue[x, y] == "bomb")
                        {
                            OpenTableButton();
                            Message("lose-bomb");
                            Application.Restart();
                        }
                        //попал в пустую область - открытие области
                        else if (buttonValue[x, y] == "0")
                        {
                            GetNullArea(x, y);
                        }
                    }
                }
                //установка флага в кнопку правой кнопкой мыши
                else if (e.Button == MouseButtons.Right)
                {
                    if (buttonStatus[x, y] == 0)
                    {
                        buttonStatus[x, y] = 2;
                    }
                    //повторное нажатие снимает флаг
                    else if (buttonStatus[x, y] == 2)
                    {
                        buttonStatus[x, y] = 0;
                    }
                    CreateButton(x, y);
                }
                //проверка условия победы и остановка игры
                if (WinInGame())
                {
                    timer1.Stop(); 
                    Message("win");
                    Application.Restart();
                }
            }

        }

        //открывает всю таблицу (все кнопки)
        private void OpenTableButton()
        {
            for (int i = 1; i < countY + 1; i++)
            {
                for (int j = 1; j < countX + 1; j++)
                {
                    buttonStatus[j, i] = 1;
                    CreateButton(j, i);
                }
            }
        }

        //проверка победы (открыты все ли нужные кнопки или нет)
        private bool WinInGame()
        {
            for (int i = 1; i < countY + 1; i++)
            {
                for (int j = 1; j < countX + 1; j++)
                {
                    if((buttonStatus[j, i] == 0 && buttonValue[j, i] != "bomb") || (buttonStatus[j, i] == 2 && buttonValue[j, i] != "bomb") || (buttonStatus[j, i] == 1 && buttonValue[j, i] == "bomb"))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //открытие смежных непустых кнопок, располагающимися рядос с пустыми кнопками
        private void OpenNearButton(int x, int y)
        {
            if (buttonValue[x - 1, y] != "0" && buttonValue[x - 1, y] != null)
            {
                buttonStatus[x - 1, y] = 1;
                CreateButton(x - 1, y);
            }
            if (buttonValue[x + 1, y] != "0" && buttonValue[x + 1, y] != null)
            {
                buttonStatus[x + 1, y] = 1;
                CreateButton(x + 1, y);
            }
            if (buttonValue[x - 1, y - 1] != "0" && buttonValue[x - 1, y - 1] != null)
            {
                buttonStatus[x - 1, y - 1] = 1;
                CreateButton(x - 1, y - 1);
            }
            if (buttonValue[x + 1, y - 1] != "0" && buttonValue[x + 1, y - 1] != null)
            {
                buttonStatus[x + 1, y - 1] = 1;
                CreateButton(x + 1, y - 1);
            }
            if (buttonValue[x, y - 1] != "0" && buttonValue[x, y - 1] != null)
            {
                buttonStatus[x, y - 1] = 1;
                CreateButton(x, y - 1);
            }
            if (buttonValue[x - 1, y + 1] != "0" && buttonValue[x - 1, y + 1] != null)
            {
                buttonStatus[x - 1, y + 1] = 1;
                CreateButton(x - 1, y + 1);
            }
            if (buttonValue[x + 1, y + 1] != "0" && buttonValue[x + 1, y + 1] != null)
            {
                buttonStatus[x + 1, y + 1] = 1;
                CreateButton(x + 1, y + 1);
            }
            if (buttonValue[x, y + 1] != "0" && buttonValue[x, y + 1] != null)
            {
                buttonStatus[x, y + 1] = 1;
                CreateButton(x, y + 1);
            }
        }

        //открывает пустые области
        //проверяет значение и статус соседней кнопки 
        //открывает кнопку, соседние непустые кнопки
        //запускает рекурсию
        private void GetNullArea(int x, int y)
        {
            
            if (buttonValue[x - 1, y - 1] == "0" && buttonStatus[x - 1, y - 1] == 0)
            {
                buttonStatus[x - 1, y - 1] = 1;
                CreateButton(x - 1, y - 1);
                GetNullArea(x - 1, y - 1);
                OpenNearButton(x - 1, y - 1);
            }
            if (buttonValue[x, y - 1] == "0" && buttonStatus[x, y - 1] == 0)
            {
                buttonStatus[x, y - 1] = 1;
                CreateButton(x, y - 1);
                GetNullArea(x, y - 1);
                OpenNearButton(x, y - 1);
            }
            if (buttonValue[x + 1, y - 1] == "0" && buttonStatus[x + 1, y - 1] == 0)
            {
                buttonStatus[x + 1, y - 1] = 1;
                CreateButton(x + 1, y - 1);
                GetNullArea(x + 1, y - 1);
                OpenNearButton(x + 1, y - 1);
            }
            if (buttonValue[x - 1, y + 1] == "0" && buttonStatus[x - 1, y + 1] == 0)
            {
                buttonStatus[x - 1, y + 1] = 1;
                CreateButton(x - 1, y + 1);
                GetNullArea(x - 1, y + 1);
                OpenNearButton(x - 1, y + 1);
            }
            if (buttonValue[x, y + 1] == "0" && buttonStatus[x, y + 1] == 0)
            {
                buttonStatus[x, y + 1] = 1;
                CreateButton(x, y + 1);
                GetNullArea(x, y + 1);
                OpenNearButton(x, y + 1);
            }
            if (buttonValue[x + 1, y + 1] == "0" && buttonStatus[x + 1, y + 1] == 0)
            {
                buttonStatus[x + 1, y + 1] = 1;
                CreateButton(x + 1, y + 1);
                GetNullArea(x + 1, y + 1);
                OpenNearButton(x + 1, y + 1);
            }
            if (buttonValue[x - 1, y] == "0" && buttonStatus[x - 1, y] == 0)
            {
                buttonStatus[x - 1, y] = 1;
                CreateButton(x - 1, y);
                GetNullArea(x - 1, y);
                OpenNearButton(x - 1, y);
            }
            if (buttonValue[x + 1, y] == "0" && buttonStatus[x + 1, y] == 0)
            {
                buttonStatus[x + 1, y] = 1;
                CreateButton(x + 1, y);
                GetNullArea(x + 1, y);
                OpenNearButton(x + 1, y);
            }
            OpenNearButton(x, y);
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
                Message("lose-timeout");
                Application.Restart();
            }
        }

        //блок с выводимыми сообщениями
        public void Message(string message)
        {
            //сообщение о победе
            if (message == "win")
            {
                MessageBox.Show("Поздравляем с победой", "Вы выграли!");
            }
            //сообщение о проигрыше (попадение в бомбу)
            else if (message == "lose-bomb")
            {
                MessageBox.Show("Вы попали в бомбу", "Вы проиграли!");
            }
            //сообщение о проигрыше (истечение времени)
            else if (message == "lose-timeout")
            {
                MessageBox.Show("Время вышло", "Вы проиграли!");
            }
            //сообщение об ошибке (неверное количество бомб)
            else if (message == "mistake-limitofbomb")
            {
                MessageBox.Show("Количество бомб не может быть больше, чем количество клеток", "Ошибка!");
            }
        }
    }
}
