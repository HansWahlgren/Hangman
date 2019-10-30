using System;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Hangman!");
            Console.WriteLine("You have 10 chances to guess the word");
            string secretWord = GetSecretWord();
            System.Threading.Thread.Sleep(3000);
            Console.Clear();
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
            // string wrongLetters = string.BUILDER

            while(guessesLeft > 0)
            {
                // VISA PLATSER & BOKSTÄVER

                Console.WriteLine($"You have {guessesLeft} guesses left");
                Console.WriteLine("Guess a letter in the word or the word itself");
                string userguess = Console.ReadLine();

                if (userguess.Length < 2)
                {
                    char letterGuess = char.Parse(userguess);
                    var result = CheckChar(letterGuess, secretWord);

                    //RÄTT ELLER FEL CHAR, LÄTT TILL I STRING BUILD ELLER CHAR ARRAY
                }

                //ORDET RÄTT ELLER GAME OVER
                else
                {
                    if (userguess == secretWord)
                    {
                        EndGame(true);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("That was not the word");
                        guessesLeft--;
                        System.Threading.Thread.Sleep(1500);
                    }
                }
            }
            EndGame(false);
        }

        static (char,bool) CheckChar(char userguess, string secretWord)
        {
            return ('T' , true);
        }

        //END OF GAME
        static void EndGame(bool checkVictory)
        {
            Console.Clear();
            if (checkVictory == true)
            {
                Console.WriteLine("You have won the game!");
            }
            else
            {
                Console.WriteLine("You have lost the game!");
            }

            System.Threading.Thread.Sleep(4000);
            string secretWord = GetSecretWord();
            GameScreen(10, secretWord);
        }
    }
}
