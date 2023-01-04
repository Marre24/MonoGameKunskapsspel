using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MonoGameKunskapsspel
{
    public class SideWall : Component
    {
        private readonly List<Rectangle> hitBoxes;
        private const int multiplyer = 4;
        private const int xSize = 14 * multiplyer;
        readonly Texture2D edgeWallContinuation;
        readonly Texture2D edgeWallStart;
        readonly Texture2D edgeWallEnd;
        public SideWall(int ySize, Point location, KunskapsSpel kunskapsSpel, string key) : base(kunskapsSpel)
        {
            edgeWallContinuation = kunskapsSpel.Content.Load<Texture2D>("Dungeon/EdgeWallContinuation");
            edgeWallStart = kunskapsSpel.Content.Load<Texture2D>("Dungeon/EdgeWallStart");
            edgeWallEnd = kunskapsSpel.Content.Load<Texture2D>("Dungeon/EdgeWallEnd");
            haveColisison = true;

            ySize *= 96;

            if (key == "LeftWall")
            {
                location = new Point(location.X * 96, location.Y * 96);
                location -= new Point(xSize, 148);
            }
            if (key == "RightWall")
            {
                location = new Point(location.X * 96, location.Y * 96);
                location -= new Point(0, 148);
            }

            hitBoxes = new List<Rectangle>()
            {
                new Rectangle(location + new Point(0, ySize), new(xSize, 148)),
                new Rectangle(location + new Point(0, 8), new(xSize, ySize - 8)),
                new Rectangle(location, new(xSize, 8)),
            };
            hitBox = new(location, new(xSize, ySize));
        }

        public SideWall(int ySize, Point location, KunskapsSpel kunskapsSpel, string key, bool nearWall) : base(kunskapsSpel)
        {
            edgeWallContinuation = kunskapsSpel.Content.Load<Texture2D>("Dungeon/EdgeWallContinuation");
            edgeWallStart = kunskapsSpel.Content.Load<Texture2D>("Dungeon/EdgeWallStart");
            edgeWallEnd = kunskapsSpel.Content.Load<Texture2D>("Dungeon/EdgeWallEnd");

            ySize *= 96;

            if (key == "LeftWall")
            {
                location = new Point(location.X * 96, location.Y * 96);
                location -= new Point(xSize, 0);
            }
            if (key == "RightWall")
            {
                location = new Point(location.X * 96, location.Y * 96);
                location -= new Point(0, 0);
            }

            hitBoxes = new List<Rectangle>()
            {
                new Rectangle(location + new Point(0, ySize - 148), new(xSize, 148)),
                new Rectangle(location + new Point(0, 8), new(xSize, ySize - 156)),
                new Rectangle(location, new(xSize, 8)),
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Rectangle source = hitBoxes[1];
            source.Location = Point.Zero;

            spriteBatch.Draw(edgeWallStart, hitBoxes[0], Color.White);
            spriteBatch.Draw(edgeWallContinuation, hitBoxes[1], source, Color.White);
            spriteBatch.Draw(edgeWallEnd, hitBoxes[2], Color.White);
        }

        public override void Update(GameTime gameTime)
        {


        }
    }
}
