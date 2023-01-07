using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MonoGameKunskapsspel
{
    public class Wall : Component
    {
        private readonly List<Rectangle> hitBoxes;
        private const int multiplyer = 4;
        private const int ySize = 37 * multiplyer;
        readonly Texture2D wallTexture;
        readonly Texture2D StartAndEndOfWallTexture;
        public Wall(int xSize, Point location, KunskapsSpel kunskapsSpel) : base(kunskapsSpel)              //pass bottom left corner of the wall as position
        {
            wallTexture = kunskapsSpel.Content.Load<Texture2D>("Dungeon/Wall");
            StartAndEndOfWallTexture = kunskapsSpel.Content.Load<Texture2D>("Dungeon/WallStartAndEnd");

            xSize *= 96;

            location -= new Point(0, ySize);

            hitBoxes = new List<Rectangle>()
            {
                new Rectangle(location, new(multiplyer, ySize)),
                new Rectangle(location + new Point(multiplyer, 0), new(xSize - (2 * multiplyer), ySize)),
                new Rectangle(location + new Point(xSize - multiplyer, 0), new(multiplyer, ySize)),
            };

            hitBox = new(location, new(xSize, ySize));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Rectangle source = hitBoxes[1];
            source.Location = Point.Zero;

            spriteBatch.Draw(StartAndEndOfWallTexture, hitBoxes[0], Color.White);
            spriteBatch.Draw(wallTexture, hitBoxes[1], source, Color.White);
            spriteBatch.Draw(StartAndEndOfWallTexture, hitBoxes[2], Color.White);
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
