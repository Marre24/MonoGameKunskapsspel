using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameKunskapsspel
{
    public abstract class Component
    {
        public readonly KunskapsSpel kunskapsSpel;
        public bool haveColisison = false;
        public Rectangle hitBox;
        public Rectangle interactHitBox;
        public bool hasInteracted = false;

        protected Component(KunskapsSpel kunskapsSpel)
        {
            this.kunskapsSpel = kunskapsSpel;
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);


    }
}
