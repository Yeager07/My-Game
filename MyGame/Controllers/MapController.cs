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
    public class MapController
    {
        public int[,] map;
        public bool redraw;

        // Величины квадратов
        public int cellSizeX = 51;
        public int cellSizeY = 51;


        public Image imgMap;

        public int cellLimitX;
        public int cellLimitY;

        public MapController()
        {
            redraw = true;
        }

        public void Init()
        {
            imgMap = new Bitmap(@"..\..\..\Sprites\Back.png");
        }

        public void DrawMap(Graphics g, string currColor)
        {
            cellLimitX = GetWidth() / cellSizeX;
            cellLimitY = GetHeight() / cellSizeY;

            for (int i = 0; i < cellLimitY; i++)
                for (int j = 0; j < cellLimitX; j++)
                {
                    if (map[i, j] == 2)
                        g.DrawImage(new Bitmap(@"..\..\..\Sprites\wall" + currColor + ".png"), new Rectangle(new Point(cellSizeX * j, cellSizeY * i), new Size(cellSizeX, cellSizeY)), 0, 0, 59, 59, GraphicsUnit.Pixel);

                    else if (map[i, j] == 3)
                        g.DrawImage(new Bitmap(@"..\..\..\Sprites\glass" + currColor + ".png"), cellSizeX * j, cellSizeY * i, 52, 52);

                    else
                        g.DrawImage(imgMap,
                        new Rectangle(new Point(cellSizeX * j, cellSizeY * i), new Size(cellSizeX, cellSizeY)),
                        cellSizeX * j, cellSizeY * i,
                        cellSizeX, cellSizeY,
                        GraphicsUnit.Pixel);
                }
        }

        public int[,] GetMap(string color)
        {
            if (color == "Green")
                map = Map.greenMap;

            else if (color == "Purple")
                map = Map.purpleMap;

            else if (color == "Blue")
                map = Map.blueMap;

            else
                map = Map.redMap;

            return map;
        }

        public int GetWidth()
        {
            return imgMap.Size.Width;
        }

        public int GetHeight()
        {
            return imgMap.Size.Height;
        }
    }
}
