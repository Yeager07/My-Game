using MyGame.Controllers;
using MyGame.Entites;
using MyGame.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGame
{
    public partial class Form1 : Form
    {
        public Image playerSprites;
        public Player player;
        //public Form1 form = new Form1();
        public int speed = 20;
        public int currAnimation;
        public string skin;
        public int currFlip;
        public string currColor = "Red";
        public int currScore;
        private MapController mapController;

        public Form1()
        {
            InitializeComponent();
            Map.InitMaps();
            mapController = new();
            timer1.Interval = 5;
            timer1.Tick += new EventHandler(Update);

            MessageBox.Show("1) Соберите все колбочки, чтобы завершить уровень; \n" +
                "2) Чтоы включить паузу, нажмите 'Escape'; \n" +
                "3) С помощью клавиш '1', '2', '3', '4' (не на Num-паде) вы сможете менять цвет комнаты (попробуйте, и узнаете зачем это нужно);\n" +
                "4) Управление осуществляется клавишами-стрелочками вашего устройства;\n" +
                "5) Движение по диагонали запрещено; \n" +
                "6) Чтобы собрать колбочку, пройдите под ней; \n" +
                "Удачи, у вас все получится!!! \n", "Правила Игры");

            KeyDown += new KeyEventHandler(OnKeyDown);
            KeyUp += new KeyEventHandler(OnKeyUp);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;

            FormClosing += (sender, eventArgs) =>
            {
                var result = MessageBox.Show("Вы действительно хотите закрыть приложение?", "Выход",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    eventArgs.Cancel = true;
            };

            Init();
        }


        private void MakeMove(int isFlip, int speedX, int speedY, int numberAnimation)
        {
            player.flip = isFlip;
            currFlip = isFlip;
            player.dirX = speedX;
            player.dirY = speedY;
            player.isMoving = true;
            currAnimation = numberAnimation;
            player.SetAnimation(currAnimation);
        }

        private void MakeSkin(string nameSkin)
        {
            playerSprites = new Bitmap(@"..\..\..\Sprites\" + nameSkin + ".png");
            player.Init(playerSprites);
        }

        /// <summary>
        /// Метод перемещает персонажа при помощи WASD управления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    MakeMove(1, 0, -speed, 3);
                    break;
                case Keys.Down:
                    MakeMove(1, 0, speed, 1);
                    break;
                case Keys.Right:
                    MakeMove(-1, speed, 0, 5);
                    break;
                case Keys.Left:
                    MakeMove(1, -speed, 0, 5);
                    break;
                case Keys.D1:
                    currColor = "Red";
                    player.color = currColor;
                    MakeSkin("koldunred");
                    break;
                case Keys.D2:
                    currColor = "Blue";
                    player.color = currColor;
                    MakeSkin("koldunblue");
                    break;
                case Keys.D3:
                    currColor = "Green";
                    player.color = currColor;
                    MakeSkin("koldungreen");
                    break;
                case Keys.D4:
                    currColor = "Purple";
                    player.color = currColor;
                    MakeSkin("koldunpurple");
                    break;
                case Keys.Escape:
                    timer1.Stop();
                    MessageBox.Show("Игра приостановлена","Пауза");
                    timer1.Start();
                    break;
            }
        }

        /// <summary>
        /// Метод отсанавливает персонажа при отпускании клавиши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    currAnimation = 2;
                    player.dirY = 0;
                    break;
                case Keys.Down:
                    currAnimation = 0;
                    player.dirY = 0;
                    break;
                case Keys.Right:
                    currAnimation = 4;
                    player.dirX = 0;
                    break;
                case Keys.Left:
                    currAnimation = 4;
                    player.dirX = 0;
                    break;
            }
            if (player.dirX == 0 && player.dirY == 0)
            {
                player.isMoving = false;
                player.SetAnimation(currAnimation);
            }
        }

        private void Update(object sender, EventArgs e)
        {
            WalkController walkController = new();
            walkController.mapController = mapController;
            walkController.IsCollide(player);

            if (player.isMoving)
                player.Move();

            for (int i = player.posX / mapController.cellSizeX; i < (player.posX + mapController.cellSizeX) / mapController.cellSizeX; i++)
            {
                for (int j = player.posY / mapController.cellSizeY; j < (player.posY + mapController.cellSizeY) / mapController.cellSizeY; j++)
                {
                    if(mapController.map[j, i] == 3)
                        currScore += 1;
                    else
                        currScore = 0;
                }
            }
            if (currColor == "Red")
            {
                player.redScore += currScore;
            }
            else if (currColor == "Green")
                player.greenScore += currScore;
            else if (currColor == "Purple")
                player.purpleScore += currScore;
            else
                player.blueScore += currScore;

            if (currColor == "Red")
                score.Text = "Собрано колбочек: " + player.redScore.ToString() + " из 23";
            else if (currColor == "Green")
                score.Text = "Собрано колбочек: " + player.greenScore.ToString() + " из 24";
            else if (currColor == "Purple")
                score.Text = "Собрано колбочек: " + player.purpleScore.ToString() + " из 61";
            else
                score.Text = "Собрано колбочек: " + player.blueScore.ToString() + " из 49";

            if (player.redScore + player.greenScore + player.purpleScore + player.blueScore == 150)
            {
                timer1.Stop();
                MessageBox.Show("Поздравляю, вы собрали почти все колбочки!!!", "Концовка");
                timer1.Start();
            }

            Invalidate();
        }

        private void Init()
        {
            mapController.Init();
            this.Width = 1900;
            this.Height = 1000;

            playerSprites = new Bitmap(@"..\..\..\Sprites\" + "koldunred" + ".png");
            player = new Player(this.Width / 2, this.Height / 2, Hero.walkUpFrames, Hero.walkDownFrames, Hero.walkSideFrames, Hero.stayUpFrames, Hero.stayDownFrames, Hero.staySideFrames, 0, 0, 0, 0);
            player.Init(playerSprites);
            timer1.Start();
        }

        public void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            mapController.GetMap(currColor);
            mapController.DrawMap(g, currColor);

            for (int i = player.posX / mapController.cellSizeX; i < (player.posX + mapController.cellSizeX) / mapController.cellSizeX; i++)
                for (int j = player.posY / mapController.cellSizeY; j < (player.posY + mapController.cellSizeY) / mapController.cellSizeY; j++)
                    if (mapController.map[j, i] == 3)
                        mapController.map[j, i] = 1;

            player.PlayAnimation(g);
        }
    }
}