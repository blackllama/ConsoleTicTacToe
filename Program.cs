using System;
using System.Linq;

namespace TicTacToe
{
    /*
    *Tic tac toe
    * 3x3 grid
    * Console App, 2 players, take input in turns
    * Print out the board after every move
    * Print out winner at end of game
    * Winner = first to get 3 in row
    * Tie if board full without 3 in row
    * Input: x,y coordinate
    *
    * - | - | -
    * - | - | -
    * - | - | -
    *
    *  edge cases to handle:
    * out of bounds of board
    * input not an coordinate pair
    * duplicate coordinate entered
    */

    public class Program
    {
        public static void Main(string[] args)
        {
            char[,] board = new char[3, 3]
            {
                {'-', '-', '-',},
                {'-', '-', '-',},
                {'-', '-', '-',}
            };

            PrintInstructions();
            PrintBoard(board);

            PlayGame(board);
        }

        private static void PrintInstructions()
        {
            Console.WriteLine("Welcome to Tic Tac Toe!");
            Console.WriteLine("To play this game, when its your turn, enter in the coordinates that you would like to place your piece");
            Console.WriteLine("The format of the input should look like '0,0'");
            Console.WriteLine("The board will update and display after every move");
            Console.WriteLine("The board currently looks like this: ");
        }

        public static bool HasWon(char[,] board)
        {
            //vertical match
            for (int colIndex = 0; colIndex < 3; colIndex++)
            {
                var column = Enumerable.Range(0, board.GetLength(0))
                    .Select(x => board[x, colIndex])
                    .ToArray();

                if (column.Distinct().Count() == 1 && !column.Distinct().First().Equals('-'))
                {
                    PrintWinner(column.Distinct().First());
                    return true;
                }
            }

            //horizontal match
            for (int rowIndex = 0; rowIndex < 3; rowIndex++)
            {
                var row = Enumerable.Range(0, board.GetLength(1))
                    .Select(x => board[rowIndex, x])
                    .ToArray();

                if (row.Distinct().Count() == 1 && !row.Distinct().First().Equals('-'))
                {
                    PrintWinner(row.Distinct().First());
                    return true;
                }
            }

            //diagonal match
            if (!board[0, 0].Equals('-') && board[0, 0].Equals(board[1, 1]) && board[0, 0].Equals(board[2, 2]))
            {
                PrintWinner(board[0, 0]);
                return true;
            }
            else if (!board[0, 2].Equals('-') && board[0, 2].Equals(board[1, 1]) && board[0, 2].Equals(board[2, 0]))
            {
                PrintWinner(board[0, 2]);
                return true;
            }

            return false;
        }

        public static void PrintWinner(char w)
        {
            Console.WriteLine(w.Equals('O') ? "Player 1 wins!" : "Player 2 wins!");
        }

        private static void PlayGame(char[,] board)
        {
            var hasWon = false;
            var moves = 1;

            while (!hasWon && moves < 10)
            {
                var player = moves % 2 == 0 ? 2 : 1;
                Console.WriteLine($"Player {player}'s turn");
                var input = Console.ReadLine();
                var coordinate = ParseInput(input);
                while (!ValidateInput(coordinate, board))
                {
                    Console.WriteLine($"Player {player}'s turn");
                    input = Console.ReadLine();
                    coordinate = ParseInput(input);
                }

                MakeMove(coordinate, board, player);
                moves++;
                hasWon = HasWon(board);
            }

            if (moves == 10 && !hasWon)
            {
                Console.WriteLine("Tie!");
            }
        }

        public static int[] ParseInput(string input)
        {
            //TODO: Handle invalid input better so user can re-enter
            try
            {
                var x = Convert.ToInt32(Char.GetNumericValue(input, 0));
                var y = Convert.ToInt32(Char.GetNumericValue(input, 2));
                return new int[] { x, y };
            }
            catch (Exception exception)
            {
                Console.WriteLine("Invalid Input");
            }

            return null;
        }

        //"1,1"
        private static bool ValidateInput(int[] play, char[,] board)
        {
            var x = play[0];
            var y = play[1];
            if (x >= 0 && y >= 0 && x < 3 && y < 3 && board[x, y] == '-')
            {
                return true;
            }
            else
            {
                Console.WriteLine("Invalid input, try again");
                return false;
            }
        }

        private static void MakeMove(int[] play, char[,] board, int player)
        {
            if (player == 1)
            {
                board[play[0], play[1]] = 'O';
            }
            else if (player == 2)
            {
                board[play[0], play[1]] = 'X';
            }

            PrintBoard(board);
        }

        public static void PrintBoard(char[,] board)
        {
            Console.WriteLine();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(board[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
