using System;
using System.Text;
using System.Collections.Generic;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n\tWelcome to Hangman!");
            Console.WriteLine("\tYou have 10 chances to guess the word");
            string secretWord = GetSecretWord();
            System.Threading.Thread.Sleep(3000);
            GameScreen(10, secretWord);
        }

        static string GetSecretWord()
        {
            string[] wordList = new string[] {"Sweden", "England", "Ireland", "Scottland", "China", "France",
            "India", "Finland", "Norway" };
            Random random = new Random();
            int randomNumber = random.Next(0, wordList.Length - 1);
            return (wordList[randomNumber]);
        }

        static void GameScreen(int guessesLeft, string secretWord)
        {
            char[] secretLetters = new char[secretWord.Length];
            for (int i = 0; i < secretWord.Length; i++)
            {
                secretLetters[i] = '_';
            }
            StringBuilder wrongLetters = new StringBuilder();

            while (guessesLeft > 0)
            {
                try
                {
                    ShowUpdate(secretLetters, wrongLetters, guessesLeft);
                    string userguess = Console.ReadLine();
                    userguess = userguess.ToUpper();
                    string secretUpperWord = secretWord.ToUpper();
                    bool isLetter = CheckIfLetter(userguess);
                   
                    if (userguess != "" && isLetter == true)
                    {
                        if (userguess.Length == 1)
                        {
                            char letterGuess = char.Parse(userguess);
                            bool letterExists = CheckDuplicate(letterGuess, secretLetters, wrongLetters);
                            if (letterExists == false)
                            {
                                var result = CheckChar(letterGuess, secretUpperWord);
                                if (result.Item1 == true)
                                {
                                    for (int i = 0; i < result.Item2.Count; i++)
                                    {
                                        secretLetters[result.Item2[i]] = letterGuess;
                                    }
                                }
                                else
                                {
                                    guessesLeft--;
                                    wrongLetters.Append(letterGuess).Append("  ");
                                }
                            }
                        }
                        else
                        {
                            if (userguess == secretUpperWord)
                            {
                                EndGame(true);
                                break;
                            }
                            else
                            {
                                guessesLeft--;
                            }
                        }
                    }
                    if (Array.IndexOf(secretLetters, '_') == -1)
                    {
                        EndGame(true);
                        break;
                    }
                }
                catch
                {
                    Console.WriteLine("An exception has occurred, you maybe tried to input numbers or symbols instead of letters");
                }
            }
            EndGame(false);
        }

        static void ShowUpdate(char[] secretLetters, StringBuilder wrongLetters,int guessesLeft)
        {
            Console.Clear();
            Console.WriteLine($"\n\tYou have {guessesLeft} guesses left");
            Console.WriteLine("\tGuess a letter in the word or the word itself");
            Console.WriteLine();
            for (int i = 0; i < secretLetters.Length; i++)
            {
                Console.Write($"\t{secretLetters[i]}");
            }
            Console.WriteLine("\n" + "\n");
            Console.WriteLine($"\tLetters not in the word: \n\t{wrongLetters}\n");
        }

        static bool CheckIfLetter(string userguess)
        {
            bool isLetter = true;
            foreach (char checkLetter in userguess)
            {
                if (!char.IsLetter(checkLetter))
                {
                    return false;
                }
            }
            return isLetter;
        }

        static bool CheckDuplicate(char letterGuess, char[] secretLetters, StringBuilder wrongLetters)
        {
            if (wrongLetters.ToString().IndexOf(letterGuess) == -1 && Array.IndexOf(secretLetters, letterGuess) == -1)
            {
                return (false);
            }
            else
            {
                return (true);
            }
        }

        static (bool, List<int>) CheckChar(char letterGuess, string secretWord)
        {
            List<int> indexPositions = new List<int>();
            int i = 0;
            while ((i = secretWord.IndexOf(letterGuess, i)) != -1)
            {
                indexPositions.Add(i);
                i++;
            }
            if (indexPositions.Count > 0)
            {
                return (true, indexPositions);
            }
            else
            {
                return (false, indexPositions);
            }
        }

        static void EndGame(bool checkVictory)
        {
            if (checkVictory == true)
            {
                Console.WriteLine("\n\tYou have won the game!");
            }
            else
            {
                Console.WriteLine("\n\tYou have lost the game!");
            }
            System.Threading.Thread.Sleep(3000);
            string secretWord = GetSecretWord();
            GameScreen(10, secretWord);
        }
    }
}
