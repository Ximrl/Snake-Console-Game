using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Food : Point
    {
        ConsoleColor color;
        public ConsoleColor Color { get { return color; } set { color = value; } }
        ushort min_xbound;
        ushort max_xbound;
        ushort min_ybound;
        ushort max_ybound;

        public Food(ushort min_xbound, ushort max_xbound, ushort min_ybound, ushort max_ybound, char symbol, ConsoleColor color) : base (min_xbound, max_xbound, min_ybound, max_ybound, symbol)
        {
            this.min_xbound = min_xbound;
            this.max_xbound = max_xbound;
            this.min_ybound = min_ybound;
            this.max_ybound = max_ybound;
            this.color = color;
        }

        public Food(Food food) : base (food.min_xbound, food.max_xbound, food.min_ybound, food.max_ybound, food.symbol)
        {
            min_xbound = food.min_xbound;
            max_xbound = food.max_xbound;
            min_ybound = food.min_ybound;
            max_ybound = food.max_ybound;
            color = food.color;
        }

        public Food (Point p, ConsoleColor color) : base (p)
        {
            this.color = color;
        }

        public Food Bypass(Line line)
        {
            List<Point> ObjCore = line.PointsList;
            Food food = new Food(new Point(x, y, symbol), color);
            food.min_xbound = min_xbound;
            food.max_xbound = max_xbound; 
            food.min_ybound = min_ybound;
            food.max_ybound = max_ybound;
            bool cert;
            do
            {
                cert = true;
                foreach (Point p in ObjCore)
                {
                    while(food.CheckHit(p, "xy"))
                    {
                        food = new Food(food);
                        cert = false;
                    }  
                }
            } while (cert != true);
            return food;
        }

        public override void Draw()
        {
            Console.ForegroundColor = color;
            base.Draw();
        }
    }
}
