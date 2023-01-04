using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MonoGameKunskapsspel
{
    public class Chest : Component
    {
        private readonly Point size = new(64, 80);
        private readonly string problem;
        private readonly int rightAnswer;
        private readonly List<string> solutions;
        public bool open = false;
        private readonly Texture2D openTexture;
        private readonly Texture2D closedTexture;
        private readonly int amountOfKeysInChest;
        public bool canBeInteractedWith;

        public Chest(Point location, KunskapsSpel kunskapsSpel, int amountOfKeysInChest) : base(kunskapsSpel)
        {
            hitBox = new Rectangle(location, size);
            interactHitBox = new Rectangle(location - new Point(20,20), size + new Point(40,40));
            (problem, rightAnswer, solutions) = kunskapsSpel.problems.GetCurrentProblem();

            openTexture = kunskapsSpel.Content.Load<Texture2D>("Dungeon./OpenChest");
            closedTexture = kunskapsSpel.Content.Load<Texture2D>("Dungeon./ClosedChest");
            haveColisison = true;
            this.amountOfKeysInChest = amountOfKeysInChest;
            canBeInteractedWith = true;
        }

        public Chest(Point location, KunskapsSpel kunskapsSpel) : base(kunskapsSpel)
        {
            hitBox = new Rectangle(location, size);
            interactHitBox = new Rectangle(location - new Point(20, 20), size + new Point(40, 40));
            (problem, rightAnswer, solutions) = kunskapsSpel.problems.GetCurrentProblem();

            openTexture = kunskapsSpel.Content.Load<Texture2D>("Dungeon./OpenChest");
            closedTexture = kunskapsSpel.Content.Load<Texture2D>("Dungeon./ClosedChest");
            haveColisison = true;
            amountOfKeysInChest = 0;
            canBeInteractedWith = false;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (open)
            {
                spriteBatch.Draw(openTexture, hitBox, Color.White);
                return;
            }
            spriteBatch.Draw(closedTexture, hitBox, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            if (open)
                return;

            if (!canBeInteractedWith)
                return;

            if (!PlayerCanInteract(kunskapsSpel.player))
                return;

            if (!Keyboard.GetState().IsKeyDown(Keys.Space))
                return;

            kunskapsSpel.player.velocity = Point.Zero;  
            _ = new UnlockChestWindow(rightAnswer, problem, solutions, kunskapsSpel, kunskapsSpel.camera, kunskapsSpel.player, this, kunskapsSpel.player.activeState);
        }

        public bool PlayerCanInteract(Player player)
        {
            return (IsBetweenX(player.hitBox.Right) || IsBetweenX(player.hitBox.Left)) && (IsBetweenY(player.hitBox.Top) || IsBetweenY(player.hitBox.Bottom));
        }

        private bool IsBetweenX(int xCord)
        {
            return interactHitBox.Left <= xCord && interactHitBox.Right >= xCord;
        }

        private bool IsBetweenY(int yCord)
        {
            return interactHitBox.Top <= yCord && interactHitBox.Bottom >= yCord;
        }

        private readonly Point diff = new(0, 24);

        public void Open()
        {
            open = true;
            kunskapsSpel.player.keyAmount += amountOfKeysInChest;
            hitBox = new Rectangle(hitBox.Location - diff, size + diff);
        }

    }
}
