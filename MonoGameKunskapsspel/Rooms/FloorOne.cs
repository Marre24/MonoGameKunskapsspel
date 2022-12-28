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
                new FloorSegment(new(12, 6), new(0, 0), kunskapsSpel, "Grass"),                                         //Main
                new FloorSegment(new(2, 12), new(4 * 96, - 12 * 96), kunskapsSpel, "Grass"),                            //Going North
                new FloorSegment(new(12, 2), new(12 * 96, 3 * 96), kunskapsSpel, "Grass"),                              //Going East
                new FloorSegment(new(2, 8), new(12 * 96 + 10 * 96, 3 * 96 - 8 * 96), kunskapsSpel, "Grass"),            //Going North from East road
                new FloorSegment(new(4, 4), new Point(2112, -480) - new Point(96, 3 * 96), kunskapsSpel, "Grass"),      //Around the Cave Door
            };

            floorSegments[0].tiles[0][4].ChangeToMiddleTexture();
            floorSegments[0].tiles[0][5].ChangeToMiddleTexture();
            floorSegments[1].tiles[11][0].ChangeToEdgeTexture("Left");
            floorSegments[1].tiles[11][1].ChangeToEdgeTexture("Right");

            floorSegments[0].tiles[3][11].ChangeToMiddleTexture();
            floorSegments[0].tiles[4][11].ChangeToMiddleTexture();
            floorSegments[2].tiles[0][0].ChangeToEdgeTexture("Top");
            floorSegments[2].tiles[1][0].ChangeToEdgeTexture("Bottom");

            floorSegments[2].tiles[0][10].ChangeToMiddleTexture();
            floorSegments[2].tiles[0][11].ChangeToEdgeTexture("Right");
            floorSegments[3].tiles[7][0].ChangeToEdgeTexture("Left");
            floorSegments[3].tiles[7][1].ChangeToEdgeTexture("Right");
            floorSegments[3].tiles[0][0].ChangeToMiddleTexture();
            floorSegments[3].tiles[0][1].ChangeToMiddleTexture();

            floorSegments[4].tiles[3][1].ChangeToMiddleTexture();
            floorSegments[4].tiles[3][2].ChangeToMiddleTexture();

            //Create NPC
            npc = new NPC(new(500, 200, 200, 200), kunskapsSpel, new()
            {
                "Hej det här rummet är det första",
                "Ooga booga"
            }, kunskapsSpel.animations);
        }

        public override void CreateDoors()
        {
            backDoor = new Door(new(new(floorSegments[1].hitBox.Left, floorSegments[1].hitBox.Top + 500),
                new(floorSegments[1].hitBox.Width, 60)), back, kunskapsSpel);

            frontDoor = new Door(
                new Rectangle(floorSegments[3].hitBox.Location - new Point(0, floorSegments[3].hitBox.Width),
                new(floorSegments[3].hitBox.Width, floorSegments[3].hitBox.Width)), 
                true, front, kunskapsSpel,
                kunskapsSpel.Content.Load<Texture2D>("Enviroment/CaveEntrance"),
                kunskapsSpel.Content.Load<Texture2D>("Enviroment/CaveEntrance"));
            SetDoorLocations();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (FloorSegment floorSegment in floorSegments)
                floorSegment.Draw(gameTime, spriteBatch);

            //frontDoor.Draw(gameTime, spriteBatch);
            //backDoor.Draw(gameTime, spriteBatch);

            npc.Draw(gameTime, spriteBatch);

        }

        public override void SetDoorLocations()
        {
            backSpawnLocation = new(floorSegments[1].hitBox.Left + 48, floorSegments[1].hitBox.Top + 700);
            frontSpawnLocation = floorSegments[3].hitBox.Location + new Point(48, 0);
        }

        public override void Update(GameTime gameTime)
        {
            npc.Update(gameTime);

            //frontDoor.Update(gameTime);
            //backDoor.Update(gameTime);
        }
    }
}
