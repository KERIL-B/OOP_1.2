using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Threading;

namespace Crowd
{
    static class DrawGreed
    {
        static Cell[,] cells;

        static Random rnd = new Random();

        static List<Thread> thrdList = new List<Thread>();

        static public void CreateGrid(int widthC, int heightC, Canvas canvas)
        {
            cells = new Cell[widthC, heightC];
            for (int i = 0; i < widthC; i++)
                for (int j = 0; j < heightC; j++)
                {
                    cells[i, j] = new Cell(i, j);
                    DrawCell(cells[i, j], canvas);
                }

            AddDoors(widthC / 5, canvas);
        }

        static public void DrawCell(Cell cell, Canvas canvas)
        {
            canvas.Children.Add(cell.rect);
        }

        static public void Add_Remove_Person(double x, double y, Canvas canvas)
        {
            int i = (int)((x - 25) / 40);
            int j = (int)((y - 15) / 40);
            if (cells[i, j].isEmpty)
            {
                cells[i, j].person = new Person(i,j, canvas);
                System.Windows.MessageBox.Show(i + "." + j);
                Move(cells[i,j],cells[0,0]);
                /*thrdList.Add(new Thread(() =>
                    {
                        System.Windows.MessageBox.Show("ПОТОК ПОШЕЛ");
                        
                    }));
                thrdList[thrdList.Count - 1].Start();
                */
            }
            else
            {
                canvas.Children.Remove(cells[i, j].person.body);
                cells[i, j].person = null;
            }
        }

        static public void AddDoors(int n, Canvas canvas)
        {
            int x = cells.GetUpperBound(0) + 1; //width (i)
            n = (n > x) ? (x) : (n);
            for (int i = 0; i < n; i++)
            {
                cells[i * 5, 1].haveDoor = true;
                canvas.Children.Add(cells[i * 5, 0].doorRect);
            }
        }

        static private bool Move(Cell fromCell, Cell toCell)
        {
            if (toCell.isEmpty)
            {
                toCell.person = fromCell.person;
                fromCell.person = null;
                toCell.person.I = toCell.i;
                toCell.person.J = toCell.j;
                System.Windows.MessageBox.Show(toCell.person.I + "." + toCell.person.J);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
