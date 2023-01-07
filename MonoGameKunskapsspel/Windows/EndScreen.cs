using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace MonoGameKunskapsspel
{
    public class EndScreen : Window
    {
        private readonly List<string> endingText = new()
        {
            "programmerare  Maximilian Ellnestam",          //14 char
            "vice programmerare  Felix Strömberg",          //19
            "Speciellt tack till: Rasmus Junel, Theodor Lindberg \n och testgruppen som testar vårt spel",
        };
        private Vector2 position1;
        private Vector2 position2;
        private Vector2 position3;
        private readonly Texture2D buttonDownTexture;
        private readonly Texture2D buttonUpTexture;
        private readonly SpriteFont buttonFont;
        private Rectangle buttonHitBox;
        private Rectangle star1Box;
        private Rectangle star2Box;
        private Rectangle star3Box;
        private readonly SpriteFont playerReady;
        private readonly Vector2 Speed = new(0,2);
        private readonly Rectangle window;
        private readonly Dictionary<bool, Texture2D> starTextures = new();
        private readonly bool star1;
        private bool star2;
        private readonly bool star3;


        public EndScreen(KunskapsSpel kunskapsSpel, Camera camera, Player player, State previousState) : base(kunskapsSpel, camera, player, previousState)
        {
            window = new Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            camera.Follow(window);
            playerReady = kunskapsSpel.Content.Load<SpriteFont>("LargePlayerReady");
            position1 = window.Center.ToVector2() + new Vector2(0, window.Size.Y) + new Vector2(-14 * 40, 0);//0
            position2 = window.Center.ToVector2() + new Vector2(0, window.Size.Y) + new Vector2(-18 * 40 + 13, 100);
            position3 = window.Center.ToVector2() + new Vector2(0, window.Size.Y) + new Vector2(-40 * 20, 400);
            buttonDownTexture = kunskapsSpel.Content.Load<Texture2D>("UI/ButtonDown");
            buttonUpTexture = kunskapsSpel.Content.Load<Texture2D>("UI/ButtonUp");
            buttonFont = kunskapsSpel.Content.Load<SpriteFont>("PlayerReady");
            starTextures.Add(true, kunskapsSpel.Content.Load<Texture2D>("Msc/ColoredStar"));
            starTextures.Add(false, kunskapsSpel.Content.Load<Texture2D>("Msc/EmptyStar"));

            star1 = true;
            star3 = player.wrongAnswers <= 4;

            Point size = new(64, 68);
            int xOffset = 500;

            star1Box = new(window.Center - new Point(xOffset, 400), size);
            star2Box = new(window.Center - new Point(xOffset, 200), size);
            star3Box = new(window.Center - new Point(xOffset, 0), size);
        }
        private bool phaseTwo = false;
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!phaseTwo)
            {
                spriteBatch.DrawString(playerReady, endingText[0], position1, Color.White);
                spriteBatch.DrawString(playerReady, endingText[1], position2, Color.White);
                spriteBatch.DrawString(playerReady, endingText[2], position3, Color.White);
                return;
            }

            spriteBatch.Draw(starTextures[star1], star1Box, Color.White);
            spriteBatch.DrawString(playerReady, $"Du klarade spelet", star1Box.Location.ToVector2() + new Vector2(star1Box.Width + 20, 10), Color.White);
            spriteBatch.Draw(starTextures[star2], star2Box, Color.White);
            spriteBatch.DrawString(playerReady, $"Din tid var {timeInMinuits}min   Max: 17 min", star2Box.Location.ToVector2() + new Vector2(star2Box.Width + 20, 10), Color.White);
            spriteBatch.Draw(starTextures[star3], star3Box, Color.White);
            spriteBatch.DrawString(playerReady, $"Du gissade fel {player.wrongAnswers} gånger   Max: 4st", star3Box.Location.ToVector2() + new Vector2(star3Box.Width + 20, 10), Color.White);


            if (buttonIsUp)
                spriteBatch.Draw(buttonUpTexture, buttonHitBox, Color.White);
            else
                spriteBatch.Draw(buttonDownTexture, buttonHitBox, Color.White);
            spriteBatch.DrawString(buttonFont, "Avsluta", buttonHitBox.Location.ToVector2() + new Point(70, 20).ToVector2(), Color.White);

        }
        private int i = 0;
        private bool buttonIsUp;
        double timeInMinuits;

        public override void Update(GameTime gameTime)
        {
            if (i == 0)
            {
                timeInMinuits = Math.Round(gameTime.TotalGameTime.TotalMinutes - (player.startTime / 60), 1);
                star2 = gameTime.TotalGameTime.TotalSeconds - player.startTime < 17 * 60;
            }


            if (position3.Y < -700)
            {
                PhaseTwo();
                return;
            }



            if (i++ == 2)
                Thread.Sleep(1000);
            camera.Follow(window);
            position1 -= Speed;
            position2 -= Speed;
            position3 -= Speed;
        }

        private void PhaseTwo()
        {
            phaseTwo = true;
            var mouseState = Mouse.GetState();
            if (!buttonHitBox.Contains(mouseState.Position))
            {
                buttonIsUp = true;
                buttonHitBox = new(window.Center - new Point(120, -200), new(46 * 6, 14 * 6));
                return;
            }
            buttonIsUp = false;
            buttonHitBox = new(window.Center - new Point(120, -206), new(46 * 6, 13 * 6));
            if (mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                kunskapsSpel.Exit();
        }
    }
}
