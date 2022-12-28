using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonoGameKunskapsspel
{
    public class Tile : Component
    {
        private Texture2D activeTexture;
        private Rectangle hitbox;
        private readonly List<Texture2D> tileTextures;

        public Tile(Point index, Point tileAmount, Point location, KunskapsSpel kunskapsSpel, List<Texture2D> tileTextures) : base(kunskapsSpel)
        {
            this.tileTextures = tileTextures;

            hitbox = new Rectangle(location, new(96, 96));

            if (index.X == 0 && index.Y == tileAmount.Y - 1)
            {
                activeTexture = tileTextures[1];           //BotLeft
                return;
            }
            if (index.Y == tileAmount.Y - 1 && index.X == tileAmount.X - 1)
            {
                activeTexture = tileTextures[2];           //BotRight
                return;
            }
            if (index.Y == 0 && index.X == 0)
            {
                activeTexture = tileTextures[7];           //TopLeft
                return;
            }
            if (index.Y == 0 && index.X == tileAmount.X - 1)
            {
                activeTexture = tileTextures[8];           //TopRight
                return;
            }
            if (index.X == 0)
            {
                activeTexture = tileTextures[3];           //Left
                return;
            }
            if (index.Y == 0)
            {
                activeTexture = tileTextures[6];           //Top
                return;
            }
            if (index.Y == tileAmount.Y - 1)
            {
                activeTexture = tileTextures[0];           //Bottom
                return;
            }
            if (index.X == tileAmount.X - 1)
            {
                activeTexture = tileTextures[5];           //Right
                return;
            }


            activeTexture = tileTextures[4];               //Middle
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(activeTexture, hitbox, Color.White);
        }

        public void ChangeToMiddleTexture()
        {
            activeTexture = tileTextures[4];
        }
        public void ChangeToEdgeTexture(string edge)
        {
            if (edge == "Top")
                activeTexture = tileTextures[6];
            if (edge == "Bottom")
                activeTexture = tileTextures[0];
            if (edge == "Right")
                activeTexture = tileTextures[5];
            if (edge == "Left")
                activeTexture = tileTextures[3];
        }



        public override void Update(GameTime gameTime)
        {

        }
    }
}
