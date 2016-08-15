using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grow
{
    class GameFactory
    {
        private TileFactory map;
        private PlayerFactory players;

        public GameFactory(Tuple<int, int> size, int numOfTiles, float connectedness, int numOfPlayers)
        {
            // Initialize the map
            map = new TileFactory(size, numOfTiles, connectedness);
            // Create the players
            players = new PlayerFactory(numOfPlayers + 1);

            // Intially, set the controller of all tiles in the map to player 0
            foreach (Tile tile in map.getTiles())
            {
                tile.setController(players.getPlayers()[0]);
            }

            // Spawn the players on the map
            players.spawnPlayers(map.getTiles());
        }

        public void Draw(SpriteBatch spriteBatch, int scale)
        {
            // Draw the map
            map.Draw(spriteBatch, scale);
        }

        public static void LoadContent(ContentManager Content)
        {
            // Load the content in TileFactory
            TileFactory.LoadContent(Content);
        }
    }
}