using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameKunskapsspel
{
    public class TrainingRoom : Room
    {
        public TrainingRoom(int RoomID, KunskapsSpel kunskapsSpel) : base(RoomID, kunskapsSpel) { }

        public override void CreateDoors()
        {
            frontDoor = new Door(new(new(floorSegments[1].hitBox.Left, floorSegments[1].hitBox.Top + 650), 
                new(floorSegments[1].hitBox.Width, 60)), front, kunskapsSpel);
            SetDoorLocations();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (FloorSegment floorSegment in floorSegments)
                floorSegment.Draw(gameTime, spriteBatch);

            frontDoor.Draw(gameTime, spriteBatch);

            npc.Draw(gameTime, spriteBatch);
        }

        public override void Initialize()
        {
            //Create Floors
            floorSegments = new List<FloorSegment> {
                new FloorSegment(new(8, 8), new(0, 0), kunskapsSpel, "Grass"),                                         //Main
                new FloorSegment(new(2, 14), new(3, 8), kunskapsSpel, "Grass"),                            //Going North
            };

            floorSegments[0].tiles[7][3].ChangeToMiddleTexture();
            floorSegments[0].tiles[7][4].ChangeToMiddleTexture();
            floorSegments[1].tiles[0][0].ChangeToEdgeTexture("Left");
            floorSegments[1].tiles[0][1].ChangeToEdgeTexture("Right");

            //Create NPC
            npc = new NPC(new(500, 200, 200, 200), kunskapsSpel, new()
            {
                "Hej det här rummet är det första",
                "Ooga booga"
            }, kunskapsSpel.animations);
        }

        public override void SetDoorLocations()
        {
            frontSpawnLocation = frontDoor.hitBox.Location + new Point(48, - 100);
        }

        public override void Update(GameTime gameTime)
        {
            npc.Update(gameTime);

            frontDoor.Update(gameTime);
        }
    }
}
