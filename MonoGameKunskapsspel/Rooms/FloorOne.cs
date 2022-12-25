using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameKunskapsspel
{
    public class FloorOne : Room
    {
        public FloorOne(int RoomID, KunskapsSpel kunskapsSpel) : base(RoomID, kunskapsSpel) { }

        public override void Initialize()
        {
            //Create Floors
            floorSegments = new List<FloorSegment> { new FloorSegment(new(10, 5), new(0, 0), kunskapsSpel) };

            //Create NPC
            npc = new NPC(new(500, 200, 200, 200), kunskapsSpel, new()
            {
                "Hej det här rummet är det första",
                "Ooga booga"
            }, kunskapsSpel.animations);
        }

        public override void CreateDoors()
        {
            //backDoor = new Door(new(new(1000, -104), new(128, 104)), back, kunskapsSpel);

            //frontDoor = new Door(new(new(1000, -104), new(128, 104)), back, kunskapsSpel);

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (FloorSegment floorSegment in floorSegments)
                floorSegment.Draw(gameTime, spriteBatch);

            //frontDoor.Draw(gameTime, spriteBatch);
            //backDoor.Draw(gameTime, spriteBatch);
            npc.Draw(gameTime, spriteBatch);


            //foreach (Rectangle wall in walls)
            //    kunskapsSpel.spriteBatch.Draw(kunskapsSpel.Content.Load<Texture2D>("WallTiles"), wall, wall, Color.White);

        }

        public override void SetDoorLocations()
        {


        }

        public override void Update(GameTime gameTime)
        {
            npc.Update(gameTime);
        }
    }
}
