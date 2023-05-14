using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGame.Entites;

namespace MyGame.Controllers
{
    public static class MapController
    {
        // Величины квадратов
        public static int cellSizeX = 51;
        public static int cellSizeY = 51;


        public static int[,] map;
        public static Image imgMap;

        public static int cellLimitX;
        public static int cellLimitY;

        public static void Init()
        {
            imgMap = new Bitmap(@"..\..\..\Sprites\Back.png");
        }

        public static void DrawMap(Graphics g, string currColor)
        {
            cellLimitX = GetWidth() / cellSizeX;
            cellLimitY = GetHeight() / cellSizeY;

            for (int i = 0; i < cellLimitY; i++)
            {
                for (int j = 0; j < cellLimitX; j++)
                {
                    if (map[i, j] == 2)
                    {
                        g.DrawImage(new Bitmap(@"..\..\..\Sprites\wall" + currColor + ".png"), new Rectangle(new Point(cellSizeX * j, cellSizeY * i), new Size(cellSizeX, cellSizeY)), 0, 0, 59, 59, GraphicsUnit.Pixel);
                    }
                    else if (map[i,j] == 3)
                    {
                        g.DrawImage(new Bitmap(@"..\..\..\Sprites\glass" + currColor + ".png"), cellSizeX * j, cellSizeY * i, 52, 52);
                    }
                    else
                        g.DrawImage(imgMap,
                        new Rectangle(new Point(cellSizeX * j, cellSizeY * i), new Size(cellSizeX, cellSizeY)),
                        cellSizeX * j, cellSizeY * i,
                        cellSizeX, cellSizeY,
                        GraphicsUnit.Pixel);
                }
            }
        }

        public static int[,] GetMap(string color)
        {
            var mapGlobal = new Map();
            if (color == "Green")
                map = mapGlobal.greenMap;
            else if (color == "Purple")
                map = mapGlobal.purpleMap;
            else if (color == "Blue")
                map = mapGlobal.blueMap;
            else
                map = mapGlobal.redMap;
            return map;
        }

        public static int GetWidth()
        {
            return imgMap.Size.Width;
        }

        public static int GetHeight()
        {
            return imgMap.Size.Height;
        }
    }
}
