using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;

namespace Snakes
{
    class Food
    {
        public double x, y;
        public Ellipse ell = new Ellipse();
        public Food(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public void setfoodposition()
        {
            ell.Width = ell.Height = 10;
            ell.Fill = Brushes.Blue;
            Canvas.SetLeft(ell, x);
            Canvas.SetTop(ell, y);
        }
    }
}

