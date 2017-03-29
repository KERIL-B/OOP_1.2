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

        static List<Person> people = new List<Person>();

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

            AddDoors(canvas);
        }

        static public void DrawCell(Cell cell, Canvas canvas)
        {
            canvas.Children.Add(cell.rect);
        }

        static public void Add_Person(double x, double y, Canvas canvas)
        {
            int i = (int)((x - 25) / 40);
            int j = (int)((y - 15) / 40);

            if (cells[i, j].isEmpty)
            {
                Person currentPerson = new Person(cells[i, j], canvas);
                people.Add(currentPerson);
                cells[i, j].isEmpty = false;

                PathFinding(currentPerson, canvas);
            }
        }

        static private void PathFinding(Person currentPerson, Canvas canvas)
        {
            string direction;
            do
            { direction = ChooseDirection(currentPerson); }
            while (Move(currentPerson, "up") || (Move(currentPerson, direction)));

            IsExit(currentPerson, canvas);
        }

        static private string ChooseDirection(Person currentPerson)
        {
            string tmpDirection;
            if (currentPerson.I % 5 != 0)
                if (currentPerson.I % 5 < 3)
                    tmpDirection = "left";
                else
                {
                    int needThatI = currentPerson.I + (5 - currentPerson.I % 5) + 1;
                    if (needThatI > cells.GetLength(0))
                        tmpDirection = "left";
                    else
                        tmpDirection = "right";
                }
            else tmpDirection = null;

            return tmpDirection;
        }

        static private bool IsExit(Person currentPerson, Canvas canvas)
        {
            if (currentPerson.cell.haveDoor)
            {
                canvas.Children.Remove(currentPerson.body);
                currentPerson.cell.isEmpty = true;
                people.Remove(currentPerson);
                return true;
            }
            else return false;
        }

        static public void AddDoors(Canvas canvas)
        {
            int n = cells.GetUpperBound(0) + 1; //width (i)
            for (int i = 0; i < n; i++)
            {
                if (cells[i, 0].haveDoor)
                    canvas.Children.Add(cells[i, 0].doorRect);
            }
        }

        static private bool Move(Person person, string direction)
        {
            int deltaI = 0;
            int deltaJ = 0;

            switch (direction)
            {
                case "up": deltaJ = -1;
                    break;
                case "left": deltaI = -1;
                    break;
                case "right": deltaI = 1;
                    break;

                default: return false;
            }

            int oldI = person.I;
            int oldJ = person.J;

            int newI = person.I + deltaI;
            int newJ = person.J + deltaJ;

            if ((newI >= 0) && (newJ >= 0) && (cells[newI, newJ].isEmpty))
            {
                person.cell = cells[newI, newJ];
                cells[person.I, person.J].isEmpty = true;
                person.I = newI;
                person.J = newJ;
                cells[person.I, person.J].isEmpty = false;

                //System.Windows.MessageBox.Show("(" + oldI + ", " + oldJ + ") -> (" + newI + ", " + newJ + ")", direction);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
