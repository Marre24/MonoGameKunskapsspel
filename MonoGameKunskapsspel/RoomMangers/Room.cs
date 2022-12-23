﻿using Microsoft.Xna.Framework;
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
        public List<FloorSegment> floorSegments;
        public List<Rectangle> walls;
        public NPC generalGoofy;
        public int ID;
        public Room backDoorLeedsTo;
        public Room frontDoorLeedsTo;
        public Point frontDoorLocation; 
        public Point backDoorLocation;
        public Door frontDoor;
        public Door backDoor;
        public KunskapsSpel kunskapsSpel;
        public List<Sign> signs = new List<Sign>();
        public Mathias mathias;

        public Room(int ID)
        {
            this.ID = ID;
        }
        public abstract void Initialize(KunskapsSpel kunskapsSpel);
        public abstract void Update(GameTime gameTime, KunskapsSpel kunskapsSpel);
        public abstract void Draw(KunskapsSpel kunskapsSpel, GameTime gameTime);

        public abstract void SetDoorLocations();

        public virtual void SetDestinations(Room backDoorLeedsTo, Room frontDoorLeedsTo)
        {
            this.backDoorLeedsTo = backDoorLeedsTo;
            this.frontDoorLeedsTo = frontDoorLeedsTo;
            CreateDoors();
        }

        public abstract void CreateDoors();
    }
}
