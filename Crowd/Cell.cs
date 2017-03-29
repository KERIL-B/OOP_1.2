using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;

namespace Crowd
{
    class Cell
    {
        public Rectangle rect;
        public Rectangle doorRect;
        const int width = 40;
        const int height = 40;
        const int defaultMarginTop = 15;
        const int defaultMarginLeft = 25;
        int x;
        int y;
        public int i;
        public int j;

        public bool haveDoor;
        public bool isEmpty;

        public Cell(int i, int j)
        {
            this.i = i;
            this.j = j;
            x = defaultMarginLeft + width * i;
            y = defaultMarginTop + height * j;
            rect = new Rectangle();
            rect.Height = height;
            rect.Width = width;
            rect.Margin = new System.Windows.Thickness(x, y, 0, 0);
            rect.Stroke = Brushes.DeepSkyBlue;

            if ((j == 0) && (i % 5 == 0))
            {
                haveDoor = true;
                doorRect = new Rectangle();
                doorRect.Height = 10;
                doorRect.Width = width;
                doorRect.Margin = new System.Windows.Thickness(x, y - 10, 0, 0);
                doorRect.Fill = Brushes.Black;
            }
            else
                haveDoor = false;
            isEmpty = true;
        }
    }
}
