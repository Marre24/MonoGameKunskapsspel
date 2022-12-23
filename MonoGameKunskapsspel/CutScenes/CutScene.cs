using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameKunskapsspel
{
    public abstract class CutScene
    {
        public Vector2 hiddenFollowPoint;
        public Player player;
        public readonly KunskapsSpel kunskapsSpel;
        public int phaseCounter = 1;

        public CutScene(Player player, KunskapsSpel kunskapsSpel, Room room, List<string> dialogue)
        {
            this.player = player;
            this.kunskapsSpel = kunskapsSpel;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void StartScene();
        public abstract void EndScene();

    }
}
