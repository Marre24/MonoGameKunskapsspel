using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameKunskapsspel
{
    public class FirstRoom : Room
    {

        public FirstRoom(int ID) : base(ID) {   }

        public override void Initialize(KunskapsSpel kunskapsSpel)
        {
            floorSegments = new List<FloorSegment> { new(new(0, 0, 4000, 1200), kunskapsSpel)};
        }

        public override void Draw(KunskapsSpel kunskapsSpel, GameTime gameTime)
        {
            foreach (FloorSegment floorSegment in floorSegments)
            {
                floorSegment.Draw(gameTime, kunskapsSpel.spriteBatch);
            }
        }


        public override void Update(GameTime gameTime, KunskapsSpel kunskapsSpel)
        {
        }
    }
}
