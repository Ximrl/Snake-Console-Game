using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Snake : Line
    {
        char symbol;
        public char Symbol { get { return symbol; } set { symbol = value; } }
        ushort length;
        public ushort Length { get { return length; } set { length = value; } }
        Direction direction;
        public Direction Direction { get { return direction; } set { direction = value; } }
        byte speed;
        public byte Speed { get {
                if (tic <= 250)
                    speed = 1;
                if (tic <= 200)
                    speed = 2;
                if (tic <= 150)
                    speed = 3;
                if (tic <= 100)
                    speed = 4;
                if (tic <= 50)
                    speed = 5;
                return speed; } set { speed = value; } }
        ConsoleColor color;
        public ConsoleColor Color { get { return color; } set { color = value; } }
        Point startpos;
        public Point StartPos { get { return startpos; } set { startpos = value; } }
        bool boost;
        public bool Boost { get { return boost; } set { boost = value; } }

        public Snake(char symbol, ushort length, Point startpos, byte speed, Direction direction, ConsoleColor color, bool boost = false)
        {
            this.symbol = symbol;
            this.length = length;
            this.direction = direction;
            this.speed = speed;
            this.color = color;
            this.startpos = startpos;
            this.boost = boost;
            plist = new List<Point>();
            for (ushort block = 0; block < length; block++)
            {
                Point unit = new Point(startpos, symbol);
                unit.Move(block, direction);
                plist.Add(unit);
            }
            Haste();
        }

        Point GenNextPoint()
        {
            Point head = plist.Last();
            Point nextpoint = new Point(head);
            nextpoint.Move(1, direction);
            return nextpoint;
        }
        public void Move()
        {
            if (CheckTime())
            {
                Point tail = plist.First();
                plist.Remove(tail);
                tail.Clear();
                Point head = GenNextPoint();
                plist.Add(head);
                head.Draw(color);
            }
        }

        public override void Draw()
        {
            Console.ForegroundColor = color;
            base.Draw();
        }

        public void Reaction(ConsoleKey key)
        {
            if ((key == ConsoleKey.UpArrow || key == ConsoleKey.W || key == ConsoleKey.NumPad8) && direction != Direction.DOWN)
            {
                direction = Direction.UP;
            }
            else if ((key == ConsoleKey.DownArrow || key == ConsoleKey.S || key == ConsoleKey.NumPad2) && direction != Direction.UP)
            {
                direction = Direction.DOWN;
            }
            else if ((key == ConsoleKey.LeftArrow || key == ConsoleKey.A || key == ConsoleKey.NumPad4) && direction != Direction.RIGHT)
            {
                direction = Direction.LEFT;
            }
            else if ((key == ConsoleKey.RightArrow || key == ConsoleKey.D || key == ConsoleKey.NumPad6) && direction != Direction.LEFT)
            {
                direction = Direction.RIGHT;
            }
            if (key == ConsoleKey.Spacebar)
            {
                for (sbyte b = -1; b == -1;)
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.Spacebar)
                        b *= -1;
                }
            }
        }

        double tic;
        void Haste()
        {
            switch (speed)
            {
                case 1:
                    tic = 250;
                    break;
                case 2:
                    tic = 200;
                    break;
                case 3:
                    tic = 150;
                    break;
                case 4:
                    tic = 100;
                    break;
                case 5:
                    tic = 50;
                    break;
            }
        }
        
        public void Booster(byte value)
        {
            if (boost == true)
            {
                if (tic > 30)
                    tic -= (tic / 100) * value;
            }
        }

        bool speed_s = false;
        void SpeedCorrection()
        {
            if (speed_s == false && (direction == Direction.UP || direction == Direction.DOWN))
            {
                tic *= 1.5;
                speed_s = true;
            }
            if (speed_s == true && (direction == Direction.LEFT || direction == Direction.RIGHT))
            {
                tic /= 1.5;
                speed_s = false;
            }
        }

        static DateTime timer = DateTime.Now;
        bool CheckTime()
        {
            SpeedCorrection();
            if ((DateTime.Now - timer).TotalMilliseconds > tic)
            {
                timer = DateTime.Now;
                return true;
            }
            else
                return false;
        }

        internal bool Eat(Food food)
        {
            Point head = GenNextPoint();
            if (head.CheckHit(food, "xy"))
            {
                Point nosh = new Point(food);
                nosh.CopyParameter(head, "s");
                plist.Add(nosh);
                nosh.Draw(color);
                return true;
            }
            else
                return false;
        }

        public bool TouchSelf()
        {
            Point head = plist.Last();
            for (ushort n = 0; n < plist.Count - 4; n++)
            {
                if (head.CheckHit((plist[n]), "xy"))
                    return true;
            }
            return false;
        }

        public bool TouchBounds(ushort min_xbound, ushort max_xbound, ushort min_ybound, ushort max_ybound)
        {
            Point head = plist.Last();
            if (head.CheckHit(new Point(min_xbound, min_ybound), "xory") || head.CheckHit(new Point(max_xbound, max_ybound), "xory"))
                return true;
            else
                return false;
        }
    }
}
