using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonoGameKunskapsspel
{
    public class SideWall : Component
    {
        private List<Rectangle> hitBox;
        private const int multiplyer = 4;
        private const int xSize = 14 * multiplyer;
        Texture2D edgeWallContinuation;
        Texture2D edgeWallStart;
        Texture2D edgeWallEnd;
        public SideWall(int ySize, Point location, KunskapsSpel kunskapsSpel, string key) : base(kunskapsSpel)
        {
            edgeWallContinuation = kunskapsSpel.Content.Load<Texture2D>("Dungeon/EdgeWallContinuation");
            edgeWallStart = kunskapsSpel.Content.Load<Texture2D>("Dungeon/EdgeWallStart");
            edgeWallEnd = kunskapsSpel.Content.Load<Texture2D>("Dungeon/EdgeWallEnd");

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

            hitBox = new List<Rectangle>()
            {
                new Rectangle(location + new Point(0, ySize), new(xSize, 148)),
                new Rectangle(location + new Point(0, 8), new(xSize, ySize - 8)),
                new Rectangle(location, new(xSize, 8)),
            };
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

            hitBox = new List<Rectangle>()
            {
                new Rectangle(location + new Point(0, ySize - 148), new(xSize, 148)),
                new Rectangle(location + new Point(0, 8), new(xSize, ySize - 156)),
                new Rectangle(location, new(xSize, 8)),
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Rectangle source = hitBox[1];
            source.Location = Point.Zero;

            spriteBatch.Draw(edgeWallStart, hitBox[0], Color.White);
            spriteBatch.Draw(edgeWallContinuation, hitBox[1], source, Color.White);
            spriteBatch.Draw(edgeWallEnd, hitBox[2], Color.White);
        }

        public override void Update(GameTime gameTime)
        {


        }
    }
}
