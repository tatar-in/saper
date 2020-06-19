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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            Text = "Об игре";
            Width = 300;
            Height = 200;
            Label text = new Label();
            text.Location = new Point(20, 20);
            text.Width = Width - 20 * 2;
            text.Height = Height - 20 * 2;
            text.Text = "Игра создана ручками начинающего программиста-самоучки";
            Controls.Add(text);
        }
    }
}
