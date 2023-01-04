using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Windows.Forms;

namespace MonoGameKunskapsspel
{
    class DeathWindow : Window
    {
        private readonly SpriteFont buttonFont;
        private readonly Texture2D buttonUpTexture;
        private readonly SpriteFont font;
        private readonly Rectangle window;
        private readonly Texture2D buttonDownTexture;
        private readonly Enemy enemy;
        private Rectangle buttonHitBox;
        private bool buttonIsUp = true;
        public DeathWindow(KunskapsSpel kunskapsSpel, Camera camera, Enemy enemy, Player player, State prevousState) : base(kunskapsSpel, camera, player, prevousState)
        {
            kunskapsSpel.musicManager.ChangeSlowlyToEnding();
            font = kunskapsSpel.Content.Load<SpriteFont>("LargePlayerReady");
            window = new(0,0,Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            buttonDownTexture = kunskapsSpel.Content.Load<Texture2D>("UI/ButtonDown");
            buttonUpTexture = kunskapsSpel.Content.Load<Texture2D>("UI/ButtonUp");
            buttonFont = kunskapsSpel.Content.Load<SpriteFont>("PlayerReady");
            buttonHitBox = new(window.Center - new Point(92, 0), new(46 * 4, 14 * 4));
            this.enemy = enemy;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Du dog", window.Center.ToVector2() - new Point(6 * 15, 465).ToVector2(), Color.Wheat);
            if (buttonIsUp)
                spriteBatch.Draw(buttonUpTexture, buttonHitBox, Color.White);
            else
                spriteBatch.Draw(buttonDownTexture, buttonHitBox, Color.White);
            spriteBatch.DrawString(buttonFont, "Försök Igen", buttonHitBox.Center.ToVector2() - new Point(11 * 10, 20).ToVector2(), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            camera.Follow(window);
            kunskapsSpel.IsMouseVisible = true;
            var mouseState = Mouse.GetState();

            if (!buttonHitBox.Contains(mouseState.Position))
            {
                buttonIsUp = true;
                buttonHitBox = new(window.Center - new Point(92, 0), new(46 * 6, 14 * 6));
                return;
            }
            buttonIsUp = false;
            buttonHitBox = new(window.Center - new Point(92, -6), new(46 * 6, 13 * 6));
            if (mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                kunskapsSpel.player.activeState = State.Walking;
                kunskapsSpel.activeWindow = null;
                kunskapsSpel.musicManager.increment = 0;
                enemy.hasInteracted = false;
            }
        }
    }
}
