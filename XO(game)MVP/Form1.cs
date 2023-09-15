using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XO_game_MVP
{
    public partial class Form1 : Form, IView
    {
        private Button[,] board = new Button[3,3];

        public Form1()
        {
            InitializeComponent();

            Board[0, 0] = button1;
            Board[0, 1] = button2;
            Board[0, 2] = button3;
            Board[1, 0] = button4;
            Board[1, 1] = button5;
            Board[1, 2] = button6;
            Board[2, 0] = button7;
            Board[2, 1] = button8;
            Board[2, 2] = button9;

            SelectingCross = radioButton1;
            SelectingNull = radioButton2;
            SelectingSimpleLevel = radioButton3;
            SelectingHardLevel = radioButton4;
        }


        public Button[,] Board
        {
            get
            {
                return board; 
            }
            set
            {
                board = value;
            }
        }

        public RadioButton SelectingCross { get; set; }
        public RadioButton SelectingNull { get; set; }
        public RadioButton SelectingSimpleLevel { get; set ; }
        public RadioButton SelectingHardLevel { get; set; }
 
        public event EventHandler<SelectionEventArgs> SelectionEvent;
        public event EventHandler<EventArgs> RepeatEvent;
        public event EventHandler<EventArgs> RadioEvent;

        //метод выбора клетки на игровом поле
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                Button clickedButton = sender as Button;

                if (clickedButton != null)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (Board[i, j] == clickedButton)
                            {
                                SelectionEvent?.Invoke(this, new SelectionEventArgs(i, j));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //метод выбора кнопки рестарта
        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                RepeatEvent?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //метод выбора радиокнопки
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                RadioEvent?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
