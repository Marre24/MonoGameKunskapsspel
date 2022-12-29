using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameKunskapsspel
{
    public class ChangeRoomAnimation : CutScene
    {
        private Rectangle window;
        private readonly Camera camera;
        private readonly Door door;
        private readonly Texture2D texture;
        private float fadeOpacity = 0.1f;
        private bool fadeIn = false;
        private const float opacityPerSecond = 0.01f;
        

        public ChangeRoomAnimation(Player player, Camera camera, KunskapsSpel kunskapsSpel, Door door) : base(player, kunskapsSpel)
        {
            this.camera = camera;
            this.door = door;
            texture = kunskapsSpel.Content.Load<Texture2D>("Msc/LargeDialogbox");
        }

        public override void Update(GameTime gameTime)
        {
            window = camera.window;
            door.Update(gameTime);
            player.Update(gameTime);
            camera.Follow(player.hitBox);

            if (fadeIn)
                fadeOpacity -= opacityPerSecond;
            else
                fadeOpacity += opacityPerSecond;

            if (fadeOpacity * 100 >= 100)
            {
                fadeIn = true;
                changeRoom = true;
            }
            
            if (fadeOpacity <= 0)
                EndScene();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, window, Color.Black * fadeOpacity);
        }

        public override void StartScene()
        {
        }

        public override void EndScene()
        {
            kunskapsSpel.activeCutscene = null;
            kunskapsSpel.player.activeState = State.Walking;
            door.first = true;
        }
    }
}
