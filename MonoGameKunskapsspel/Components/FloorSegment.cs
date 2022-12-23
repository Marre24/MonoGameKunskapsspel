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

        public FloorSegment(Rectangle hitBox, KunskapsSpel kunskapsSpel) : base(kunskapsSpel)
        {
            this.hitBox = hitBox;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            kunskapsSpel.spriteBatch.Draw(kunskapsSpel.Content.Load<Texture2D>("FloorTile"), hitBox, hitBox, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
