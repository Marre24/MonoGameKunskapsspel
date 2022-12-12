using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;

namespace MonoGameKunskapsspel
{
    public class Player
    {
        readonly SpriteBatch spriteBatch;
        readonly GraphicsDeviceManager graphicsDevice;

        private readonly Texture2D front;
        private readonly Texture2D back;
        private readonly Texture2D right;
        private readonly Texture2D left;
        public Texture2D activeTexture;

        public Vector2 position;
        public Rectangle hitBox;
        private const int movementSpeed = 5;
        public Player(SpriteBatch spriteBatch, GraphicsDeviceManager graphicsDevice, ContentManager content)
        {
            this.spriteBatch = spriteBatch;
            this.graphicsDevice = graphicsDevice;

            front = content.Load<Texture2D>("RobotFront");
            back = content.Load<Texture2D>("RobotBack");
            right = content.Load<Texture2D>("RobotRight");
            left = content.Load<Texture2D>("RobotLeft");

            activeTexture = front;
            position = new Vector2(100f, 100f);
            hitBox = new Rectangle();
        }


        public void Update(GameTime gameTime)
        {
            Move();
        }

        private void Move()
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W))
            {
                position.Y -= movementSpeed;
                activeTexture = back;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                position.Y += movementSpeed;
                activeTexture = front;
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                position.X -= movementSpeed;
                activeTexture = left;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                position.X += movementSpeed;
                activeTexture = right;
            }
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(activeTexture, new Rectangle((int)position.X, (int)position.Y, 50, 100), Color.White);
        }



    }
}
