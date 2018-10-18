using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Map
    {
        ushort width;
        public ushort Width { get { return width; } set { width = value; } }
        ushort height;
        public ushort Height { get { return height; } set { height = value; } }
        ConsoleColor bgcolor;
        public ConsoleColor BackgroundColor { get { return bgcolor; } set { bgcolor = value; } }
        ConsoleColor fgcolor;
        public ConsoleColor ForegroundColor { get { return fgcolor; } set { fgcolor = value; } }
        ushort top_bound;
        public ushort TopBound { get { return top_bound; } }
        ushort left_bound;
        public ushort LeftBound { get { return left_bound; } }

        public Map(ushort width, ushort height, ConsoleColor bgcolor, ConsoleColor fgcolor, ushort top_lines = 0, ushort bottom_lines = 0, ushort left_columns = 0, ushort right_columns = 0)
        {
            Console.Clear();
            this.width = width;
            this.height = height;
            this.bgcolor = bgcolor;
            this.fgcolor = fgcolor;
            Console.CursorVisible = false;
            Console.SetWindowSize(width + left_columns + right_columns + 1, height + top_lines + bottom_lines + 2);
            Console.SetBufferSize(width + left_columns + right_columns + 1, height + top_lines + bottom_lines + 2);
            Console.BackgroundColor = bgcolor;
            Console.ForegroundColor = fgcolor;
            Console.Clear();
        }

        void TopSide()
        {
            top_bound = (ushort)Console.CursorTop;
        }

        void BottomSide()
        {

        }

        void LeftSide()
        {
            left_bound = (ushort)Console.CursorLeft;
        }

        void RigthSide()
        {

        }

        public void Sides()
        {
            TopSide();
            BottomSide();
            LeftSide();
            RigthSide();
        }
    }
}
