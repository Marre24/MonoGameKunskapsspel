﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace MonoGameKunskapsspel
{
    public class UnlockDoorWindow : Window
    {
        private readonly KunskapsSpel kunskapsSpel;
        private Rectangle window;
        private Point size = new(Screen.PrimaryScreen.Bounds.Height, Screen.PrimaryScreen.Bounds.Height);

        public UnlockDoorWindow(KunskapsSpel kunskapsSpel) 
        {
            this.kunskapsSpel = kunskapsSpel;
            kunskapsSpel.activeWindow = this;

            window = new Rectangle(new(
                kunskapsSpel.player.hitBox.X + kunskapsSpel.player.size.X / 2 - size.X / 2,
                kunskapsSpel.player.hitBox.Y + kunskapsSpel.player.size.Y / 2 - Screen.PrimaryScreen.Bounds.Height / 2), size);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(kunskapsSpel.Content.Load<Texture2D>("DialogueBox"), window, Color.White);

        }

        public override void Update(GameTime gameTime)
        {



        }
        public override void End()
        {
            kunskapsSpel.activeWindow = null;
        }
    }
}