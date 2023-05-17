using MyGame.Entites;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame.Controllers
{
    public class WalkController
    {
        public MapController mapController;
        public const int Hit = 24;
        public void IsCollide(Player player, int dirX = 0, int dirY = 0)
        {
            for (int i = player.posX / mapController.cellSizeX; i < (player.posX + mapController.cellSizeX) / mapController.cellSizeX; i++)
                for (int j = player.posY / mapController.cellSizeY; j < (player.posY + mapController.cellSizeY) / mapController.cellSizeY; j++)
                    if (mapController.map[j, i] == 0 || mapController.map[j, i] == 2)
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
