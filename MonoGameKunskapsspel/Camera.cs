using Microsoft.Xna.Framework;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameKunskapsspel
{
    public class Camera
    {
        public Matrix Transform { get; private set; }

        public void Follow(Player target)
        {
            var position = Matrix.CreateTranslation(
                -target.position.X - (target.hitBox.Width / 2),
                -target.position.Y - (target.hitBox.Height / 2), 0);

            var offset = Matrix.CreateTranslation(
                    KunskapsSpel.screenWidth / 2,
                    KunskapsSpel.screenHeight / 2, 0 );

            Transform = position * offset;
        }


    }
}
