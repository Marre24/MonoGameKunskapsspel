using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace MonoGameKunskapsspel
{
    public class DialogueWindow : Window
    {
        private Rectangle dialogueWindow;
        private readonly Point size = new Point(900, 300);

        Vector2 textPosition;

        List<string> dialogue;
        KunskapsSpel kunskapsSpel;
        private SpriteFont pixel;
        private SpriteFont playerReady;
        private State activeState;

        enum State
        {
            ReadingText,
            Waiting,
            Done,
        }

        public DialogueWindow(KunskapsSpel kunskapsSpel)
        {
            this.kunskapsSpel = kunskapsSpel;
            kunskapsSpel.activeWindow = this;

            pixel = kunskapsSpel.Content.Load<SpriteFont>("Pixel");
            playerReady = kunskapsSpel.Content.Load<SpriteFont>("PlayerReady");
        }

        public void Init(List<string> dialogue)
        {
            this.dialogue = dialogue.ToList();
            words = dialogue[0].Split(" ").ToList();
            activeState = State.ReadingText;

            dialogueWindow = new Rectangle(new(
                kunskapsSpel.player.hitBox.X + kunskapsSpel.player.size.X / 2 - size.X / 2,
                kunskapsSpel.player.hitBox.Y + kunskapsSpel.player.size.Y / 2 + 350 - size.Y / 2), size);

            textPosition = new Vector2(dialogueWindow.X + 30, dialogueWindow.Y + 30);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(kunskapsSpel.Content.Load<Texture2D>("DialogueBox"), dialogueWindow, Color.White);

            spriteBatch.DrawString(playerReady, sentence, textPosition, Color.White);
        }

        private double timeSpan = 0.0;
        private const double interval = 1.0;
        private string sentence = "";
        private List<string> words;

        public override void Update(GameTime gameTime)
        {
            if (activeState == State.Done)
                End();

            if (activeState == State.Waiting && Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (dialogue.Count == 0)
                    activeState = State.Done;
                else
                {
                    words = dialogue[0].Split(" ").ToList();
                    sentence = "";
                    activeState = State.ReadingText;
                }
            }

            if (activeState == State.ReadingText && timeSpan + interval <= gameTime.TotalGameTime.TotalSeconds)
            {
                timeSpan = gameTime.ElapsedGameTime.TotalSeconds;
                sentence += words[0] + " ";
                words.RemoveAt(0);

                if (words.Count == 0)
                {
                    dialogue.RemoveAt(0);
                    activeState = State.Waiting;
                }
                
            }
        }

        public override void End()
        {
            kunskapsSpel.activeWindow = null;
        }
    }
}
