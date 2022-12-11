using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;

namespace MonoGameKunskapsspel
{
    class Player
    {
        SpriteBatch spriteBatch;
        GraphicsDeviceManager graphicsDevice;

        private Texture2D front;
        private Texture2D back;
        private Texture2D right;
        private Texture2D left;
        public Texture2D activeTexture;

        Vector2 position;
        Rectangle hitBox;
        public Player(SpriteBatch spriteBatch, GraphicsDeviceManager graphicsDevice, ContentManager content)
        {
            this.spriteBatch = spriteBatch;
            this.graphicsDevice = graphicsDevice;

            front = content.Load<Texture2D>("RobotFront");
            back = content.Load<Texture2D>("RobotBack");
            right = content.Load<Texture2D>("RobotRight");
            left = content.Load<Texture2D>("RobotLeft");

            activeTexture = front;
            position = new Vector2(100f,100f);
            hitBox = new Rectangle();
        }


        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(activeTexture, new Rectangle((int)position.X, (int)position.Y, 50 , 100), Color.White);
        }
    }
}
