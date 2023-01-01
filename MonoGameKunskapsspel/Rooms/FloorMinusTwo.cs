using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameKunskapsspel
{
    public class FloorMinusTwo : Room
    {
        public FloorMinusTwo(int RoomID, KunskapsSpel kunskapsSpel) : base(RoomID, kunskapsSpel)
        {
        }

        public override void CreateDoors()
        {
            backDoor = new Door(new(floorSegments[14].hitBox.Left, floorSegments[14].hitBox.Bottom - kunskapsSpel.player.hitBox.Height - 20,floorSegments[14].hitBox.Width, kunskapsSpel.player.hitBox.Height),
                back, kunskapsSpel);

            frontDoor = new Door(new(floorSegments[0].hitBox.Location + new Point(0, -184), new(2 * 96, 184)), false, front, kunskapsSpel, kunskapsSpel.Content.Load<Texture2D>("Dungeon/ClosedDoor"),
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


            frontDoor.Draw(gameTime, spriteBatch);
            backDoor.Draw(gameTime, spriteBatch);
        }

        public override void Initialize()
        {
            const int x = 6;

            //Create Floors
            floorSegments = new List<FloorSegment> {
                new FloorSegment(new(2, 2 * x), new(2 * x, 4 * x), kunskapsSpel, "Dungeon"),                      //1
                new FloorSegment(new(x, 2), new(2 * x + 2, 6 * x - 2), kunskapsSpel, "Dungeon"),                  //2        
                new FloorSegment(new(2, 2 * x + 1), new(3 * x, 6 * x), kunskapsSpel, "Dungeon"),                  //3
                new FloorSegment(new(2 * x, 2), new(x, 7 * x + 2), kunskapsSpel, "Dungeon"),                      //4
                new FloorSegment(new(2, 3 * x), new(x, 7 * x + 4), kunskapsSpel, "Dungeon"),                      //5
                new FloorSegment(new(2 * x, 2), new(x + 2, 10 * x + 2), kunskapsSpel, "Dungeon"),                 //6
                new FloorSegment(new(2, 2 * x), new(3 * x + 2, 10 * x + 2), kunskapsSpel, "Dungeon"),             //7
                new FloorSegment(new(x, 2), new(2 * x + 2, 12 * x), kunskapsSpel, "Dungeon"),                     //8
                new FloorSegment(new(2, x), new(2 * x + 2, 12 * x + 2), kunskapsSpel, "Dungeon"),                 //9
                new FloorSegment(new(2 * x, 2), new(2 * x + 4, 13 * x), kunskapsSpel, "Dungeon"),                 //10
                new FloorSegment(new(2, x + 4), new(4 * x + 2, 13 * x + 2), kunskapsSpel, "Dungeon"),             //11
                new FloorSegment(new(3 * x, 2), new(x + 4, 15 * x), kunskapsSpel, "Dungeon"),                     //12
                new FloorSegment(new(2, 2 * x), new(x + 2, 15 * x), kunskapsSpel, "Dungeon"),                     //13
                new FloorSegment(new(x + 3, 2), new(x + 4, 17 * x - 2), kunskapsSpel, "Dungeon"),                 //14
                new FloorSegment(new(2, 3 * x), new(3 * x + 1, 17 * x - 2), kunskapsSpel, "Dungeon"),             //15
            };

            //Create Walls
            walls = new()
            {

            };

            sideWalls = new()
            {

            };

            chests = new()
            {
                new Chest(floorSegments[12].hitBox.Location - new Point(0,40), kunskapsSpel, 1),
                new Chest(floorSegments[9].hitBox.Location + new Point(floorSegments[9].hitBox.Width - 64, -40), kunskapsSpel, 1),
                new Chest(floorSegments[6].hitBox.Location - new Point(-64,40), kunskapsSpel, 1),
            };

            foreach (FloorSegment floorSegment in floorSegments)
                components.Add(floorSegment);
            foreach (Chest chest in chests)
                components.Add(chest);
        }

        public override void SetDoorLocations()
        {
            backSpawnLocation = backDoor.hitBox.Location - new Point(0, 200);
            frontSpawnLocation = frontDoor.hitBox.Location + new Point(0, 300);
        }

        public override void Update(GameTime gameTime)
        {
            backDoor.Update(gameTime);
            frontDoor.Update(gameTime);
            foreach (Chest chest in chests)
                chest.Update(gameTime);
        }
    }
}
