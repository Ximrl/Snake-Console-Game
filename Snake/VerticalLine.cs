using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class VerticalLine : Line
    {
        internal VerticalLine(ushort x, ushort yTop, ushort yBottom, char symbol)
        {
            plist = new List<Point>();
            for (ushort y = yTop; y <= yBottom; y++)
            {
                Point p = new Point(x, y, symbol);
                PointsList.Add(p);
            }
        }
    }
}
