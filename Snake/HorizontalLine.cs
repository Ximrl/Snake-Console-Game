using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class HorizontalLine : Line
    {
        internal HorizontalLine(ushort xLeft, ushort xRight, ushort y, char symbol)
        {
            plist = new List<Point>();
            for (ushort x = xLeft; x <= xRight; x++)
            {
                Point p = new Point(x, y, symbol);
                PointsList.Add(p);
            }
        }
    }
}
