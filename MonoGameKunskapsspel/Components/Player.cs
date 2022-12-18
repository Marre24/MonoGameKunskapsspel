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
    public class Player : Component
    {
        readonly KunskapsSpel kunskapsSpel;

        private readonly Texture2D front;
        private readonly Texture2D back;
        private readonly Texture2D right;
        private readonly Texture2D left;
        public Texture2D activeTexture;

        private readonly Point position = new(100, 100);
        public Point size = new(75, 100);
        private Vector2 velocity = new(0f, 0f);
        public Rectangle hitBox;
        private const int movementSpeed = 5;
        public Player(KunskapsSpel kunskapsSpel)
        {
            this.kunskapsSpel = kunskapsSpel;

            hitBox = new Rectangle(position, size);

            front = kunskapsSpel.Content.Load<Texture2D>("RobotFront");
            back = kunskapsSpel.Content.Load<Texture2D>("RobotBack");
            right = kunskapsSpel.Content.Load<Texture2D>("RobotRight");
            left = kunskapsSpel.Content.Load<Texture2D>("RobotLeft");

            activeTexture = front;
        }


        public override void Update(GameTime gameTime)
        {
            Move();
        }

        private void Move()
        {
            (velocity.X, velocity.Y) = GetOffset();

            (bool canMoveX, bool canMoveY) = CanMoveTo((int)velocity.X, (int)velocity.Y);

            if (canMoveX)
                hitBox.Location = new Point(hitBox.Location.X - (int)velocity.X, hitBox.Location.Y);

            if (canMoveY)
                hitBox.Location = new Point(hitBox.Location.X, hitBox.Location.Y - (int)velocity.Y);

            velocity = new(0, 0);
        }

        private Tuple<bool, bool> CanMoveTo(int x, int y)
        {
            bool CanMoveX = false;
            bool CanMoveY = false;

            foreach (FloorSegment floorSegment in kunskapsSpel.roomManager.GetActiveRoom().floorSegments)
            {
                if (floorSegment.hitBox.Location.X + x <= hitBox.Location.X && floorSegment.hitBox.Location.X + floorSegment.hitBox.Width + x >= hitBox.Location.X + size.X)
                    CanMoveX = true;

                if (floorSegment.hitBox.Location.Y + y <= hitBox.Location.Y + size.Y && floorSegment.hitBox.Location.Y + floorSegment.hitBox.Height + y >= hitBox.Location.Y + size.Y)
                    CanMoveY = true;
            }

            return Tuple.Create(CanMoveX, CanMoveY);

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(activeTexture, hitBox, Color.White);
        }

        private Tuple<int, int> GetOffset()
        {
            int x = 0;
            int y = 0;

            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W))
            {
                y += movementSpeed;
                activeTexture = back;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                y -= movementSpeed;
                activeTexture = front;
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                x += movementSpeed;
                activeTexture = left;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                x -= movementSpeed;
                activeTexture = right;
            }

            return Tuple.Create(x, y);
        }
    }
}
