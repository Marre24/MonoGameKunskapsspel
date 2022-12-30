using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameKunskapsspel
{
    public class FloorMinusOne : Room
    {
        public FloorMinusOne(int RoomID, KunskapsSpel kunskapsSpel) : base(RoomID, kunskapsSpel)
        {
        }

        public override void CreateDoors()
        {
            backDoor = new Door(
                new Rectangle(floorSegments[0].hitBox.Location - new Point(0, 184), new(128, 184)),
                true, back, kunskapsSpel,
                kunskapsSpel.Content.Load<Texture2D>("Dungeon/ClosedDoor"),
                kunskapsSpel.Content.Load<Texture2D>("Dungeon/OpenDoor"), 0);

            frontDoor = new Door(
                new Rectangle(floorSegments[11].hitBox.Location + new Point(floorSegments[11].hitBox.Width - 128, -184),
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
            foreach (Chest chest in chests)
                chest.Draw(gameTime, spriteBatch);

            frontDoor.Draw(gameTime, spriteBatch);
            backDoor.Draw(gameTime, spriteBatch);
        }

        public override void Initialize()
        {
            const int x = 6;

            //Create Floors
            floorSegments = new List<FloorSegment> {
                new FloorSegment(new(4 * x, x), new(0, 0), kunskapsSpel, "Dungeon"),                                 //1
                new FloorSegment(new(x, x), new(3 * x, x), kunskapsSpel, "Dungeon"),                                 //2
                new FloorSegment(new(2 * x, 2 * x), new(2 * x, 2 * x), kunskapsSpel, "Dungeon"),                     //3        
                new FloorSegment(new(4 * x, x), new(- 2 * x, 2 * x), kunskapsSpel, "Dungeon"),                       //4
                new FloorSegment(new(x, x), new(- 2 * x, 3 * x), kunskapsSpel, "Dungeon"),                           //5
                new FloorSegment(new(2 * x, x), new(- 2 * x, 4 * x), kunskapsSpel, "Dungeon"),                       //6
                new FloorSegment(new(x, x / 2), new(0, 4 * x), kunskapsSpel, "Dungeon"),                             //7
                new FloorSegment(new(3 * x, x / 2), new(x / 2, 4 * x + x / 2), kunskapsSpel, "Dungeon"),             //8
                new FloorSegment(new(x / 2, x), new(3 * x, 5 * x), kunskapsSpel, "Dungeon"),                         //9
                new FloorSegment(new(5 * x, x), new(- x - x / 2, 6 * x), kunskapsSpel, "Dungeon"),                   //10
                new FloorSegment(new(x, x), new(- x - x / 2, 7 * x), kunskapsSpel, "Dungeon"),                       //11
                new FloorSegment(new(5 * x, x), new( - x - x / 2, 8 * x), kunskapsSpel, "Dungeon"),                  //12
            };

            //Create Walls
            walls = new()
            {
                
            };

            //Create SideWalls
            sideWalls = new()
            {
                
            };

            //Create Chests
            chests = new()
            {
                new Chest(floorSegments[1].hitBox.Location + new Point(0,80), kunskapsSpel),
                new Chest(floorSegments[4].hitBox.Location + new Point(floorSegments[4].hitBox.Width - 64, 80) , kunskapsSpel),
                new Chest(floorSegments[7].hitBox.Location + new Point(0, 120), kunskapsSpel),
                new Chest(floorSegments[10].hitBox.Location + new Point(floorSegments[10].hitBox.Width - 64, 80) , kunskapsSpel),
            };


            foreach (FloorSegment floorSegment in floorSegments)
                components.Add(floorSegment);
            foreach (Chest chest in chests)
                components.Add(chest);
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
            foreach (Chest chest in chests)
                chest.Update(gameTime);
        }
    }
}
