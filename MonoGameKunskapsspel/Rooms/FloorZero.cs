using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameKunskapsspel
{
    public class FloorZero : Room
    {
        public FloorZero(int RoomID, KunskapsSpel kunskapsSpel) : base(RoomID, kunskapsSpel)
        {
        }

        public override void CreateDoors()
        {
            backDoor = new Door(
                new Rectangle(floorSegments[0].hitBox.Location + new Point(floorSegments[0].hitBox.Width / 2 - 64, - 184),
                new(128, 184)), 
                true, back, kunskapsSpel,
                kunskapsSpel.Content.Load<Texture2D>("Dungeon/ClosedDoor"),
                kunskapsSpel.Content.Load<Texture2D>("Dungeon/OpenDoor"), 5);

            frontDoor = new Door(
                new Rectangle(floorSegments[6].hitBox.Location + new Point(floorSegments[6].hitBox.Width / 2 - 64, - 184),
                new(128, 184)),
                false, front, kunskapsSpel,
                kunskapsSpel.Content.Load<Texture2D>("Dungeon/ClosedDoor"),
                kunskapsSpel.Content.Load<Texture2D>("Dungeon/OpenDoor"), 5);

            SetDoorLocations();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (FloorSegment floorSegment in floorSegments)
                floorSegment.Draw(gameTime, spriteBatch);

            foreach (Wall wall in walls)
                wall.Draw(gameTime, spriteBatch);

            foreach (SideWall sideWall in sideWalls)
                sideWall.Draw(gameTime, spriteBatch);

            frontDoor.Draw(gameTime, spriteBatch);
            backDoor.Draw(gameTime, spriteBatch);
        }

        public override void Initialize()
        {
            const int x = 6;

            //Create Floors
            floorSegments = new List<FloorSegment> {
                new FloorSegment(new(x, x * 6), new(0, 0), kunskapsSpel, "Dungeon"),                                       //1
                new FloorSegment(new(3 * x, 2), new(x, 2 * x), kunskapsSpel, "Dungeon"),                                   //2
                new FloorSegment(new(2, x), new(2 * x, x), kunskapsSpel, "Dungeon"),                                       //3        
                new FloorSegment(new(2 * x, x / 2), new(x + x / 2, x / 2), kunskapsSpel, "Dungeon"),                       //4
                new FloorSegment(new(2, x), new(2 * x, 2 * x + 2), kunskapsSpel, "Dungeon"),                               //5
                new FloorSegment(new(2 * x, x / 2), new(x + x / 2, 3 * x + 2), kunskapsSpel, "Dungeon"),                   //6
                new FloorSegment(new(x, x * 5), new(4 * x, 0), kunskapsSpel, "Dungeon"),                                   //7
                new FloorSegment(new(2 * x + x / 2, x / 2), new(x, 6 * x - x / 2), kunskapsSpel, "Dungeon"),               //8
                new FloorSegment(new(x / 2, x / 2), new(3 * x, 5 * x), kunskapsSpel, "Dungeon"),                           //9
                new FloorSegment(new(2 * x, x / 2), new(x + x / 2, 5 * x - x / 2), kunskapsSpel, "Dungeon"),               //10
            };

            //Create Walls
            walls = new()
            {
                new Wall(x, new(0,0), kunskapsSpel),
                new Wall(2 * x, floorSegments[3].hitBox.Location, kunskapsSpel),
                new Wall(x, floorSegments[1].hitBox.Location, kunskapsSpel),
                new Wall((2 * x) - 2, floorSegments[1].hitBox.Location + new Point((x + 2) * 96, 0), kunskapsSpel),
                new Wall(x / 2, floorSegments[5].hitBox.Location, kunskapsSpel),
                new Wall(2 * x - (x / 2) - 2, floorSegments[5].hitBox.Location + new Point(((x / 2) + 2) * 96, 0), kunskapsSpel),
                new Wall(x, floorSegments[6].hitBox.Location, kunskapsSpel),
                new Wall(2 * x, floorSegments[7].hitBox.Location, kunskapsSpel),
                new Wall(2 * x, floorSegments[9].hitBox.Location, kunskapsSpel),
            };

            sideWalls = new()
            {
                new SideWall(6 * x, new(0, 0), kunskapsSpel, "LeftWall"),
                new SideWall(2 * x, new(x, 0), kunskapsSpel, "RightWall"),
                new SideWall(4 * x - (x / 2) - 2, new(x, 2 * x + 2), kunskapsSpel, "RightWall", false),
                new SideWall(x / 2, new (x + x / 2, x / 2), kunskapsSpel, "LeftWall"),
                new SideWall(x / 2, new (3 * x + x / 2, x / 2), kunskapsSpel, "RightWall"),
                new SideWall(x, new (2 * x, x), kunskapsSpel, "LeftWall", false),
                new SideWall(x, new (2 * x + 2, x), kunskapsSpel, "RightWall", false),
                new SideWall(x, new (2 * x, 2 * x + 2), kunskapsSpel, "LeftWall", false),
                new SideWall(x, new (2 * x + 2, 2 * x + 2), kunskapsSpel, "RightWall", false),
                new SideWall(x / 2, new (x + x / 2, 3 * x + 2), kunskapsSpel, "LeftWall"),
                new SideWall(x / 2, new (3 * x + x / 2, 3 * x + 2), kunskapsSpel, "RightWall"),
                new SideWall(3 * x - 2, new(4 * x,  2 * x + 2), kunskapsSpel, "LeftWall", false),
                new SideWall(2 * x, new(4 * x, 0), kunskapsSpel, "LeftWall"),
                new SideWall(5 * x, new(5 * x, 0), kunskapsSpel, "RightWall"),
                new SideWall(x / 2, new (3 * x, 5 * x), kunskapsSpel, "LeftWall", false),
                new SideWall(x / 2, new(x + x / 2, 5 * x - x / 2), kunskapsSpel, "LeftWall"),
                new SideWall(x + x / 2, new(3 * x + x / 2, 5 * x - x / 2), kunskapsSpel, "RightWall"),
            };


        }

        public override void SetDoorLocations()
        {
            backSpawnLocation = backDoor.hitBox.Location + new Point(0, 200);
            frontSpawnLocation = frontDoor.hitBox.Location + new Point(0, 200);
        }

        public override void Update(GameTime gameTime)
        {
            frontDoor.Update(gameTime);
            backDoor.Update(gameTime);
        }
    }
}
