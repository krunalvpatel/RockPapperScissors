using System;
using System.Collections.Generic;

namespace RockPapperScissors
{
    public class Program
    {
        private static readonly List<string> Choices = new List<string> { "rock", "paper", "scissors" };

        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Rock, Paper, Scissors!");

            Console.WriteLine("Enter the name of Player 1:");
            Player player1 = new Player { Name = Console.ReadLine() };
            
            Console.WriteLine("Enter the name of Player 2:");
            Player player2 = new Player { Name = Console.ReadLine() };

            Game game = new Game();
            game.AddPlayer(player1);
            game.AddPlayer(player2);

            do
            {
                Console.WriteLine("Enter the total number of rounds (default is 3):");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int totalRounds) && totalRounds > 0)
                {
                    game.TotalRounds = totalRounds;
                }
                else
                {
                    Console.WriteLine("Invalid input, using default of 3 rounds.");
                }
            } while (game.TotalRounds <= 0);

            game.Rounds++;
            do
            {
                Console.WriteLine($"Round {game.Rounds}:");
                DetermineWinner(player1, player2);
                Console.WriteLine($"Scores: {player1.Name} - {player1.Score}, {player2.Name} - {player2.Score}");
                game.Rounds++;
            } while (game.Rounds <= game.TotalRounds);

            Console.WriteLine($"Final Scores: {player1.Name} - {player1.Score}, {player2.Name} - {player2.Score}");
        }

        private static void DetermineWinner(Player player1, Player player2)
        {
            player1.Choice = GetPlayerChoice(player1.Name);
            player2.Choice = GetPlayerChoice(player2.Name);

            if (player1.Choice == player2.Choice)
            {
                Console.WriteLine("It's a tie!");
            }
            else if ((player1.Choice == "rock" && player2.Choice == "scissors") ||
                    (player1.Choice == "scissors" && player2.Choice == "paper") ||
                     (player1.Choice == "paper" && player2.Choice == "rock"))
            {
                Console.WriteLine($"{player1.Name} wins!");
                player1.Score++;
            }
            else
            {
                Console.WriteLine($"{player2.Name} wins!");
                player2.Score++;
            }
        }

        private static string GetPlayerChoice(string playerName)
        {
            string choice;
            do
            {
                Console.WriteLine($"{playerName}, enter your choice (rock, paper, scissors) or type `random` for random selection:");
                choice = Console.ReadLine().ToLower();

                if (choice == "random")
                {
                    Random random = new Random();
                    choice = Choices[random.Next(Choices.Count)];
                    Console.WriteLine($"{playerName} chose: {choice}");
                }

            } while (!Choices.Contains(choice));

            return choice;
        }
    }

    class Player
    {
        public string Name { get; set; }
        public string Choice { get; set; }
        public int Score { get; set; } = 0;
    }

    class Game
    {
        public List<Player> Players { get; set; } = new List<Player>();
        public int Rounds { get; set; }
        public int TotalRounds { get; set; } = 3; // Default best of 3 rounds
        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }
    }
}