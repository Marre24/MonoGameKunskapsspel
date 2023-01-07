using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using System.Collections.Generic;

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
                new Rectangle(floorSegments[0].hitBox.Location + new Point(floorSegments[0].hitBox.Width / 2 - 64, -184),
                new(128, 184)),
                true, back, kunskapsSpel,
                kunskapsSpel.Content.Load<Texture2D>("Dungeon/ClosedDoor"),
                kunskapsSpel.Content.Load<Texture2D>("Dungeon/OpenDoor"), 5);

            frontDoor = new Door(
                new Rectangle(floorSegments[6].hitBox.Location + new Point(floorSegments[6].hitBox.Width / 2 - 64, -184),
                new(128, 184)),
                false, front, kunskapsSpel,
                kunskapsSpel.Content.Load<Texture2D>("Dungeon/ClosedDoor"),
                kunskapsSpel.Content.Load<Texture2D>("Dungeon/OpenDoor"), 3);

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

            foreach (Table table in tables)
                table.Draw(gameTime, spriteBatch);

            foreach (BoxesAndBarrels boxesAndBarrels in boxesAndBarrels)
                boxesAndBarrels.Draw(gameTime, spriteBatch);

            frontDoor.Draw(gameTime, spriteBatch);
            backDoor.Draw(gameTime, spriteBatch);
        }

        public override void Initialize()
        {
            const int x = 8;

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

            //Create Chests
            chests = new()
            {
                new Chest(floorSegments[9].hitBox.Location + new Point(100, -20), kunskapsSpel, 1),
                new Chest(floorSegments[5].hitBox.Location + new Point(floorSegments[5].hitBox.Width - 100, -20), kunskapsSpel, 1),
                new Chest(floorSegments[3].hitBox.Location + new Point(100, -20), kunskapsSpel, 1),
            };

            //Create Tables
            tables = new()
            {
                new Table(floorSegments[3].hitBox.Location + new Point(floorSegments[3].hitBox.Width - 500, floorSegments[3].hitBox.Bottom - 400), "Horizontal", kunskapsSpel),
                new Table(floorSegments[5].hitBox.Location + new Point(0, floorSegments[5].hitBox.Height), "Horizontal", kunskapsSpel),
                new Table(floorSegments[6].hitBox.Location + new Point(200, floorSegments[6].hitBox.Height ), "Horizontal", kunskapsSpel),
                //new Table(floorSegments[0].hitBox.Location + new Point(0, 1536), "Vertical", kunskapsSpel),
            };

            boxesAndBarrels = new()
            {
                new(floorSegments[0].hitBox.Location + new Point(700, 300), true, kunskapsSpel),
                new(floorSegments[0].hitBox.Location + new Point(50, 800), true, kunskapsSpel),
                new(floorSegments[0].hitBox.Location + new Point(400, 1500), false, kunskapsSpel),
                new(floorSegments[0].hitBox.Location + new Point(500, 2800), false, kunskapsSpel),
                new(floorSegments[0].hitBox.Location + new Point(200, 3000), true, kunskapsSpel),
                new(floorSegments[0].hitBox.Location + new Point(0, 4200), false, kunskapsSpel),

                new(floorSegments[6].hitBox.Location + new Point(600, 400), false, kunskapsSpel),
                new(floorSegments[6].hitBox.Location + new Point(200, 900), true, kunskapsSpel),
                new(floorSegments[6].hitBox.Location + new Point(300, 1500), false, kunskapsSpel),
                new(floorSegments[6].hitBox.Location + new Point(500, 3000), true, kunskapsSpel),
            };

            foreach (BoxesAndBarrels boxesAndBarrels in boxesAndBarrels)
                components.Add(boxesAndBarrels.hitBox);


            foreach (Table table in tables)

                if (table.typeOfTable == "Horizontal")
                {
                    components.Add(table.downChairBox1);
                    components.Add(table.downChairBox2);
                    components.Add(table.leftChairBox);
                    components.Add(table.rightChairBox);
                    components.Add(table.upChairBox1);
                    components.Add(table.upChairBox2);
                    components.Add(table.tableBox);
                }
                else
                {
                    components.Add(table.downChairBox1);
                    components.Add(table.leftChairBox);
                    components.Add(table.rightChairBox);
                    components.Add(table.upChairBox1);
                    components.Add(table.tableBox);
                }

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
