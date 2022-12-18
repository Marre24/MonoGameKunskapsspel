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
        private KunskapsSpel kunskapsSpel;
        private List<string> dialogue = new()
        {
            "Halloj Maxi trevligt att se dig här",
            "Mitt namn är General Goofy jag vet inte om jag kommer att byta namn :)"
        };

        public NPC(Rectangle hitBox, KunskapsSpel kunskapsSpel)
        {
            this.hitBox = hitBox;
            this.kunskapsSpel = kunskapsSpel;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(kunskapsSpel.Content.Load<Texture2D>("GeneralGoofy"), hitBox, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            if (!PlayerCanInteract(kunskapsSpel.player))
                return;

            if (!Keyboard.GetState().IsKeyDown(Keys.Space))
                return;

            DialogueWindow dialogueWindow = new DialogueWindow(kunskapsSpel);
            dialogueWindow.Init(dialogue);
        }

        public bool PlayerCanInteract(Player player)
        {
            return IsTouchingDoor(player);
        }

        private bool IsTouchingDoor(Player player)
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
