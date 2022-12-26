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
        public Dictionary<string, Animation> animations;

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
            animations = new Dictionary<string, Animation>()
            {
                {"WalkLeft", new Animation(Content.Load<Texture2D>("Player/RunLeft"), 6) },
                {"WalkRight", new Animation(Content.Load<Texture2D>("Player/RunRight"), 6) },
                {"Idle", new Animation(Content.Load<Texture2D>("Player/Idle"), 4) },
                {"NpcIdle", new Animation(Content.Load<Texture2D>("Npc/Idle-Sheet"), 4) },
            };

            spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new Player(this, animations);
            camera = new Camera(player.hitBox);

            //Add rooms in list
            roomManager.Add(new FloorOne(0, this));

            roomManager.Add(new FirstRoom(1, this));
            roomManager.Add(new SecondRoom(2, this));

            //Set destinations for doors in Rooms
            roomManager.rooms[0].CreateDoorsThatLeedsTo(roomManager.rooms[1], roomManager.rooms[2]);
            //roomManager.rooms[1].CreateDoorsThatLeedsTo(null, roomManager.rooms[1]);
            //roomManager.rooms[2].CreateDoorsThatLeedsTo(roomManager.rooms[0], null);

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
            _graphics.GraphicsDevice.Clear(Color.SkyBlue);
            spriteBatch.Begin(transformMatrix: camera.transform, samplerState: SamplerState.LinearWrap);

            roomManager.Draw(gameTime, spriteBatch);

            player.Draw(gameTime, spriteBatch);

            activeWindow?.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}