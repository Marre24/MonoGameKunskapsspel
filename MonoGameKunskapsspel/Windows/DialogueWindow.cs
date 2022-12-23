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
        private SpriteFont playerReady;
        private readonly Player player;
        private readonly Camera camera;

        public DialogueWindow(KunskapsSpel kunskapsSpel, Player player, Camera camera, List<string> dialogue)
        {
            this.kunskapsSpel = kunskapsSpel;
            this.player = player;
            this.camera = camera;
            kunskapsSpel.activeWindow = this;

            playerReady = kunskapsSpel.Content.Load<SpriteFont>("PlayerReady");

            Init(dialogue);
        }

        private void Init(List<string> dialogue)
        {
            this.dialogue = dialogue.ToList();
            List<string> wordStrings = dialogue[0].Split(" ").ToList();
            foreach (string word in wordStrings)
            {
                words.Add(word.ToCharArray().ToList());
            }
            player.activeState = State.ReadingText;

            dialogueWindow = new Rectangle(new(
                camera.window.X + camera.window.Size.X / 2 - size.X / 2,
                camera.window.Y + camera.window.Size.Y / 2 + 350 - size.Y / 2), size);

            textPosition = new Vector2(dialogueWindow.X + 30, dialogueWindow.Y + 30);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(kunskapsSpel.Content.Load<Texture2D>("DialogueBox"), dialogueWindow, Color.White);

            spriteBatch.DrawString(playerReady, sentence, textPosition, Color.White);
        }

        private double timeSpan = 0.0;
        private double interval = 0.08;
        private string sentence = "";
        private List<List<char>> words = new List<List<char>>();
        private List<char> activeWord = new List<char>();
        private int rowCount = 1;


        public override void Update(GameTime gameTime)
        {
            if (player.activeState == State.ReadingText && timeSpan + interval <= gameTime.TotalGameTime.TotalSeconds)
            {
                WriteOutText(gameTime);
            }

            if (player.activeState == State.WaitingForNextLine && Keyboard.GetState().IsKeyDown(Keys.Right))                         //Initializes the next phrase
            {
                if (dialogue.Count == 0)
                {
                    player.activeState = State.Walking;
                    End();
                }
                else
                {
                    List<string> wordStrings = dialogue[0].Split(" ").ToList();
                    foreach (string word in wordStrings)
                        words.Add(word.ToCharArray().ToList());

                    sentence = "";
                    player.activeState = State.ReadingText;
                }
            }
        }

        private void WriteOutText(GameTime gameTime)
        {
            if (Keyboard.GetState().GetPressedKeys().Contains(Keys.Up))                                                     //Fast forward
                interval = 0;

            timeSpan = gameTime.TotalGameTime.TotalSeconds;

            activeWord = words[0];

            if ((dialogueWindow.Width - 30) * rowCount < 26.7 * (sentence.Length + activeWord.Count))          //Checks if there is space for the next word
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

        public override void End()
        {
            kunskapsSpel.activeWindow = null;
        }
    }
}
