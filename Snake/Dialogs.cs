using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Snake
{
    class Dialogs
    {
        static string answ_input;
        static ConsoleColor temp_fgcolor;
        static ConsoleColor temp_bgcolor;

        static void MapSets(string message = null)
        {
            Console.Clear();
            temp_bgcolor = Console.BackgroundColor;
            temp_fgcolor = Console.ForegroundColor;
            Scripts.PastInMiddle_H((ushort)Console.CursorLeft, Properties.Settings.Default.DialogsWindowsWidth, '=', " КАРТА ", 7, ConsoleColor.Yellow);
            Console.WriteLine(" Доступные параметры для изменения:\n" +
                $" Размер карты (--size): (ширина х высота) {Properties.Settings.Default.MapWidth} x {Properties.Settings.Default.MapHeight}.");
            Console.BackgroundColor = Properties.Settings.Default.MapBGColor;
            Console.ForegroundColor = Properties.Settings.Default.MapFGColor;
            Console.WriteLine($" Цвет (--color): заднего плана - {Properties.Settings.Default.MapBGColor} и символов  - {Properties.Settings.Default.MapFGColor}.");
            Console.BackgroundColor = temp_bgcolor;
            Console.ForegroundColor = temp_fgcolor;
            Console.WriteLine(" Для применения настроек по-умолчанию к группе параметров введите '--default'.\n" +
                " Введите '--menu' или '--sets' для перехода к главному меню или к настройкам.\n");
            Scripts.PastInMiddle_H((ushort)Console.CursorLeft, Properties.Settings.Default.DialogsWindowsWidth, '*', " Terminal ", 10, ConsoleColor.DarkCyan);
            Console.Write(message);
            Console.Write(" Ввод: ");
            string input = Console.ReadLine().ToLower();
            while (input != "--size" && input != "--color" && input != "--default" && input != "--menu" && input != "--sets")
            {
                Scripts.FalseInput();
                input = Console.ReadLine().ToLower();
            }
            if (input == "--size")
            {
                MapSize();
                MapSets(" Размер карты задан.\n");
            }
            else if (input == "--color")
            {
                MapColor();
                MapSets(" Цвет карты задан.\n");
            }
            else if (input == "--default")
            {
                MapDefault();
                MapSets(" Параметры карты заданы по-умолчанию.\n");
            }
            else if (input == "--menu")
                MainMenu();
            else if (input == "--sets")
                SettingsMenu();
        }

        static void MapDefault()
        {
            Properties.Settings.Default.MapWidth = 50;
            Properties.Settings.Default.MapHeight = 20;
            Properties.Settings.Default.MapBGColor = ConsoleColor.White;
            Properties.Settings.Default.MapFGColor = ConsoleColor.Black;
        }

        static void MapSize()
        {
            do
            {
                Console.WriteLine("\n Задать размер игрового поля по умолчанию? (y/n)");
                answ_input = Scripts.Answer_Input();
                if (answ_input == "n")
                {
                    Console.WriteLine($"\n Введите ширину игрового поля.\n Доступный диапазон от {0 + 30} до {Console.LargestWindowWidth - 1 - 1}.");
                    Properties.Settings.Default.MapWidth = (ushort)Scripts.DigitsRequest();
                    while(Properties.Settings.Default.MapWidth < 0 + 30 || Properties.Settings.Default.MapWidth > Console.LargestWindowWidth - 1)
                    {
                        Scripts.FalseInput();
                        Console.WriteLine();
                        Properties.Settings.Default.MapWidth = (ushort)Scripts.DigitsRequest();
                    }
                    Console.WriteLine($"\n Введите высоту игрового поля.\n Доступный диапазон от {0 + 10} до {Console.LargestWindowHeight - 2 - 1}.");
                    Properties.Settings.Default.MapHeight = (ushort)Scripts.DigitsRequest();
                    while(Properties.Settings.Default.MapHeight < 0 + 10 || Properties.Settings.Default.MapHeight > Console.LargestWindowHeight - 2)
                    {
                        Scripts.FalseInput();
                        Console.WriteLine();
                        Properties.Settings.Default.MapHeight = (ushort)Scripts.DigitsRequest();
                    }
                }
                else if (answ_input == "y")
                {
                    Console.WriteLine("\n Размер игрового поля будет задан по-умолчанию.");
                    Properties.Settings.Default.MapWidth = 50;
                    Properties.Settings.Default.MapHeight = 20;
                }
            }
            while (Scripts.Apply_Continue() != true);
        }

        static void MapColor()
        {
            do
            {
                Console.WriteLine("\n Задать цвет игрового поля и его символов по-умолчанию? (y/n)");
                answ_input = Scripts.Answer_Input();
                if (answ_input == "n")
                {
                    Properties.Settings.Default.MapBGColor = Scripts.BGColorOffer();
                    Properties.Settings.Default.MapFGColor = Scripts.FGColorOffer();
                }
                else if (answ_input == "y")
                {
                    Console.WriteLine("\n Цвета игрового поля будут заданы по-умолчанию.");
                    Properties.Settings.Default.MapBGColor = ConsoleColor.White;
                    Properties.Settings.Default.MapFGColor = ConsoleColor.Black;
                }
            }
            while (Scripts.Apply_Continue() != true);
        }

        static void BoundSets(string message = null)
        {
            Console.Clear();
            temp_bgcolor = Console.BackgroundColor;
            temp_fgcolor = Console.ForegroundColor;
            Scripts.PastInMiddle_H((ushort)Console.CursorLeft, Properties.Settings.Default.DialogsWindowsWidth, '=', " ОГРАЖДЕНИЕ ", 12, ConsoleColor.Yellow);
            Console.WriteLine(" Доступные параметры для изменения:\n" +
                $" Символы (--symbol): по-горизонтали - '{Properties.Settings.Default.BoundHSymbol}' и по-вертикали - '{Properties.Settings.Default.BoundVSymbol}'.");
            Console.BackgroundColor = Properties.Settings.Default.BoundBGColor;
            Console.ForegroundColor = Properties.Settings.Default.BoundFGColor;
            Console.WriteLine($" Цвет (--color): заднего плана - {Properties.Settings.Default.BoundBGColor} и символов  - {Properties.Settings.Default.BoundFGColor}.");
            Console.BackgroundColor = temp_bgcolor;
            Console.ForegroundColor = temp_fgcolor;
            Console.WriteLine(" Для применения настроек по-умолчанию к группе параметров введите '--default'.\n" +
                " Введите '--menu' или '--sets' для перехода к главному меню или к настройкам.\n");
            Scripts.PastInMiddle_H((ushort)Console.CursorLeft, Properties.Settings.Default.DialogsWindowsWidth, '*', " Terminal ", 10, ConsoleColor.DarkCyan);
            Console.Write(message);
            Console.Write(" Ввод: ");
            string input = Console.ReadLine().ToLower();
            while (input != "--symbol" && input != "--color" && input != "--default" && input != "--menu" && input != "--sets")
            {
                Scripts.FalseInput();
                input = Console.ReadLine().ToLower();
            }
            if (input == "--symbol")
            {
                BoundSymbol();
                BoundSets(" Символы ограждения заданы.\n");
            }
            else if (input == "--color")
            {
                BoundColor();
                BoundSets(" Цвет ограждения задан.\n");
            }
            else if (input == "--default")
            {
                BoundDefault();
                BoundSets(" Параметры ограждения заданы по-умолчанию.\n");
            }
            else if (input == "--menu")
                MainMenu();
            else if (input == "--sets")
                SettingsMenu();
        }

        static void BoundDefault()
        {
            Properties.Settings.Default.BoundHSymbol = '~';
            Properties.Settings.Default.BoundVSymbol = '|';
            Properties.Settings.Default.BoundBGColor = Properties.Settings.Default.MapBGColor;
            Properties.Settings.Default.BoundFGColor = Properties.Settings.Default.MapFGColor;
        }

        static void BoundSymbol()
        {
            string same_symbols;
            do
            {
                Console.WriteLine("\n Задать символы ограждения по умолчанию? (y/n)");
                answ_input = Scripts.Answer_Input();
                if (answ_input == "n")
                {
                    Console.WriteLine("\n Задать одинаковыми горизонтальные и вертикальные символы ограждения? (y/n)");
                    same_symbols = Scripts.Answer_Input();
                    if (same_symbols == "y")
                    {
                        Console.WriteLine("\n Задайте символ для горизонтальной и вертикальной линии ограждения.");
                        Properties.Settings.Default.BoundHSymbol = Scripts.SymbolRequest();
                        Properties.Settings.Default.BoundVSymbol = Properties.Settings.Default.BoundHSymbol;
                    }
                    else if (same_symbols == "n")
                    {
                        Console.WriteLine("\n Задайте символ горизонтальной линии ограждения.");
                        Properties.Settings.Default.BoundHSymbol = Scripts.SymbolRequest();
                        Console.WriteLine("\n Задайте символ вертикальной линии ограждения.");
                        Properties.Settings.Default.BoundVSymbol = Scripts.SymbolRequest();
                    }
                }
                else if (answ_input == "y")
                {
                    Console.WriteLine("\n Символы ограждения будут заданы по-умолчанию.");
                    Properties.Settings.Default.BoundHSymbol = '~';
                    Properties.Settings.Default.BoundVSymbol = '|';
                }
            }
            while (Scripts.Apply_Continue() != true);
        }

        static void BoundColor()
        {
            do
            {
                Console.WriteLine("\n Задать цвет символов и заднего плана ограждения по-умолчанию? (y/n)");
                answ_input = Scripts.Answer_Input();
                if (answ_input == "n")
                {
                    Properties.Settings.Default.BoundBGColor = Scripts.BGColorOffer();
                    Properties.Settings.Default.BoundFGColor = Scripts.FGColorOffer();
                }
                else if (answ_input == "y")
                {
                    Console.WriteLine("\n Цвета будут заданы по-умолчанию.");
                    Properties.Settings.Default.BoundBGColor = Properties.Settings.Default.MapBGColor;
                    Properties.Settings.Default.BoundFGColor = Properties.Settings.Default.MapFGColor;
                }
            }
            while (Scripts.Apply_Continue() != true);
        }

        static void SnakeSets(string message = null)
        {
            Console.Clear();
            temp_bgcolor = Console.BackgroundColor;
            temp_fgcolor = Console.ForegroundColor;
            Scripts.PastInMiddle_H((ushort)Console.CursorLeft, Properties.Settings.Default.DialogsWindowsWidth, '=', " ЗМЕЙКА ", 8, ConsoleColor.Yellow);
            Console.Write(" Доступные параметры для изменения:\n" +
                $" Символ (--symbol): '{Properties.Settings.Default.SnakeSymbol}'     " +
                $" Длина (--length): {Properties.Settings.Default.SnakeLength}     " +
                $" Скорость (--speed): {Properties.Settings.Default.SnakeSpeed}\n" +
                $" Ускорение (--boost): ");
            Scripts.Status(Properties.Settings.Default.SnakeBoost);
            Console.BackgroundColor = Properties.Settings.Default.MapBGColor;
            Console.ForegroundColor = Properties.Settings.Default.SnakeColor;
            Console.WriteLine($" Цвет (--color): {Properties.Settings.Default.SnakeColor}.");
            Console.ForegroundColor = temp_fgcolor;
            Console.BackgroundColor = temp_bgcolor;
            Console.WriteLine(" Для применения настроек по-умолчанию к группе параметров введите '--default'.\n" +
                " Введите '--menu' или '--sets' для перехода к главному меню или к настройкам.\n");
            Scripts.PastInMiddle_H((ushort)Console.CursorLeft, Properties.Settings.Default.DialogsWindowsWidth, '*', " Terminal ", 10, ConsoleColor.DarkCyan);
            Console.Write(message);
            Console.Write(" Ввод: ");
            string input = Console.ReadLine().ToLower();
            while (input != "--symbol" && input != "--length" && input != "--speed" && input != "--color" && input != "--boost" && input != "--default" && input != "--menu" && input != "--sets")
            {
                Scripts.FalseInput();
                input = Console.ReadLine().ToLower();
            }
            if (input == "--symbol")
            {
                SnakeSymbol();
                SnakeSets(" Символ змейки задан.\n");
            }
            else if (input == "--length")
            {
                SnakeLength();
                SnakeSets(" Длина змейки задана.\n");
            }
            else if (input == "--speed")
            {
                SnakeSpeed();
                SnakeSets(" Скорость змейки задана.\n");
            }
            else if (input == "--color")
            {
                SnakeColor();
                SnakeSets(" Цвет змейки задан.\n");
            }
            else if (input == "--boost")
            {
                SnakeBoost();
                SnakeSets(" Параметр 'Ускорение' - установлен.\n");
            }
            else if (input == "--default")
            {
                SnakeDefault();
                SnakeSets(" Параметры змейки заданы по-умолчанию.\n");
            }
            else if (input == "--menu")
                MainMenu();
            else if (input == "--sets")
                SettingsMenu();
        }

        static void SnakeDefault()
        {
            Properties.Settings.Default.SnakeSymbol = '+';
            Properties.Settings.Default.SnakeLength = 4;
            Properties.Settings.Default.SnakeSpeed = 3;
            Properties.Settings.Default.SnakeColor = ConsoleColor.Red;
            Properties.Settings.Default.SnakeBoost = false;
        }

        static void SnakeSymbol()
        {
            do
            {
                Console.WriteLine("\n Задать символ змейки по-умолчанию? (y/n)");
                answ_input = Scripts.Answer_Input();
                if (answ_input == "n")
                {
                    Console.WriteLine("\n Задайте символ змейки.");
                    Properties.Settings.Default.SnakeSymbol = Scripts.SymbolRequest();
                }
                else if (answ_input == "y")
                {
                    Console.WriteLine("\n Символ змейки будет принят по-умолчанию.");
                    Properties.Settings.Default.SnakeSymbol = '+';
                }
            }
            while (Scripts.Apply_Continue() != true);
        }

        static void SnakeLength()
        {
            do
            {
                Console.WriteLine("\n Задать длину змейки по-умолчанию? (y/n)");
                answ_input = Scripts.Answer_Input();
                if (answ_input == "n")
                {
                    Console.WriteLine($"\n Задайте длину змейки.\n Доступный диапазон от {0 + 2} до {Scripts.SmallerDigit(Console.LargestWindowWidth / 2, Console.LargestWindowHeight / 2)}.");
                    Properties.Settings.Default.SnakeLength = (ushort)Scripts.DigitsRequest();
                    while(Properties.Settings.Default.SnakeLength > Scripts.SmallerDigit(Console.LargestWindowWidth / 2, Console.LargestWindowHeight / 2))
                    {
                        Scripts.FalseInput();
                        Console.WriteLine();
                        Properties.Settings.Default.SnakeLength = (ushort)Scripts.DigitsRequest();
                    }
                }
                else if (answ_input == "y")
                {
                    Console.WriteLine("\n Длина змейки будет задана по-умолчанию.");
                    Properties.Settings.Default.SnakeLength = 4;
                }
            }
            while (Scripts.Apply_Continue() != true);
        }

        static void SnakeSpeed()
        {
            do
            {
                Console.WriteLine("\n Задать сокрость змейки по-умолчанию? (y/n)");
                answ_input = Scripts.Answer_Input();
                if (answ_input == "n")
                {
                    Console.WriteLine($"\n Задайте скорость змейки.\n Доступный диапазон от {1} до {5}.");
                    Properties.Settings.Default.SnakeSpeed = (byte)Scripts.DigitsRequest();
                    while (Properties.Settings.Default.SnakeSpeed < 1 || Properties.Settings.Default.SnakeSpeed > 5)
                    {
                        Scripts.FalseInput();
                        Console.WriteLine();
                        Properties.Settings.Default.SnakeSpeed = (byte)Scripts.DigitsRequest();
                    }
                }
                else if (answ_input == "y")
                {
                    Console.WriteLine("\n Скорость змейки будет задана по-умолчанию.");
                    Properties.Settings.Default.SnakeSpeed = 3;
                }
            }
            while (Scripts.Apply_Continue() != true);
        }

        static void SnakeColor()
        {
            do
            {
                Console.WriteLine("\n Задать цвет змейки по-умолчанию? (y/n)");
                answ_input = Scripts.Answer_Input();
                if (answ_input == "n")
                {
                    Properties.Settings.Default.SnakeColor = Scripts.FGColorOffer();
                }
                else if (answ_input == "y")
                {
                    Console.WriteLine("\n Цвет змейки будет задан по-умолчанию.");
                    Properties.Settings.Default.SnakeColor = ConsoleColor.Red;
                }
            }
            while (Scripts.Apply_Continue() != true);
        }

        static void SnakeBoost()
        {
            Console.WriteLine("\n Влючить ускорение змейки? (y/n)");
            answ_input = Scripts.Answer_Input();
            if (answ_input == "y")
                Properties.Settings.Default.SnakeBoost = true;
            else if (answ_input == "n")
                Properties.Settings.Default.SnakeBoost = false;
        }

        static void FoodSets(string message = null)
        {
            Console.Clear();
            temp_bgcolor = Console.BackgroundColor;
            temp_fgcolor = Console.ForegroundColor;
            Scripts.PastInMiddle_H((ushort)Console.CursorLeft, Properties.Settings.Default.DialogsWindowsWidth, '=', " ЕДА ", 5, ConsoleColor.Yellow);
            Console.WriteLine(" Доступные параметры для изменения:\n" +
                $" Символ (--symbol): '{Properties.Settings.Default.FoodSymbol}'");
            Console.BackgroundColor = Properties.Settings.Default.MapBGColor;
            Console.ForegroundColor = Properties.Settings.Default.FoodColor;
            Console.WriteLine($" Цвет (--color): {Properties.Settings.Default.FoodColor}.");
            Console.ForegroundColor = temp_fgcolor;
            Console.BackgroundColor = temp_bgcolor;
            Console.WriteLine(" Для применения настроек по-умолчанию к группе параметров введите '--default'.\n" +
                " Введите '--menu' или '--sets' для перехода к главному меню или к настройкам.\n");
            Scripts.PastInMiddle_H((ushort)Console.CursorLeft, Properties.Settings.Default.DialogsWindowsWidth, '*', " Terminal ", 10, ConsoleColor.DarkCyan);
            Console.Write(message);
            Console.Write(" Ввод: ");
            string input = Console.ReadLine().ToLower();
            while (input != "--symbol" &&  input != "--color" && input != "--default" && input != "--menu" && input != "--sets")
            {
                Scripts.FalseInput();
                input = Console.ReadLine().ToLower();
            }
            if (input == "--symbol")
            {
                FoodSymbol();
                SnakeSets(" Символ еды задан.\n");
            }
            else if (input == "--color")
            {
                FoodColor();
                SnakeSets(" Цвет еды задан.\n");
            }
            else if (input == "--default")
            {
                FoodDefault();
                SnakeSets(" Параметры еды заданы по-умолчанию.\n");
            }
            else if (input == "--menu")
                MainMenu();
            else if (input == "--sets")
                SettingsMenu();
        }

        static void FoodDefault()
        {
            Properties.Settings.Default.FoodSymbol = '$';
            Properties.Settings.Default.FoodColor = ConsoleColor.DarkGreen;
        }

        static void FoodSymbol()
        {
            do
            {
                Console.WriteLine("\n Задать символ еды по-умолчанию? (y/n)");
                answ_input = Scripts.Answer_Input();
                if (answ_input == "n")
                {
                    Console.WriteLine("\n Задайте символ еды.");
                    Properties.Settings.Default.FoodSymbol = Scripts.SymbolRequest();
                }
                else if (answ_input == "y")
                {
                    Console.WriteLine("\n Символ еды будет принят по-умолчанию.");
                    Properties.Settings.Default.FoodSymbol = '$';
                }
            }
            while (Scripts.Apply_Continue() != true);
        }

        static void FoodColor()
        {
            do
            {
                Console.WriteLine("\n Задать цвет еды? (y/n)");
                answ_input = Scripts.Answer_Input();
                if (answ_input == "y")
                {
                    Properties.Settings.Default.FoodColor = Scripts.FGColorOffer();
                }
                else if (answ_input == "n")
                {
                    Console.WriteLine("\n Цвет еды будет задан по-умолчанию.");
                    Properties.Settings.Default.FoodColor = ConsoleColor.DarkGreen;

                }
            }
            while (Scripts.Apply_Continue() != true);
        }

        internal static void MainMenu()
        {
            Console.ResetColor();
            Console.Clear();
            Console.SetWindowSize(Properties.Settings.Default.DialogsWindowsWidth, Properties.Settings.Default.DialogsWindowsHeight);
            Console.SetBufferSize(Properties.Settings.Default.DialogsWindowsWidth, Properties.Settings.Default.DialogsWindowsHeight * 2);
            temp_bgcolor = Console.BackgroundColor;
            temp_fgcolor = Console.ForegroundColor;
            //Main menu
            Scripts.PastInMiddle_H((ushort)Console.CursorLeft, Properties.Settings.Default.DialogsWindowsWidth, '=', " ГЛАВНОЕ МЕНЮ ", 14, ConsoleColor.Yellow);
            Console.WriteLine(" Для старты игры введите: '-g'.");
            //Parameters
            Scripts.PastInMiddle_H((ushort)Console.CursorLeft, Properties.Settings.Default.DialogsWindowsWidth, '=', " ПАРАМЕТРЫ ТЕКУЩЕЙ ИГРЫ ", 24, ConsoleColor.Yellow);
            Console.WriteLine(" Для изменения настроек игры введите: '--sets'.");
            //Map
            Scripts.PastInMiddle_H((ushort)Console.CursorLeft, Properties.Settings.Default.DialogsWindowsWidth, '=', " Карта ", 7, ConsoleColor.Yellow);
            Console.WriteLine($" Размер карты: (ширина х высота) {Properties.Settings.Default.MapWidth} x {Properties.Settings.Default.MapHeight}.");
            Console.BackgroundColor = Properties.Settings.Default.MapBGColor;
            Console.ForegroundColor = Properties.Settings.Default.MapFGColor;
            Console.WriteLine($" Цвет: заднего плана - {Properties.Settings.Default.MapBGColor} и символов  - {Properties.Settings.Default.MapFGColor}.");
            Console.BackgroundColor = temp_bgcolor;
            Console.ForegroundColor = temp_fgcolor;
            //Bound
            Scripts.PastInMiddle_H((ushort)Console.CursorLeft, Properties.Settings.Default.DialogsWindowsWidth, '=', " Ограждение ", 12, ConsoleColor.Yellow);
            Console.WriteLine($" Символы: по-горизонтали - '{Properties.Settings.Default.BoundHSymbol}' и по-вертикали - '{Properties.Settings.Default.BoundVSymbol}'.");
            Console.BackgroundColor = Properties.Settings.Default.BoundBGColor;
            Console.ForegroundColor = Properties.Settings.Default.BoundFGColor;
            Console.WriteLine($" Цвет: заднего плана - {Properties.Settings.Default.BoundBGColor} и символов  - {Properties.Settings.Default.BoundFGColor}.");
            Console.BackgroundColor = temp_bgcolor;
            Console.ForegroundColor = temp_fgcolor;
            //Snake
            Scripts.PastInMiddle_H((ushort)Console.CursorLeft, Properties.Settings.Default.DialogsWindowsWidth, '=', " Змейка ", 8, ConsoleColor.Yellow);
            Console.Write($" Символ: '{Properties.Settings.Default.SnakeSymbol}'\t" +
                $" Длина: {Properties.Settings.Default.SnakeLength}\t" +
                $" Скорость: {Properties.Settings.Default.SnakeSpeed}\t" +
                $" Ускорение: ");
            Scripts.Status(Properties.Settings.Default.SnakeBoost);
            Console.BackgroundColor = Properties.Settings.Default.MapBGColor;
            Console.ForegroundColor = Properties.Settings.Default.SnakeColor;
            Console.WriteLine($" Цвет: {Properties.Settings.Default.SnakeColor}.");
            Console.ForegroundColor = temp_fgcolor;
            Console.BackgroundColor = temp_bgcolor;
            //Food
            Scripts.PastInMiddle_H((ushort)Console.CursorLeft, Properties.Settings.Default.DialogsWindowsWidth, '=', " Еда ", 5, ConsoleColor.Yellow);
            Console.WriteLine($" Символ: '{Properties.Settings.Default.FoodSymbol}'.");
            Console.BackgroundColor = Properties.Settings.Default.MapBGColor;
            Console.ForegroundColor = Properties.Settings.Default.FoodColor;
            Console.WriteLine($" Цвет: {Properties.Settings.Default.FoodColor}.");
            Console.ForegroundColor = temp_fgcolor;
            Console.BackgroundColor = temp_bgcolor;
            // Music
            Scripts.PastInMiddle_H((ushort)Console.CursorLeft, Properties.Settings.Default.DialogsWindowsWidth, '=', " Музыка ", 8, ConsoleColor.DarkGray);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($" Статус музыки: {"off"}.\n Выбранная мелодия: {"Отсутсвует"}.");
            Console.ForegroundColor = temp_fgcolor;
            Scripts.PastInMiddle_H((ushort)Console.CursorLeft, Properties.Settings.Default.DialogsWindowsWidth, '*', " Terminal ", 10, ConsoleColor.DarkCyan);
            Console.Write(" Ввод: ");
            string input = Console.ReadLine().ToLower();
            while (input != "-g" && input != "--sets")
            {
                Scripts.FalseInput();
                input = Console.ReadLine().ToLower();
            }
            if (input == "-g")
                ToStart();
            else if (input == "--sets")
                SettingsMenu();
        }

        static void ToStart()
        {
            temp_fgcolor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("\n Н А Ч А Л О  И Г Р Ы  Ч Е Р Е З ... ");
            for(byte sec = 3; sec > 0; sec--)
            {
                Thread.Sleep(1000);
                if (sec == 1)
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                else if (sec == 2)
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                else if (sec == 3)
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write($" {sec} ");
            }
            Thread.Sleep(750);
        }

        static void SettingsMenu()
        {
            Console.Clear();
            Scripts.PastInMiddle_H((ushort)Console.CursorLeft, Properties.Settings.Default.DialogsWindowsWidth, '#', " НАСТРОЙКИ ", 11, ConsoleColor.Yellow);
            Console.WriteLine("\n Для изменения настроек выберите группу параметров.\n (--map|--bound|--snake|--food|--info).\n" +
                " Для применения всех настроек по-умолчанию введите '--default'.\n" +
                " Введите '--menu' для перехода к главному меню.");
            Scripts.PastInMiddle_H((ushort)Console.CursorLeft, Properties.Settings.Default.DialogsWindowsWidth, '*', " Terminal ", 10, ConsoleColor.DarkCyan);
            Console.Write(" Ввод: ");
            string input = Console.ReadLine().ToLower();
            while (input != "--map" && input != "--bound" && input != "--snake" && input != "--food" && input != "--info" && input != "--default" && input != "--menu")
            {
                Scripts.FalseInput();
                input = Console.ReadLine().ToLower();
            }
            if (input == "--map")
                MapSets();
            else if (input == "--bound")
                BoundSets();
            else if (input == "--snake")
                SnakeSets();
            else if (input == "--food")
                FoodSets();
            else if (input == "--info")
                InfoTable();
            else if (input == "--default")
            {
                MapDefault();
                BoundDefault();
                SnakeDefault();
                FoodDefault();
                Properties.Settings.Default.InfoScoreCount = ConsoleColor.DarkMagenta;
                Properties.Settings.Default.InfoScorePoint = ConsoleColor.DarkCyan;
                MainMenu();
            }
            else if (input == "--menu")
                MainMenu();
        }

        internal static void GameOverLogo()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Clear();
            Console.SetCursorPosition((Console.WindowWidth - Console.WindowLeft - 26) / 2, (Console.WindowHeight - Console.WindowTop - 4) / 2);
            Console.Write("==========================");
            Console.SetCursorPosition((Console.WindowWidth - Console.WindowLeft - 26) / 2, (Console.WindowHeight - Console.WindowTop - 2) / 2);
            Console.Write(" И Г Р А  О К О Н Ч Е Н А ");
            Console.SetCursorPosition((Console.WindowWidth - Console.WindowLeft - 26) / 2, (Console.WindowHeight - Console.WindowTop - 0) / 2);
            Console.Write("==========================");
            Console.SetCursorPosition((Console.WindowWidth - Console.WindowLeft - 26) / 2, (Console.WindowHeight - Console.WindowTop + 2) / 2);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Выход в главное меню...");
            Thread.Sleep(2750);
        }

        static void InfoTable()
        {
            do
            {
                Console.WriteLine("\n Задать цвет информационного блока по-умолчанию? (y/n)");
                answ_input = Scripts.Answer_Input();
                if (answ_input == "n")
                {
                    Console.WriteLine(" Цвет надписи 'Количество очков':");
                    Properties.Settings.Default.InfoScoreCount = Scripts.FGColorOffer();
                    Console.WriteLine(" Цвет надписи счетчика:");
                    Properties.Settings.Default.InfoScorePoint = Scripts.FGColorOffer();
                }
                else if (answ_input == "y")
                {
                    Console.WriteLine("\n Цвет информационного блока будет задан по-умолчанию.");
                    Properties.Settings.Default.InfoScoreCount = ConsoleColor.DarkMagenta;
                    Properties.Settings.Default.InfoScorePoint = ConsoleColor.DarkCyan;
                }
            }
            while (Scripts.Apply_Continue() != true);
        }
    }
}
