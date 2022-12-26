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
            floorSegments = new List<FloorSegment> { 
                new FloorSegment(new(12, 6), new(0, 0), kunskapsSpel),
                new FloorSegment(new(2, 8), new(4 * 96, - 8 * 96), kunskapsSpel),
                new FloorSegment(new(8, 2), new(12 * 96, 3 * 96), kunskapsSpel), 
            };

            floorSegments[0].tiles[0][4].ChangeToMiddleTexture();
            floorSegments[0].tiles[0][5].ChangeToMiddleTexture();
            floorSegments[1].tiles[7][0].ChangeToEdgeTexture("Left");
            floorSegments[1].tiles[7][1].ChangeToEdgeTexture("Right");

            floorSegments[0].tiles[3][11].ChangeToMiddleTexture();
            floorSegments[0].tiles[4][11].ChangeToMiddleTexture();
            floorSegments[2].tiles[0][0].ChangeToEdgeTexture("Top");
            floorSegments[2].tiles[1][0].ChangeToEdgeTexture("Bottom");
            

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
