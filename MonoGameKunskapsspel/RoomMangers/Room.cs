using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameKunskapsspel
{
    public abstract class Room
    {
        public List<FloorSegment> floorSegments;
        public int ID;

        public Room(int ID)
        {
            this.ID = ID;
        }
        public abstract void Initialize(KunskapsSpel kunskapsSpel);
        public abstract void Update(GameTime gameTime, KunskapsSpel kunskapsSpel);
        public abstract void Draw(KunskapsSpel kunskapsSpel, GameTime gameTime);

    }
}
