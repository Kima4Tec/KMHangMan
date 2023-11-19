using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMHangMan
{
    internal class FixedData
    {
        public static string[] secretWordsArray = { "mclaren", "porsche", "lamborghini", "tuatara", "mercedes", "ferrari", "bentley", "zenvo", "koenigsegg", "bugatti", "jaguar" };
        public static void IntroText(int y) 
        {
            string gameInfo1 = @"HangMan is a game in which one player tries to guess ";
            string gameInfo2 = @"letters of a word. In this game you have to guess";
            string gameInfo3 = @"the word of a special car. If you fail to find the";
            string gameInfo4 = @"right letter after six times, you hang.";
            Pos(((Console.WindowWidth) - gameInfo1.Length) / 2, y + 0, gameInfo1, ConsoleColor.DarkRed);
            Pos(((Console.WindowWidth) - gameInfo1.Length) / 2, y + 1, gameInfo2, ConsoleColor.Red);
            Pos(((Console.WindowWidth) - gameInfo1.Length) / 2, y + 2, gameInfo3, ConsoleColor.DarkYellow);
            Pos(((Console.WindowWidth) - gameInfo1.Length) / 2, y + 3, gameInfo4, ConsoleColor.Yellow);
        }

        public static void PrintInfoOnObscuredText(int y)
        {
            string infoText = "This is the word obscured:";
            Pos(((Console.WindowWidth) - infoText.Length) / 2, y + 0, infoText, ConsoleColor.White);
        }

        public static void PrintKeyText(int y)
        {
            string writeText = "Write a letter:";
            Pos(((Console.WindowWidth) - writeText.Length) / 2, y + 0, writeText, ConsoleColor.White);
        }

        public static void PrintObscuredWord(int y)
        {
            Pos(((Console.WindowWidth) - Program.obscuredWord.Length) / 2, y + 0, Program.obscuredWord, ConsoleColor.White);
        }

        public static void PrintUsedLetter(int y)
        {
            Pos(27, y, Program.usedLetters.PadRight(15), ConsoleColor.Cyan);
        }

        public static void PrintKeyNotValid(int y)
        {
            string KeyNotValid = "You have to press a letter.";
            Pos(((Console.WindowWidth) - KeyNotValid.Length) / 2, y, KeyNotValid, ConsoleColor.Red);
        }

        public static void PrintPlayAgain(int y)
        {
            string playAgainText = "Play again? (Y/N)";
            Pos(((Console.WindowWidth) - playAgainText.Length) / 2, y, playAgainText, ConsoleColor.Green);
        }

        public static void PrintYouLost(int y)
        {
            string youLostText = "You failed guessing the word.";
            Pos(((Console.WindowWidth) - youLostText.Length) / 2, y, youLostText.PadRight(30), ConsoleColor.Red);
        }
        public static void PrintYouWin(int y)
        {
            string youLostText = "You guessed the word. You win!";
            Pos(((Console.WindowWidth) - youLostText.Length) / 2, y, youLostText.PadRight(30), ConsoleColor.Green);
        }


        public static void EraseoldText()
        {
            Graphics.Pos(((Console.WindowWidth) - 30) / 2, 22, "".PadRight(30));
            Graphics.Pos(((Console.WindowWidth) - 30) / 2, 23, "".PadRight(30));
        }

        public static void Logo(int x, int y)
        {
            LogoPos(x, y + 0, @"       _    _                   __  __                   ");
            LogoPos(x, y + 1, @"      | |  | |                 |  \/  |                  ");
            LogoPos(x, y + 2, @"      | |__| | __ _ _ __   __ _| \  / | __ _ _ __        ");
            LogoPos(x, y + 3, @"      |  __  |/ _` | '_ \ / _` | |\/| |/ _` | '_ \       ");
            LogoPos(x, y + 4, @"      | |  | | (_| | | | | (_| | |  | | (_| | | | |      ");
            LogoPos(x, y + 5, @"      |_|  |_|\__,_|_| |_|\__, |_|  |_|\__,_|_| |_|      ");
            LogoPos(x, y + 6, @"                           __/ |                         ");
            LogoPos(x, y + 7, @"                           |___/                         ");

        }
        public static void DrawGallow(int x, int y)
        {
            Pos(x, y + 0, @"   _______", ConsoleColor.DarkYellow);
            Pos(x, y + 1, @"  |/    | ", ConsoleColor.DarkYellow);
            Pos(x, y + 2, @"  |       ", ConsoleColor.DarkYellow);
            Pos(x, y + 3, @"  |       ", ConsoleColor.DarkYellow);
            Pos(x, y + 4, @"  |       ", ConsoleColor.DarkYellow);
            Pos(x, y + 5, @"  |       ", ConsoleColor.DarkYellow);
            Pos(x, y + 6, @"__|___    ", ConsoleColor.DarkYellow);
        }

        public static void DrawTree(int x, int y)
        {
            Pos(x, y + 0, @"    **    ", ConsoleColor.Green);
            Pos(x, y + 1, @"   ****   ", ConsoleColor.Green);
            Pos(x, y + 2, @"  ******  ", ConsoleColor.Green);
            Pos(x, y + 3, @" ******** ", ConsoleColor.Green);
            Pos(x, y + 4, @"**********", ConsoleColor.Green);
            Pos(x, y + 5, @"    ||    ", ConsoleColor.DarkYellow);
            Pos(x, y + 6, @"    ||    ", ConsoleColor.DarkYellow);
        }

        public static void LogoPos(int x, int y, string tekst, ConsoleColor color = ConsoleColor.DarkRed) //positioning text
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y);
            Console.Write(tekst);
        }

        public static void Pos(int x, int y, string tekst, ConsoleColor color = ConsoleColor.DarkMagenta) //positioning text
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y);
            Console.Write(tekst);
            Console.ResetColor();
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
