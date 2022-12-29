using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonoGameKunskapsspel
{
    internal class StartScreen : Room
    {
        
        private readonly Texture2D texture;
        public StartScreen(int RoomID, KunskapsSpel kunskapsSpel) : base(RoomID, kunskapsSpel)
        {
            texture = kunskapsSpel.Content.Load<Texture2D>("StartScreen");
            window = new(0, 0, 2500, 800);
        }

        public override void CreateDoors()
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, window, Color.White);
        }

        public override void Initialize()
        {

        }

        public override void SetDoorLocations()
        {
        }

        public override void Update(GameTime gameTime)
        {
            kunskapsSpel.IsMouseVisible = true;

        }
    }
}
