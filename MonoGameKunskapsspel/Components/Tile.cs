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
        private List<Texture2D> grassTextures = new();
        private Texture2D activeTexture;
        private Rectangle hitbox;


        public Tile(Point size, Point index, Point tileAmount, Point location, KunskapsSpel kunskapsSpel) : base(kunskapsSpel)
        {
            grassTextures.Add(kunskapsSpel.Content.Load<Texture2D>("Enviroment/GrassTileBottom"));          //0
            grassTextures.Add(kunskapsSpel.Content.Load<Texture2D>("Enviroment/GrassTileBottomLeft"));
            grassTextures.Add(kunskapsSpel.Content.Load<Texture2D>("Enviroment/GrassTileBottomRight"));

            grassTextures.Add(kunskapsSpel.Content.Load<Texture2D>("Enviroment/GrassTileLeft"));            //3
            grassTextures.Add(kunskapsSpel.Content.Load<Texture2D>("Enviroment/GrassTileMiddle"));
            grassTextures.Add(kunskapsSpel.Content.Load<Texture2D>("Enviroment/GrassTileRight"));

            grassTextures.Add(kunskapsSpel.Content.Load<Texture2D>("Enviroment/GrassTileTop"));             //6
            grassTextures.Add(kunskapsSpel.Content.Load<Texture2D>("Enviroment/GrassTileTopLeft"));
            grassTextures.Add(kunskapsSpel.Content.Load<Texture2D>("Enviroment/GrassTileTopRight"));

            hitbox = new Rectangle(location, size);

            if (index.X == 0 && index.Y == tileAmount.Y - 1)
            {
                activeTexture = grassTextures[1];           //BotLeft
                return;
            }
            if (index.Y == tileAmount.Y - 1 && index.X == tileAmount.X - 1)
            {
                activeTexture = grassTextures[2];           //BotRight
                return;
            }
            if (index.Y == 0 && index.X == 0)
            {
                activeTexture = grassTextures[7];           //TopLeft
                return;
            }
            if (index.Y == 0 && index.X == tileAmount.X - 1)
            {
                activeTexture = grassTextures[8];           //TopRight
                return;
            }
            if (index.X == 0)
            {
                activeTexture = grassTextures[3];           //Left
                return;
            }
            if (index.Y == 0)
            {
                activeTexture = grassTextures[6];           //Top
                return;
            }
            if (index.Y == tileAmount.Y - 1)
            {
                activeTexture = grassTextures[0];           //Bottom
                return;
            }
            if (index.X == tileAmount.X - 1)
            {
                activeTexture = grassTextures[5];           //Right
                return;
            }


            activeTexture = grassTextures[4];               //Middle
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(activeTexture, hitbox, Color.White);
        }

        public void ChangeToMiddleTexture()
        {
            activeTexture = grassTextures[4];
        }
        public void ChangeToEdgeTexture(string edge)
        {
            if (edge == "Top")
                activeTexture = grassTextures[6];
            if (edge == "Bottom")
                activeTexture = grassTextures[0];
            if (edge == "Right")
                activeTexture = grassTextures[5];
            if (edge == "Left")
                activeTexture = grassTextures[3];
        }



        public override void Update(GameTime gameTime)
        {

        }
    }
}
