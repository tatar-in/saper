using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper
{
    /// <summary>
    /// Класс содержит методы построения логики игры
    /// </summary>
    class Logic
    {
        /// <summary>
        /// Запускает игру
        /// </summary>
        public static void StartGame()
        {
            //генерируем массивы значений и статусов кнопок
            Data.ButtonValue = new string[Game.CountX, Game.CountY];
            Data.ButtonStatus = new int[Game.CountX, Game.CountY];
            //расстановки бомб 
            CreateBomb();
            //рассчет значений кнопки
            CountAroundBomb();
        }

        private static Random rand = new Random();

        /// <summary>
        /// Расставляет бомбы в кнопки 
        /// </summary>
        public static void CreateBomb()
        {
            for (int i = 0; i < Game.CountBomb; i++)
            {
                //чтобы рандом не попадал в ту же ячейку
                while (true)
                {
                    int x = rand.Next(Game.CountX);
                    int y = rand.Next(Game.CountY);
                    if (Data.ButtonValue[x, y] != "bomb")
                    {
                        Data.ButtonValue[x, y] = "bomb";
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Рассчет числового значения кнопок находящихся рядом с бомбой (количество бомб вокруг)
        /// </summary>
        public static void CountAroundBomb()
        {
            for (int y = 0; y < Game.CountY; y++)
            {
                for (int x = 0; x < Game.CountX; x++)
                {
                    int bomb = 0;
                    if (Data.ButtonValue[x, y] != "bomb")
                    {
                        if (x > 0 && x < Game.CountX - 1 && y > 0 && y < Game.CountY - 1)
                        {
                            if (Data.ButtonValue[x - 1, y - 1] == "bomb") bomb++;
                            if (Data.ButtonValue[x, y - 1] == "bomb") bomb++;
                            if (Data.ButtonValue[x + 1, y - 1] == "bomb") bomb++;
                            if (Data.ButtonValue[x - 1, y + 1] == "bomb") bomb++;
                            if (Data.ButtonValue[x, y + 1] == "bomb") bomb++;
                            if (Data.ButtonValue[x + 1, y + 1] == "bomb") bomb++;
                            if (Data.ButtonValue[x - 1, y] == "bomb") bomb++;
                            if (Data.ButtonValue[x + 1, y] == "bomb") bomb++;
                        }
                        else if (x == 0 && y > 0 && y < Game.CountY - 1)
                        { 
                            if (Data.ButtonValue[x, y - 1] == "bomb") bomb++;
                            if (Data.ButtonValue[x + 1, y - 1] == "bomb") bomb++;
                            if (Data.ButtonValue[x, y + 1] == "bomb") bomb++;
                            if (Data.ButtonValue[x + 1, y + 1] == "bomb") bomb++;
                            if (Data.ButtonValue[x + 1, y] == "bomb") bomb++;
                        }
                        else if (x == Game.CountX - 1 && y > 0 && y < Game.CountY - 1)
                        {
                            if (Data.ButtonValue[x - 1, y - 1] == "bomb") bomb++;
                            if (Data.ButtonValue[x, y - 1] == "bomb") bomb++;
                            if (Data.ButtonValue[x - 1, y + 1] == "bomb") bomb++;
                            if (Data.ButtonValue[x, y + 1] == "bomb") bomb++;
                            if (Data.ButtonValue[x - 1, y] == "bomb") bomb++;
                        }
                        else if (y == 0 && x > 0 && x < Game.CountX - 1)
                        {
                            if (Data.ButtonValue[x - 1, y + 1] == "bomb") bomb++;
                            if (Data.ButtonValue[x, y + 1] == "bomb") bomb++;
                            if (Data.ButtonValue[x + 1, y + 1] == "bomb") bomb++;
                            if (Data.ButtonValue[x - 1, y] == "bomb") bomb++;
                            if (Data.ButtonValue[x + 1, y] == "bomb") bomb++;
                        }
                        else if (y == Game.CountY - 1 && x > 0 && x < Game.CountX - 1)
                        {
                            if (Data.ButtonValue[x - 1, y - 1] == "bomb") bomb++;
                            if (Data.ButtonValue[x, y - 1] == "bomb") bomb++;
                            if (Data.ButtonValue[x + 1, y - 1] == "bomb") bomb++;
                            if (Data.ButtonValue[x - 1, y] == "bomb") bomb++;
                            if (Data.ButtonValue[x + 1, y] == "bomb") bomb++;
                        }
                        else if (x == 0 && y == 0)
                        {
                            if (Data.ButtonValue[x, y + 1] == "bomb") bomb++;
                            if (Data.ButtonValue[x + 1, y + 1] == "bomb") bomb++;
                            if (Data.ButtonValue[x + 1, y] == "bomb") bomb++;
                        }
                        else if (x == 0 && y == Game.CountY - 1)
                        {
                            if (Data.ButtonValue[x, y - 1] == "bomb") bomb++;
                            if (Data.ButtonValue[x + 1, y - 1] == "bomb") bomb++;
                            if (Data.ButtonValue[x + 1, y] == "bomb") bomb++;
                        }
                        else if (y == 0 && x == Game.CountX - 1)
                        {
                            if (Data.ButtonValue[x - 1, y + 1] == "bomb") bomb++;
                            if (Data.ButtonValue[x, y + 1] == "bomb") bomb++;
                            if (Data.ButtonValue[x - 1, y] == "bomb") bomb++;
                        }
                        else if (x == Game.CountX - 1 && y == Game.CountY - 1)
                        {
                            if (Data.ButtonValue[x - 1, y - 1] == "bomb") bomb++;
                            if (Data.ButtonValue[x, y - 1] == "bomb") bomb++;
                            if (Data.ButtonValue[x - 1, y] == "bomb") bomb++;
                        }
                        Data.ButtonValue[x, y] = bomb.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// Открывает смежные кнопки, располагающиеся рядом
        /// </summary>
        /// <param name="x">Положение Х кнопки в массиве всех кнопок</param>
        /// <param name="y">Положение Y кнопки в массиве всех кнопок</param>
        public static void OpenNearButton(int x, int y)
        {
            if (x > 0 && x < Game.CountX - 1 && y > 0 && y < Game.CountY - 1)
            {
                Data.ButtonStatus[x - 1, y] = 1;
                Data.ButtonStatus[x + 1, y] = 1;
                Data.ButtonStatus[x - 1, y - 1] = 1;
                Data.ButtonStatus[x + 1, y - 1] = 1;
                Data.ButtonStatus[x, y - 1] = 1;
                Data.ButtonStatus[x - 1, y + 1] = 1;
                Data.ButtonStatus[x + 1, y + 1] = 1;
                Data.ButtonStatus[x, y + 1] = 1;
            }
            else if (x == 0 && y > 0 && y < Game.CountY - 1)
            {
                Data.ButtonStatus[x + 1, y] = 1;
                Data.ButtonStatus[x + 1, y - 1] = 1;
                Data.ButtonStatus[x, y - 1] = 1;
                Data.ButtonStatus[x + 1, y + 1] = 1;
                Data.ButtonStatus[x, y + 1] = 1;
            }
            else if (x == Game.CountX - 1 && y > 0 && y < Game.CountY - 1)
            {
                Data.ButtonStatus[x - 1, y] = 1;
                Data.ButtonStatus[x - 1, y - 1] = 1;
                Data.ButtonStatus[x, y - 1] = 1;
                Data.ButtonStatus[x - 1, y + 1] = 1;
                Data.ButtonStatus[x, y + 1] = 1;
            }
            else if (y == 0 && x > 0 && x < Game.CountX - 1)
            {
                Data.ButtonStatus[x - 1, y] = 1;
                Data.ButtonStatus[x + 1, y] = 1;
                Data.ButtonStatus[x - 1, y + 1] = 1;
                Data.ButtonStatus[x + 1, y + 1] = 1;
                Data.ButtonStatus[x, y + 1] = 1;
            }
            else if (y == Game.CountY - 1 && x > 0 && x < Game.CountX - 1)
            {
                Data.ButtonStatus[x - 1, y] = 1;
                Data.ButtonStatus[x + 1, y] = 1;
                Data.ButtonStatus[x - 1, y - 1] = 1;
                Data.ButtonStatus[x + 1, y - 1] = 1;
                Data.ButtonStatus[x, y - 1] = 1;
            }
            else if (x == 0 && y == 0)
            {
                Data.ButtonStatus[x + 1, y] = 1;
                Data.ButtonStatus[x + 1, y + 1] = 1;
                Data.ButtonStatus[x, y + 1] = 1;
            }
            else if (x == 0 && y == Game.CountY - 1)
            {
                Data.ButtonStatus[x + 1, y] = 1;
                Data.ButtonStatus[x + 1, y - 1] = 1;
                Data.ButtonStatus[x, y - 1] = 1;
            }
            else if (y == 0 && x == Game.CountX - 1)
            {
                Data.ButtonStatus[x - 1, y] = 1;
                Data.ButtonStatus[x - 1, y + 1] = 1;
                Data.ButtonStatus[x, y + 1] = 1;
            }
            else if (x == Game.CountX - 1 && y == Game.CountY - 1)
            {
                Data.ButtonStatus[x - 1, y] = 1;
                Data.ButtonStatus[x - 1, y - 1] = 1;
                Data.ButtonStatus[x, y - 1] = 1;
            }
        }

        /// <summary>
        /// Открывает пустые области
        /// </summary>
        /// <param name="x">Положение Х кнопки в массиве всех кнопок</param>
        /// <param name="y">Положение Y кнопки в массиве всех кнопок</param>
        public static void GetNullArea(int x, int y)
        {
            if (x > 0 && x < Game.CountX - 1 && y > 0 && y < Game.CountY - 1)
            {
                if (Data.ButtonValue[x - 1, y - 1] == "0" && Data.ButtonStatus[x - 1, y - 1] == 0)
                {
                    Data.ButtonStatus[x - 1, y - 1] = 1;
                    GetNullArea(x - 1, y - 1);
                    OpenNearButton(x - 1, y - 1);
                }
                if (Data.ButtonValue[x, y - 1] == "0" && Data.ButtonStatus[x, y - 1] == 0)
                {
                    Data.ButtonStatus[x, y - 1] = 1;
                    GetNullArea(x, y - 1);
                    OpenNearButton(x, y - 1);
                }
                if (Data.ButtonValue[x + 1, y - 1] == "0" && Data.ButtonStatus[x + 1, y - 1] == 0)
                {
                    Data.ButtonStatus[x + 1, y - 1] = 1;
                    GetNullArea(x + 1, y - 1);
                    OpenNearButton(x + 1, y - 1);
                }
                if (Data.ButtonValue[x - 1, y + 1] == "0" && Data.ButtonStatus[x - 1, y + 1] == 0)
                {
                    Data.ButtonStatus[x - 1, y + 1] = 1;
                    GetNullArea(x - 1, y + 1);
                    OpenNearButton(x - 1, y + 1);
                }
                if (Data.ButtonValue[x, y + 1] == "0" && Data.ButtonStatus[x, y + 1] == 0)
                {
                    Data.ButtonStatus[x, y + 1] = 1;
                    GetNullArea(x, y + 1);
                    OpenNearButton(x, y + 1);
                }
                if (Data.ButtonValue[x + 1, y + 1] == "0" && Data.ButtonStatus[x + 1, y + 1] == 0)
                {
                    Data.ButtonStatus[x + 1, y + 1] = 1;
                    GetNullArea(x + 1, y + 1);
                    OpenNearButton(x + 1, y + 1);
                }
                if (Data.ButtonValue[x - 1, y] == "0" && Data.ButtonStatus[x - 1, y] == 0)
                {
                    Data.ButtonStatus[x - 1, y] = 1;
                    GetNullArea(x - 1, y);
                    OpenNearButton(x - 1, y);
                }
                if (Data.ButtonValue[x + 1, y] == "0" && Data.ButtonStatus[x + 1, y] == 0)
                {
                    Data.ButtonStatus[x + 1, y] = 1;
                    GetNullArea(x + 1, y);
                    OpenNearButton(x + 1, y);
                }
            }
            else if (x == 0 && y > 0 && y < Game.CountY - 1)
            {
                if (Data.ButtonValue[x, y - 1] == "0" && Data.ButtonStatus[x, y - 1] == 0)
                {
                    Data.ButtonStatus[x, y - 1] = 1;
                    GetNullArea(x, y - 1);
                    OpenNearButton(x, y - 1);
                }
                if (Data.ButtonValue[x + 1, y - 1] == "0" && Data.ButtonStatus[x + 1, y - 1] == 0)
                {
                    Data.ButtonStatus[x + 1, y - 1] = 1;
                    GetNullArea(x + 1, y - 1);
                    OpenNearButton(x + 1, y - 1);
                }
                if (Data.ButtonValue[x, y + 1] == "0" && Data.ButtonStatus[x, y + 1] == 0)
                {
                    Data.ButtonStatus[x, y + 1] = 1;
                    GetNullArea(x, y + 1);
                    OpenNearButton(x, y + 1);
                }
                if (Data.ButtonValue[x + 1, y + 1] == "0" && Data.ButtonStatus[x + 1, y + 1] == 0)
                {
                    Data.ButtonStatus[x + 1, y + 1] = 1;
                    GetNullArea(x + 1, y + 1);
                    OpenNearButton(x + 1, y + 1);
                }
                if (Data.ButtonValue[x + 1, y] == "0" && Data.ButtonStatus[x + 1, y] == 0)
                {
                    Data.ButtonStatus[x + 1, y] = 1;
                    GetNullArea(x + 1, y);
                    OpenNearButton(x + 1, y);
                }
            }
            else if (x == Game.CountX - 1 && y > 0 && y < Game.CountY - 1)
            {
                if (Data.ButtonValue[x - 1, y - 1] == "0" && Data.ButtonStatus[x - 1, y - 1] == 0)
                {
                    Data.ButtonStatus[x - 1, y - 1] = 1;
                    GetNullArea(x - 1, y - 1);
                    OpenNearButton(x - 1, y - 1);
                }
                if (Data.ButtonValue[x, y - 1] == "0" && Data.ButtonStatus[x, y - 1] == 0)
                {
                    Data.ButtonStatus[x, y - 1] = 1;
                    GetNullArea(x, y - 1);
                    OpenNearButton(x, y - 1);
                }
                if (Data.ButtonValue[x - 1, y + 1] == "0" && Data.ButtonStatus[x - 1, y + 1] == 0)
                {
                    Data.ButtonStatus[x - 1, y + 1] = 1;
                    GetNullArea(x - 1, y + 1);
                    OpenNearButton(x - 1, y + 1);
                }
                if (Data.ButtonValue[x, y + 1] == "0" && Data.ButtonStatus[x, y + 1] == 0)
                {
                    Data.ButtonStatus[x, y + 1] = 1;
                    GetNullArea(x, y + 1);
                    OpenNearButton(x, y + 1);
                }
                if (Data.ButtonValue[x - 1, y] == "0" && Data.ButtonStatus[x - 1, y] == 0)
                {
                    Data.ButtonStatus[x - 1, y] = 1;
                    GetNullArea(x - 1, y);
                    OpenNearButton(x - 1, y);
                }
            }
            else if (y == 0 && x > 0 && x < Game.CountX - 1)
            {
                if (Data.ButtonValue[x - 1, y + 1] == "0" && Data.ButtonStatus[x - 1, y + 1] == 0)
                {
                    Data.ButtonStatus[x - 1, y + 1] = 1;
                    GetNullArea(x - 1, y + 1);
                    OpenNearButton(x - 1, y + 1);
                }
                if (Data.ButtonValue[x, y + 1] == "0" && Data.ButtonStatus[x, y + 1] == 0)
                {
                    Data.ButtonStatus[x, y + 1] = 1;
                    GetNullArea(x, y + 1);
                    OpenNearButton(x, y + 1);
                }
                if (Data.ButtonValue[x + 1, y + 1] == "0" && Data.ButtonStatus[x + 1, y + 1] == 0)
                {
                    Data.ButtonStatus[x + 1, y + 1] = 1;
                    GetNullArea(x + 1, y + 1);
                    OpenNearButton(x + 1, y + 1);
                }
                if (Data.ButtonValue[x - 1, y] == "0" && Data.ButtonStatus[x - 1, y] == 0)
                {
                    Data.ButtonStatus[x - 1, y] = 1;
                    GetNullArea(x - 1, y);
                    OpenNearButton(x - 1, y);
                }
                if (Data.ButtonValue[x + 1, y] == "0" && Data.ButtonStatus[x + 1, y] == 0)
                {
                    Data.ButtonStatus[x + 1, y] = 1;
                    GetNullArea(x + 1, y);
                    OpenNearButton(x + 1, y);
                }
            }
            else if (y == Game.CountY - 1 && x > 0 && x < Game.CountX - 1)
            {
                if (Data.ButtonValue[x - 1, y - 1] == "0" && Data.ButtonStatus[x - 1, y - 1] == 0)
                {
                    Data.ButtonStatus[x - 1, y - 1] = 1;
                    GetNullArea(x - 1, y - 1);
                    OpenNearButton(x - 1, y - 1);
                }
                if (Data.ButtonValue[x, y - 1] == "0" && Data.ButtonStatus[x, y - 1] == 0)
                {
                    Data.ButtonStatus[x, y - 1] = 1;
                    GetNullArea(x, y - 1);
                    OpenNearButton(x, y - 1);
                }
                if (Data.ButtonValue[x + 1, y - 1] == "0" && Data.ButtonStatus[x + 1, y - 1] == 0)
                {
                    Data.ButtonStatus[x + 1, y - 1] = 1;
                    GetNullArea(x + 1, y - 1);
                    OpenNearButton(x + 1, y - 1);
                }
                if (Data.ButtonValue[x - 1, y] == "0" && Data.ButtonStatus[x - 1, y] == 0)
                {
                    Data.ButtonStatus[x - 1, y] = 1;
                    GetNullArea(x - 1, y);
                    OpenNearButton(x - 1, y);
                }
                if (Data.ButtonValue[x + 1, y] == "0" && Data.ButtonStatus[x + 1, y] == 0)
                {
                    Data.ButtonStatus[x + 1, y] = 1;
                    GetNullArea(x + 1, y);
                    OpenNearButton(x + 1, y);
                }
            }
            else if (x == 0 && y == 0)
            {
                if (Data.ButtonValue[x, y + 1] == "0" && Data.ButtonStatus[x, y + 1] == 0)
                {
                    Data.ButtonStatus[x, y + 1] = 1;
                    GetNullArea(x, y + 1);
                    OpenNearButton(x, y + 1);
                }
                if (Data.ButtonValue[x + 1, y + 1] == "0" && Data.ButtonStatus[x + 1, y + 1] == 0)
                {
                    Data.ButtonStatus[x + 1, y + 1] = 1;
                    GetNullArea(x + 1, y + 1);
                    OpenNearButton(x + 1, y + 1);
                }
                if (Data.ButtonValue[x + 1, y] == "0" && Data.ButtonStatus[x + 1, y] == 0)
                {
                    Data.ButtonStatus[x + 1, y] = 1;
                    GetNullArea(x + 1, y);
                    OpenNearButton(x + 1, y);
                }
            }
            else if (x == 0 && y == Game.CountY - 1)
            {
                if (Data.ButtonValue[x, y - 1] == "0" && Data.ButtonStatus[x, y - 1] == 0)
                {
                    Data.ButtonStatus[x, y - 1] = 1;
                    GetNullArea(x, y - 1);
                    OpenNearButton(x, y - 1);
                }
                if (Data.ButtonValue[x + 1, y - 1] == "0" && Data.ButtonStatus[x + 1, y - 1] == 0)
                {
                    Data.ButtonStatus[x + 1, y - 1] = 1;
                    GetNullArea(x + 1, y - 1);
                    OpenNearButton(x + 1, y - 1);
                }
                if (Data.ButtonValue[x + 1, y] == "0" && Data.ButtonStatus[x + 1, y] == 0)
                {
                    Data.ButtonStatus[x + 1, y] = 1;
                    GetNullArea(x + 1, y);
                    OpenNearButton(x + 1, y);
                }
            }
            else if (y == 0 && x == Game.CountX - 1)
            {
                if (Data.ButtonValue[x - 1, y + 1] == "0" && Data.ButtonStatus[x - 1, y + 1] == 0)
                {
                    Data.ButtonStatus[x - 1, y + 1] = 1;
                    GetNullArea(x - 1, y + 1);
                    OpenNearButton(x - 1, y + 1);
                }
                if (Data.ButtonValue[x, y + 1] == "0" && Data.ButtonStatus[x, y + 1] == 0)
                {
                    Data.ButtonStatus[x, y + 1] = 1;
                    GetNullArea(x, y + 1);
                    OpenNearButton(x, y + 1);
                }
                if (Data.ButtonValue[x - 1, y] == "0" && Data.ButtonStatus[x - 1, y] == 0)
                {
                    Data.ButtonStatus[x - 1, y] = 1;
                    GetNullArea(x - 1, y);
                    OpenNearButton(x - 1, y);
                }
            }
            else if (x == Game.CountX - 1 && y == Game.CountY - 1)
            {
                if (Data.ButtonValue[x - 1, y - 1] == "0" && Data.ButtonStatus[x - 1, y - 1] == 0)
                {
                    Data.ButtonStatus[x - 1, y - 1] = 1;
                    GetNullArea(x - 1, y - 1);
                    OpenNearButton(x - 1, y - 1);
                }
                if (Data.ButtonValue[x, y - 1] == "0" && Data.ButtonStatus[x, y - 1] == 0)
                {
                    Data.ButtonStatus[x, y - 1] = 1;
                    GetNullArea(x, y - 1);
                    OpenNearButton(x, y - 1);
                }
                if (Data.ButtonValue[x - 1, y] == "0" && Data.ButtonStatus[x - 1, y] == 0)
                {
                    Data.ButtonStatus[x - 1, y] = 1;
                    GetNullArea(x - 1, y);
                    OpenNearButton(x - 1, y);
                }
            }
            OpenNearButton(x, y);
        }

        /// <summary>
        /// Открывает все кнопки (все поле игры)
        /// </summary>
        public static void OpenTableButton()
        {
            for (int y = 0; y < Game.CountY; y++)
            {
                for (int x = 0; x < Game.CountX; x++)
                {
                    Data.ButtonStatus[x, y] = 1;
                }
            }
        }

        /// <summary>
        /// Проверяет условия победы
        /// </summary>
        /// <returns>Возращает true - если победа, false - игра не завершена</returns>
        public static bool WinInGame()
        {
            for (int y = 0; y < Game.CountY; y++)
            {
                for (int x = 0; x < Game.CountX; x++)
                {
                    //false, если
                    //1. под закрытой кнопкой не бомба
                    //2. под флагом не бомба
                    //3. открытая кнопка есть бомба
                    if ((Data.ButtonStatus[x, y] == 0 && Data.ButtonValue[x, y] != "bomb") || 
                        (Data.ButtonStatus[x, y] == 2 && Data.ButtonValue[x, y] != "bomb") || 
                        (Data.ButtonStatus[x, y] == 1 && Data.ButtonValue[x, y] == "bomb"))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
