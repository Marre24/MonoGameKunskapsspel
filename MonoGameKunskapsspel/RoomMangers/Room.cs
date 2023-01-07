using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MonoGameKunskapsspel
{
    public abstract class Room
    {
        public KunskapsSpel kunskapsSpel;
        public Rectangle window;

        public List<Rectangle> components = new();
        public List<FloorSegment> floorSegments = new();
        public List<Wall> walls = new();
        public List<SideWall> sideWalls = new();
        public List<Chest> chests = new();
        public List<Enemy> enemies;
        public List<Table> tables;
        public List<BoxesAndBarrels> boxesAndBarrels = new();

        public NPC npc;
        public Room back;
        public Room front;
        public Door frontDoor;
        public Door backDoor;

        public Point frontSpawnLocation;
        public Point backSpawnLocation;
        public int RoomID;


        public Room(int RoomID, KunskapsSpel kunskapsSpel)
        {
            this.RoomID = RoomID;
            this.kunskapsSpel = kunskapsSpel;
        }
        public abstract void Initialize();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void SetDoorLocations();

        public virtual void CreateDoorsThatLeedsTo(Room back, Room front)
        {
            this.back = back;
            this.front = front;
            CreateDoors();
        }

        public abstract void CreateDoors();
    }
}
