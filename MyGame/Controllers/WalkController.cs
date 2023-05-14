using MyGame.Entites;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Controllers
{
    public static class WalkController
    {
        public const int Hit = 24;
        public static void IsCollide(Player player, int dirX = 0, int dirY = 0)
        {
            for (int i = player.posX / MapController.cellSizeX; i < (player.posX + MapController.cellSizeX) / MapController.cellSizeX; i++)
            {
                for (int j = player.posY / MapController.cellSizeY; j < (player.posY + MapController.cellSizeY) / MapController.cellSizeY; j++)
                {
                    if (MapController.map[j, i] == 3)
                    {
                        player.score += 1;
                        MapController.map[j, i] = 1;
                    }

                    if (MapController.map[j, i] == 0 || MapController.map[j, i] == 2)
                    {
                        if (player.dirY > 0)
                            player.dirY -= Hit;
                        else if (player.dirY < 0)
                            player.dirY += Hit;
                        else if (player.dirX > 0)
                            player.dirX -= Hit;
                        else
                            player.dirX += Hit;
                    }
                }
            }
        }
    }
}
