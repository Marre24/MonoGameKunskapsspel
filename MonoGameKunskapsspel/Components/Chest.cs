using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using MessageBox = System.Windows.Forms.MessageBox;

namespace MonoGameKunskapsspel
{
    public class Chest : Component
    {
        private readonly Point size = new Point(64, 80);
        private readonly Rectangle hitBox;
        private string problem;
        private List<string> solutions;
        private bool open = false;
        private readonly Texture2D openTexture;
        private readonly Texture2D closedTexture;

        public Chest(Point locaiton, KunskapsSpel kunskapsSpel) : base(kunskapsSpel)
        {
            hitBox = new Rectangle(locaiton, size);
            (problem, solutions) = kunskapsSpel.problems.GetCurrentProblem();

            openTexture = kunskapsSpel.Content.Load<Texture2D>("Dungeon./OpenChest");
            closedTexture = kunskapsSpel.Content.Load<Texture2D>("Dungeon./ClosedChest");
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

            if (!PlayerCanInteract(kunskapsSpel.player))
                return;

            if (!Keyboard.GetState().IsKeyDown(Keys.Space))
                return;

            MessageBox.Show(problem);
        }

        public bool PlayerCanInteract(Player player)
        {
            return (IsBetweenX(player.hitBox.Right) || IsBetweenX(player.hitBox.Left)) && IsBetweenY(player.hitBox.Top);
        }

        private bool IsBetweenX(int xCord)
        {
            return hitBox.Left <= xCord && hitBox.Right >= xCord;
        }

        private bool IsBetweenY(int yCord)
        {
            return hitBox.Top <= yCord && hitBox.Bottom >= yCord;
        }
    }
}
