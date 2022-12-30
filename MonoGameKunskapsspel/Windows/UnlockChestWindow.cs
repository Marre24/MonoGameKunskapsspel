﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Timers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageBox = System.Windows.Forms.MessageBox;
using System.Windows.Forms;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace MonoGameKunskapsspel
{
    public class UnlockChestWindow : Window
    {
        private Rectangle window;
        private Point size = new(Screen.PrimaryScreen.Bounds.Height, Screen.PrimaryScreen.Bounds.Height);
        private readonly int rightAnswer;
        private readonly List<string> solutions;
        private readonly Chest chest;
        private readonly List<Texture2D> numberLockTextures;
        private Texture2D activeNumberTexture;
        private Texture2D padlockTexture;
        private Rectangle padlockBox;
        private readonly Rectangle numberBox;
        private readonly Texture2D paperScroll;
        private readonly Rectangle upperPaperScrollBox;
        private readonly Rectangle lowerPaperScrollBox;
        private int lockNumber = 1;

        public UnlockChestWindow(int rightAnswer, string problem, List<string> solutions, KunskapsSpel kunskapsSpel, Camera camera, Player player, Chest chest) : base(kunskapsSpel, camera, player)
        {
            player.activeState = State.SolvingProblems;
            kunskapsSpel.activeWindow = this;
            this.rightAnswer = rightAnswer;
            this.solutions = solutions;
            this.chest = chest;
            padlockBox = new(camera.window.Center + new Point(-131, 200), new(232, 268));
            numberBox = new(padlockBox.Center + new Point(-50, 50), new(100, 36));
            upperPaperScrollBox = new(camera.window.Center - new Point(400, 500), new(800, 300));
            lowerPaperScrollBox = new(camera.window.Center - new Point(420, 200), new(840, 392));

            window = new Rectangle(new(
                camera.window.X + camera.window.Size.X / 2 - size.X / 2,
                camera.window.Y + camera.window.Size.Y / 2 - Screen.PrimaryScreen.Bounds.Height / 2), size);

            numberLockTextures = new()
            {
                kunskapsSpel.Content.Load<Texture2D>("Msc/Codelock1"),
                kunskapsSpel.Content.Load<Texture2D>("Msc/Codelock2"),
                kunskapsSpel.Content.Load<Texture2D>("Msc/Codelock3"),
                kunskapsSpel.Content.Load<Texture2D>("Msc/Codelock4"),
            };

            padlockTexture = kunskapsSpel.Content.Load<Texture2D>("Msc/LockedPadlock");
            paperScroll = kunskapsSpel.Content.Load<Texture2D>("Msc/PaperScroll");
            activeNumberTexture = numberLockTextures[lockNumber - 1];

            List<string> words = problem.Split(" ").ToList();

            foreach (string word in words)
            {
                if ((upperPaperScrollBox.Width - 150) * rowCount < 20 * (sentence.Length + word.Length))                   //Checks if there is space for the next word
                {
                    sentence += "\n";
                    rowCount++;
                    
                }
                if (rowCount == 1)
                    firstRowWords += word.Length + 1;
                sentence += $"{word} ";
            }
        }
        private readonly string sentence = "";
        private readonly int firstRowWords = 0;
        readonly int rowCount = 1;

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(kunskapsSpel.Content.Load<Texture2D>("Msc/LargeDialogbox"), window, Color.White);

            spriteBatch.Draw(padlockTexture, padlockBox, Color.White);
            spriteBatch.Draw(activeNumberTexture, numberBox, Color.White);
            spriteBatch.Draw(paperScroll, upperPaperScrollBox, Color.White);
            spriteBatch.Draw(paperScroll, lowerPaperScrollBox, Color.White);


            spriteBatch.DrawString(kunskapsSpel.Content.Load<SpriteFont>("PlayerReady"),
                sentence, upperPaperScrollBox.Center.ToVector2() - new Vector2(20 * firstRowWords / 2, 20 * rowCount / 2), Color.White);

            spriteBatch.DrawString(kunskapsSpel.Content.Load<SpriteFont>("PlayerReady"),
                "1)" + solutions[0], lowerPaperScrollBox.Location.ToVector2() + new Vector2(150, 100), Color.White);
            spriteBatch.DrawString(kunskapsSpel.Content.Load<SpriteFont>("PlayerReady"),
                "2)" + solutions[1], lowerPaperScrollBox.Location.ToVector2() + new Vector2(lowerPaperScrollBox.Width / 2, 100), Color.White);
            spriteBatch.DrawString(kunskapsSpel.Content.Load<SpriteFont>("PlayerReady"),
                "3)" + solutions[2], lowerPaperScrollBox.Location.ToVector2() + new Vector2(150, lowerPaperScrollBox.Height / 2), Color.White);
            spriteBatch.DrawString(kunskapsSpel.Content.Load<SpriteFont>("PlayerReady"),
                "4)" + solutions[3], lowerPaperScrollBox.Location.ToVector2() + new Vector2(lowerPaperScrollBox.Width / 2, lowerPaperScrollBox.Height / 2), Color.White);
        }

        private const double interval = 0.2;
        private double elsapsedTime = 0;
        public override void Update(GameTime gameTime)
        {
            if (elsapsedTime + interval > gameTime.TotalGameTime.TotalSeconds)
                return;

            var state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Right))
            {
                elsapsedTime = gameTime.TotalGameTime.TotalSeconds;
                lockNumber++;
            }

            if (state.IsKeyDown(Keys.Left))
            {
                elsapsedTime = gameTime.TotalGameTime.TotalSeconds;
                lockNumber--;
            }

            if (lockNumber < 1)
                lockNumber = 4;

            if (lockNumber > 4)
                lockNumber = 1;
            activeNumberTexture = numberLockTextures[lockNumber - 1];

            if (state.IsKeyDown(Keys.Enter))
                CheckAnswer();
        }

        private void CheckAnswer()
        {
            if (lockNumber == rightAnswer)
            {
                padlockBox = new(camera.window.Center + new Point(-131, 180), new(232, 288));
                padlockTexture = kunskapsSpel.Content.Load<Texture2D>("Msc/UnlockedPadlock");
                kunskapsSpel.activeWindow = null;
                player.activeState = State.Walking;
                chest.Open();
                return;
            }
            MessageBox.Show("false");
        }


        public override void EndScene()
        {
            player.activeState = State.Walking;
            kunskapsSpel.activeWindow = null;
        }
    }
}