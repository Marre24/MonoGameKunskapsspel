using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Drawing;
using Color = Microsoft.Xna.Framework.Color;

namespace MonoGameKunskapsspel
{
    public class KunskapsSpel : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch spriteBatch;
        private Camera camera;
        private Player player;
        private RoomManager roomManager = new RoomManager();

        public static int screenHeight;
        public static int screenWidth;

        public KunskapsSpel()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            screenHeight = _graphics.PreferredBackBufferHeight;
            screenWidth = _graphics.PreferredBackBufferWidth;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new Player(spriteBatch, _graphics, Content);
            camera = new Camera();

            roomManager.Add(new StartScreen(0), this);
            roomManager.Add(new FirstRoom(1), this);

            roomManager.SetActiveRoom(1);
        }

        protected override void Update(GameTime gameTime)
        {

            roomManager.Update(gameTime, this);
            player.Update(gameTime);
            camera.Follow(player);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(transformMatrix: camera.Transform); 

            roomManager.Draw(this);
            player.Draw(gameTime);

            spriteBatch.End();
        }
    }
}