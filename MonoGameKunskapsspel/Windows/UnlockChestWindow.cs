using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Timers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace MonoGameKunskapsspel
{
    public class UnlockChestWindow : Window
    {
        private Rectangle window;
        private Point size = new(Screen.PrimaryScreen.Bounds.Height, Screen.PrimaryScreen.Bounds.Height);
        private readonly Chest chest;
        private readonly List<Texture2D> numberLockTextures;
        private Texture2D activeNumberTexture;
        private Texture2D padlockTexture;
        private readonly Rectangle padlockBox;
        private readonly Rectangle numberBox;
        private readonly Texture2D paperScroll;
        private readonly Rectangle upperPaperScrollBox;
        private readonly Rectangle lowerPaperScrollBox;
        private int lockNumber = 1;

        public UnlockChestWindow(KunskapsSpel kunskapsSpel, Camera camera, Player player, Chest chest) : base(kunskapsSpel, camera, player)
        {
            player.activeState = State.SolvingProblems;
            kunskapsSpel.activeWindow = this;
            this.chest = chest;

            padlockBox = new(camera.window.Center + new Point(-131, 200), new(232, 268));
            numberBox = new(padlockBox.Center + new Point(-50, 50), new(100, 36));
            upperPaperScrollBox = new(camera.window.Center - new Point(300, 500), new(600,  280));
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
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(kunskapsSpel.Content.Load<Texture2D>("DialogueBox"), window, Color.White);
                
            spriteBatch.Draw(padlockTexture, padlockBox, Color.White);
            spriteBatch.Draw(activeNumberTexture, numberBox, Color.White);
            spriteBatch.Draw(paperScroll, upperPaperScrollBox, Color.White);
            spriteBatch.Draw(paperScroll, lowerPaperScrollBox, Color.White);
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
        }

        public override void EndScene()
        {
            player.activeState = State.Walking;
            kunskapsSpel.activeWindow = null;
        }
    }
}
