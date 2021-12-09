using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teeko__alpha_beta_pruning
{
    public partial class Game : Form
    {
        char[,] map = new char[Constants.mapSize, Constants.mapSize];
        Image blackFigure, whiteFigure;
        Label win, lose;
        Button prevButton;
        Button[,] buttons;
        public Game()
        {
            InitializeComponent();
            
            blackFigure = new Bitmap(new Bitmap(Constants.blackColor),
                new Size(Constants.cellSize - 15, Constants.cellSize - 15));
            whiteFigure = new Bitmap(new Bitmap(Constants.whiteColor),
                new Size(Constants.cellSize - 15, Constants.cellSize - 15));
            Icon = new Icon(Constants.appIcon);
            Text = "Teeko";

            Init();
        }
        public void Init()
        {
            prevButton = null;

            map = new char[,]
            {
                { '0','0','0','0','0'},
                { '0','0','0','0','0'},
                { '0','0','0','0','0'},
                { '0','0','0','0','0'},
                { '0','0','0','0','0'}
            };
            CreateMap();
        }
        public void CreateMap()
        {
            Width = (Constants.cellSize * Constants.mapSize) + 17;
            Height = (Constants.cellSize * Constants.mapSize) + 40;

            buttons = new Button[Constants.mapSize, Constants.mapSize];

            CreateLabels();

            for (int i = 0; i < Constants.mapSize; i++)
            {
                for (int j = 0; j < Constants.mapSize; j++)
                {
                    Button button = new Button();
                    buttons[i, j] = button;
                    button.Location = new Point(j * Constants.cellSize, i * Constants.cellSize);
                    button.Size = new Size(Constants.cellSize, Constants.cellSize);
                    button.Click += new EventHandler(PlayerTurnToPut);

                    if (map[i, j] == 'C')
                        button.Image = blackFigure;
                    else if (map[i, j] == 'P')
                        button.Image = whiteFigure;

                    button.BackColor = Color.White;
                    if (i % 2 != 0)
                        if (j % 2 != 0)
                            button.BackColor = Color.Gray;
                    if (i % 2 == 0)
                        if (j % 2 == 0)
                            button.BackColor = Color.Gray;
                    Controls.Add(button);
                }
            }
        }
        public void CreateLabels()
        {
            lose = new Label();
            lose.Text = "YOU LOSE!";
            lose.ForeColor = Color.Red;
            lose.Font = new Font("Arial", 28, FontStyle.Bold, GraphicsUnit.Point, ((Byte)(0)));
            lose.Top = 500;
            lose.Left = 140;
            lose.AutoSize = true;
            Controls.Add(lose);
            lose.Visible = false;
            win = new Label();
            win.Text = "YOU WIN!";
            win.ForeColor = Color.Green;
            win.Font = new Font("Arial", 30F, FontStyle.Bold, GraphicsUnit.Point, ((Byte)(0)));
            win.Top = 500;
            win.Left = 150;
            win.AutoSize = true;
            win.Visible = false;
            Controls.Add(win);
        }

        public void PlayerTurnToPut(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;
            if (map[pressedButton.Location.Y / Constants.cellSize, pressedButton.Location.X / Constants.cellSize] == '0')
            {
                map[pressedButton.Location.Y / Constants.cellSize, pressedButton.Location.X / Constants.cellSize] = 'P';
                pressedButton.Image = whiteFigure;
                Update();
                if (!GameOver('P'))
                {
                    ComputerTurnToPut();
                    GameOver('C');
                }
                if (FirstStageIsCompleted())
                    for (int i = 0; i < Constants.mapSize; i++)
                        for (int j = 0; j < Constants.mapSize; j++)
                        {
                            buttons[i, j].Click -= PlayerTurnToPut;
                            buttons[i, j].Click += new EventHandler(PlayerTurnToMove);
                        }
            }
        }
        public void ComputerTurnToPut()
        {
            //TO DO: strategy
            Random random = new Random();
            bool changeComplete = false;
            do
            {
                int i = random.Next(0, 5), j = random.Next(0, 5);
                if (map[i, j] == '0')
                {
                    map[i, j] = 'C';
                    buttons[i, j].Image = blackFigure;
                    changeComplete = true;
                }
            }
            while (!changeComplete);
        }
        public void PlayerTurnToMove(object sender, EventArgs e)
        {
            Button pressedButton = sender as Button;
            if (prevButton == null)
            {
                if(map[pressedButton.Location.Y / Constants.cellSize, pressedButton.Location.X / Constants.cellSize] == 'P')
                {
                    prevButton = pressedButton;
                    pressedButton.BackColor = Color.Red;
                }
            }
            else
            {
                prevButton.BackColor = GetPrevButtonColor();
                if (map[pressedButton.Location.Y / Constants.cellSize, pressedButton.Location.X / Constants.cellSize] == '0'
                    && MoveLimitation((prevButton.Location.Y / Constants.cellSize, prevButton.Location.X / Constants.cellSize), 
                    (pressedButton.Location.Y / Constants.cellSize, pressedButton.Location.X / Constants.cellSize)))
                {
                    char buff = map[pressedButton.Location.Y / Constants.cellSize, pressedButton.Location.X / Constants.cellSize];
                    map[pressedButton.Location.Y / Constants.cellSize, pressedButton.Location.X / Constants.cellSize] =
                        map[prevButton.Location.Y / Constants.cellSize, prevButton.Location.X / Constants.cellSize];
                    map[prevButton.Location.Y / Constants.cellSize, prevButton.Location.X / Constants.cellSize] = buff;
                    pressedButton.Image = prevButton.Image;
                    
                    prevButton.Image = null;
                    Update();
                    if (!GameOver('P'))
                    {
                        ComputerTurnToMove();
                        GameOver('C');
                    }
                }
                prevButton = null;
            }
        }
        public void ComputerTurnToMove()
        {
            map = AI.MiniMax(map, 4, true).Item1;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (map[i, j] == 'C')
                        buttons[i, j].Image = blackFigure;
                    else if (map[i, j] == 'P')
                        buttons[i, j].Image = whiteFigure;
                    else
                        buttons[i, j].Image = null;
                }
            }
            Update();
            //TO DO: minimax
            //Thread.Sleep(5000);
        }

        public bool MoveLimitation((int,int) from, (int, int) to)
        {
            if ((from.Item1 - to.Item1 == 1 || from.Item1 - to.Item1 == 0 || from.Item1 - to.Item1 == -1)
                && (from.Item2 - to.Item2 == 1 || from.Item2 - to.Item2 == 0 || from.Item2 - to.Item2 == -1))
                return true;
            return false;
        }
        public bool FirstStageIsCompleted()
        {
            int checkersCounter = 0;
            for (int i = 0; i < Constants.mapSize; i++)
                for (int j = 0; j < Constants.mapSize; j++)
                    if (map[i, j] != '0')
                        checkersCounter++;
            return (checkersCounter == 10) ? true : false;
        }
        public Color GetPrevButtonColor()
        {
            if ((prevButton.Location.Y / Constants.cellSize) % 2 != 0)
                if ((prevButton.Location.X / Constants.cellSize) % 2 != 0)
                    return Color.Gray;
            if ((prevButton.Location.Y / Constants.cellSize) % 2 == 0)
                if ((prevButton.Location.X / Constants.cellSize) % 2 == 0)
                    return Color.Gray;
            return Color.White;
        }
        public bool GameOver(char symbol)
        {
            bool result = WinnerFinder.CheckWinner(map, symbol);
            if (WinnerFinder.CheckWinner(map, symbol))
            {
                for (int i = 0; i < Constants.mapSize; i++)
                    for (int j = 0; j < Constants.mapSize; j++)
                        buttons[i, j].Enabled = false;
                if (symbol == 'P')
                    win.Visible = true;
                if (symbol == 'C')
                    lose.Visible = true;
                Height += 50;
            }
            return result;
        }
    }
}
