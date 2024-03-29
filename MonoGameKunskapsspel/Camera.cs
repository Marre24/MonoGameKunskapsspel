﻿using Microsoft.Xna.Framework;
using System.Windows.Forms;

namespace MonoGameKunskapsspel
{
    public class Camera
    {
        public Matrix transform;

        private readonly Point screenSize = new(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        public Rectangle window;

        public Camera(Rectangle target)
        {
            UpdateWindow(target);
        }

        public void Follow(Rectangle target)
        {
            UpdateWindow(target);
            var position = Matrix.CreateTranslation(
                -target.Location.X - (target.Width / 2),
                -target.Location.Y - (target.Height / 2), 0);

            var offset = Matrix.CreateTranslation(
                    Screen.PrimaryScreen.Bounds.Width / 2,
                    Screen.PrimaryScreen.Bounds.Height / 2, 0);

            transform = offset * position;
        }

        public void Follow(Vector2 target)
        {
            UpdateWindow(target);
            var position = Matrix.CreateTranslation(
                -target.X,
                -target.Y, 0);

            var offset = Matrix.CreateTranslation(
                    Screen.PrimaryScreen.Bounds.Width / 2,
                    Screen.PrimaryScreen.Bounds.Height / 2, 0);

            transform = offset * position;
        }

        private void UpdateWindow(Vector2 target)
        {
            window = new((int)target.X - Screen.PrimaryScreen.Bounds.Width / 2, (int)target.Y - Screen.PrimaryScreen.Bounds.Height / 2, screenSize.X, screenSize.Y);
        }

        private void UpdateWindow(Rectangle target)
        {
            window = new Rectangle(
                target.Location.X + (target.Width / 2) - Screen.PrimaryScreen.Bounds.Width / 2,
                target.Location.Y + (target.Height / 2) - Screen.PrimaryScreen.Bounds.Height / 2, 
                screenSize.X , screenSize.Y);
        }
    }
}
