using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

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
                new(128, 184)), false, front, kunskapsSpel,
                kunskapsSpel.Content.Load<Texture2D>("Dungeon/ClosedDoor"),
                kunskapsSpel.Content.Load<Texture2D>("Dungeon/OpenDoor"), 4);

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

            foreach (BoxesAndBarrels boxesAndBarrels in boxesAndBarrels)
                boxesAndBarrels.Draw(gameTime, spriteBatch);

            tables[0].Draw(gameTime, spriteBatch);
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
                new Wall(4 * x, new(0, 0), kunskapsSpel),
                new Wall(5 * x, floorSegments[3].hitBox.Location, kunskapsSpel),
                new Wall(2 * x, floorSegments[5].hitBox.Location + new Point(96 * x, 0), kunskapsSpel),
                new Wall(2 * x + x / 2, floorSegments[7].hitBox.Location + new Point(48 * x, 0), kunskapsSpel),
                new Wall(4 * x + x / 2, floorSegments[9].hitBox.Location, kunskapsSpel),
                new Wall(4 * x, floorSegments[11].hitBox.Location + new Point(96 * x, 0), kunskapsSpel),
            };

            //Create SideWalls
            sideWalls = new()
            {
                new SideWall(x, new(0, 0), kunskapsSpel, "LeftWall"),
                new SideWall(4 * x, new(4 * x, 0), kunskapsSpel, "RightWall"),
                new SideWall(x, new(3 * x, x), kunskapsSpel, "LeftWall", false),
                new SideWall(x, new(2 * x, 3 * x), kunskapsSpel, "LeftWall", false),
                new SideWall(3 * x, new(-2 * x, 2 * x), kunskapsSpel, "LeftWall"),
                new SideWall(x, new(-x, 3 * x), kunskapsSpel, "LeftWall", false),
                new SideWall(x / 2, new(x, 4 * x), kunskapsSpel, "RightWall"),
                new SideWall(2 * x + x / 2, new(3 * x + x / 2, 4 * x + x / 2), kunskapsSpel, "RightWall"),
                new SideWall(x, new(3 * x, 5 * x), kunskapsSpel, "LeftWall", false),
                new SideWall(3 * x, new(-x - x / 2, 6 * x), kunskapsSpel, "LeftWall"),
                new SideWall(x, new(-x / 2, 7 * x), kunskapsSpel, "RightWall", false),
                new SideWall(x, new(3 * x + x / 2 , 8 * x), kunskapsSpel, "RightWall"),
            };

            //Create Chests
            chests = new()
            {
                new Chest(floorSegments[0].hitBox.Location + new Point(floorSegments[0].hitBox.Width - 100, -20), kunskapsSpel, 1),
                new Chest(floorSegments[3].hitBox.Location + new Point(64, -20) , kunskapsSpel, 1),
                new Chest(floorSegments[6].hitBox.Location + new Point(floorSegments[6].hitBox.Width - 64, -20), kunskapsSpel, 1),
                new Chest(floorSegments[9].hitBox.Location + new Point(64, -20) , kunskapsSpel, 1),
            };

            tables = new()
            {
                new Table(floorSegments[2].hitBox.Location + new Point(500, 1100), "Horizontal", kunskapsSpel),
            };

            boxesAndBarrels = new()
            {
                new(floorSegments[0].hitBox.Location + new Point(700, 300), true, kunskapsSpel),
                new(floorSegments[0].hitBox.Location + new Point(2000, 100), true, kunskapsSpel),

                new(floorSegments[2].hitBox.Location + new Point(1000, 1000), false, kunskapsSpel),
                new(floorSegments[2].hitBox.Location + new Point(700, 500), false, kunskapsSpel),

                new(floorSegments[4].hitBox.Location + new Point(200, 0), true, kunskapsSpel),
                new(floorSegments[4].hitBox.Location + new Point(450, 200), false, kunskapsSpel),

                new(floorSegments[7].hitBox.Location + new Point(200, 190), false, kunskapsSpel),
                new(floorSegments[7].hitBox.Location + new Point(280, 200), true, kunskapsSpel),

                new(floorSegments[10].hitBox.Location + new Point(500, 100), false, kunskapsSpel),
            };


            foreach (BoxesAndBarrels boxesAndBarrels in boxesAndBarrels)
                components.Add(boxesAndBarrels.hitBox);

            components.Add(tables[0].downChairBox1);
            components.Add(tables[0].downChairBox2);
            components.Add(tables[0].leftChairBox);
            components.Add(tables[0].rightChairBox);
            components.Add(tables[0].upChairBox1);
            components.Add(tables[0].upChairBox2);
            components.Add(tables[0].tableBox);

            foreach (Chest chest in chests)
                components.Add(chest.hitBox);
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
