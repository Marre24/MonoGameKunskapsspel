using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Content;

namespace MonoGameKunskapsspel
{
    public class FloorSegment : Component
    {
        private List<List<Tile>> tiles = new();
        public Rectangle hitBox;

        private readonly Point tileSize = new Point(96, 96);

        public FloorSegment(Point tileAmount, Point location, KunskapsSpel kunskapsSpel) : base(kunskapsSpel)
        {

            hitBox = new Rectangle(location, tileAmount * tileSize);

            for (int y = 0; y < tileAmount.Y; y++)
            {
                tiles.Add(new List<Tile>());
                for (int x = 0; x < tileAmount.X; x++)
                    tiles[y].Add(new Tile(tileSize, new(x,y), tileAmount, kunskapsSpel));
            }

        }

        public void ChangeTileWithIndexToMiddleTexture(int x, int y)
        {



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
