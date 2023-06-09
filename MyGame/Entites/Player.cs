﻿using MyGame.Controllers;
using System.Drawing;

namespace MyGame.Entites
{
    public class Player
    {
        // Положение персонажа
        public int posX;
        public int posY;

        // Направление движения персонажа
        public int dirX;
        public int dirY;

        // Состояние персонажа
        public bool isMoving;
        public int flip;
        public string color;
        // Фреймы для отрисовки анимации

        public int walkUpFrames;
        public int walkDownFrames;
        public int walkSideFrames;
        public int stayUpFrames;
        public int stayDownFrames;
        public int staySideFrames;
        public int redScore;
        public int greenScore;
        public int purpleScore;
        public int blueScore;

        // Текущий фрейм, фнимация и ограничение
        public int currFrame;
        public int currAnimation;
        public int currLimit;

        // Размер бокса персонажа
        public int sizeX;
        public int sizeY;

        // Картинка персонажа
        public Image PlayerSprites;

        // Инициализация персонажа
        public Player(int posX, int posY, int walkUpFrames, int walkDownFrames, int walkSideFrames, int stayUpFrames, int stayDownFrames, int staySideFrames, int redScore, int greenScore, int blueScore, int purpleScore)
        {
            this.posX = posX;
            this.posY = posY;
            this.walkUpFrames = walkUpFrames;
            this.walkDownFrames = walkDownFrames;
            this.walkSideFrames = walkSideFrames;
            this.stayUpFrames = stayUpFrames;
            this.stayDownFrames = stayDownFrames;
            this.staySideFrames = staySideFrames;
            this.redScore = redScore;
            this.greenScore = greenScore;
            this.blueScore = blueScore;
            this.purpleScore = purpleScore;
            sizeX = 70;
            sizeY = 110;
            currAnimation = 0;
            currFrame = 0;
            currLimit = walkUpFrames;
            flip = 1;
        }

        public void Init(Image PlayerSprites)
        {
            this.PlayerSprites = PlayerSprites;
        }

        // Движение персонажа
        public void Move()
        {
                posX += dirX;
                posY += dirY;
        }


        public void PlayAnimation(Graphics g)
        {
            if (currFrame < currLimit - 1)
                currFrame++;
            else currFrame = 0;

            g.DrawImage(PlayerSprites,
                new Rectangle(new Point(posX - flip*sizeX / 2, posY), new Size(flip * sizeX, sizeY)), 
                sizeX * currFrame, sizeY * currAnimation, 
                sizeX, sizeY,
                GraphicsUnit.Pixel);
        }

        public void SetAnimation(int currAnimation)
        {
            this.currAnimation = currAnimation;

            switch (currAnimation)
            {
                case 0:
                    currLimit = stayDownFrames;
                    break;
                case 1:
                    currLimit = walkDownFrames;
                    break;
                case 2:
                    currLimit = stayUpFrames;
                    break;
                case 3:
                    currLimit = walkUpFrames;
                    break;
                case 4:
                    currLimit = staySideFrames;
                    break;
                case 5:
                    currLimit = walkSideFrames;
                    break;
            }
        }
    }
}
