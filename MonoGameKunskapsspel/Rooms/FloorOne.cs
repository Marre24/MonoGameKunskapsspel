﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

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
                new FloorSegment(new(2, 14), new(4, - 14), kunskapsSpel, "Grass"),                            //Going North
                new FloorSegment(new(12, 2), new(12, 3), kunskapsSpel, "Grass"),                              //Going East
                new FloorSegment(new(2, 8), new(22, - 5), kunskapsSpel, "Grass"),            //Going North from East road
                new FloorSegment(new(4, 4), new Point(21, - 8), kunskapsSpel, "Grass"),      //Around the Cave Door
            };

            floorSegments[0].tiles[0][4].ChangeToMiddleTexture();
            floorSegments[0].tiles[0][5].ChangeToMiddleTexture();
            floorSegments[1].tiles[13][0].ChangeToEdgeTexture("Left");
            floorSegments[1].tiles[13][1].ChangeToEdgeTexture("Right");

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
            npc = new NPC(new(floorSegments[0].hitBox.Right - 100, 200), kunskapsSpel, new()
            {
                "Super!",
                "Du ser stark, smart och tapper ut, det är inte så att du kan hjälpa mig?",
                "Tack så mycket, vill du ha någon betalning för detta?",
                "Är du säker på att du inte vill ha någon betalning?",
                "Om du säger det så...",
                "Följ den vänstra stigen som går mot norr så möter jag dig där och förklarar mer",
            }, kunskapsSpel.animations);



            foreach (Chest chest in chests)
                components.Add(chest.hitBox);
            components.Add(npc.hitBox);
        }

        public override void CreateDoors()
        {
            backDoor = new Door(new(new(floorSegments[1].hitBox.Left, floorSegments[1].hitBox.Top + 550),
                new(floorSegments[1].hitBox.Width, 60)), back, kunskapsSpel);

            frontDoor = new Door(
                new Rectangle(floorSegments[3].hitBox.Location - new Point(0, floorSegments[3].hitBox.Width),
                new(floorSegments[3].hitBox.Width, floorSegments[3].hitBox.Width)), 
                true, front, kunskapsSpel,
                kunskapsSpel.Content.Load<Texture2D>("Enviroment/CaveEntrance"),
                kunskapsSpel.Content.Load<Texture2D>("Enviroment/CaveEntrance"), 0);
            SetDoorLocations();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (FloorSegment floorSegment in floorSegments)
                floorSegment.Draw(gameTime, spriteBatch);

            frontDoor.Draw(gameTime, spriteBatch);
            backDoor.Draw(gameTime, spriteBatch);

            npc.Draw(gameTime, spriteBatch);

            foreach (Chest chest in chests)
                chest.Draw(gameTime, spriteBatch);
        }

        public override void SetDoorLocations()
        {
            backSpawnLocation = new(floorSegments[1].hitBox.Left + 48, floorSegments[1].hitBox.Top + 650);
            frontSpawnLocation = frontDoor.hitBox.Location + new Point(30, 200);
        }

        public override void Update(GameTime gameTime)
        {
            kunskapsSpel.IsMouseVisible = false;
            npc.Update(gameTime);
            frontDoor.Update(gameTime);
            backDoor.Update(gameTime);

            foreach (Chest chest in chests)
                chest.Update(gameTime);
        }
    }
}
