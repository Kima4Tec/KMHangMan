using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHangMan
{
    internal class Graphics
    {
        public static void Logo(int x, int y, ConsoleColor color = ConsoleColor.Yellow)
        {
            Console.ForegroundColor = color;
            Pos(x, y + 0, @"       _    _                   __  __                   ");
            Pos(x, y + 1, @"      | |  | |                 |  \/  |                  ");
            Pos(x, y + 2, @"      | |__| | __ _ _ __   __ _| \  / | __ _ _ __        ");
            Pos(x, y + 3, @"      |  __  |/ _` | '_ \ / _` | |\/| |/ _` | '_ \       ");
            Pos(x, y + 4, @"      | |  | | (_| | | | | (_| | |  | | (_| | | | |      ");
            Pos(x, y + 5, @"      |_|  |_|\__,_|_| |_|\__, |_|  |_|\__,_|_| |_|      ");
            Pos(x, y + 6, @"                           __/ |                         ");
            Pos(x, y + 7, @"                           |___/                         ");

        }


        public static void Pos(int x, int y, string tekst) //positioning text
        {
            Console.SetCursorPosition(x, y);
            Console.Write(tekst);
        }
        public static void Frames() //Frames araound Logo and textfield
        {


            //1. horizontal line
            for (int i = 0; i < 67; i++)
            {
                Pos(26 + i, 1, "─");
            }
            //2. horizontal line
            for (int i = 0; i < 67; i++)
            {
                Pos(26 + i, 10, "─");
            }
            //3. horizontal line
            for (int i = 0; i < 67; i++)
            {
                Pos(26 + i, 24, "─");
            }
            //left vertical line
            for (int i = 0; i < 22; i++)
            {
                Pos(25, 2 + i, "│");
            }
            //right vertical line
            for (int i = 0; i < 22; i++)
            {
                Pos(93, 2 + i, "│");
            }
            //left top corner
            Pos(25, 1, "┌");

            //right top corner
            Pos(93, 1, "┐");

            //left m corner
            Pos(25, 10, "├");

            //right m corner
            Pos(93, 10, "┤");

            //left m corner
            Pos(25, 24, "└");

            //right m corner
            Pos(93, 24, "┘");

        }
        public static void EmptyBox(int x, int y) //clearing text
        {
            Pos(x, y + 0, "                                                                   ");
            Pos(x, y + 1, "                                                                   ");
            Pos(x, y + 2, "                                                                   ");
        }
    }
}