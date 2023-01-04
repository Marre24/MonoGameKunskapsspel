using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace MonoGameKunskapsspel
{
    public enum State
    {
        InStartScreen,
        ReadingText,
        WaitingForNextLine,
        WatchingCutScene,
        Walking,
        SolvingProblems,
        Dead,
        Ended,
    }

    public class Player : Component
    {
        readonly AnimationManager animationManager;
        readonly Dictionary<string, Animation> animations;
        public State activeState = State.InStartScreen;

        private Rectangle inventoryHitbox = new(-10000,-10000,0,0);
        private Texture2D inventoryTexture;
        private readonly Point position = new(700, 50);
        public Point size = new(48, 62);

        public Point velocity = new(0, 0);
        private const int movementSpeed = 5;

        public int keyAmount = 0;
        public int wrongAnswers;

        public Player(KunskapsSpel kunskapsSpel, Dictionary<string, Animation> animations) : base(kunskapsSpel)
        {
            hitBox = new(position, size);
            this.animations = animations;
            animationManager = new AnimationManager(animations.First().Value);
            inventoryTexture = kunskapsSpel.Content.Load<Texture2D>("Msc/KeyInventory");

        }

        public override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            

            if (keyboardState.IsKeyDown(Keys.O) && keyboardState.IsKeyDown(Keys.I))
                hitBox.Location = kunskapsSpel.roomManager.GetActiveRoom().backSpawnLocation;

            velocity = new(0, 0);

            if (activeState == State.Walking)
                Move();

            animationManager.Position = hitBox.Location.ToVector2();
            animationManager.Update(gameTime);

            if (velocity == Point.Zero)                                                                 //Standing still
            {
                animationManager.Play(animations["Idle"]);
                velocity = new(0, 0);
                return;
            }
            
            inventoryHitbox = new(kunskapsSpel.camera.window.Right - 200, kunskapsSpel.camera.window.Top + 50, 150, 80);
            if (velocity.X < 0)
                animationManager.Play(animations["WalkRight"]);
            else if (velocity.X > 0)
                animationManager.Play(animations["WalkLeft"]);
            else if (velocity.Y < 0)
                animationManager.Play(animations["WalkRight"]);
            else if (velocity.Y > 0)
                animationManager.Play(animations["WalkRight"]);
        }

        private void Move()
        {
            (velocity.X, velocity.Y) = GetVelocity();

            if (velocity == Point.Zero)                                                                 //Standing still
                return;

            if (!kunskapsSpel.roomManager.rooms[2].chests[0].open)
            {
                if (kunskapsSpel.roomManager.GetActiveRoom().RoomID == 1 && WillBeInsideOfComponent(kunskapsSpel.roomManager.GetActiveRoom().floorSegments[2]))
                {
                    _ = new DialogueWindow(kunskapsSpel, kunskapsSpel.player, kunskapsSpel.camera, new()
                    {
                        "Jag tror att Edazor sa att jag skulle ta den vänstra stigen innan jag gör något annat"
                    }, State.Walking);
                }
            }

            (bool canMoveX, bool canMoveY) = CanMove();

            if (canMoveX)                                                                               
                hitBox.Location = new Point(hitBox.Location.X - velocity.X, hitBox.Location.Y);
            if (canMoveY)
                hitBox.Location = new Point(hitBox.Location.X, hitBox.Location.Y - velocity.Y);

        }

        private static Tuple<int, int> GetVelocity()
        {
            int x = 0;
            int y = 0;

            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
            {
                y += movementSpeed;
            }
            if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
            {
                y -= movementSpeed;
            }
            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
            {
                x += movementSpeed;
            }
            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                x -= movementSpeed;
            }

            return Tuple.Create(x, y);
        }

        private Tuple<bool, bool> CanMove()
        {
            bool CanMoveX = false;
            bool CanMoveY = false;

            foreach (FloorSegment floorSegment in kunskapsSpel.roomManager.GetActiveRoom().floorSegments)
            {
                if (AreInsideOfComponent(floorSegment))
                {
                    if (floorSegment.hitBox.Left <= hitBox.Left - velocity.X && floorSegment.hitBox.Right >= hitBox.Right - velocity.X)
                        CanMoveX = true;

                    if (floorSegment.hitBox.Top <= hitBox.Bottom - velocity.Y && floorSegment.hitBox.Bottom >= hitBox.Bottom - velocity.Y)
                        CanMoveY = true;
                }
                else if (WillBeInsideOfComponent(floorSegment))
                {
                    if (floorSegment.hitBox.Left <= hitBox.Right - velocity.X && floorSegment.hitBox.Right >= hitBox.Left - velocity.X)
                        CanMoveX = true;

                    if (floorSegment.hitBox.Top <= hitBox.Bottom - velocity.Y && floorSegment.hitBox.Bottom >= hitBox.Bottom - velocity.Y)
                        CanMoveY = true;
                }
            }

            bool colidesX = false;
            bool colidesY = false;
            foreach (Component component in kunskapsSpel.roomManager.GetActiveRoom().components)
            {
                if (component.haveColisison == true && WillBeInsideOfComponent(component))
                {
                    if (component.hitBox.Contains(new Vector2(hitBox.Right - velocity.X, hitBox.Bottom)) || component.hitBox.Contains(new Vector2(hitBox.Left - velocity.X, hitBox.Bottom)))
                        colidesX = true;

                    if (component.hitBox.Contains(new Vector2(hitBox.Right, hitBox.Bottom - velocity.Y)) || component.hitBox.Contains(new Vector2(hitBox.Left, hitBox.Bottom - velocity.Y)))
                        colidesY = true;
                }
            }
                

            return Tuple.Create(CanMoveX && !colidesX, CanMoveY && !colidesY);
        }

        private bool WillBeInsideOfComponent(Component component)
        {
            return (component.hitBox.Left <= hitBox.Right - velocity.X && component.hitBox.Right >= hitBox.Left - velocity.X) &&
                (component.hitBox.Top <= hitBox.Bottom - velocity.Y && component.hitBox.Bottom >= hitBox.Bottom - velocity.Y);
        }

        private bool AreInsideOfComponent(Component component)
        {
            return (component.hitBox.Left <= hitBox.Left && component.hitBox.Right >= hitBox.Right) &&
                (component.hitBox.Top <= hitBox.Bottom && component.hitBox.Bottom >= hitBox.Bottom);
        }
        

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationManager.Draw(spriteBatch);
            inventoryTexture??= kunskapsSpel.Content.Load<Texture2D>("Msc/EmptyInventory");

            if (activeState == State.Walking)
            {
                spriteBatch.Draw(inventoryTexture, inventoryHitbox, Color.White);
                spriteBatch.DrawString(kunskapsSpel.Content.Load<SpriteFont>("PlayerReady"), "x" + keyAmount.ToString(), inventoryHitbox.Center.ToVector2() + new Vector2(20, -15), Color.White);
            }
            
        }
    }
}
