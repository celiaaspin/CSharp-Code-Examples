using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Code_Examples.Basics
{
    class TicTacToe
        //Author: Celia Aspin
        //Player can play a game of Tic Tac Toe against another player or a computer using minimax


    {
        static char[] board = { '1', '2', '3', '4', '5', '6', '7', '8', '9' }; // Initial board setup with positions
        static char currentPlayer = 'X'; //Start with player x
        public static void run()
        {
            int playerOrComp;
            int playerInput;
            bool gamePlayer = true;
            bool gameComputer = true;

            //Prompt to choose game mode
            Console.WriteLine("Would you like to play against: \n" +
                              "1. A Player \n" +
                              "2. A Computer");
            playerOrComp = int.Parse(Console.ReadLine());

            if (playerOrComp == 1) //If playing against player
            {
                while (gamePlayer)
                {
                    Console.Clear();
                    displayBoard();

                    Console.WriteLine($"Player {currentPlayer}, enter your move (1-9): ");
                    playerInput = int.Parse(Console.ReadLine());

                    //Validating players move
                    if (playerInput < 1 || playerInput > 9 || board[playerInput - 1] == 'X' || board[playerInput - 1] == 'O')
                    {
                        Console.WriteLine("Invalid move, try again.");
                        continue;
                    }

                    board[playerInput - 1] = currentPlayer; //Placing move on board

                    if (checkWin()) //Check if current player has won
                    {
                        Console.Clear();
                        displayBoard();
                        Console.WriteLine($"Player {currentPlayer} wins!");
                        gamePlayer = false;
                    }
                    else if (checkDraw()) //Check if palyers have drawn
                    {
                        Console.Clear();
                        displayBoard();
                        Console.WriteLine("It's a draw!");
                        gamePlayer = false;
                    }
                    else
                    {
                        currentPlayer = (currentPlayer == 'X') ? 'O' : 'X'; //Switch players
                    }
                }
            }
            else if (playerOrComp == 2) //Playing against computer
            {
                while (gameComputer)
                {
                    Console.Clear();
                    displayBoard();

                    //Players turn
                    if (currentPlayer == 'X')
                    {
                        Console.WriteLine($"Player {currentPlayer}, enter your move (1-9): ");
                        playerInput = int.Parse(Console.ReadLine());

                        //Validating players move
                        if (playerInput < 1 || playerInput > 9 || board[playerInput - 1] == 'X' || board[playerInput - 1] == 'O')
                        {
                            Console.WriteLine("Invalid move, try again.");
                            continue;
                        }

                        board[playerInput - 1] = currentPlayer; //Place move on board

                        if (checkWin()) //Check if player has won
                        {
                            Console.Clear();
                            displayBoard();
                            Console.WriteLine($"Player {currentPlayer} wins!");
                            gameComputer = false;
                        }
                        else if (checkDraw()) //Check if players have drawn
                        {
                            Console.Clear();
                            displayBoard();
                            Console.WriteLine("It's a draw!");
                            gameComputer = false;
                        }
                        else
                        {
                            currentPlayer = 'O'; // Switch to computer's turn
                        }
                    }
                    else //Computer's turn
                    {
                        Console.WriteLine("Computer is making a move...");
                        int bestMove = findBestMove(); //Finds the best move for computer
                        board[bestMove] = currentPlayer; //Place computer move on board

                        if (checkWin()) //Check if computer won
                        {
                            Console.Clear();
                            displayBoard();
                            Console.WriteLine("Computer Wins!");
                            gameComputer = false;
                        }
                        else if (checkDraw()) //Check if the game is a draw
                        {
                            Console.Clear();
                            displayBoard();
                            Console.WriteLine("It's a draw!");
                            gameComputer = false;
                        }
                        else
                        {
                            currentPlayer = 'X'; // Switch back to player's turn
                        }
                    }
                }
            }
        }

        static void displayBoard()
        {
            //Display current board state
            Console.WriteLine($" {board[0]} | {board[1]} | {board[2]} ");
            Console.WriteLine("---+---+---");
            Console.WriteLine($" {board[3]} | {board[4]} | {board[5]} ");
            Console.WriteLine("---+---+---");
            Console.WriteLine($" {board[6]} | {board[7]} | {board[8]} ");
        }

        static bool checkWin()
        {
            // Check rows, columns, and diagonals for a win
            return (board[0] == currentPlayer && board[1] == currentPlayer && board[2] == currentPlayer) ||
                   (board[3] == currentPlayer && board[4] == currentPlayer && board[5] == currentPlayer) ||
                   (board[6] == currentPlayer && board[7] == currentPlayer && board[8] == currentPlayer) ||
                   (board[0] == currentPlayer && board[3] == currentPlayer && board[6] == currentPlayer) ||
                   (board[1] == currentPlayer && board[4] == currentPlayer && board[7] == currentPlayer) ||
                   (board[2] == currentPlayer && board[5] == currentPlayer && board[8] == currentPlayer) ||
                   (board[0] == currentPlayer && board[4] == currentPlayer && board[8] == currentPlayer) ||
                   (board[2] == currentPlayer && board[4] == currentPlayer && board[6] == currentPlayer);
        }

        static bool checkDraw()
        {
            // Check if all cells are filled
            foreach (char c in board)
            {
                if (c != 'X' && c != 'O')
                {
                    return false;
                }
            }
            return true;
        }

        static int minimax(char[] newBoard, bool isMaximizing)
        {
            char originalPlayer = currentPlayer; // Save the original currentPlayer
            //Check for states
            if (checkWin())
            {
                return isMaximizing ? -1 : 1;
            }

            if (checkDraw())
            {
                return 0;
            }

            //Maximizing player (Computer)
            if (isMaximizing)
            {
                int bestScore = int.MinValue;
                for (int i = 0; i < 9; i++)
                {
                    if (newBoard[i] != 'X' && newBoard[i] != 'O')
                    {
                        char backup = newBoard[i];
                        newBoard[i] = 'O';
                        currentPlayer = 'O'; 
                        int score = minimax(newBoard, false);
                        newBoard[i] = backup;
                        bestScore = Math.Max(score, bestScore);
                    }
                }
                currentPlayer = originalPlayer; // Restore the original currentPlayer
                return bestScore;
            }
            else
            {
                //Minimazing Player (Player)
                int bestScore = int.MaxValue;
                for (int i = 0; i < 9; i++)
                {
                    if (newBoard[i] != 'X' && newBoard[i] != 'O')
                    {
                        char backup = newBoard[i];
                        newBoard[i] = 'X';
                        currentPlayer = 'X';
                        int score = minimax(newBoard, true);
                        newBoard[i] = backup;
                        bestScore = Math.Min(score, bestScore);
                    }
                }
                currentPlayer = originalPlayer; // Restore the original currentPlayer
                return bestScore;
            }
        }

        static int findBestMove()
        {
            int bestMove = -1;
            int bestScore = int.MinValue;
            for (int i = 0; i < 9; i++)
            {
                if (board[i] != 'X' && board[i] != 'O')
                {
                    char backup = board[i];
                    board[i] = 'O';
                    currentPlayer = 'O'; 
                    int score = minimax(board, false); //Call minimax to get best move
                    board[i] = backup;
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = i;
                    }
                }
            }
            return bestMove;
        }
    }
}


