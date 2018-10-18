using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            //Title of Applycation
            Console.Title = "Snake";

            //Scripts
            Scripts.SizingWindows(80, out ushort dialogs_width, 25, out ushort dialogs_height);
            Properties.Settings.Default.DialogsWindowsWidth = dialogs_width;
            Properties.Settings.Default.DialogsWindowsHeight = dialogs_height;
            do
            {
                //MainMenu
                Dialogs.MainMenu();

                // Initialization  of Map
                Map map = new Map(Properties.Settings.Default.MapWidth, Properties.Settings.Default.MapHeight, Properties.Settings.Default.MapBGColor, Properties.Settings.Default.MapFGColor, 1, 0, 1, 0);
                Scripts.InfoTable(Properties.Settings.Default.InfoScoreCount, Properties.Settings.Default.InfoScorePoint);
                Console.CursorLeft += 1;
                map.Sides();
                Properties.Settings.Default.MapLeftBound = map.LeftBound;
                Properties.Settings.Default.MapTopBound = map.TopBound;

                // Initialization of Bound
                Bound frame = new Bound(Properties.Settings.Default.MapLeftBound, Properties.Settings.Default.MapTopBound, Properties.Settings.Default.MapWidth, Properties.Settings.Default.MapHeight, Properties.Settings.Default.BoundHSymbol, Properties.Settings.Default.BoundVSymbol, Properties.Settings.Default.BoundBGColor, Properties.Settings.Default.BoundFGColor);
                frame.Draw();

                // Initialization of Snake
                Point startp = new Point((ushort)(Properties.Settings.Default.MapLeftBound + 1), (ushort)(Properties.Settings.Default.MapWidth - 1), (ushort)(Properties.Settings.Default.MapTopBound + 1), (ushort)(Properties.Settings.Default.MapHeight - 1));
                Snake snake = new Snake(Properties.Settings.Default.SnakeSymbol, Properties.Settings.Default.SnakeLength, startp, Properties.Settings.Default.SnakeSpeed, Scripts.RandomDirection(startp), Properties.Settings.Default.SnakeColor);
                snake.Boost = Properties.Settings.Default.SnakeBoost;
                snake.Draw();

                // Initialization of Food
                Food food = new Food((ushort)(Properties.Settings.Default.MapLeftBound + 1), (ushort)(Properties.Settings.Default.MapWidth - 1), (ushort)(Properties.Settings.Default.MapTopBound + 1), (ushort)(Properties.Settings.Default.MapHeight - 1), Properties.Settings.Default.FoodSymbol, Properties.Settings.Default.FoodColor);
                food = new Food(food.Bypass(snake));
                food.Draw();

                // Initialization of Variables
                ConsoleKeyInfo keys;
                bool escape = false;
                ushort point = 0;

                // Initialization Process
                Thread.Sleep(250);
                while (snake.TouchSelf() == false && snake.TouchBounds(map.LeftBound, map.Width, map.TopBound, map.Height) == false)
                {
                    if (Console.KeyAvailable)
                    {
                        keys = Console.ReadKey(true);
                        if (Scripts.EscapeGame(keys.Key) == true)
                        {
                            escape = true;
                            break;
                        }
                        snake.Reaction(keys.Key);
                    }
                    if (snake.Eat(food))
                    {
                        food = new Food(food.Bypass(snake));
                        food.Draw();
                        Scripts.InfoTable(Properties.Settings.Default.InfoScoreCount, Properties.Settings.Default.InfoScorePoint, ++point);
                        snake.Booster(5);
                    }
                    snake.Move();
                }
                //Game Over
                if (escape == false)
                    Dialogs.GameOverLogo();
            } while (true);
        }
    }
}
