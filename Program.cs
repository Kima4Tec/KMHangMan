using System;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace KMHangMan
{
    internal class Program
    {
        public static string[] secretWords = { "mclaren", "porsche", "lamborghini", "tuatara", "mercedes", "ferrari", "bentley", "zenvo", "koenigsegg", "bugatti","jaguar" };
        public static bool IsWordGuessed = false;
        public static int guessWrongNumber = 0;
        public static string points = "Life: OOOOO";

        static void Main(string[] args)
        {
            PreGame();
        }
        public static void PreGame()
        {
            //setting up text and frames
            SettingUp();

            //starting the game
            StartHangman();
        }

        private static void SettingUp()
        {

            Console.ForegroundColor = ConsoleColor.Yellow;
            Graphics.Logo(((Console.WindowWidth) - 57) / 2, 2);
            Graphics.Frames();
            string description1 = "HangMan is a game in which one player tries to guess ";
            string description2 = "the letters of a word. In this game you have to guess";
            string description3 = "the word of a special car. If you fail to find the";
            string description4 = "right letter after five times, you hang.";
            Graphics.Pos(((Console.WindowWidth) - description1.Length) / 2, 11, description1, ConsoleColor.DarkRed);
            Graphics.Pos(((Console.WindowWidth) - description1.Length) / 2, 12, description2, ConsoleColor.Red);
            Graphics.Pos(((Console.WindowWidth) - description1.Length) / 2, 13, description3, ConsoleColor.DarkYellow);
            Graphics.Pos(((Console.WindowWidth) - description1.Length) / 2, 14, description4, ConsoleColor.Yellow);
            Graphics.Pos(27, 23, points, ConsoleColor.Green);
        }



        public static void StartHangman()
        {
            //random instance
            Random rnd = new Random();

            //get a random number which is in the scope of the array secretWords
            int number = rnd.Next(1, secretWords.Length);

            //create a varible containing the secret word
            string secretWordPicked = secretWords[number];

            //write the secret word
            Console.WriteLine();

            //find the length of the secret word picked
            int GetLineLength = secretWordPicked.Length;

            //create a variable with - in the length of the secret word
            string userWord = new string('-', GetLineLength);

            //write the secret word
            string infoText = "This is the word obscured: \n\n";

            //Color change to white
            Console.ForegroundColor = ConsoleColor.White;

            //Print out userWord - - - - - 
            Graphics.Pos(((Console.WindowWidth) - userWord.Length) / 2, 18, userWord, ConsoleColor.White);

            //Print out info text
            Graphics.Pos(((Console.WindowWidth) - infoText.Length) / 2, 16, infoText, ConsoleColor.White);

            ConvertWordToArray(secretWordPicked, userWord);
        }



            public static void ConvertWordToArray(string secretWord, string userWord)
        {
            //break up the word in char and put in a new array
            //check if the array contains the letter
            char[] wordInChar = secretWord.ToCharArray();
            char[] userWordInChar = userWord.ToCharArray();

            //Call method to get user input
            GetUserInput(wordInChar, userWordInChar);
        }

        /// <summary>
        /// Info to user. Getting key and making it lower. Checking if user
        /// press ENTER og SPACEBAR and tells user to press a letter.
        /// Call method CheckWord with three arguments: wordInChar, userWordInChar, userInputKey
        /// </summary>
        /// <param name="wordInChar"></param>
        /// <param name="userWordInChar"></param>
        public static void GetUserInput(char[] wordInChar, char[] userWordInChar)
        {
            string userMessage = "Write a character: ";
            Graphics.Pos(((Console.WindowWidth) - userMessage.Length) / 2, 20, userMessage, ConsoleColor.White);
            char inputKey = Console.ReadKey().KeyChar;
            char userInputKey = char.ToLower(inputKey);
            InfoWrongKeyStroke(userInputKey);
            switch (userInputKey)
            {
                case (char)ConsoleKey.Enter:
                    GetUserInput(wordInChar, userWordInChar);
                    break;
                case (char)ConsoleKey.Spacebar:
                    GetUserInput(wordInChar, userWordInChar);
                    break;
            }
            EraseoldText();
            CheckWord(wordInChar, userWordInChar, userInputKey);
        }

        /// <summary>
        /// clearing old info text
        /// </summary>
        private static void EraseoldText()
        {
            Graphics.Pos(((Console.WindowWidth) - 21) / 2, 22, "                     ");
            Graphics.Pos(((Console.WindowWidth) - 27) / 2, 23, "                           ");
        }

        /// <summary>
        /// Info when ENTER or SPACEBAR has been pressed.
        /// </summary>
        /// <param name="userInputKey"></param>
        static void InfoWrongKeyStroke(char userInputKey)
        {
            string wrongKeys = "You have to press a letter.";
            Graphics.Pos(((Console.WindowWidth) - wrongKeys.Length) / 2, 23, wrongKeys, ConsoleColor.Cyan);
            return;
        }

        /// <summary>
        /// Checking if the keys pressed is in the secret word while bool IsWordGuessed is false.
        /// Counts also the times user fails to guess a letter. Points are taken away and shown in left lower corner. Pos(27,23).
        /// Checking if word is found, and checking if mistakes are 5, which means user lost
        /// </summary>
        /// <param name="wordInChar"></param>
        /// <param name="userWordInChar"></param>
        /// <param name="userInputKey"></param>
        private static void CheckWord(char[] wordInChar, char[] userWordInChar, char userInputKey)
        {
            bool charFound = false;

            while (!IsWordGuessed)
            {
                for (int i = 0; i < wordInChar.Length; i++)
                {
                    if (wordInChar[i] == userInputKey)
                    {
                        charFound = true;
                        userWordInChar[i] = userInputKey;
                    }
                }

                if (!charFound)
                {
                    string letterNotThere = $"{userInputKey} is not in the word.";
                        Graphics.Pos(((Console.WindowWidth) - letterNotThere.Length) / 2, 22, letterNotThere, ConsoleColor.Red);

                    guessWrongNumber++;
                    Graphics.Pos(27, 23, "                ");
                    string newPoints = points.Substring(0, points.Length - guessWrongNumber);
                    Graphics.Pos(27, 23, newPoints, ConsoleColor.Green);
                }

                RefreshLineWord(userWordInChar);
                IsWrongGuessTooMany(guessWrongNumber);

                IsWordFound(wordInChar, userWordInChar);
            }
        }

        /// <summary>
        /// Method returning a bool, checking whether fails of guesses succeds 4.
        /// When 5 fails, game is over.
        /// </summary>
        /// <param name="guessWrongNumber"></param>
        /// <returns></returns>
        public static bool IsWrongGuessTooMany(int guessWrongNumber)
        {
            if (guessWrongNumber > 4)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Refreshing the char array userWordInChar, after user input
        /// </summary>
        /// <param name="userWordInChar"></param>
        static void RefreshLineWord(char[] userWordInChar)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(((Console.WindowWidth) - userWordInChar.Length) / 2, 18);
            foreach (char s in userWordInChar)
            {
                Console.Write(s);
            }
        }

        /// <summary>
        /// Checking whether the secret word and if the user has found the right word.
        /// In case the word is guessed, then IsWordGuessed = true, and user has won.
        /// Otherwise get user to try to press another key.
        /// </summary>
        /// <param name="wordInChar"></param>
        /// <param name="userWordInChar"></param>
        public static void IsWordFound(char[] wordInChar, char[] userWordInChar)
        {
            if(guessWrongNumber != 5)
            {
                if (wordInChar.SequenceEqual(userWordInChar))
                {
                    IsWordGuessed = true;
                    UserWon();
                }
                else
                {
                    GetUserInput(wordInChar, userWordInChar);
                }
            }
            else
            {
                Fail(wordInChar);
            }

            ///Winner message and question to play again
        }
        public static void UserWon()
        {
            string youWon = "You Won and will stay alive!";
            Graphics.Pos(((Console.WindowWidth) - youWon.Length) / 2, 22, youWon, ConsoleColor.Green);
            Console.SetCursorPosition(0, 25);
            Graphics.Pos(73, 23, "Play again? (Y/N): ", ConsoleColor.Green);
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                Console.Clear();
                guessWrongNumber = 0;
                points = "Life: OOOOO";
                IsWordGuessed = false;
                PreGame();
            }
        }

        /// <summary>
        /// Loser message and question to play again
        /// </summary>
        /// <param name="wordInChar"></param>
        public static void Fail(char[] wordInChar)
        {
            string secretWordRevealed = new string(wordInChar);
            string youLost = $"You failed guessing the word. The word was: {secretWordRevealed}";
            Graphics.Pos(((Console.WindowWidth) - youLost.Length) / 2, 22, youLost, ConsoleColor.Red);
            Console.SetCursorPosition(0, 25);
            Graphics.Pos(71, 23, "Play again? (Y/N): ", ConsoleColor.Green);
            //Get input. Check if input is a "Y"
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                Console.Clear();
                guessWrongNumber = 0;
                points = "Life: OOOOO";
                IsWordGuessed = false;
                PreGame();
            }

        }
    }
}