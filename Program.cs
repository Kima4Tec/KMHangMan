using System;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace KMHangMan
{
    internal class Program
    {
        public static string jsonFilePath = "secretWords.json";
        public static int wrongGuessNumber;
        public static string usedLetters = "";
        public static string secretWord = "";
        public static string obscuredWord = "";


        static void Main(string[] args)
        {
            Setup();
        }
        /// <summary>
        /// Setting up text and graphic
        /// </summary>
        static void Setup()
        {
            string[] loadedWordsArray = LoadArrayFromJsonFile(jsonFilePath);
            Console.Clear();
            FixedData.Logo(((Console.WindowWidth) - 57) / 2, 2);
            FixedData.Frames();
            FixedData.IntroText(11);
            FixedData.PrintInfoOnObscuredText(16);
            FixedData.DrawGallow(29, 16);
            FixedData.DrawTree(79, 16);
            FixedData.PrintKeyText(20);
            ProcessWord(loadedWordsArray);
        }
        /// <summary>
        /// Loading data from json file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>Return an array of names on cars from JSON file: secretWords.json if file exists</returns>
        static string[] LoadArrayFromJsonFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<string[]>(jsonString);
            }
            else
            {
                return Array.Empty<string>();
            }
        }
        /// <summary>
        /// Picking a random number 
        /// </summary>
        /// <param name="loadedWordsArray"></param>
        static void ProcessWord(string[] loadedWordsArray)
        {
            //random instance
            Random rnd = new();

            //get a random number which is in the scope of the array pickWord
            int number = rnd.Next(1, loadedWordsArray.Length);

            //create a varible containing the secret word
            secretWord = loadedWordsArray[number];

            //find the length of the secret word picked
            int GetLineLength = secretWord.Length;

            obscuredWord = new string('-', GetLineLength);
            wrongGuessNumber = 0;

            //create a variable with -------- in the length of the secret word
            FixedData.PrintObscuredWord(18);

            StartGame();
        }

        /// <summary>
        /// This methods runs the overall of the program. 
        /// Dividing the secretword up into a char array, and create an array for user input.
        /// Getting keyinput, and checking it for validity, word guessed and attmepts left calling 
        /// the different methods.
        /// Calling DrawHangMan() for wrong answers and adding the number in wrongGuessNumber.
        /// End result is if you win or lose.
        /// </summary>
        static void StartGame()
        {
            char[] obscuredWordChar = obscuredWord.ToCharArray();
            char[] secretWordChar = secretWord.ToCharArray();
            while (!IsWordGuessed(obscuredWordChar) && IsThereMoreAttempts())
            {
                Console.SetCursorPosition(68, 20);
                char inputKey = Console.ReadKey(intercept: true).KeyChar;
                char userInputKey = char.ToLower(inputKey);
                if (IsKeyValid(userInputKey))
                {
                    FixedData.EraseoldText();
                    if (IsKeyInWord(userInputKey, secretWordChar, obscuredWordChar))
                    {
                        KeyUsed(userInputKey);
                    }
                    else
                    {
                        KeyUsed(userInputKey);
                        wrongGuessNumber++;
                        DrawHangMan();
                    }
                }

            }

            if (IsWordGuessed(obscuredWordChar))
            {
                YouWin();
            }
            if (!IsThereMoreAttempts())
            {
                YouLose();
            }

        }
        /// <summary>
        /// Checking whether the key input is valid and if is already is tried,
        /// by checking the variable usedLetters, which contains the used letters from user.
        /// </summary>
        /// <param name="userInputKey"></param>
        /// <returns></returns>
        static bool IsKeyValid(char userInputKey)
        {
            if (!char.IsLetter(userInputKey) || usedLetters.Contains(userInputKey))
            {
                string wrongKeys = "That is not a valid letter.";
                Graphics.Pos(((Console.WindowWidth) - wrongKeys.Length) / 2, 23, wrongKeys, ConsoleColor.Cyan);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checking whether the input key is a letter in the secretword or not
        /// </summary>
        /// <param name="userInputKey"></param>
        /// <param name="secretWordChar"></param>
        /// <param name="obscuredWordChar"></param>
        /// <returns></returns>
        static bool IsKeyInWord(char userInputKey, char[] secretWordChar, char[] obscuredWordChar)
        {
            if (secretWord.Contains(userInputKey))
            {
                for (int i = 0; i < secretWord.Length; i++)
                {
                    if (secretWordChar[i] == userInputKey)
                    {
                        obscuredWordChar[i] = userInputKey;
                    }
                }
                ShowKeyInWord(obscuredWordChar);
                return true;
            }
            else
            {
                string letterNotThere = $"{userInputKey} is not in the word.";
                Graphics.Pos(((Console.WindowWidth) - letterNotThere.Length) / 2, 22, letterNotThere, ConsoleColor.Red);
                return false;
            }
        }
        /// <summary>
        /// keeping tracks of attempts used. The number is used in the switch, drawing the Hangman
        /// </summary>
        /// <returns></returns>
        static bool IsThereMoreAttempts()
        {
            if (wrongGuessNumber < 6)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Drawing the Hangman where each case is initialised by the number of wrong guesses
        /// Using positioning to place it on the screen
        /// </summary>
        static void DrawHangMan()
        {
            switch (wrongGuessNumber)
            {
                case 1:
                    Graphics.Pos(36, 18, @"(_)", ConsoleColor.Gray);
                    break;
                case 2:
                    Graphics.Pos(36, 19, @"/", ConsoleColor.DarkGreen);
                    break;
                case 3:
                    Graphics.Pos(37, 19, @"|", ConsoleColor.DarkGreen);
                    break;
                case 4:
                    Graphics.Pos(38, 19, @"\", ConsoleColor.DarkGreen);
                    break;
                case 5:
                    Graphics.Pos(36, 20, @"/", ConsoleColor.Blue);
                    break;
                case 6:
                    Graphics.Pos(38, 20, @"\", ConsoleColor.Blue);
                    break;
            }
        }
        /// <summary>
        /// Adding the key input to the variable usedLetters
        /// </summary>
        /// <param name="userInputKey"></param>
        static void KeyUsed(char userInputKey)
        {
            usedLetters += userInputKey;
            FixedData.PrintUsedLetter(23);
            Console.SetCursorPosition(0, 27);
        }

        /// <summary>
        /// Writing the status quo of the guessed word 
        /// </summary>
        /// <param name="obscuredWordChar"></param>
        static void ShowKeyInWord(char[] obscuredWordChar)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(((Console.WindowWidth) - obscuredWordChar.Length) / 2, 18);
            foreach (char s in obscuredWordChar)
            {
                Console.Write(s);
            }
            IsWordGuessed(obscuredWordChar);
        }

        /// <summary>
        /// Checking if the word is guessed by comparison of the status quo of the guessed word (obscuredword) and
        /// the secretword
        /// </summary>
        /// <param name="obscuredWordChar"></param>
        /// <returns></returns>
        static bool IsWordGuessed(char[] obscuredWordChar)
        {
            string obscuredWord = new string(obscuredWordChar);
            return obscuredWord == secretWord;
        }
        /// <summary>
        /// You lose statement and asking if user wants to play again.
        /// </summary>
        static void YouLose()
        {
            FixedData.PrintYouLost(22);
            IsPlayAgain();
        }
        /// <summary>
        /// You win statement and asking if user wants to play again.
        /// </summary>
        static void YouWin()
        {
            FixedData.PrintYouWin(22);
            IsPlayAgain();
        }
        /// <summary>
        /// Resetting values and starting the program again. 
        /// </summary>
        static void IsPlayAgain()
        {
            FixedData.PrintPlayAgain(23);
            bool validKeyPress = false;
            while (!validKeyPress)
            {
                ConsoleKeyInfo x = Console.ReadKey();

                if (x.Key == ConsoleKey.Y)
                {
                    usedLetters = "";
                    FixedData.PrintUsedLetter(23);
                    FixedData.EraseoldText();
                    Setup();
                    validKeyPress = true;
                }
                if (x.Key == ConsoleKey.N)
                {
                    EndGame();
                    validKeyPress = true;
                }
            }
        }

        static void EndGame()
        {

        }
    }
}