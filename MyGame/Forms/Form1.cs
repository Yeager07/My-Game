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
        public int speed = 20;
        public int currAnimation;
        public string skin;
        public int currFlip;
        public int currColor = 1;


        public Form1()
        {
            InitializeComponent();

            timer1.Interval = 10;
            timer1.Tick += new EventHandler(Update);

            MessageBox.Show("1) Соберите все колбочки, чтобы завершить уровень; \n" +
                "2) Старайтесь не попасться монстрам (иначе вам придется начать сначала);\n" +
                @"3) С помощью клавиш ""1"", ""2"", ""3"", 4"" (не на Num-паде) вы сможете менять цвет комнаты (попробуйте, и узнаете зачем это нужно); \n" +
                @"4) Управление осуществляется клавишами ""WASD"" вашего устройства; \n" +
                "5) Движение по диагонали запрещено; \n" +
                "Удачи, у вас все получится!!! \n", "Правила Игры") ;

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


        /// <summary>
        /// Метод перемещает персонажа при помощи WASD управления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
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

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    MakeMove(1, 0, -speed, 3);
                    break;
                case Keys.S:
                    MakeMove(1, 0, speed, 1);
                    break;
                case Keys.D:
                    MakeMove(-1, speed, 0, 5);
                    break;
                case Keys.A:
                    MakeMove(1, -speed, 0, 5);
                    break;
                case Keys.D1:
                    currColor = 1;
                    MakeSkin("koldunred");
                    break;
                case Keys.D2:
                    currColor = 2;
                    MakeSkin("koldunblue");
                    break;
                case Keys.D3:
                    currColor = 3;
                    MakeSkin("koldungreen");
                    break;
                case Keys.D4:
                    currColor = 4;
                    MakeSkin("koldunpurple");
                    break;
            }
        }

        /// <summary>
        /// Метод отсанавливает персонажа при отпускании клавиши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    currAnimation = 2;
                    player.dirY = 0;
                    break;
                case Keys.S:
                    currAnimation = 0;
                    player.dirY = 0;
                    break;
                case Keys.D:
                    currAnimation = 4;
                    player.dirX = 0;
                    break;
                case Keys.A:
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
            WalkController.IsCollide(player);
            if (player.isMoving)
                player.Move();

            Invalidate();
        }

        private void Init()
        {
            MapController.Init();
            this.Width = 1900;
            this.Height = 1000;



            playerSprites = new Bitmap(@"..\..\..\Sprites\" + "koldunred" + ".png");
            player = new Player(this.Width / 2, this.Height / 2, Hero.walkUpFrames, Hero.walkDownFrames, Hero.walkSideFrames, Hero.stayUpFrames, Hero.stayDownFrames, Hero.staySideFrames);
            player.Init(playerSprites);
            timer1.Start();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            MapController.DrawMap(g);
            player.PlayAnimation(g);

        }
    }
}
