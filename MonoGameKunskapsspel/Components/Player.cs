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
using System.Windows.Forms;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using SharpDX.Direct2D1.Effects;

namespace MonoGameKunskapsspel
{
    public class Player
    {
        readonly SpriteBatch spriteBatch;
        readonly GraphicsDeviceManager graphicsDevice;
        readonly KunskapsSpel kunskapsSpel;

        private readonly Texture2D front;
        private readonly Texture2D back;
        private readonly Texture2D right;
        private readonly Texture2D left;
        public Texture2D activeTexture;

        public Vector2 position;
        private Vector2 velocity;
        public Rectangle hitBox;
        private Point size;
        private const int movementSpeed = 5;
        public Player(SpriteBatch spriteBatch, GraphicsDeviceManager graphicsDevice, KunskapsSpel kunskapsSpel)
        {
            this.spriteBatch = spriteBatch;
            this.graphicsDevice = graphicsDevice;
            this.kunskapsSpel = kunskapsSpel;

            position = new Vector2(100f, 100f);
            velocity = new Vector2(0f, 0f);
            size = new Point(50, 50);
            hitBox = new Rectangle(position.ToPoint(), size);

            front = kunskapsSpel.Content.Load<Texture2D>("RobotFront");
            back = kunskapsSpel.Content.Load<Texture2D>("RobotBack");
            right = kunskapsSpel.Content.Load<Texture2D>("RobotRight");
            left = kunskapsSpel.Content.Load<Texture2D>("RobotLeft");

            activeTexture = front;

        }


        public void Update(GameTime gameTime)
        {
            Move();
        }

        private void Move()
        {
            (velocity.X, velocity.Y) = GetOffset();

            (bool canMoveX, bool canMoveY) = CanMoveTo((int)velocity.X, (int)velocity.Y);

            if (canMoveX)
                position.X -= velocity.X;

            if (canMoveY)
                position.Y -= velocity.Y;

            velocity = new(0, 0);
        }

        private Tuple<bool, bool> CanMoveTo(int x, int y)
        {
            bool CanMoveX = false;
            bool CanMoveY = false;

            foreach (FloorSegment floorSegment in kunskapsSpel.roomManager.GetActiveRoom().floorSegments)
            {
                if (floorSegment.hitBox.Location.X + x <= position.X && floorSegment.hitBox.Location.X + floorSegment.hitBox.Width + x >= position.X + size.X)
                    CanMoveX = true;

                if (floorSegment.hitBox.Location.Y + y <= position.Y + size.Y && floorSegment.hitBox.Location.Y + floorSegment.hitBox.Height + y >= position.Y + size.Y)
                    CanMoveY = true;
            }

            return Tuple.Create(CanMoveX, CanMoveY);

        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(activeTexture, new Rectangle((int)position.X, (int)position.Y, 50, 100), Color.White);
        }

        private Tuple<int, int> GetOffset()
        {
            int x = 0;
            int y = 0;

            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W))
                y += movementSpeed;
            if (keyboardState.IsKeyDown(Keys.S))
                y -= movementSpeed;
            if (keyboardState.IsKeyDown(Keys.A))
                x += movementSpeed;
            if (keyboardState.IsKeyDown(Keys.D))
                x -= movementSpeed;

            return Tuple.Create(x, y);
        }

    }
}
