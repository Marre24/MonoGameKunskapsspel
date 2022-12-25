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
    public enum State
    {
        ReadingText,
        WaitingForNextLine,
        WatchingCutScene,
        Walking,
    }

    public class Player : Component
    {
        AnimationManager animationManager;

        Dictionary<string, Animation> animations;

        public State activeState = State.Walking;

        private readonly Point position = new(100, 100);
        public Point size = new(48, 62);
        public Rectangle hitBox;

        private Point velocity = new(0, 0);
        private const int movementSpeed = 5;

        public Player(KunskapsSpel kunskapsSpel, Dictionary<string, Animation> animations) : base(kunskapsSpel)
        {
            hitBox = new(position, size);
            this.animations = animations;
            animationManager = new AnimationManager(animations.First().Value);
        }

        public override void Update(GameTime gameTime)
        {
            Move();

            animationManager.Position = hitBox.Location.ToVector2();
            animationManager.Update(gameTime);

            if (velocity == Point.Zero)                                                                 //Standing still
            {
                animationManager.Play(animations["Idle"]);
                velocity = new(0, 0);
            }

            if (velocity.X < 0)
                animationManager.Play(animations["WalkRight"]);
            else if (velocity.X > 0)
                animationManager.Play(animations["WalkLeft"]);
            else if (velocity.Y < 0)
                animationManager.Play(animations["WalkLeft"]);
            else if (velocity.Y > 0)
                animationManager.Play(animations["WalkRight"]);
            

        }

        private void Move()
        {
            (velocity.X, velocity.Y) = GetVelocity();

            if (velocity == Point.Zero)                                                                 //Standing still
                return;

            (bool canMoveX, bool canMoveY) = CanMoveTo(velocity.X, velocity.Y);

            if (canMoveX)                                                                               
                hitBox.Location = new Point(hitBox.Location.X - velocity.X, hitBox.Location.Y);
            if (canMoveY)
                hitBox.Location = new Point(hitBox.Location.X, hitBox.Location.Y - velocity.Y);

        }

        private Tuple<int, int> GetVelocity()
        {
            int x = 0;
            int y = 0;

            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W))
            {
                y += movementSpeed;
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                y -= movementSpeed;
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                x += movementSpeed;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                x -= movementSpeed;
            }

            return Tuple.Create(x, y);
        }

        private Tuple<bool, bool> CanMoveTo(int xVelocity, int yVelocity)
        {
            bool CanMoveX = false;
            bool CanMoveY = false;

            foreach (FloorSegment floorSegment in kunskapsSpel.roomManager.GetActiveRoom().floorSegments)
            {
                if (floorSegment.hitBox.Location.X + xVelocity <= hitBox.Location.X - 20 && 
                    floorSegment.hitBox.Location.X + floorSegment.hitBox.Width + xVelocity >= hitBox.Location.X + size.X + 20)
                    CanMoveX = true;

                if (floorSegment.hitBox.Location.Y + yVelocity <= hitBox.Location.Y + size.Y - 30 && 
                    floorSegment.hitBox.Location.Y + floorSegment.hitBox.Height + yVelocity >= hitBox.Location.Y + size.Y + 30)
                    CanMoveY = true;
            }

            return Tuple.Create(CanMoveX, CanMoveY);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationManager.Draw(spriteBatch);
        }
    }
}
