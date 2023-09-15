using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XO_game_MVP
{
    internal interface IView
    {
        event EventHandler<SelectionEventArgs> SelectionEvent;

        event EventHandler<EventArgs> RepeatEvent;
        event EventHandler<EventArgs> RadioEvent;
        RadioButton SelectingCross { get; set; }
        RadioButton SelectingNull { get; set; }
        RadioButton SelectingSimpleLevel { get; set; }
        RadioButton SelectingHardLevel { get; set; } 
        Button [,] Board {get; set;}
        void ShowMessage(string message);
    }
    public class SelectionEventArgs : EventArgs
    {
        public int I { get; }
        public int J { get; }

        public SelectionEventArgs(int i, int j)
        {
            I = i;
            J = j;
        }
    }

}
