using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonoGameKunskapsspel
{
    public class Mathias : Component
    {
        public Rectangle hitBox;
        public readonly CutScene cutScene;
        public bool hasInteracted = false;
        private readonly Point size = new(238, 254);

        public Mathias(KunskapsSpel kunskapsSpel, Point position, CutScene cutScene) : base(kunskapsSpel)
        {
            this.cutScene = cutScene;

            hitBox = new(position, size);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!hasInteracted)
                spriteBatch.Draw(kunskapsSpel.Content.Load<Texture2D>("Devil"), hitBox, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            kunskapsSpel.camera.Follow(cutScene.hiddenFollowPoint);
            if (kunskapsSpel.camera.window.Contains(hitBox) && !hasInteracted)
                cutScene.StartScene();
        }
    }
}
