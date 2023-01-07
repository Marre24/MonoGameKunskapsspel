using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameKunskapsspel
{
    public class Table : Component
    {
        private const int scale = 4;

        private readonly Point verticalTableSize = new(scale * 28, scale * 41);
        private readonly Point horizontalTableSize = new(scale * 48, scale * 29);
        private readonly Point upAndSideChairSize = new(scale * 14, scale * 29);
        private readonly Point downChairSize = new(scale * 16, scale * 17);

        public readonly Rectangle tableBox;
        public readonly Rectangle leftChairBox;
        public readonly Rectangle rightChairBox;
        public readonly Rectangle upChairBox1;
        public readonly Rectangle upChairBox2;
        public readonly Rectangle downChairBox1;
        public readonly Rectangle downChairBox2;

        private readonly Texture2D tableTexture;
        private readonly Texture2D rightChairTexture;
        private readonly Texture2D leftChairTexture;
        private readonly Texture2D upChairTexture;
        private readonly Texture2D downChairTexture;
        public readonly string typeOfTable;

        public Table(Point location, string typeOfTable, KunskapsSpel kunskapsSpel) : base(kunskapsSpel)
        {
            this.typeOfTable = typeOfTable;
            haveColisison = true;

            rightChairTexture = kunskapsSpel.Content.Load<Texture2D>("Decoration/ChairFacingLeft");
            leftChairTexture = kunskapsSpel.Content.Load<Texture2D>("Decoration/ChairFacingRight");
            upChairTexture = kunskapsSpel.Content.Load<Texture2D>("Decoration/ChairFacingFront");
            downChairTexture = kunskapsSpel.Content.Load<Texture2D>("Decoration/ChairFacingBack");


            if (typeOfTable == "Horizontal")
            {
                tableTexture = kunskapsSpel.Content.Load<Texture2D>("Decoration/SideWaysTable");

                Rectangle bounds = new(location - new Point(0, 80 * scale), new(80 * scale,80 * scale));

                tableBox = new(bounds.Location + new Point(16 * scale, 30 * scale), horizontalTableSize);
                leftChairBox = new(new(bounds.X, bounds.Y + 26 * scale), upAndSideChairSize);
                rightChairBox = new(bounds.Location + new Point(66 * scale, 26 * scale), upAndSideChairSize);
                upChairBox1 = new(bounds.Location + new Point(23 * scale, 0), upAndSideChairSize);
                upChairBox2 = new(bounds.Location + new Point(45 * scale, 0), upAndSideChairSize);
                downChairBox1 = new(bounds.Location + new Point(21 * scale, 62 * scale), downChairSize);
                downChairBox2 = new(bounds.Location + new Point(43 * scale, 62 * scale), downChairSize);

                return;
            }

            tableTexture = kunskapsSpel.Content.Load<Texture2D>("Decoration/UpWardsTable");

            Rectangle bounds2 = new(location - new Point(0, 90 * scale), new(62 * scale, 90 * scale));

            tableBox = new(bounds2.Location + new Point(18 * scale, 30 * scale), verticalTableSize);
            leftChairBox = new(bounds2.Location + new Point(0, 32 * scale), upAndSideChairSize);
            rightChairBox = new(bounds2.Location + new Point(48 * scale, 32 * scale), upAndSideChairSize);
            upChairBox1 = new(bounds2.Location + new Point(25 * scale, 0), upAndSideChairSize);
            downChairBox1 = new(bounds2.Location + new Point(24 * scale, 73 * scale), downChairSize);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tableTexture, tableBox, Color.White);

            spriteBatch.Draw(rightChairTexture, rightChairBox, Color.White);
            spriteBatch.Draw(leftChairTexture, leftChairBox, Color.White);
            spriteBatch.Draw(upChairTexture, upChairBox1, Color.White);
            spriteBatch.Draw(downChairTexture, downChairBox1, Color.White);

            if (typeOfTable == "Vertical")
                return;

            spriteBatch.Draw(upChairTexture, upChairBox2, Color.White);
            spriteBatch.Draw(downChairTexture, downChairBox2, Color.White);

        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
