using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonoGameKunskapsspel
{
    public class Door : Component
    {
        private readonly Room doorLeedsTo;
        public readonly bool open;
        public Rectangle hitBox;
        private KunskapsSpel kunskapsSpel;
        private readonly bool showDoor;

        public Door(Rectangle hitBox, bool open, Room doorLeedsTo, KunskapsSpel kunskapsSpel)
        {
            this.open = open;
            this.doorLeedsTo = doorLeedsTo;
            this.hitBox = hitBox;
            this.kunskapsSpel = kunskapsSpel;
            showDoor = true;
        }
        public Door(Rectangle hitBox, Room doorLeedsTo, KunskapsSpel kunskapsSpel, bool showDoor)
        {
            open = true;
            this.doorLeedsTo = doorLeedsTo;
            this.hitBox = hitBox;
            this.kunskapsSpel = kunskapsSpel;
            this.showDoor = showDoor;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (open && showDoor)
                spriteBatch.Draw(kunskapsSpel.Content.Load<Texture2D>("OpenDoorHotPink"), hitBox, Color.White);
            else if (showDoor)
                spriteBatch.Draw(kunskapsSpel.Content.Load<Texture2D>("ShutDoorHotPink"), hitBox, Color.White);
        }

        public override void Update(GameTime gameTime)
        {

        }

        public void GoThroughDoor(RoomManager roomManager)
        {
            roomManager.SetActiveRoom(doorLeedsTo);
            kunskapsSpel.player.hitBox.Location = doorLeedsTo.frontDoorLocation;
        }

        public bool PlayerCanInteract(Player player)
        {
            return IsTouchingDoor(player);
        }

        public bool IsTouchingDoor(Player player)
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
        public void TryToOpen()
        {
            new UnlockDoorWindow(kunskapsSpel);
        }
    }
}
