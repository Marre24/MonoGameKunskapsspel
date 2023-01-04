using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MonoGameKunskapsspel
{
    public class FloorSegment : Component
    {
        public List<List<Tile>> tiles = new();

        private readonly Point tileSize = new Point(96, 96);

        public FloorSegment(Point tileAmount, Point location, KunskapsSpel kunskapsSpel, string key) : base(kunskapsSpel)
        {
            List<Texture2D> tileTextures = new();
            if (key == "Grass")
            {
                tileTextures.Add(kunskapsSpel.Content.Load<Texture2D>("Enviroment/GrassTileBottom"));          //0
                tileTextures.Add(kunskapsSpel.Content.Load<Texture2D>("Enviroment/GrassTileBottomLeft"));
                tileTextures.Add(kunskapsSpel.Content.Load<Texture2D>("Enviroment/GrassTileBottomRight"));

                tileTextures.Add(kunskapsSpel.Content.Load<Texture2D>("Enviroment/GrassTileLeft"));            //3
                tileTextures.Add(kunskapsSpel.Content.Load<Texture2D>("Enviroment/GrassTileMiddle"));
                tileTextures.Add(kunskapsSpel.Content.Load<Texture2D>("Enviroment/GrassTileRight"));

                tileTextures.Add(kunskapsSpel.Content.Load<Texture2D>("Enviroment/GrassTileTop"));             //6
                tileTextures.Add(kunskapsSpel.Content.Load<Texture2D>("Enviroment/GrassTileTopLeft"));
                tileTextures.Add(kunskapsSpel.Content.Load<Texture2D>("Enviroment/GrassTileTopRight"));
            }

            if (key == "Dungeon")
            {
                tileTextures.Add(kunskapsSpel.Content.Load<Texture2D>("Dungeon/BotMid"));          //0
                tileTextures.Add(kunskapsSpel.Content.Load<Texture2D>("Dungeon/BotLeft"));
                tileTextures.Add(kunskapsSpel.Content.Load<Texture2D>("Dungeon/BotRight"));

                tileTextures.Add(kunskapsSpel.Content.Load<Texture2D>("Dungeon/MidLeft"));            //3
                tileTextures.Add(kunskapsSpel.Content.Load<Texture2D>("Dungeon/Mid"));
                tileTextures.Add(kunskapsSpel.Content.Load<Texture2D>("Dungeon/MidRight"));

                tileTextures.Add(kunskapsSpel.Content.Load<Texture2D>("Dungeon/TopMid"));             //6
                tileTextures.Add(kunskapsSpel.Content.Load<Texture2D>("Dungeon/TopLeft"));
                tileTextures.Add(kunskapsSpel.Content.Load<Texture2D>("Dungeon/TopRight"));
            }

            hitBox = new Rectangle(location * tileSize, tileAmount * tileSize);

            for (int y = 0; y < tileAmount.Y; y++)
            {
                tiles.Add(new List<Tile>());
                for (int x = 0; x < tileAmount.X; x++)
                    tiles[y].Add(new Tile(new Point(x,y), tileAmount, new Point(x, y) * tileSize + location * tileSize, kunskapsSpel, tileTextures));
            }

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (List<Tile> tiles in tiles)
                foreach (Tile tile in tiles)
                    tile.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
