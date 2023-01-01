﻿using Microsoft.Xna.Framework;
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
        private readonly Texture2D shutTexture;
        private readonly Texture2D openTexture;
        private int amountOfKeysToOpen;
        public bool open;
        private readonly bool showDoor;

        public Door(Rectangle hitBox, bool open, Room doorLeedsTo, KunskapsSpel kunskapsSpel, Texture2D shutTexture, Texture2D openTexture, int amountOfKeysToOpen) : base(kunskapsSpel)              //Visible door
        {
            this.open = open;
            this.doorLeedsTo = doorLeedsTo;
            this.shutTexture = shutTexture;
            this.openTexture = openTexture;
            this.amountOfKeysToOpen = amountOfKeysToOpen;
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
                spriteBatch.Draw(openTexture, hitBox, Color.White);
                return;
            }
            spriteBatch.Draw(shutTexture, hitBox, Color.White);
        }

        public bool first = true;
        public override void Update(GameTime gameTime)
        {
            if (!PlayerCanInteract(kunskapsSpel.player))
                return;

            if (open)
            {
                if (first)
                {
                    kunskapsSpel.activeCutscene = new ChangeRoomAnimation(kunskapsSpel.player, kunskapsSpel.camera, kunskapsSpel, this);
                    first = false;
                }
                kunskapsSpel.player.activeState = State.WatchingCutScene;
                if (kunskapsSpel.activeCutscene.changeRoom)
                    GoThroughDoor(kunskapsSpel.roomManager);
            }


            if (!Keyboard.GetState().IsKeyDown(Keys.Space))
                return;

            if (!open)
                TryToOpen();
        }

        public void GoThroughDoor(RoomManager roomManager)
        {
            if (this == roomManager.GetActiveRoom().backDoor)
            {
                roomManager.SetActiveRoom(doorLeedsTo);
                kunskapsSpel.player.hitBox.Location = doorLeedsTo.frontSpawnLocation;
                return;
            }
            roomManager.SetActiveRoom(doorLeedsTo);
            kunskapsSpel.player.hitBox.Location = doorLeedsTo.backSpawnLocation;
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
            if (kunskapsSpel.player.keyAmount >= amountOfKeysToOpen)
            {
                kunskapsSpel.player.keyAmount -= amountOfKeysToOpen;
                _ = new DialogueWindow(kunskapsSpel, kunskapsSpel.player, kunskapsSpel.camera, new()
                {
                    "*Knak*"
                }, kunskapsSpel.player.activeState);
                open = true;
                return;
            }

            amountOfKeysToOpen -= kunskapsSpel.player.keyAmount;
            kunskapsSpel.player.keyAmount = 0;
            _ = new DialogueWindow(kunskapsSpel, kunskapsSpel.player, kunskapsSpel.camera, new()
            {
                $"Du behöver {amountOfKeysToOpen} st nycklar till för att öppna denna dörr"
            }, kunskapsSpel.player.activeState);
        }
    }
}
