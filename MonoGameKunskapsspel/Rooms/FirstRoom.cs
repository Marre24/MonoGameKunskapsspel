using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameKunskapsspel
{
    public class FirstRoom : Room
    {
        Vector2 position = new Vector2(0f,0f);
        public FirstRoom(int ID) : base(ID)
        {
        }

        public override void Draw(KunskapsSpel kunskapsSpel)
        {
            kunskapsSpel.spriteBatch.Draw(kunskapsSpel.Content.Load<Texture2D>("StartScreen_WithoutText"), position, Color.White);
        }

        public override void Initialize(KunskapsSpel kunskapsSpel)
        {
        }

        public override void Update(GameTime gameTime, KunskapsSpel kunskapsSpel)
        {
        }
    }
}
