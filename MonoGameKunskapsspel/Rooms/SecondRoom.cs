using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameKunskapsspel
{
    public class SecondRoom : Room
    {
        
        public SecondRoom(int ID) : base(ID)
        {

        }


        public override void Initialize(KunskapsSpel kunskapsSpel)
        {
            floorSegments = new List<FloorSegment> { new(new(0, 0, 2000, 700), kunskapsSpel) };
            Point size = new(floorSegments[0].hitBox.Width, 300);
            walls = new List<Rectangle>()
            {
                new(floorSegments[0].hitBox.Location - new Point(0, size.Y), size)
            };
        }

        public override void Draw(KunskapsSpel kunskapsSpel, GameTime gameTime)
        {
            foreach (FloorSegment floorSegment in floorSegments)
                floorSegment.Draw(gameTime, kunskapsSpel.spriteBatch);

            //foreach (Rectangle wall in walls)
            //    kunskapsSpel.spriteBatch.Draw(kunskapsSpel.Content.Load<Texture2D>("WallTiles"), wall, wall, Color.White);
        }

        public override void Update(GameTime gameTime, KunskapsSpel kunskapsSpel)
        {


        }

        public override void CreateDoors()
        {
        }
    }
}
