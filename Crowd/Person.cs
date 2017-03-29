using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;

namespace Crowd
{
    class Person
    {
        public System.Timers.Timer delayTimer;
        int timeValue;

        public int TimeValue
        { get { return timeValue; }
            private set { timeValue = value; }
        }

        const int defaultMarginTop = 15;
        const int defaultMarginLeft = 25;

        public Ellipse body;

        int i;
        int j;

        public Cell cell;

        public int I
        {
            get { return i; }
            set
            {
                body.Margin = new System.Windows.Thickness(GetX(value), GetY(j), 0, 0);
                i = value;
            }
        }

        public int J
        {
            get { return j; }
            set
            {
                body.Margin = new System.Windows.Thickness(GetX(i), GetY(value), 0, 0);
                j = value;
            }
        }

        public Person(Cell cell, Canvas canvas)
        {
            this.cell = cell;
            this.i = cell.i;
            this.j = cell.j;
            int x = GetX(i);
            int y = GetY(j);
            body = new Ellipse();
            body.Height = body.Width = 40;
            body.Fill = Brushes.SkyBlue;
            body.StrokeThickness = 0;
            body.Margin = new System.Windows.Thickness(x, y, 0, 0);
            canvas.Children.Add(body);

            delayTimer = new System.Timers.Timer(150);
            delayTimer.Elapsed += delayTimer_Elapsed;
            delayTimer.Start();
        }

        ~Person()
        {
            delayTimer.Stop();
        }

        void delayTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timeValue++;
        }

        private int GetX(int i)
        {
            return defaultMarginLeft + 40 * i;
        }
        private int GetY(int j)
        {
            return defaultMarginTop + 40 * j;
        }
        private int GetI(int x)
        {
            return (x - defaultMarginLeft) / 40;
        }
        private int GetJ(int y)
        {
            return (y - defaultMarginTop) / 40;
        }

        
    }
}
