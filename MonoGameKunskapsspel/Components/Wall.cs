using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameKunskapsspel
{
    public class Wall : Component
    {
        private List<Rectangle> hitBox;
        private const int multiplyer = 4;
        private const int ySize = 37 * multiplyer;
        Texture2D wallTexture;
        Texture2D StartAndEndOfWallTexture;
        public Wall(int xSize, Point location, KunskapsSpel kunskapsSpel) : base(kunskapsSpel)
        {
            wallTexture = kunskapsSpel.Content.Load<Texture2D>("Dungeon/Wall");
            StartAndEndOfWallTexture = kunskapsSpel.Content.Load<Texture2D>("Dungeon/WallStartAndEnd");

            xSize *= 96;

            location -= new Point(0, ySize);

            hitBox = new List<Rectangle>()
            {
                new Rectangle(location, new(multiplyer, ySize)),
                new Rectangle(location + new Point(multiplyer, 0), new(xSize - (2 * multiplyer), ySize)),
                new Rectangle(location + new Point(xSize - multiplyer, 0), new(multiplyer, ySize)),
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Rectangle source = hitBox[1];
            source.Location = Point.Zero;

            spriteBatch.Draw(StartAndEndOfWallTexture, hitBox[0], Color.White);
            spriteBatch.Draw(wallTexture, hitBox[1], source, Color.White);
            spriteBatch.Draw(StartAndEndOfWallTexture, hitBox[2], Color.White);
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
