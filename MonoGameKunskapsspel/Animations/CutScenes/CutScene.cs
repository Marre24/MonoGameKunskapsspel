using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameKunskapsspel
{
    public abstract class CutScene
    {
        public Vector2 hiddenFollowPoint;
        public readonly Player player;
        public readonly KunskapsSpel kunskapsSpel;
        public int phaseCounter = 1;
        public bool changeRoom = false;
        public CutScene(Player player, KunskapsSpel kunskapsSpel)
        {
            this.player = player;
            this.kunskapsSpel = kunskapsSpel;
        }

        public abstract void Update(GameTime gameTime);
        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
        public abstract void StartScene();
        public abstract void EndScene();

    }
}
