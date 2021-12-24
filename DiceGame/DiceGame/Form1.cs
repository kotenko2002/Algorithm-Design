using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiceGame
{
    public partial class Form1 : Form
    {
        int playerWins = 0, computerWins = 0;
        bool playerIsFirst = true, playerTurn = false;
        int playerPoints = 0, computerPoints = 0;
        bool playerSayEnough = false, computerSayEnough = false;
        Image cube1, cube2, cube3, cube4, cube5, cube6;
        public Form1()
        {
            InitializeComponent();

            cube1 = new Bitmap(new Bitmap(@"D:\University\University\AD\Labs\графика\Kub1.jpg"), new Size(147, 147));
            cube2 = new Bitmap(new Bitmap(@"D:\University\University\AD\Labs\графика\Kub2.jpg"), new Size(147, 147));
            cube3 = new Bitmap(new Bitmap(@"D:\University\University\AD\Labs\графика\Kub3.jpg"), new Size(147, 147));
            cube4 = new Bitmap(new Bitmap(@"D:\University\University\AD\Labs\графика\Kub4.jpg"), new Size(147, 147));
            cube5 = new Bitmap(new Bitmap(@"D:\University\University\AD\Labs\графика\Kub5.jpg"), new Size(147, 147));
            cube6 = new Bitmap(new Bitmap(@"D:\University\University\AD\Labs\графика\Kub6.jpg"), new Size(147, 147));
            Text = "Очко";

            PictureBox backGround = new PictureBox();
            backGround.Image = new Bitmap(@"D:\University\University\AD\Labs\графика\BackGround.jpg");
            backGround.Height = 778;
            backGround.Width = 625;
            Controls.Add(backGround);

            if (playerIsFirst)
            {
                playerTurn = true;
                ComputerButton.Enabled = false;
                EnoughButton.Enabled = false;
            } 
            else
            {
                EnoughButton.Enabled = false;
                MoreButton.Enabled = false;
            }
        }

        private void EnoughButton_Click(object sender, EventArgs e)
        {
            if(playerPoints > 0)
            {
                playerSayEnough = true;
                EnoughButton.Enabled = false;
                MoreButton.Enabled = false;
                ComputerButton.Enabled = (!playerSayEnough) ? false : true;
            }
        }

        private void MoreButton_Click(object sender, EventArgs e)
        {
            if (playerTurn && !playerSayEnough)
            {
                var playerDices = RollDise();
                playerPoints += (playerDices.Item1 + playerDices.Item2);
                playerPointsLabel.Text = $"Очки игрока: {playerPoints}";

                SetDiceImage(playerDices.Item1, CubeBox1);
                SetDiceImage(playerDices.Item2, CubeBox2);
                Update();
                
                playerTurn = false;
                ComputerButton.Enabled = true;
                EnoughButton.Enabled = false;
                MoreButton.Enabled = false;
                GameOver();
            }
        }

        private void ComputerButton_Click(object sender, EventArgs e)
        {
            if (!playerTurn || playerSayEnough)
            {
                if(!(playerSayEnough && (playerPoints < computerPoints)))
                {
                    if (playerIsFirst) // игрок начинает
                    {
                        if ((AI.GetComputerNeedToRollSecond(playerPoints, computerPoints) && computerPoints!=20) || (playerPoints > computerPoints))
                        {
                            var computerDices = RollDise();
                            computerPoints += (computerDices.Item1 + computerDices.Item2);
                            ComputerPointsLabel.Text = $"Очки дедули: {computerPoints}";

                            SetDiceImage(computerDices.Item1, CubeBox1);
                            SetDiceImage(computerDices.Item2, CubeBox2);
                            Update();
                        }
                        else
                        {
                            computerSayEnough = true;
                        }
                    }
                    else // пк начинает
                    {
                        if (AI.GetComputerNeedToRollFirst(playerPoints, computerPoints) || (playerPoints > computerPoints))
                        {
                            var computerDices = RollDise();
                            computerPoints += (computerDices.Item1 + computerDices.Item2);
                            ComputerPointsLabel.Text = $"Очки дедули: {computerPoints}";

                            SetDiceImage(computerDices.Item1, CubeBox1);
                            SetDiceImage(computerDices.Item2, CubeBox2);
                            Update();
                        }
                        else
                        {
                            computerSayEnough = true;
                            ComputerButton.Enabled = false;
                        }
                    }
                }
                else 
                {
                    computerSayEnough = true;
                    ComputerButton.Enabled = false;
                }

                if (!playerSayEnough)
                {
                    playerTurn = true;
                    MoreButton.Enabled = true;
                    EnoughButton.Enabled = true;
                }
                    
                ComputerButton.Enabled = (!playerSayEnough) ? false : true;
                GameOver();
            }
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            playerIsFirst = (playerIsFirst) ? false : true;
            playerPoints = 0;
            computerPoints = 0;
            playerSayEnough = false;
            computerSayEnough = false;

            playerPointsLabel.Text = $"Очки игрока: {playerPoints}";
            ComputerPointsLabel.Text = $"Очки дедули: {computerPoints}";
            
            if (playerIsFirst)
            {
                RestartButton.Enabled = false;
                ComputerButton.Enabled = false;
                EnoughButton.Enabled = true;
                MoreButton.Enabled = true;
                playerTurn = true;
            }
            else
            {
                playerTurn = false;
                RestartButton.Enabled = false;
                ComputerButton.Enabled = true;
                EnoughButton.Enabled = false;
                MoreButton.Enabled = false;
            }
            WinLabel.Visible = false;
            LoseLabel.Visible = false;
            DrawLabel.Visible = false;
        }
        public void GameOver()
        {
            if(playerSayEnough && computerSayEnough)
            {
                TurnOffButtons();
                switch (CheckWiner(playerPoints, computerPoints))
                {
                    case "YOU WIN":
                        playerWins++;
                        WinLabel.Visible = true;
                        break;
                    case "YOU LOSE":
                        computerWins++;
                        LoseLabel.Visible = true;
                        break;
                    case "DRAW":
                        DrawLabel.Visible = true;
                        break;
                    default:
                        break;
                }
                ScoreLabel.Text = $"(И) {playerWins} : {computerWins} (Д)";
            }
            else
            {
                if(playerPoints > 21)
                {
                    computerWins++;
                    TurnOffButtons();
                    ScoreLabel.Text = $"(И) {playerWins} : {computerWins} (Д)";
                    LoseLabel.Visible = true;
                }
                if(computerPoints > 21)
                {
                    playerWins++;
                    TurnOffButtons();
                    ScoreLabel.Text = $"(И) {playerWins} : {computerWins}(Д) ";
                    WinLabel.Visible = true;
                }
            }
            
        }
        public (int, int) RollDise()
        {
            Random random = new Random();
            int resCube1 = random.Next(1, 7);
            int resCube2 = random.Next(1, 7);

            return (resCube1, resCube2);
        }
        public void SetDiceImage(int num, PictureBox pictureBox)
        {
            switch (num)
            {
                case 1:
                    pictureBox.Image = cube1;
                    break;
                case 2:
                    pictureBox.Image = cube2;
                    break;
                case 3:
                    pictureBox.Image = cube3;
                    break;
                case 4:
                    pictureBox.Image = cube4;
                    break;
                case 5:
                    pictureBox.Image = cube5;
                    break;
                case 6:
                    pictureBox.Image = cube6;
                    break;
                default:
                    break;
            }
        }
        public string CheckWiner(int Ppoints, int PCpoints)
        {
            bool playerLose = false, computerLose = false;
            if (Ppoints > 21)
                playerLose = true;
            if (PCpoints > 21)
                computerLose = true;

            if (playerLose && computerLose)
                return "DRAW";
            else if (playerLose)
                return "YOU LOSE";
            else if (computerLose)
                return "YOU WIN";
            else
            {
                if (Ppoints > PCpoints)
                    return "YOU WIN";
                else if (Ppoints < PCpoints)
                    return "YOU LOSE";
                else
                    return "DRAW";
            }
        }
        public void TurnOffButtons()
        {
            RestartButton.Enabled = true;
            ComputerButton.Enabled = false;
            EnoughButton.Enabled = false;
            MoreButton.Enabled = false;
        }
    }
}
