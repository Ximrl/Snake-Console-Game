using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Point
    {
        protected ushort x;
        public ushort X { get { return x; } }
        protected ushort y;
        public ushort Y { get { return y; } }
        protected char symbol;
        public char Symbol { get { return symbol; } }

        internal Point()
        {

        }

        public Point(ushort x, ushort y, char symbol = '\0')
        {
            this.x = x;
            this.y = y;
            this.symbol = symbol;
        }

        public Point(Point p)
        {
            x = p.x;
            y = p.y;
            symbol = p.symbol;
        }

        public Point(Point p, char symbol)
        {
            x = p.x;
            y = p.y;
            this.symbol = symbol;
        }

        public Point(ushort min_xbound, ushort max_xbound, ushort min_ybound, ushort max_ybound, char symbol = '\0')
        {
            Random randpoint = new Random();
            x = (ushort)randpoint.Next(min_xbound, max_xbound);
            y = (ushort)randpoint.Next(min_ybound, max_ybound);
            this.symbol = symbol;
        }
        
        public virtual void Draw()
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }

        public void Draw(ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }

        public void Move(ushort offset, Direction direction)
        {
            switch (direction)
            {
                case Direction.UP:
                    y = (ushort)(y - offset);
                    break;
                case Direction.RIGHT:
                    x = (ushort)(x + offset);
                    break;
                case Direction.DOWN:
                    y = (ushort)(y + offset);
                    break;
                case Direction.LEFT:
                    x = (ushort)(x - offset);
                    break;
            }
        }

        public void Clear()
        {
            symbol = ' ';
            Draw();
        }

        public bool CheckHit(Point p, string mod)
        {
            if (mod == "xy" || mod == "yx")
                return p.x == x && p.y == y;
            else if (mod == "xory" || mod == "yorx")
                return p.x == x || p.y == y;
            else
                return false;
        }

        public void CopyParameter(Point p, string mod)
        {
            if (mod == "xy" || mod == "yx")
            {
                x = p.x;
                y = p.y;
            }
            else if (mod == "xs" || mod == "sx")
            {
                x = p.x;
                symbol = p.symbol;
            }
            else if (mod == "ys" || mod == "sy")
            {
                y = p.y;
                symbol = p.symbol;
            }
            else if (mod == "x")
                x = p.x;
            else if (mod == "y")
                y = p.y;
            else if (mod == "s")
                symbol = p.symbol;
        }

        public override string ToString()
        {
            return x + ", " + y + ", " + symbol;
        }
    }
}
