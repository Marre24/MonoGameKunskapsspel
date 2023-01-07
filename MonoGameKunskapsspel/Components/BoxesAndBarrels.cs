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
    public class BoxesAndBarrels : Component
    {
        private const int scale = 4;
        private readonly Point boxSize = new(16 * scale, 23 * scale);
        private readonly Point barrelSize = new(16 * scale, 22 * scale);

        private readonly Texture2D boxTexture;
        private readonly Texture2D barrelTexture;
        private readonly bool isBarrel;

        public BoxesAndBarrels(Point location, bool isBarrel, KunskapsSpel kunskapsSpel) : base(kunskapsSpel)
        {
            boxTexture = kunskapsSpel.Content.Load<Texture2D>("Decoration/WoodenBox");
            barrelTexture = kunskapsSpel.Content.Load<Texture2D>("Decoration/WoodenBarrel");
            this.isBarrel = isBarrel;

            if (isBarrel)
            {
                hitBox = new(location, barrelSize);
                return;
            }
            hitBox = new(location, boxSize);

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (isBarrel)
                spriteBatch.Draw(barrelTexture, hitBox, Color.White);
            else
                spriteBatch.Draw(boxTexture, hitBox, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
