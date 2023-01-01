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
    public class Enemy : Component
    {
        public CutScene cutScene;
        private readonly Point size = new(238, 254);
        private AnimationManager animationManager;
        private readonly Room room;
        public bool isDead = false;

        public Enemy(KunskapsSpel kunskapsSpel, Point position, Room room) : base(kunskapsSpel)
        {
            if (room != null)
            {
                cutScene = new PanToTarget(kunskapsSpel.player, kunskapsSpel, this, room, new()
                {
                    "Muhahahaha",
                });
            }
            hitBox = new(position, size);
            animationManager = new AnimationManager(kunskapsSpel.animations["OrcIdle"]);
            haveColisison = true;
            this.room = room;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationManager?.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if (animationManager != null)
            {
                animationManager.Position = hitBox.Location.ToVector2();
                animationManager.Update(gameTime);
            }
            if (cutScene != null)
                if (PlayerCanInteract(kunskapsSpel.player, room.floorSegments[0]) && !hasInteracted && !isDead)
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
            //animationManager.Play(kunskapsSpel.animations["OrcDeath"]);
            //animationManager = null;
        }
    }
}
