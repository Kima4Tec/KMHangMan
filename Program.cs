﻿using System;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace KMHangMan
{
    internal class Program
    {
        public static int wrongGuessNumber;
        public static string usedLetters = "";
        public static string secretWord = "";
        public static string obscuredWord = "";
        

        static void Main(string[] args)
        {
            Setup();
        }

        static void Setup()
        {
            Console.Clear();
            FixedData.Logo(((Console.WindowWidth) - 57) / 2, 2);
            FixedData.Frames();
            FixedData.IntroText(11);
            FixedData.PrintInfoOnObscuredText(16);
            FixedData.DrawGallow(29, 16);
            FixedData.DrawTree(79, 16);
            FixedData.PrintKeyText(20);


            ProcessWord();
        }

        static void ProcessWord()
        {
            //random instance
            Random rnd = new();

            //get a random number which is in the scope of the array pickWord
            int number = rnd.Next(1, FixedData.secretWordsArray.Length);

            //create a varible containing the secret word
            secretWord = FixedData.secretWordsArray[number];

            //find the length of the secret word picked
            int GetLineLength = secretWord.Length;

            obscuredWord = new string('-', GetLineLength);
            wrongGuessNumber = 0;

            //create a variable with -------- in the length of the secret word
            FixedData.PrintObscuredWord(18);


            StartGame();
        }
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

        static bool IsThereMoreAttempts()
        {
            if (wrongGuessNumber < 6)
            {
                return true;
            }
             return false;
        }

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

        static void KeyUsed(char userInputKey)
        {
            usedLetters += userInputKey;
            FixedData.PrintUsedLetter(23);
            Console.SetCursorPosition(0, 27);
        }

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

        static bool IsWordGuessed(char[] obscuredWordChar)
        {
            string obscuredWord = new string(obscuredWordChar);
            return obscuredWord == secretWord;
        }

        static void YouLose()
        {
            FixedData.PrintYouLost(22);
            IsPlayAgain();
        }

        static void YouWin()
        {
            FixedData.PrintYouWin(22);
            IsPlayAgain();
        }

        static void IsPlayAgain()
        {
            FixedData.PrintPlayAgain(23);
            bool validKeyPress = false;
            while (!validKeyPress)
            {
                ConsoleKeyInfo x = Console.ReadKey();

                if ( x.Key == ConsoleKey.Y)
                {
                    usedLetters = "";
                    FixedData.PrintUsedLetter(23);
                    FixedData.EraseoldText();
                    ProcessWord();
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