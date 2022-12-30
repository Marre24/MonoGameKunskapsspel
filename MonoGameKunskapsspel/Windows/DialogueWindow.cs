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
        private readonly Point size = new(900, 300);
        private Vector2 textPosition;
        private readonly SpriteFont playerReady;

        private readonly List<string> dialogue;
        private readonly List<List<char>> words = new();
        private List<char> activeWord = new();

        private double timeSpan = 0.0;
        private double interval = 0.08;
        private int rowCount = 1;
        private string sentence = "";

        public DialogueWindow(KunskapsSpel kunskapsSpel, Player player, Camera camera, List<string> dialogue) : base(kunskapsSpel, camera, player)
        {
            kunskapsSpel.activeWindow = this;
            playerReady = kunskapsSpel.Content.Load<SpriteFont>("PlayerReady");
            playerReady.LineSpacing = 30;
            this.dialogue = dialogue.ToList();

            Init();
        }

        private void Init()
        {
            List<string> wordStrings = dialogue[0].Split(" ").ToList();

            foreach (string word in wordStrings)
                words.Add(word.ToCharArray().ToList());

            player.activeState = State.ReadingText;

            dialogueWindow = new Rectangle(new(
                camera.window.X + camera.window.Size.X / 2 - size.X / 2,
                camera.window.Y + camera.window.Size.Y / 2 + 350 - size.Y / 2), size);

            textPosition = new(dialogueWindow.X + 30, dialogueWindow.Y + 30);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(kunskapsSpel.Content.Load<Texture2D>("DialogueBox"), dialogueWindow, Color.White);

            spriteBatch.DrawString(playerReady, sentence, textPosition, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            if (player.activeState == State.ReadingText && timeSpan + interval <= gameTime.TotalGameTime.TotalSeconds)               //Writes out the phrase 
                WriteOutText(gameTime);

            if (player.activeState == State.WaitingForNextLine && Keyboard.GetState().IsKeyDown(Keys.Right))                         //Initializes the next phrase
            {
                if (dialogue.Count == 0)
                {
                    EndScene();
                    return;
                }

                List<string> wordStrings = dialogue[0].Split(" ").ToList();
                foreach (string word in wordStrings)
                    words.Add(word.ToCharArray().ToList());
                sentence = "";
                player.activeState = State.ReadingText;
            }
        }

        private void WriteOutText(GameTime gameTime)
        {
            if (Keyboard.GetState().GetPressedKeys().Contains(Keys.Up))                                                 //Fast forward
                interval = 0;

            activeWord = words[0];
            timeSpan = gameTime.TotalGameTime.TotalSeconds;

            if ((dialogueWindow.Width - 30) * rowCount < 20 * (sentence.Length + activeWord.Count))                   //Checks if there is space for the next word
            {
                sentence += "\n";
                rowCount++;
            }

            sentence += activeWord[0];
            activeWord.RemoveAt(0);

            if (activeWord.Count == 0)                  //Word has ended
            {
                words.RemoveAt(0);
                sentence += " ";
            }

            if (words.Count == 0)                       //Phrase has ended
            {
                dialogue.RemoveAt(0);
                player.activeState = State.WaitingForNextLine;
                rowCount = 1;
                interval = 0.08;
            }
        }
    }
}
