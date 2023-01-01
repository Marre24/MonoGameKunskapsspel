using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace MonoGameKunskapsspel
{
    public class NPC : Component
    {
        private readonly Dictionary<string, Animation> animations;
        private readonly AnimationManager animationManager;
        public List<string> dialogue;
        private readonly Point size = new(50, 64);
        public NPC(Point location, KunskapsSpel kunskapsSpel, List<string> dialog, Dictionary<string, Animation> animations) : base(kunskapsSpel)
        {
            this.animations = animations;
            animationManager = new AnimationManager(animations.First().Value);
            dialogue = dialog.ToList();
            haveColisison = true;
            hitBox = new(location, size);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationManager.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if (!hasInteracted && kunskapsSpel.roomManager.GetActiveRoom().RoomID == 1 && (kunskapsSpel.player.velocity.X != 0 || kunskapsSpel.player.velocity.Y != 0))
            {
                kunskapsSpel.player.activeState = State.WatchingCutScene;
                _ = new PanToTarget(kunskapsSpel.player, kunskapsSpel, this, kunskapsSpel.roomManager.GetActiveRoom(), new()
                {
                    "Välkommen nykommling, mitt namn är _____ . När du har läst klart så kan du klicka på högerpilen för att se vad mer jag har att säga",
                    "Du går med W, A, S och D. Om du vill interagera med något objekt eller prata med mig så klickar du SPACE",
                    "Testa och gå fram till mig och prata",
                }, true);
                hasInteracted = true;
            }
            animationManager.Position = hitBox.Location.ToVector2();
            animationManager.Update(gameTime);
            animationManager.Play(animations["NpcIdle"]);

            if (!PlayerCanInteract(kunskapsSpel.player))
                return;

            if (!Keyboard.GetState().IsKeyDown(Keys.Space))
                return;

            kunskapsSpel.player.activeState = State.ReadingText;
            _ = new DialogueWindow(kunskapsSpel, kunskapsSpel.player, kunskapsSpel.camera, dialogue, kunskapsSpel.player.activeState);
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
