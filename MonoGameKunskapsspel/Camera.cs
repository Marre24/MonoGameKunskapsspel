using Microsoft.Xna.Framework;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonoGameKunskapsspel
{
    public class Camera
    {
        public Matrix Transform { get; private set; }

        public void Follow(Player target)
        {
            var position = Matrix.CreateTranslation(
                -target.hitBox.Location.X - (target.hitBox.Width / 2),
                -target.hitBox.Location.Y - (target.hitBox.Height / 2), 0);

            var offset = Matrix.CreateTranslation(
                    Screen.PrimaryScreen.Bounds.Width / 2,
                    Screen.PrimaryScreen.Bounds.Height / 2, 0 );

            Transform = position * offset;
        }


    }
}
