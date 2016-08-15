using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grow
{
    class Player
    {
        // The color that represents this player
        private Color color;
        // A list of all tiles that the player currently controls
        private List<Tile> tilesControlled = new List<Tile>();

        public Player(Color color)
        {
            this.color = color;
        }

        public void addTile(Tile tile)
        {
            // Add the tile to tilesControlled
            tilesControlled.Add(tile);
        }

        public Color getColor()
        {
            return color;
        }
    }
}
