using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Color = Microsoft.Xna.Framework.Color;
using System.Windows;

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
        public CutScene activeCutscene;
        public Dictionary<string, Animation> animations;
        public Problems problems = new();

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
            roomManager.Add(new StartScreen(0, this));
            roomManager.Add(new FloorOne(1, this));
            roomManager.Add(new TrainingRoom(2, this));
            roomManager.Add(new FloorZero(3, this));
            roomManager.Add(new FloorMinusOne(4, this));
            roomManager.Add(new FloorMinusTwo(5, this));


            //Set destinations for doors in Rooms

            roomManager.rooms[1].CreateDoorsThatLeedsTo(roomManager.rooms[2], roomManager.rooms[3]);
            roomManager.rooms[2].CreateDoorsThatLeedsTo(null, roomManager.rooms[1]);
            roomManager.rooms[3].CreateDoorsThatLeedsTo(roomManager.rooms[1], roomManager.rooms[4]);
            roomManager.rooms[4].CreateDoorsThatLeedsTo(roomManager.rooms[3], roomManager.rooms[5]);
            roomManager.rooms[5].CreateDoorsThatLeedsTo(roomManager.rooms[4], null);

            roomManager.SetActiveRoom(roomManager.rooms[0]);
        }

        protected override void Update(GameTime gameTime)
        {
            if (player.activeState == State.Walking)
            {
                roomManager.Update(gameTime);
                player.Update(gameTime);
                camera.Follow(player.hitBox);
            }

            if (player.activeState == State.WaitingForNextLine || player.activeState == State.ReadingText || player.activeState == State.SolvingProblems)
                activeWindow.Update(gameTime);

            if (player.activeState == State.WatchingCutScene)
            {
                activeCutscene.Update(gameTime);
                //roomManager.GetActiveRoom().mathias.cutScene.Update(gameTime);
            }

            if (player.activeState == State.InStartScreen)
            {
                roomManager.Update(gameTime);
                camera.Follow(roomManager.GetActiveRoom().window);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (roomManager.activeRoomId > 1)
                _graphics.GraphicsDevice.Clear(Color.Black);
            else
                _graphics.GraphicsDevice.Clear(Color.DarkGreen);

            if (roomManager.activeRoomId == 0)
                _graphics.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(transformMatrix: camera.transform, samplerState: SamplerState.LinearWrap);

            if (player.activeState == State.InStartScreen)
            {
                roomManager.Draw(gameTime, spriteBatch);
                spriteBatch.End();
                return;
            }

            roomManager.Draw(gameTime, spriteBatch);
            player.Draw(gameTime, spriteBatch);
            activeWindow?.Draw(gameTime, spriteBatch);

            if (player.activeState == State.WatchingCutScene)
                activeCutscene.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}