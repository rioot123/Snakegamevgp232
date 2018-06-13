using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;


namespace Snakes
{
    class GameMap
    {
        public double x, y;
        public Rectangle rec = new Rectangle();
        public GameMap(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public void setwallposition()
        {
            rec.Width = rec.Height = 10;
            rec.Fill = Brushes.Black;
            Canvas.SetLeft(rec, x);
            Canvas.SetTop(rec, y);
        }
    }
}
