using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Scripts
    {
         internal static ConsoleColor[] ColorList()
         {
            ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
            return colors;
         }

        internal static Direction[] Directions()
        {
            Direction[] directions = (Direction[])Direction.GetValues(typeof(Direction));
            return directions;
        }

        static ConsoleColor temp_fgcolor;
        static ConsoleColor temp_bgcolor;

        internal static void FalseInput()
        {
            temp_fgcolor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(" Ошибка: Неверное значение.");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(" Повторите ввод: ");
            Console.ForegroundColor = temp_fgcolor;
        }

        internal static ConsoleColor ColorRequest()
        {
            Console.Write(" Ввод: ");
            string answer = (Console.ReadLine()).ToLower();
            ConsoleColor selectedcolor = default;
            bool input = false;
            do
            {
                if (Int32.TryParse(answer, out int numcolor) == true && numcolor <= ColorList().Length + 1 && numcolor != 0)
                {
                    selectedcolor = ColorList()[numcolor - 1];
                    input = true;
                    break;
                }
                if (Int32.TryParse(answer, out numcolor) == false)
                {
                    foreach (ConsoleColor color in ColorList())
                    {
                        if (color.ToString().ToLower() == answer)
                        {
                            selectedcolor = color;
                            input = true;
                            break;
                        }
                    }
                    if (input == true)
                        break;
                    
                }
                FalseInput();
                answer = (Console.ReadLine()).ToLower();
            } while (input != true);
            return selectedcolor;
        }

        internal static int DigitsRequest()
        {
            Console.Write(" Ввод: ");
            string request = Console.ReadLine();
            int digit;
            while(Int32.TryParse(request, out digit) != true)
            {
                FalseInput();
                request = Console.ReadLine();
            }
            if (digit < 0)
            {
                digit *= -1;
            }
            return digit;
        }

        internal static char SymbolRequest()
        {
            Console.Write(" Ввод: ");
            string request = Console.ReadLine();
            char symbol;
            while (Char.TryParse(request, out symbol) != true)
            {
                FalseInput();
                request = Console.ReadLine();
            }
            return symbol;
        }

        internal static ConsoleColor BGColorOffer()
        {
            temp_bgcolor = Console.BackgroundColor;
            temp_fgcolor = Console.ForegroundColor;
            Console.WriteLine(" Выберите цвет из списка ниже:");
            for (int numcolor = 0; numcolor < ColorList().Length; numcolor++)
            {
                if (ColorList()[numcolor] == Console.ForegroundColor)
                {
                    Console.ForegroundColor = temp_bgcolor;
                }
                Console.BackgroundColor = ColorList()[numcolor];
                Console.WriteLine($" [{numcolor +1}] Цвет заднего плана: {ColorList()[numcolor]}    ");
                if (Console.ForegroundColor != temp_fgcolor)
                    Console.ForegroundColor = temp_fgcolor;
            }
            Console.BackgroundColor = temp_bgcolor;
            return ColorRequest();
        }

        internal static ConsoleColor FGColorOffer()
        {
            temp_bgcolor = Console.BackgroundColor;
            temp_fgcolor = Console.ForegroundColor;
            Console.WriteLine(" Выберите цвет из списка ниже:");
            for (int numcolor = 0; numcolor < ColorList().Length; numcolor++)
            {
                if (ColorList()[numcolor] == Console.BackgroundColor)
                {
                    Console.BackgroundColor = temp_fgcolor;
                }
                Console.ForegroundColor = ColorList()[numcolor];
                Console.WriteLine($" [{numcolor + 1}] Цвет символов: {ColorList()[numcolor]}    ");
                if (Console.BackgroundColor != temp_bgcolor)
                    Console.BackgroundColor = temp_bgcolor;
            }
            Console.ForegroundColor = temp_fgcolor;
            return ColorRequest();
        }

        internal static bool Apply_Continue()
        {
            Console.WriteLine("\n Принять и продолжить? (y/n)");
            Console.Write(" Ввод: ");
            string next = (Console.ReadLine()).ToLower();
            while (next != "y" && next != "n")
            {
                FalseInput();
                next = (Console.ReadLine()).ToLower();
            }
            if (next == "y")
                return true;
            else
                return false;
        }

        internal static string Answer_Input()
        {
            Console.Write(" Ввод: ");
            string set = (Console.ReadLine()).ToLower();
            while (set != "y" && set != "n")
            {
                FalseInput();
                set = (Console.ReadLine()).ToLower();
            }
            return set;
        }

        internal static int SmallerDigit(int digit1, int digit2)
        {
            if (digit1 < digit2)
                return digit1;
            else
                return digit2;
        }

        internal static Direction RandomDirection(Point p)
        {
            int exception = 4;
            int safezone = Properties.Settings.Default.SnakeLength + Properties.Settings.Default.SnakeSpeed;
            if (p.Y < Properties.Settings.Default.MapTopBound + safezone && p.X < Properties.Settings.Default.MapLeftBound + safezone)
                exception = 102;
            else if (p.Y < Properties.Settings.Default.MapTopBound + safezone && p.X > Properties.Settings.Default.MapWidth - safezone)
                exception = 103;
            else if (p.Y > Properties.Settings.Default.MapHeight - safezone && p.X < Properties.Settings.Default.MapLeftBound + safezone)
                exception = 112;
            else if (p.Y > Properties.Settings.Default.MapHeight - safezone && p.X > Properties.Settings.Default.MapWidth - safezone)
                exception = 113;
            else if (p.Y < Properties.Settings.Default.MapTopBound + safezone)
                exception = 0;
            else if (p.Y > Properties.Settings.Default.MapHeight - safezone)
                exception = 1;
            else if (p.X < Properties.Settings.Default.MapLeftBound + safezone)
                exception = 2;
            else if (p.X > Properties.Settings.Default.MapWidth - safezone)
                exception = 3;
            int direction = new Random().Next(Directions().Length);
            if (exception < 4)
            {
                while (direction == exception)
                    direction = new Random().Next(Directions().Length);
                return Directions()[direction];
            }
            else if (exception > 4)
            {
                List<Direction> RangeDir = new List<Direction>(Directions());
                if (exception == 102)
                {
                    RangeDir.Remove(Direction.UP);
                    RangeDir.Remove(Direction.LEFT);
                }
                if (exception == 103)
                {
                    RangeDir.Remove(Direction.UP);
                    RangeDir.Remove(Direction.RIGHT);
                }
                if (exception == 112)
                {
                    RangeDir.Remove(Direction.DOWN);
                    RangeDir.Remove(Direction.LEFT);
                }
                if (exception == 113)
                {
                    RangeDir.Remove(Direction.DOWN);
                    RangeDir.Remove(Direction.RIGHT);
                }
                direction = new Random().Next(RangeDir.Count);
                return RangeDir[direction];
            }
            else
                return Directions()[direction];
        }

        internal static void SizingWindows(ushort width, out ushort available_width, ushort height, out ushort available_height)
        {
            available_width = (width > (ushort)Console.LargestWindowWidth - 1) ? (ushort)(Console.LargestWindowWidth - 1) : width;
            available_height = (height > (ushort)Console.LargestWindowHeight - 1) ? (ushort)(Console.LargestWindowHeight - 1) : height;
        }

        internal static void PastInMiddle_H(ushort indent, ushort width, char symbol, string text, ushort numchars, ConsoleColor color)
        {
            temp_fgcolor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            for (int x = indent; x < width; x++)
            {
                for (; x >= ((width - indent) - numchars) / 2 && x <= ((width - indent) + numchars) / 2; x++)
                {
                    if(x == ((width - indent) - numchars) / 2)
                    {
                        Console.Write(text);
                    }
                }
                Console.Write(symbol);
            }
            Console.ForegroundColor = temp_fgcolor;
            Console.WriteLine();
        }

        internal static bool EscapeGame(ConsoleKey key)
        {
            if (key == ConsoleKey.Escape)
            {

                return true;
            }
            else
                return false;
        }

        static int score_cursor_x;
        static int score_cursor_y;
        internal static void InfoTable( ConsoleColor text, ConsoleColor point, ushort score = 0)
        {
            temp_fgcolor = Console.ForegroundColor;
            if (score == 0)
            {
                Console.ForegroundColor = text;
                Console.Write($" КОЛИЧЕСТВО ОЧКОВ:\t");
                score_cursor_x = Console.CursorLeft;
                score_cursor_y = Console.CursorTop;
                Console.ForegroundColor = point;
                Console.WriteLine(score);
                Console.ForegroundColor = temp_fgcolor;
            }
            else
            {
                Console.ForegroundColor = point;
                Console.SetCursorPosition(score_cursor_x, score_cursor_y);
                Console.Write(score);
                Console.ForegroundColor = temp_fgcolor;
            }
        }

        internal static void Status(bool status)
        {
            temp_fgcolor = Console.ForegroundColor;
            if (status == true)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("Включено\n");
                Console.ForegroundColor = temp_fgcolor;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("Отключено\n");
                Console.ForegroundColor = temp_fgcolor;
            }
        }
    }
}
