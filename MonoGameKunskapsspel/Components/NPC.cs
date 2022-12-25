using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace MonoGameKunskapsspel
{
    public class NPC : Component
    {
        private Rectangle hitBox;
        private readonly Dictionary<string, Animation> animations;
        private readonly AnimationManager animationManager;
        private readonly List<string> dialogue;

        public NPC(Rectangle hitBox, KunskapsSpel kunskapsSpel, List<string> dialog, Dictionary<string, Animation> animations) : base(kunskapsSpel)
        {
            this.hitBox = hitBox;
            this.animations = animations;
            animationManager = new AnimationManager(animations.First().Value);
            dialogue = dialog.ToList();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationManager.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            animationManager.Position = hitBox.Location.ToVector2();
            animationManager.Update(gameTime);
            animationManager.Play(animations["NpcIdle"]);

            if (!PlayerCanInteract(kunskapsSpel.player))
                return;

            if (!Keyboard.GetState().IsKeyDown(Keys.Space))
                return;

            _ = new DialogueWindow(kunskapsSpel, kunskapsSpel.player, kunskapsSpel.camera, dialogue);
        }

        public bool PlayerCanInteract(Player player)
        {
            return (IsBetweenX(player.hitBox.Right) || IsBetweenX(player.hitBox.Left)) && IsBetweenY(player.hitBox.Top);
        }

        private bool IsBetweenX(int xCord)
        {
            return hitBox.Left <= xCord && hitBox.Right >= xCord;
        }

        private bool IsBetweenY(int yCord)
        {
            return hitBox.Top <= yCord && hitBox.Bottom >= yCord;
        }
    }
}
