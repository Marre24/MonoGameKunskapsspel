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

        private readonly Point position = new(0, 0);
        public Point size = new(48, 62);
        public Rectangle hitBox;

        private Point velocity = new(0, 0);
        private const int movementSpeed = 5;

        public int keyAmount;

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

        private readonly Point wallOffset = new(20,20);

        private Tuple<bool, bool> CanMoveTo(int xVelocity, int yVelocity)
        {
            bool CanMoveX = false;
            bool CanMoveY = false;

            foreach (FloorSegment floorSegment in kunskapsSpel.roomManager.GetActiveRoom().floorSegments)
            {
                if (AreInsideOfFloorsegment(floorSegment))
                {
                    if (floorSegment.hitBox.Left <= hitBox.Left - velocity.X && floorSegment.hitBox.Right >= hitBox.Right - velocity.X)
                        CanMoveX = true;

                    if (floorSegment.hitBox.Top <= hitBox.Bottom - velocity.Y && floorSegment.hitBox.Bottom >= hitBox.Bottom - velocity.Y)
                        CanMoveY = true;
                }
                else if (WillBeInsideOfFloorsegment(floorSegment))
                {
                    if (floorSegment.hitBox.Left <= hitBox.Right - velocity.X && floorSegment.hitBox.Right >= hitBox.Left - velocity.X)
                        CanMoveX = true;

                    if (floorSegment.hitBox.Top <= hitBox.Bottom - velocity.Y && floorSegment.hitBox.Bottom >= hitBox.Bottom - velocity.Y)
                        CanMoveY = true;
                }
                
            }

            return Tuple.Create(CanMoveX, CanMoveY);
        }

        private bool WillBeInsideOfFloorsegment(FloorSegment floorSegment)
        {
            return (floorSegment.hitBox.Left <= hitBox.Right - velocity.X && floorSegment.hitBox.Right >= hitBox.Left - velocity.X) &&
                (floorSegment.hitBox.Top <= hitBox.Bottom - velocity.Y && floorSegment.hitBox.Bottom >= hitBox.Bottom - velocity.Y);
        }

        private bool AreInsideOfFloorsegment(FloorSegment floorSegment)
        {
            return (floorSegment.hitBox.Left <= hitBox.Left && floorSegment.hitBox.Right >= hitBox.Right) &&
                (floorSegment.hitBox.Top <= hitBox.Bottom && floorSegment.hitBox.Bottom >= hitBox.Bottom);
        }
        

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationManager.Draw(spriteBatch);
        }
    }
}
