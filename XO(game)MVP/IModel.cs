using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XO_game_MVP
{
    internal interface IModel
    {
        string[,] Board { get; }
        string CurrentPlayer { get; set; }
        string CurrentLevel{ get; set; } 
        Image ImageCross { get; }
        Image ImageNull { get; }
        Image BaseImage { get; }
        Tuple<int, int> ComputerMoveSimple();
        bool IsBoardFull(int x, int y);
        void ResetGame();
        string CheckWinCondition();
        Tuple<int, int> FindBestMove();
        int Minimax(int a, bool f);
        int Evaluate();
    }
}
