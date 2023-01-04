using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MonoGameKunskapsspel
{
    internal class StartScreen : Room
    {
        private readonly SpriteFont font;
        private readonly SpriteFont buttonFont;
        private readonly Texture2D buttonUpTexture;
        private readonly Texture2D buttonDownTexture;
        private Rectangle buttonHitBox;
        private bool buttonIsUp = true;
        public StartScreen(int RoomID, KunskapsSpel kunskapsSpel) : base(RoomID, kunskapsSpel)
        {
            window = new(new(0, 0), new (Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height));
            buttonDownTexture = kunskapsSpel.Content.Load<Texture2D>("UI/ButtonDown");
            buttonUpTexture = kunskapsSpel.Content.Load<Texture2D>("UI/ButtonUp");
            font = kunskapsSpel.Content.Load<SpriteFont>("LargePlayerReady");
            buttonFont = kunskapsSpel.Content.Load<SpriteFont>("PlayerReady");
            buttonHitBox = new(window.Center - new Point(92, 0), new(46 * 4, 14 * 4));
            npc = new NPC(new(1270, 50), kunskapsSpel, new List<string>(), kunskapsSpel.animations);
        }

        public override void CreateDoors()
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            kunskapsSpel.player.Draw(gameTime, spriteBatch);
            npc.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(font, "Mattehjälten", window.Center.ToVector2() - new Point(180, 465).ToVector2(), Color.Wheat);
            if (buttonIsUp)
                spriteBatch.Draw(buttonUpTexture, buttonHitBox, Color.White);
            else
                spriteBatch.Draw(buttonDownTexture, buttonHitBox, Color.White);
            spriteBatch.DrawString(buttonFont, "Starta spelet", buttonHitBox.Center.ToVector2() - new Point(13 * 10, 10).ToVector2(), Color.White);
        }

        public override void Initialize()
        {

        }

        public override void SetDoorLocations()
        {
        }

        public override void Update(GameTime gameTime)
        {
            kunskapsSpel.player.Update(gameTime);
            npc.Update(gameTime);
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
                kunskapsSpel.player.hitBox.Location = new Point(100,150);
                kunskapsSpel.roomManager.SetActiveRoom(kunskapsSpel.roomManager.rooms[1]);
            }

        }
    }
}
