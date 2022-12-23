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
        private readonly GraphicsDeviceManager _graphics;
        public SpriteBatch spriteBatch;
        public Camera camera;
        public Player player;
        public RoomManager roomManager = new();
        public Window activeWindow;


        public KunskapsSpel()
        {
            _graphics = new(this);

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
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new Player(this);
            camera = new Camera(player.hitBox);

            //Add rooms in list
            roomManager.Add(new FirstRoom(0, this));
            roomManager.Add(new SecondRoom(1, this));

            //Set destinations for doors in Rooms
            roomManager.rooms[0].CreateDoorsThatLeedsTo(null, roomManager.rooms[1]);
            roomManager.rooms[1].CreateDoorsThatLeedsTo(roomManager.rooms[0], null);

            roomManager.SetActiveRoom(0);
        }

        protected override void Update(GameTime gameTime)
        {
            if (player.activeState == State.Walking)
            {
                roomManager.Update(gameTime);
                player.Update(gameTime);
                camera.Follow(player.hitBox);
            }

            if (player.activeState == State.WaitingForNextLine || player.activeState == State.ReadingText)
                activeWindow.Update(gameTime);

            if (player.activeState == State.WatchingCutScene)
            {
                camera.Follow(roomManager.GetActiveRoom().mathias.cutScene.hiddenFollowPoint);
                roomManager.GetActiveRoom().mathias.cutScene.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(transformMatrix: camera.transform, samplerState: SamplerState.LinearWrap);

            roomManager.Draw(gameTime, spriteBatch);

            player.Draw(gameTime, spriteBatch);

            activeWindow?.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}