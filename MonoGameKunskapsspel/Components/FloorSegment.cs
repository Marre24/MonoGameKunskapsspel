using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameKunskapsspel
{
    public class FloorSegment : Component
    {
        public Rectangle hitBox;
        private readonly KunskapsSpel kunskapsSpel;

        public FloorSegment(Rectangle hitBox, KunskapsSpel kunskapsSpel)
        {
            this.hitBox = hitBox;
            this.kunskapsSpel = kunskapsSpel;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            kunskapsSpel.spriteBatch.Draw(kunskapsSpel.Content.Load<Texture2D>("StartScreen_WithoutText"), hitBox, Color.White);
        }

        public override void Update(GameTime gameTime)
        {


        }
    }
}
