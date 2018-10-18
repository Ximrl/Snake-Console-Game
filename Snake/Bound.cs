using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Bound
    {
        char hsymbol;
        public char HorizontalSymbol { get { return hsymbol; } set { hsymbol = value; } }
        char vsymbol;
        public char VerticalSymbol { get { return vsymbol; } set { vsymbol = value; } }
        ConsoleColor bgcolor;
        public ConsoleColor BackgroundColor { get { return bgcolor; } set { bgcolor = value; } }
        ConsoleColor fgcolor;
        public ConsoleColor ForegroundColor { get { return fgcolor; } set { fgcolor = value; } }
        List<Line> wlist;

        internal Bound(ushort h_startp, ushort v_startp, ushort mapWidth, ushort mapHeight, char hsymbol, char vsymbol, ConsoleColor bgcolor, ConsoleColor fgcolor)
        {
            wlist = new List<Line>();
            this.hsymbol = hsymbol;
            this.vsymbol = vsymbol;
            this.bgcolor = bgcolor;
            this.fgcolor = fgcolor;
            HorizontalLine topside = new HorizontalLine((ushort)(h_startp + 1), (ushort)(mapWidth - 1), v_startp, hsymbol);
            HorizontalLine bottomside = new HorizontalLine((ushort)(h_startp + 1), (ushort)(mapWidth - 1), mapHeight, hsymbol);
            VerticalLine leftside = new VerticalLine(h_startp, v_startp, mapHeight, vsymbol);
            VerticalLine rightside = new VerticalLine(mapWidth, v_startp, mapHeight, vsymbol);
            wlist.Add(topside);
            wlist.Add(bottomside);
            wlist.Add(leftside);
            wlist.Add(rightside);
        }

        public void Draw()
        {
            ConsoleColor temp_bgcolor = Console.BackgroundColor;
            ConsoleColor temp_fgcolor = Console.ForegroundColor;
            Console.BackgroundColor = bgcolor;
            Console.ForegroundColor = fgcolor;
            foreach (Line wall in wlist)
                wall.Draw();
            Console.BackgroundColor = temp_bgcolor;
            Console.ForegroundColor = temp_fgcolor;
        }
    }
}
