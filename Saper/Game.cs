using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Saper
{
    /// <summary>
    /// Класс содержит параметры игры
    /// </summary>
    class Game
    {
        /// <summary>
        /// Получает или задает количество кнопок по оси Х
        /// </summary>
        public static int CountX { get => countX; set => countX = value; }
        private static int countX = 10;
        
        /// <summary>
        /// Получает или задает количество кнопок по оси У
        /// </summary>
        public static int CountY { get => countY; set => countY = value; }
        private static int countY = 10;
        
        /// <summary>
        /// Получает или задает количество бомб в игре
        /// </summary>
        public static int CountBomb { get => countBomb; set => countBomb = value; }
        private static int countBomb = 10;
        
        /// <summary>
        /// Получает или задает размер кнопки
        /// </summary>
        public static int ButtonWidthHeight { get => buttonWidthHeight; set => buttonWidthHeight = value; }
        private static int buttonWidthHeight = 30;

        /// <summary>
        /// Получает или задает начальную точку позиции игры
        /// </summary>
        public static Point StartPointGame { get => startPointGame; set => startPointGame = value; }
        private static Point startPointGame = new Point(0, 30);
    }
}
