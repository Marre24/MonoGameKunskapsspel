using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace MonoGameKunskapsspel
{
    public class NPC : Component
    {
        private readonly Dictionary<string, Animation> animations;
        private readonly AnimationManager animationManager;
        public List<string> dialogue;
        private readonly Point size = new(50, 64);
        public int interactionPhase = 0;
        public NPC(Point location, KunskapsSpel kunskapsSpel, List<string> dialog, Dictionary<string, Animation> animations) : base(kunskapsSpel)
        {
            this.animations = animations;
            animationManager = new AnimationManager(animations.First().Value);
            dialogue = dialog.ToList();
            haveColisison = true;
            hitBox = new(location, size);
            interactHitBox = new Rectangle(location - new Point(20, 20), size + new Point(40, 40));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationManager.Draw(spriteBatch);
        }
        private bool spaceWasUp = false;
        private bool first = true;
        private double gameTimeElapsed = 0;
        private const double interval = 2;

        public override void Update(GameTime gameTime)
        {
            if (first && kunskapsSpel.roomManager.GetActiveRoom().RoomID == 1)
            {
                gameTimeElapsed = gameTime.TotalGameTime.TotalSeconds;
                first = false;
            }
            if (!hasInteracted && kunskapsSpel.roomManager.GetActiveRoom().RoomID == 1 && interval + gameTimeElapsed < gameTime.TotalGameTime.TotalSeconds)
                StartCutScene(gameTime);

            animationManager.Position = hitBox.Location.ToVector2();
            animationManager.Update(gameTime);
            animationManager.Play(animations["NpcIdle"]);


            if (Keyboard.GetState().IsKeyDown(Keys.Space) && PlayerCanInteract(kunskapsSpel.player) && spaceWasUp && kunskapsSpel.player.activeState == State.Walking)
            {
                spaceWasUp = false;
                kunskapsSpel.player.activeState = State.ReadingText;
                kunskapsSpel.player.velocity = new(0, 0);
                if (dialogue != tempDialogue)
                {
                    interactionPhase++;
                    tempDialogue = dialogue;
                }
                _ = new DialogueWindow(kunskapsSpel, kunskapsSpel.player, kunskapsSpel.camera, dialogue, State.Walking);
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Space) && kunskapsSpel.player.activeState == State.Walking)
                spaceWasUp = true;
        }
        List<string> tempDialogue;
        private void StartCutScene(GameTime gameTime)
        {
            kunskapsSpel.player.Update(gameTime);
            kunskapsSpel.player.activeState = State.WatchingCutScene;
            _ = new PanToTarget(kunskapsSpel.player, kunskapsSpel, this, kunskapsSpel.roomManager.GetActiveRoom(), new()
            {
                "Välkommen nykommling, mitt namn är Edazor. När du har läst klart så kan du klicka på SPACE för att se vad mer jag har att säga",
                "Du går med W, A, S och D eller Pilarna. Om du vill interagera med något objekt eller prata med mig så klickar du SPACE",
                "Testa och gå fram till mig och prata",
            }, true);
            hasInteracted = true;
        }

        public bool PlayerCanInteract(Player player)
        {
            return (IsBetweenX(player.hitBox.Right) || IsBetweenX(player.hitBox.Left)) && (IsBetweenY(player.hitBox.Top) || IsBetweenY(player.hitBox.Bottom));
        }

        private bool IsBetweenX(int xCord)
        {
            return interactHitBox.Left <= xCord && interactHitBox.Right >= xCord;
        }

        private bool IsBetweenY(int yCord)
        {
            return interactHitBox.Top <= yCord && interactHitBox.Bottom >= yCord;
        }
    }
}
