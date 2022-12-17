using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Color = Microsoft.Xna.Framework.Color;

namespace MonoGameKunskapsspel
{
    public class KunskapsSpel : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch spriteBatch;
        private Camera camera;
        public Player player;
        public RoomManager roomManager = new RoomManager();
        private List<Component> components = new List<Component>();

        public static int screenHeight;
        public static int screenWidth;

        public KunskapsSpel()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

            _graphics.PreferredBackBufferWidth = Screen.PrimaryScreen.Bounds.Width;
            _graphics.PreferredBackBufferHeight = Screen.PrimaryScreen.Bounds.Height;
            _graphics.SynchronizeWithVerticalRetrace = true;
            IsMouseVisible = true;
            _graphics.ApplyChanges();

            screenHeight = _graphics.PreferredBackBufferHeight;
            screenWidth = _graphics.PreferredBackBufferWidth;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new Player(this);
            camera = new Camera();

            components.Add(player);

            //Add rooms in list
            roomManager.Add(new FirstRoom(0), this);
            roomManager.Add(new SecondRoom(1), this);

            //Set destinations for these Rooms
            roomManager.rooms[0].SetDestinations(null, roomManager.rooms[1]);
            roomManager.rooms[1].SetDestinations(roomManager.rooms[0], null);


            roomManager.SetActiveRoom(0);
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
            _graphics.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(transformMatrix: camera.Transform, samplerState: SamplerState.LinearWrap);

            foreach (Component component in components)
                component.Draw(gameTime, spriteBatch);

            roomManager.Draw(this, gameTime);

            spriteBatch.End();
        }
    }
}