using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

namespace MonoGameKunskapsspel
{
    public class Enemy : Component
    {
        public CutScene cutScene;
        private readonly Point size = new(238, 254);
        private readonly AnimationManager animationManager;
        private readonly Room room;
        private readonly int id;
        public bool isDead = false;

        public Enemy(KunskapsSpel kunskapsSpel, Point position, Room room, int id) : base(kunskapsSpel)
        {
            if (room != null)
            {
                cutScene = new PanToTarget(kunskapsSpel.player, kunskapsSpel, this, room, new()
                {
                    "Huh? Vem är du och vad gör du här?",
                    "Hade du tänkt att stoppa oss?!? Hahahahaha det kommer aldrig hända",
                    "Ojoj jag skakar i mina skor för att är så rädd",
                    "Nä nu är det slutlekt, dags att dö",
                });
            }
            hitBox = new(position, size);
            animationManager = new AnimationManager(kunskapsSpel.animations[$"OrcIdle{id}"]);
            haveColisison = true;
            this.room = room;
            this.id = id;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationManager?.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if (isDead && animationManager._animation.CurrentFrame < animationManager._animation.FrameCount - 1)
            {
                animationManager.Update(gameTime);
                return;
            }

            if (isDead)
            {
                kunskapsSpel.player.activeState = State.Ended;
                kunskapsSpel.activeWindow = new EndScreen(kunskapsSpel, kunskapsSpel.camera, kunskapsSpel.player, State.InStartScreen);
                Thread.Sleep(1000);
                return;
            }

            if (animationManager != null)
            {
                animationManager.Position = hitBox.Location.ToVector2();
                animationManager.Update(gameTime);
            }
            if (cutScene != null)
                if (PlayerCanInteract(kunskapsSpel.player, room.floorSegments[0]) && !hasInteracted)
                    cutScene.StartScene();
        }

        public static bool PlayerCanInteract(Player player, FloorSegment floorSegment)
        {
            return (IsBetweenX(player.hitBox.Right, floorSegment.hitBox) || IsBetweenX(player.hitBox.Left, floorSegment.hitBox)) && IsBetweenY(player.hitBox.Bottom, floorSegment.hitBox);
        }

        private static bool IsBetweenX(int xCord, Rectangle hitBox)
        {
            return hitBox.Left <= xCord && hitBox.Right >= xCord;
        }

        private static bool IsBetweenY(int yCord, Rectangle hitBox)
        {
            return hitBox.Top <= yCord && hitBox.Bottom >= yCord;
        }

        public void Kill()
        {
            isDead = true;
            kunskapsSpel.musicManager.ChangeSlowlyToEnding();
            kunskapsSpel.animations[$"OrcDeath{id}"].FrameSpeed = 0.4f;
            animationManager.Play(kunskapsSpel.animations[$"OrcDeath{id}"]);
        }
    }
}
