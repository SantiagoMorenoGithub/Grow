using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Grow
{
    class Tile
    {
        // The player who currently controls this tile
        private Player controller;
        // List of all tiles attachted to this tile
        private List<Tile> attachments = new List<Tile>();
        // Position of this tile and it's strength
        public Vector2 pos;
        private int strength;
        // Current texture of the tile
        private static Texture2D tileTex;

        public Tile(Vector2 pos)
        {
            this.pos = pos;
            strength = 0;
        }

        public void setController(Player player)
        {
            controller = player;
        }

        public Vector2 getPos()
        {
            return pos;
        }

        public void addAttachment(Tile tile)
        {
            attachments.Add(tile);
        }

        public List<Tile> getAttachments()
        {
            return attachments;
        }

        public Player getController()
        {
            return controller;
        }

        public void Draw(SpriteBatch spriteBatch, int scale)
        {
            // Draw the tile with the position multiplied by the scale and the color as the controller's color
            spriteBatch.Draw(tileTex, pos * scale, null, controller.getColor(), 0, new Vector2(tileTex.Width / 2, tileTex.Height / 2), 1, SpriteEffects.None, 0);
        }

        public static void LoadContent(ContentManager Content)
        {
            // Load all tile sprites
            tileTex = Content.Load<Texture2D>("MapContent/tile0");
        }
    }
}
