using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonoGameKunskapsspel
{
    public abstract class Room
    {
        public KunskapsSpel kunskapsSpel;

        public List<FloorSegment> floorSegments = new();
        public List<Rectangle> walls = new();
        public List<Sign> signs = new();

        public NPC npc;
        public Room back;
        public Room front;
        public Door frontDoor;
        public Door backDoor;
        public Mathias mathias;

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
