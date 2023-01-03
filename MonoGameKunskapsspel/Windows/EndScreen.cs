using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
        private SpriteFont playerReady;
        private readonly Vector2 Speed = new(0,2);
        private readonly Rectangle window;

        public EndScreen(KunskapsSpel kunskapsSpel, Camera camera, Player player, State previousState) : base(kunskapsSpel, camera, player, previousState)
        {
            window = new Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            camera.Follow(window);
            playerReady = kunskapsSpel.Content.Load<SpriteFont>("LargePlayerReady");
            position1 = window.Center.ToVector2() + new Vector2(0, window.Size.Y) + new Vector2(-14 * 40, 0);//0
            position2 = window.Center.ToVector2() + new Vector2(0, window.Size.Y) + new Vector2(-18 * 40 + 13, 100);
            position3 = window.Center.ToVector2() + new Vector2(0, window.Size.Y) + new Vector2(-40 * 20, 400);//
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(playerReady, endingText[0], position1, Color.White);
            spriteBatch.DrawString(playerReady, endingText[1], position2, Color.White);
            spriteBatch.DrawString(playerReady, endingText[2], position3, Color.White);
            
        }
        private int i = 0;
        public override void Update(GameTime gameTime)
        {
            if (position3.Y < 0)
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



        }
    }
}
