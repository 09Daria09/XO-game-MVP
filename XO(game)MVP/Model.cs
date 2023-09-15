using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XO_game_MVP
{
    internal class Model : IModel
    {
        public string[,] Board { get; } = new string[3, 3];

        public string CurrentPlayer { get; set; }
        public string CurrentLevel { get; set; }
        public Image ImageCross
        {
            get
            {
                return Properties.Resources.Big_Cross;
            }
        }
        public Image ImageNull
        {
            get
            {
                return Properties.Resources.Big_null;
            }
        }
        public Image BaseImage
        {
            get
            {
                return Properties.Resources.Butt;
            }
        }


        public string CheckWinCondition()
        {
            for (int i = 0; i < 3; i++)
            {
                if (Board[i, 0] == Board[i, 1] && Board[i, 1] == Board[i, 2] && !string.IsNullOrEmpty(Board[i, 0]))
                {
                    return Board[i, 0];
                }
            }
            for (int j = 0; j < 3; j++)
            {
                if (Board[0, j] == Board[1, j] && Board[1, j] == Board[2, j] && !string.IsNullOrEmpty(Board[0, j]))
                {
                    return Board[0, j];
                }
            }
            if (Board[0, 0] == Board[1, 1] && Board[1, 1] == Board[2, 2] && !string.IsNullOrEmpty(Board[0, 0]))
            {
                return Board[0, 0];
            }

            if (Board[0, 2] == Board[1, 1] && Board[1, 1] == Board[2, 0] && !string.IsNullOrEmpty(Board[0, 2]))
            {
                return Board[0, 2];
            }

            return null;
        }
        public bool IsBoardFull(int x, int y)
        {
            if (Board[x, y] == null)
            {
                return false;
            }
            return true;
        }
        public void ResetGame()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Board[i, j] = null;
                }
            }
            CurrentPlayer = null;
        }
        public bool CheckingDraws()
        {
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    if (string.IsNullOrEmpty(Board[i, j]))
                    {
                        return false; 
                    }
                }
            }
            return true; 
        }
        public Tuple<int, int> ComputerMoveSimple() 
        {
            Random rand = new Random();
            int x, y;
            do
            {
                x = rand.Next(0, 3);
                y = rand.Next(0, 3);
            } while (IsBoardFull(x, y));
            return new Tuple<int, int>(x, y);
        }


        public int Minimax(int depth, bool isMax)
        {
            int score = Evaluate();

            if (score == 10)
                return score;

            if (score == -10)
                return score;

            if (AreAllCellsFilled())
                return 0;

            if (isMax)
            {
                int best = -1000;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (string.IsNullOrEmpty(Board[i, j]))
                        {
                            Board[i, j] = "X";
                            best = Math.Max(best, Minimax(depth + 1, !isMax));
                            Board[i, j] = null; 
                        }
                    }
                }
                return best;
            }
            else
            {
                int best = 1000;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (string.IsNullOrEmpty(Board[i, j]))
                        {
                            Board[i, j] = "O";
                            best = Math.Min(best, Minimax(depth + 1, !isMax));
                            Board[i, j] = null;
                        }
                    }
                }
                return best;
            }
        }
        bool AreAllCellsFilled()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (string.IsNullOrEmpty(Board[i, j]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public Tuple<int, int> FindBestMove()
        {
            int bestValue = -1000;
            Tuple<int, int> bestMove = new Tuple<int, int>(-1, -1);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (string.IsNullOrEmpty(Board[i, j]))
                    {
                        int moveValue = 0;
                        if (CurrentPlayer == "Крeстик")
                        {
                            Board[i, j] = "X";
                            moveValue = Minimax(0, false);
                        }
                        else
                        {
                            Board[i, j] = "O";
                            moveValue = Minimax(0, true);
                        }

                        Board[i, j] = null;

                        if (moveValue > bestValue)
                        {
                            bestMove = new Tuple<int, int>(i, j);
                            bestValue = moveValue;
                        }
                    }
                }
            }

            return bestMove;
        }
        public int Evaluate()
        {
            for (int row = 0; row < 3; row++)
            {
                if (Board[row, 0] == Board[row, 1] && Board[row, 1] == Board[row, 2])
                {
                    if (Board[row, 0] == "X")
                        return +10;
                    else if (Board[row, 0] == "O")
                        return -10;
                }
            }

            for (int col = 0; col < 3; col++)
            {
                if (Board[0, col] == Board[1, col] && Board[1, col] == Board[2, col])
                {
                    if (Board[0, col] == "X")
                        return +10;
                    else if (Board[0, col] == "O")
                        return -10;
                }
            }

            for (int col = 0; col < 3; col++)
            {
                if (Board[0, 2] == Board[1, 1] && Board[1, 1] == Board[2, 0] && !string.IsNullOrEmpty(Board[0, 2]))
                {
                    if (Board[0, col] == "X")
                        return +10;
                    else if (Board[0, col] == "O")
                        return -10;
                }
            }
            return 0; 
        }

    }
}
