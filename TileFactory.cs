using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Grow
{
    class TileFactory
    {
        // List of tiles held by the TileFactory
        private List<Tile> tiles = new List<Tile>();
        // Texture used for the line
        private static Texture2D lineTex;

        public TileFactory(Tuple<int, int> size, int amount, float connectedness)
        {
	        // Place and attach tiles
            placeTiles(size, amount);
            attachTiles(connectedness);
        }

        private void placeTiles(Tuple<int, int> size, int amount)
        {
            Random rand = new Random();
            // Repeats the tile generation process amount times
            for (int i = 0; i < amount; i++)
            {
                // Loop while a new tile has not been generated
                bool tileGenerated = false;
                while (!tileGenerated)
                {
                    // Generate a random position for the tile using size for the max
                    Vector2 newPos = new Vector2(rand.Next(size.Item1), rand.Next(size.Item2));
                    // Shifts every even row horizontally by half a unit 
                    if (newPos.Y % 2 == 0)
                        newPos.X += 0.5f;
                    // Loop though every tile already generated and verify that they do not occupy the same position
                    bool noTileAtNewPos = true;
                    foreach (Tile tile in tiles)
                    {
                        Vector2 tilePos = tile.getPos();
                        if (tilePos.X == newPos.X && tilePos.Y == newPos.Y)
                            noTileAtNewPos = false;
                    }
                    // If the tile's position does not conflict with any other tile, add it to tiles and set tileGenerated to true
                    if (noTileAtNewPos)
                    {
                        tiles.Add(new Tile(newPos));
                        tileGenerated = true;
                    }
                }
            }
        }

        private void attachTiles(float connectedness)
        {
            Random rand = new Random();

            // Loop through all tiles
            foreach (Tile tile1 in tiles)
            {
                // Start with the chance to attach to a nearby tile at 100%
                float connectChance = 1;
                // Loop again through all tiles
                foreach (Tile tile2 in tiles)
                {
                    // Verify that the two tiles are not the same and that they are not already attached
                    if (tile1 != tile2 && !tile1.getAttachments().Contains(tile2))
                    {
                        // Generate an edge vector as the vector from tile1 to tile2
                        Vector2 edge = tile2.getPos() - tile1.getPos();
                        // Check if the edge is short enough to be an adjacent tile
                        if (edge.Length() <= 1.75)
                        {
                            // Compare connectChance to a random double to randomdly decide whether or not to connect the tiles
                            if (connectChance >= rand.NextDouble())
                            {
                                // Add both tiles to each other's attachments
                                tile1.addAttachment(tile2);
                                tile2.addAttachment(tile1);
                                // Multiply connectChance by connectedness to reduce the chance of connecting again
                                connectChance *= connectedness;
                            }
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, int scale)
        {
            // Draws all attachments by looping through each tile
            foreach (Tile tile1 in tiles)
            {
                // Loops through all attachments of that tile
                foreach (Tile tile2 in tile1.getAttachments())
                {
                    // Calculates the vector from the two tiles and the angle
                    Vector2 edge = tile2.getPos() - tile1.getPos();
                    float angle = (float)Math.Atan2(edge.Y, edge.X);
                    // Creates and draws a rotated rectangle between the two tiles
                    Rectangle rect = new Rectangle((int)(tile1.getPos().X * scale), (int)(tile1.getPos().Y * scale), (int)edge.Length() * scale, 2);
                    spriteBatch.Draw(lineTex, null, rect, null, new Vector2(0, 0), angle, null, tile1.getController().getColor(), SpriteEffects.None, 0);
                }
            }
            // Loops through all tiles and draws them
            foreach (Tile tile in tiles)
            {
                tile.Draw(spriteBatch, scale);
            }
        }

        public static void LoadContent(ContentManager Content)
        {
            // Load the Tile content and the line texture
            Tile.LoadContent(Content);
            lineTex = Content.Load<Texture2D>("MapContent/tile0.png");
        }

        public List<Tile> getTiles()
        {
            return tiles;
        }
    }
}
