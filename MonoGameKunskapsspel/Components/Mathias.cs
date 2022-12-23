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
        private KunskapsSpel kunskapsSpel;
        public bool hasInteracted = false;
        public Rectangle hitBox;
        private Point position;
        public readonly CutScene cutScene;
        private readonly Point size = new Point(238, 254);

        public Mathias(KunskapsSpel kunskapsSpel, Point position, CutScene cutScene)
        {
            this.position = position;
            this.cutScene = cutScene;
            this.kunskapsSpel = kunskapsSpel;

            hitBox = new Rectangle(position, size);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!hasInteracted)
                spriteBatch.Draw(kunskapsSpel.Content.Load<Texture2D>("Devil"), hitBox, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            if (kunskapsSpel.camera.window.Contains(hitBox) && !hasInteracted)
                cutScene.StartScene();

        }

    }
}
