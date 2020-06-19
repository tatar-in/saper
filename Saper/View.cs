using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Saper
{
    /// <summary>
    /// Класс содержит визуальное оформление кнопок
    /// </summary>
    class View
    {
        /// <summary>
        /// Рисует поле игры
        /// </summary>
        /// <param name="g">Графика для рисования</param>
        public static void DrawGame(Graphics g)
        {
            g.FillRectangle(Brushes.Gray, 0, 0, Game.ButtonWidthHeight * Game.CountX, Game.ButtonWidthHeight * Game.CountY);
            for(int i = 0; i <= Game.ButtonWidthHeight * Game.CountX; i += Game.ButtonWidthHeight)
            {
                g.DrawLine(Pens.White, i, 0, i, Game.ButtonWidthHeight * Game.CountY);
            }
            for (int i = 0; i <= Game.ButtonWidthHeight * Game.CountY; i += Game.ButtonWidthHeight)
            {
                g.DrawLine(Pens.White, 0, i, Game.ButtonWidthHeight * Game.CountX, i);
            }
        }

        /// <summary>
        /// Рисует закрытую кнопку
        /// </summary>
        /// <param name="g">Графика для рисования</param>
        /// <param name="x">Положение Х кнопки в массиве всех кнопок</param>
        /// <param name="y">Положение Y кнопки в массиве всех кнопок</param>
        public static void CloseButton(Graphics g, int x, int y)
        {
            g.FillRectangle(Brushes.Gray, x * Game.ButtonWidthHeight, y * Game.ButtonWidthHeight, Game.ButtonWidthHeight, Game.ButtonWidthHeight);
            g.DrawRectangle(Pens.White, x * Game.ButtonWidthHeight, y * Game.ButtonWidthHeight, Game.ButtonWidthHeight, Game.ButtonWidthHeight);
        }

        /// <summary>
        /// Рисуем открытую непустую кнопку и ее значение
        /// </summary>
        /// <param name="g">Графика для рисования</param>
        /// <param name="x">Положение Х кнопки в массиве всех кнопок</param>
        /// <param name="y">Положение Y кнопки в массиве всех кнопок</param>
        public static void OpenButton(Graphics g, int x, int y)
        {
            g.FillRectangle(Brushes.White, x * Game.ButtonWidthHeight, y * Game.ButtonWidthHeight, Game.ButtonWidthHeight, Game.ButtonWidthHeight);
            g.DrawRectangle(Pens.Gray, x * Game.ButtonWidthHeight, y * Game.ButtonWidthHeight, Game.ButtonWidthHeight, Game.ButtonWidthHeight);
            if (Data.ButtonValue[x, y] == "1")
            {
                g.DrawString(Data.ButtonValue[x, y], new Font("Arial", (int)(Game.ButtonWidthHeight * 0.3), FontStyle.Bold), Brushes.Blue, new Point(x * Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight * 0.3), y * Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight * 0.3)));
            }
            else if (Data.ButtonValue[x, y] == "2")
            {
                g.DrawString(Data.ButtonValue[x, y], new Font("Arial", (int)(Game.ButtonWidthHeight * 0.3), FontStyle.Bold), Brushes.Green, new Point(x * Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight * 0.3), y * Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight * 0.3)));
            }
            else if (Data.ButtonValue[x, y] == "3")
            {
                g.DrawString(Data.ButtonValue[x, y], new Font("Arial", (int)(Game.ButtonWidthHeight * 0.3), FontStyle.Bold), Brushes.DarkOrange, new Point(x * Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight * 0.3), y * Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight * 0.3)));
            }
            else
            {
                g.DrawString(Data.ButtonValue[x, y], new Font("Arial", (int)(Game.ButtonWidthHeight * 0.3), FontStyle.Bold), Brushes.Red, new Point(x * Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight * 0.3), y * Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight * 0.3)));
            }
        }

        /// <summary>
        /// Рисует открытую пустую кнопку
        /// </summary>
        /// <param name="g">Графика для рисования</param>
        /// <param name="x">Положение Х кнопки в массиве всех кнопок</param>
        /// <param name="y">Положение Y кнопки в массиве всех кнопок</param>
        public static void NullButton(Graphics g, int x, int y)
        {
            g.FillRectangle(Brushes.LightGray, x * Game.ButtonWidthHeight, y * Game.ButtonWidthHeight, Game.ButtonWidthHeight, Game.ButtonWidthHeight);
        }

        /// <summary>
        /// Рисует бомбу
        /// </summary>
        /// <param name="g">Графика для рисования</param>
        /// <param name="x">Положение Х кнопки в массиве всех кнопок</param>
        /// <param name="y">Положение Y кнопки в массиве всех кнопок</param>
        public static void Bomb(Graphics g, int x,int y)
        {
            g.FillRectangle(Brushes.Red, x * Game.ButtonWidthHeight, y * Game.ButtonWidthHeight, Game.ButtonWidthHeight, Game.ButtonWidthHeight);
            g.FillEllipse(Brushes.Black,
                x * Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight * 0.2),
                y * Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight * 0.2),
                (int)(Game.ButtonWidthHeight * 0.6),
                (int)(Game.ButtonWidthHeight * 0.6));
            g.DrawLine(Pens.Black,
                x * Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight * 0.5),
                y * Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight * 0.1),
                x * Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight * 0.5),
                y * Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight * 0.9));
            g.DrawLine(Pens.Black,
                x * Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight * 0.1),
                y * Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight * 0.5),
                x * Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight * 0.9),
                y * Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight * 0.5));
            g.DrawLine(Pens.Black,
                x * Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight * 0.15),
                y * Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight * 0.15),
                x * Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight * 0.85),
                y * Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight * 0.85));
            g.DrawLine(Pens.Black,
                x * Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight * 0.85),
                y * Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight * 0.15),
                x * Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight * 0.15),
                y * Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight * 0.85));
        }

        /// <summary>
        /// Рисует флаг
        /// </summary>
        /// <param name="g">Графика для рисования</param>
        /// <param name="x">Положение Х кнопки в массиве всех кнопок</param>
        /// <param name="y">Положение Y кнопки в массиве всех кнопок</param>
        public static void Flag(Graphics g, int x, int y)
        {
            g.FillRectangle(Brushes.Yellow, x * Game.ButtonWidthHeight, y * Game.ButtonWidthHeight, Game.ButtonWidthHeight, Game.ButtonWidthHeight);
            g.FillPolygon(Brushes.Red, new Point[] {
                new Point(x* Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight* 0.2), y* Game.ButtonWidthHeight + (int) (Game.ButtonWidthHeight* 0.1)),
                new Point(x* Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight* 0.8), y* Game.ButtonWidthHeight + (int) (Game.ButtonWidthHeight* 0.3)),
                new Point(x* Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight* 0.2), y* Game.ButtonWidthHeight + (int) (Game.ButtonWidthHeight* 0.5))});
            g.DrawLine(Pens.Black,
                x * Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight * 0.2),
                y * Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight * 0.5),
                x * Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight * 0.2),
                y * Game.ButtonWidthHeight + (int)(Game.ButtonWidthHeight * 0.9));
        }
    }
}
