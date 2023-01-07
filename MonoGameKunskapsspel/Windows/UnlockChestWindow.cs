using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Microsoft.Xna.Framework.Audio;
using System.Threading;

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
        private readonly SpriteFont playerReady;
        private int lockNumber = 1;
        private bool anweredWrong = false;
        private int wrongNumber;
        readonly SoundEffect chestSound;
        readonly SoundEffect keySound;

        public UnlockChestWindow(int rightAnswer, string problem, List<string> solutions, KunskapsSpel kunskapsSpel, Camera camera, Player player, Chest chest, State prevousState) : base(kunskapsSpel, camera, player, prevousState)
        {
            player.activeState = State.SolvingProblems;
            kunskapsSpel.activeWindow = this;
            this.rightAnswer = rightAnswer;
            this.solutions = solutions;
            this.chest = chest;
            chestSound = kunskapsSpel.Content.Load<SoundEffect>("Music/ChestOpening");
            keySound = kunskapsSpel.Content.Load<SoundEffect>("Music/KeySound");
            padlockBox = new(camera.window.Center + new Point(-331, 200), new(232, 268));
            numberBox = new(padlockBox.Center + new Point(-50, 50), new(100, 36));
            upperPaperScrollBox = new(camera.window.Center - new Point(400, 500), new(800, 300));
            lowerPaperScrollBox = new(camera.window.Center - new Point(420, 200), new(840, 392));

            playerReady = kunskapsSpel.Content.Load<SpriteFont>("PlayerReady");
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
                    sentence += " \n ";
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

            spriteBatch.DrawString(playerReady, sentence, upperPaperScrollBox.Center.ToVector2() - new Vector2((20 * firstRowWords / 2) - 20, 20 * rowCount / 2), Color.White);

            spriteBatch.DrawString(playerReady, "1) " + solutions[0], lowerPaperScrollBox.Location.ToVector2() + new Vector2(100, 100), Color.White);
            spriteBatch.DrawString(playerReady, "2) " + solutions[1], lowerPaperScrollBox.Location.ToVector2() + new Vector2(lowerPaperScrollBox.Width / 2, 100), Color.White);
            spriteBatch.DrawString(playerReady, "3) " + solutions[2], lowerPaperScrollBox.Location.ToVector2() + new Vector2(100, lowerPaperScrollBox.Height / 2), Color.White);
            spriteBatch.DrawString(playerReady, "4) " + solutions[3], lowerPaperScrollBox.Location.ToVector2() + new Vector2(lowerPaperScrollBox.Width / 2, lowerPaperScrollBox.Height / 2), Color.White);
            if (anweredWrong)
                spriteBatch.DrawString(playerReady,$"{wrongNumber} är fel svar, försök igen",padlockBox.Center.ToVector2() + new Vector2(200, 0), Color.White);
        }

        private const double interval = 0.2;
        private double elsapsedTime = 0;
        private bool spaceWasUp = false;
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

            if (state.IsKeyDown(Keys.Space) && spaceWasUp && wrongNumber != lockNumber)
                CheckAnswer();

            if (state.IsKeyUp(Keys.Space))
                spaceWasUp = true;
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
                SoundEffect.MasterVolume = 0.6f;
                chestSound.Play();
                Thread.Sleep(1000);
                keySound.Play();
                return;
            }
            player.wrongAnswers++;
            SetWrongAnswerTo(lockNumber);
        }

        private void SetWrongAnswerTo(int lockNumber)
        {
            wrongNumber = lockNumber;
            anweredWrong = true;
        }

        public override void EndScene()
        {
            player.activeState = State.Walking;
            kunskapsSpel.activeWindow = null;
        }
    }
}
