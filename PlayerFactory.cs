using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grow
{
    class PlayerFactory
    {
        // List of players held by the PlayerFactory
        private List<Player> players = new List<Player>();

        public PlayerFactory(int numOfPlayers)
        {
            // List of all colors to set for each new player
            List<Color> colors = new List<Color> { Color.Gray, Color.Red, Color.LightBlue, Color.Lime, Color.Yellow, Color.Cyan, Color.White, Color.ForestGreen, Color.Gold };
            // Loop up to numOfPlayers (maximum of 9) and generate a new player with its corresponding color
            for (int i = 0; i < numOfPlayers && i < 9; i++)
            {
                players.Add(new Player(colors[i]));
            }
        }

        public void spawnPlayers(List<Tile> tiles)
        {
            Random rand = new Random();
            // Loop through each player excluding the first neutral player
            for (int i = 1; i < players.Count; i++)
            {
                // Loop as long as a spawn tile has not been found for the player
                bool foundSpawn = false;
                while (!foundSpawn)
                {
                    // Get a random tile and verify that it and all it's attachments are not controlled by any players
                    Tile spawnTile = tiles[rand.Next(tiles.Count)];
                    if (spawnTile.getController() == players[0] && spawnTile.getAttachments().Where(t => t.getController() != players[0]).Count() == 0)
                    {
                        // Set the tile's controller to the current player, add the tile to the player, and set foundSpawn to true
                        spawnTile.setController(players[i]);
                        players[i].addTile(spawnTile);
                        foundSpawn = true;
                    }
                }
            }
        }

        public List<Player> getPlayers()
        {
            return players;
        }
    }
}