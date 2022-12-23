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
    public class Door : Component
    {
        private readonly Room doorLeedsTo;
        public readonly bool open;
        public Rectangle hitBox;
        private readonly bool showDoor;

        public Door(Rectangle hitBox, bool open, Room doorLeedsTo, KunskapsSpel kunskapsSpel) : base(kunskapsSpel)              //Visibel door
        {
            this.open = open;
            this.doorLeedsTo = doorLeedsTo;
            this.hitBox = hitBox;
            showDoor = true;
        }

        public Door(Rectangle hitBox, Room doorLeedsTo, KunskapsSpel kunskapsSpel) : base(kunskapsSpel)         //Hidden Door
        {
            open = true;
            showDoor = false;
            this.doorLeedsTo = doorLeedsTo;
            this.hitBox = hitBox;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!showDoor)
                return;

            if (open)
            {
                spriteBatch.Draw(kunskapsSpel.Content.Load<Texture2D>("OpenDoorHotPink"), hitBox, Color.White);
                return;
            }

            spriteBatch.Draw(kunskapsSpel.Content.Load<Texture2D>("ShutDoorHotPink"), hitBox, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            if (!PlayerCanInteract(kunskapsSpel.player))
                return;

            if (open)
                GoThroughDoor(kunskapsSpel.roomManager);

            if (!Keyboard.GetState().IsKeyDown(Keys.Space))
                return;

            if (!open)
                TryToOpen();
        }

        public void GoThroughDoor(RoomManager roomManager)
        {
            roomManager.SetActiveRoom(doorLeedsTo);
            kunskapsSpel.player.hitBox.Location = doorLeedsTo.frontSpawnLocation;
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

        public void TryToOpen()
        {
            _ = new UnlockDoorWindow(kunskapsSpel, kunskapsSpel.camera, kunskapsSpel.player);
        }
    }
}
