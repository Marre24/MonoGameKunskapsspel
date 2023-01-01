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
    public abstract class Window
    {
        public readonly KunskapsSpel kunskapsSpel;
        public readonly Camera camera;
        public readonly Player player;
        private readonly State previousState;

        public Window(KunskapsSpel kunskapsSpel, Camera camera, Player player, State previousState)
        {
            this.kunskapsSpel = kunskapsSpel;
            this.camera = camera;
            this.player = player;
            this.previousState = previousState;
        }
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void Update(GameTime gameTime);
        public virtual void EndScene()
        {
            kunskapsSpel.activeWindow = null;
            player.activeState = previousState;
        }
    }
}
