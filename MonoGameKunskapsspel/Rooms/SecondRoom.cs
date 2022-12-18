using Microsoft.Xna.Framework;
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
        
        public SecondRoom(int ID) : base(ID)
        {

        }


        public override void Initialize(KunskapsSpel kunskapsSpel)
        {
            this.kunskapsSpel = kunskapsSpel;
            //Create Floors
            floorSegments = new List<FloorSegment> { new(new(0, 0, 2000, 700), kunskapsSpel) };

            //Create NPC
            generalGoofy = new NPC(new(500, 200, 200, 200), kunskapsSpel);

            //Create Walls
            Point size = new(floorSegments[0].hitBox.Width, 300);
            walls = new List<Rectangle>()
            {
                new(floorSegments[0].hitBox.Location - new Point(0, size.Y), size)
            };
        }

        public override void Draw(KunskapsSpel kunskapsSpel, GameTime gameTime)
        {
            foreach (FloorSegment floorSegment in floorSegments)
                floorSegment.Draw(gameTime, kunskapsSpel.spriteBatch);

            backDoor.Draw(gameTime, kunskapsSpel.spriteBatch);

            //foreach (Rectangle wall in walls)
            //    kunskapsSpel.spriteBatch.Draw(kunskapsSpel.Content.Load<Texture2D>("WallTiles"), wall, wall, Color.White);
        }

        public override void Update(GameTime gameTime, KunskapsSpel kunskapsSpel)
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
            backDoor = new Door(new(new(1000, -104), new(128, 104)), true, backDoorLeedsTo, kunskapsSpel);
        }

        public override void SetDoorLocations()
        {
            backDoorLocation = backDoor.hitBox.Location + new Point(0, 40);
        }
    }
}
