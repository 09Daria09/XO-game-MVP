using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XO_game_MVP
{
    internal class Presenter
    {
        private IView _view;
        private IModel _model;

        public Presenter(IView view, IModel model)
        {
            _view = view;
            _model = model;
            _view.SelectionEvent += GameLogic; 
            _view.RepeatEvent += new EventHandler<EventArgs>(Repeat);
            _view.RadioEvent += new EventHandler<EventArgs>(Radio);
        }
        private void Radio(object sender, EventArgs e)
        {
            UpdateCurrentPlayer();
            UpdateCurrentLevel();

            if ((_view.SelectingNull.Checked || _view.SelectingCross.Checked) &&
                (_view.SelectingSimpleLevel.Checked || _view.SelectingHardLevel.Checked))
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        _view.SelectingNull.Enabled = false;
                        _view.SelectingCross.Enabled = false;
                        _view.SelectingHardLevel.Enabled = false;
                        _view.SelectingSimpleLevel.Enabled = false;
                        _view.Board[i, j].Enabled = true;
                    }
                }
            }
            if (_model.CurrentPlayer == _view.SelectingNull.Text && 
                (_view.SelectingNull.Checked || _view.SelectingCross.Checked) &&
                (_view.SelectingSimpleLevel.Checked || _view.SelectingHardLevel.Checked))
            {
                var computerMove = _model.ComputerMoveSimple();
                _model.Board[computerMove.Item1, computerMove.Item2] = "X";
                _view.Board[computerMove.Item1, computerMove.Item2].BackgroundImage = _model.ImageCross;
            }
        }
        private void Repeat(object sender, EventArgs e)
        {
            _model.ResetGame();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _view.Board[i, j].Enabled = false;
                    _view.SelectingNull.Checked = false;
                    _view.SelectingCross.Checked = false;
                    _view.SelectingHardLevel.Checked = false;
                    _view.SelectingSimpleLevel.Checked = false;

                    _view.SelectingNull.Enabled = true;
                    _view.SelectingCross.Enabled = true;
                    _view.SelectingHardLevel.Enabled = true;
                    _view.SelectingSimpleLevel.Enabled = true;

                    _view.Board[i, j].BackgroundImage = _model.BaseImage;
                }
            }
        }
        private void GameLogic(object sender, SelectionEventArgs e)
        {
            if (_model.IsBoardFull(e.I, e.J))
            {
                _view.ShowMessage("Клетка уже занята ._.\n Выберите другую.");
                return;
            }

            if (_model.CurrentPlayer == _view.SelectingCross.Text)
            {
                _model.Board[e.I,e.J] = "X";
                _view.Board[e.I, e.J].BackgroundImage = _model.ImageCross;
            }
            else
            {
                _model.Board[e.I, e.J] = "O";
                _view.Board[e.I, e.J].BackgroundImage = _model.ImageNull;

            }

            if (_model.CurrentLevel == _view.SelectingSimpleLevel.Text)
            {
                var computerMove = _model.ComputerMoveSimple();
                ShowXorO(computerMove);
            }
            else
            {
                var computerMove = _model.FindBestMove();
                ShowXorO(computerMove);
            }

            string winner = _model.CheckWinCondition();

            if (!string.IsNullOrEmpty(winner))
            {
                _view.ShowMessage($"Победил {winner}!");
                Repeat(sender, e);
                return; 
            }
            if (_model.CheckingDraws())
            {
                _view.ShowMessage("Ничья");
                Repeat(sender, e);
                return;
            }
        }
        private void UpdateCurrentPlayer()
        {
            if (_view.SelectingNull.Checked)
            {
                _model.CurrentPlayer = _view.SelectingNull.Text;
            }
            else
            {
                _model.CurrentPlayer = _view.SelectingCross.Text;
            }
        }
        private void UpdateCurrentLevel() 
        {
            if (_view.SelectingSimpleLevel.Checked)
            {
                _model.CurrentLevel = _view.SelectingSimpleLevel.Text;
            }
            else
            {
                _model.CurrentLevel = _view.SelectingHardLevel.Text;
            }
        }
        private void ShowXorO(Tuple<int, int> computerMove)
        {
            if (_model.CurrentPlayer == _view.SelectingCross.Text)
            {
                _model.Board[computerMove.Item1, computerMove.Item2] = "O";
                _view.Board[computerMove.Item1, computerMove.Item2].BackgroundImage = _model.ImageNull;
            }
            else
            {
                _model.Board[computerMove.Item1, computerMove.Item2] = "X";
                _view.Board[computerMove.Item1, computerMove.Item2].BackgroundImage = _model.ImageCross;
            }
        }

    }
}
