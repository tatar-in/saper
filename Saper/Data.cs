using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Saper
{
    /// <summary>
    /// Класс содержит значения кнопок
    /// </summary>
    class Data
    {
        /// <summary>
        /// Получает или задает значение кнопки:
        /// "0" - кнопка пустая,
        /// "1"..."8" - вокруг кнопки указанное количество бомб,
        /// "bomb" - кнопка содержит бомбу
        /// </summary>
        public static string[,] ButtonValue { get; set; }

        /// <summary>
        /// Получает или задает статус кнопки:
        /// 0 - кнопка закрыта,
        /// 1 - кнопка открыта,
        /// 2 - поставлен флаг
        /// </summary>
        public static int[,] ButtonStatus { get; set; }
    }


    
}
