using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameKunskapsspel
{
    public class SecondRoom : Room
    {
        
        public SecondRoom(int ID, KunskapsSpel kunskapsSpel) : base(ID, kunskapsSpel)
        {

        }


        public override void Initialize()
        {
            //Create Floors
            //floorSegments = new List<FloorSegment> { new FloorSegment(new(0, 0, 2000, 700), kunskapsSpel) };

            ////Create NPC
            //generalGoofy = new NPC(new(500, 200, 200, 200), kunskapsSpel, new()
            //{
            //    "Hej det här rummet är det andra",
            //    "Ooga booga :)"
            //});

            ////Create Walls
            //Point size = new(floorSegments[0].hitBox.Width, 300);
            //walls = new List<Rectangle>()
            //{
            //    new(floorSegments[0].hitBox.Location - new Point(0, size.Y), size)
            //};
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (FloorSegment floorSegment in floorSegments)
                floorSegment.Draw(gameTime, spriteBatch);

            backDoor.Draw(gameTime, spriteBatch);

            //foreach (Rectangle wall in walls)
            //    kunskapsSpel.spriteBatch.Draw(kunskapsSpel.Content.Load<Texture2D>("WallTiles"), wall, wall, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            if (!backDoor.PlayerCanInteract(kunskapsSpel.player))
                return;

            if (backDoor.open)
                backDoor.GoThroughDoor(kunskapsSpel.roomManager);

            if (!Keyboard.GetState().IsKeyDown(Keys.Space))
                return;

            if (!backDoor.open)
                backDoor.TryToOpen();
        }

        public override void CreateDoors()
        {
            backDoor = new Door(new(new(1000, -104), new(128, 104)), true, back, kunskapsSpel);
        }

        public override void SetDoorLocations()
        {
            backSpawnLocation = backDoor.hitBox.Location + new Point(0, 40);
        }
    }
}
