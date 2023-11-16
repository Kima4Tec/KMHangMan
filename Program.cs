using System;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace KMHangMan
{
    internal class Program
    {
        public static string[] secretWords = { "mclaren", "porsche", "lamborghini", "tuatara", "mercedes", "ferrari", "bentley", "zenvo", "koenigsegg", "bugatti" };
        public static bool IsWordGuessed = false;
        public static int guessWrongNumber = 0;


        static void Main(string[] args)
        {
            Graphics.Logo(((Console.WindowWidth)-57)/2,2);
            Graphics.Frames();
            string description1 = "HangMan is a game in which one player tries to guess ";
            string description2 = "the letters of a word. In this game you have to guess";
            string description3 = "the word of a special car. If you fail to find the"; 
            string description4 = "right letter five times, you hang.";
            Graphics.Pos(((Console.WindowWidth) - description1.Length) / 2,11, description1);
            Graphics.Pos(((Console.WindowWidth) - description1.Length) / 2, 12, description2);
            Graphics.Pos(((Console.WindowWidth) - description1.Length) / 2, 13, description3);
            Graphics.Pos(((Console.WindowWidth) - description1.Length) / 2, 14, description4);
            StartHangman();
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
            string theWordInLines = "This is the word obscured: \n\n";
            Console.ForegroundColor = ConsoleColor.White;
            Graphics.Pos(((Console.WindowWidth) - userWord.Length) / 2, 18, userWord);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Graphics.Pos(((Console.WindowWidth) - theWordInLines.Length) / 2, 16, theWordInLines);
            ConvertWordToArray(secretWordPicked, userWord);
        }

        public static void ConvertWordToArray(string secretWord, string userWord)
        {
            //break up the word in char and put in a new array
            //check if the array contains the letter
            char[] wordInChar = secretWord.ToCharArray();
            char[] userWordInChar = userWord.ToCharArray();
            getUserInput(wordInChar, userWordInChar);
        }

        //char[] userWordChar = secretWordPicked.ToCharArray();
        public static void getUserInput(char[] wordInChar, char[] userWordInChar)
        {
            string userMessage = "Write a letter: ";
            Graphics.Pos(((Console.WindowWidth) - userMessage.Length) / 2, 20, userMessage);
            char userInputKey = Console.ReadKey().KeyChar;
            checkWord(wordInChar, userWordInChar, userInputKey);
        }

        private static void checkWord(char[] wordInChar, char[] userWordInChar, char userInputKey)
        {
            bool letterFound = false;

            while (!IsWordGuessed)
            {
                for (int i = 0; i < wordInChar.Length; i++)
                {
                    if (wordInChar[i] == userInputKey)
                    {
                        letterFound = true;
                        userWordInChar[i] = userInputKey;
                    }
                }

                if (!letterFound)
                {
                    string letterNotThere = $"{userInputKey} is not in the word.";
                    Console.ForegroundColor = ConsoleColor.Red;
                    Graphics.Pos(((Console.WindowWidth) - letterNotThere.Length) / 2, 22, letterNotThere);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    guessWrongNumber++;

                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(((Console.WindowWidth) - userWordInChar.Length) / 2, 18);
                foreach (char s in userWordInChar)
                {
                    Console.Write(s);
                }
                if (guessWrongNumber > 4)
                {
                    IsWordGuessed = true;
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                IsWordFound(wordInChar, userWordInChar);
            }
        }
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
                    getUserInput(wordInChar, userWordInChar);
                }
            }
            else
            {
                Fail(wordInChar);
            }


        }
        public static void UserWon()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string youWon = "You Won and will stay alive!";
            Graphics.Pos(((Console.WindowWidth) - youWon.Length) / 2, 22, youWon);
            Console.ResetColor();
            Console.SetCursorPosition(0, 25);
            Console.ReadKey();
        }

        public static void Fail(char[] wordInChar)
        {
            string secretWordRevealed = new string(wordInChar);
            Console.ForegroundColor = ConsoleColor.Red;
            string youLost = $"You failed guessing the word. The word was: {secretWordRevealed}";
            Graphics.Pos(((Console.WindowWidth) - youLost.Length) / 2, 22, youLost);
            Console.ResetColor();
            Console.SetCursorPosition(0, 25);
            Console.ReadKey();

        }
    }
}