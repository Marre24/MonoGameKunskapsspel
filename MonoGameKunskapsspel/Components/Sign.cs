using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace MonoGameKunskapsspel
{
    public class Sign : Component
    {
        List<string> text;
        private Rectangle hitBox;
        private KunskapsSpel kunskapsSpel;

        public Sign(Rectangle hitBox, KunskapsSpel kunskapsSpel, List<string> text)
        {
            this.hitBox = hitBox;
            this.kunskapsSpel = kunskapsSpel;
            this.text = text;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(kunskapsSpel.Content.Load<Texture2D>("PurpleSignFacingLeft"), hitBox, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            if (!PlayerCanInteract(kunskapsSpel.player))
                return;

            if (!Keyboard.GetState().IsKeyDown(Keys.Space))
                return;

            DialogueWindow dialogueWindow = new DialogueWindow(kunskapsSpel, kunskapsSpel.player, kunskapsSpel.camera, text);
        }

        public bool PlayerCanInteract(Player player)
        {
            return IsTouching(player);
        }

        private bool IsTouching(Player player)
        {
            return ((IsBetweenX(player.hitBox.Right) || IsBetweenX(player.hitBox.Left)) && IsBetweenY(player.hitBox.Top));
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
