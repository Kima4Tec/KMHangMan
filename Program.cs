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
            Console.WriteLine(secretWordPicked);
            //find the length of the secret word picked
            int GetLineLength = secretWordPicked.Length;
            //create a variable with - in the length of the secret word
            string userWord = new string('-', GetLineLength);
            //write the secret word
            Console.WriteLine(userWord);
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
            Console.WriteLine();
            Console.Write("Write a letter: ");
            char userInputKey = Console.ReadKey().KeyChar;
            Console.WriteLine();
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
                    Console.WriteLine($"{userInputKey} is not in the word.");
                    guessWrongNumber++;
                }
                foreach (char s in userWordInChar)
                {
                    Console.Write(s);
                }
                if (guessWrongNumber > 4)
                {
                    IsWordGuessed = true;
                }
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
            Console.WriteLine("\nYou won");
        }

        public static void Fail(char[] wordInChar)
        {
            string secretWordRevealed = new string(wordInChar);
            Console.WriteLine($"You failed guessing the word. The word was: {secretWordRevealed}");

        }
    }
}