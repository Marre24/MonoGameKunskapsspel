using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace MonoGameKunskapsspel
{
    public class FirstRoom : Room
    {
        public FirstRoom(int ID) : base(ID) {   }


        public override void Initialize(KunskapsSpel kunskapsSpel)
        {
            this.kunskapsSpel = kunskapsSpel;
            //Create Floors
            floorSegments = new List<FloorSegment> { new(new(0, 0, 2000, 700), kunskapsSpel)};

            //Create NPC
            generalGoofy = new NPC(new(500, 200, 200, 200), kunskapsSpel, new()
            {
                "Hej det här rummet är det första",
                "Ooga booga"
            });

            //Create signs
            signs.Add(new Sign(new(500, 500, 100, 100), kunskapsSpel, new()
            {
                "Det här är en skylt",
                "Den gör inte så mycket men är glad att vara med i spelet"
            }));

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

            frontDoor.Draw(gameTime, kunskapsSpel.spriteBatch);

            generalGoofy.Draw(gameTime, kunskapsSpel.spriteBatch);

            foreach (Sign sign in signs)
                sign.Draw(gameTime, kunskapsSpel.spriteBatch);

            //foreach (Rectangle wall in walls)
            //    kunskapsSpel.spriteBatch.Draw(kunskapsSpel.Content.Load<Texture2D>("WallTiles"), wall, wall, Color.White);
        }

        public override void Update(GameTime gameTime, KunskapsSpel kunskapsSpel)
        {
            generalGoofy.Update(gameTime);

            foreach (Sign sign in signs)
                sign.Update(gameTime);

            frontDoor.Update(gameTime);
            
        }

        public override void CreateDoors()
        {
            frontDoor = new Door(new(new(1000, -104), new(128, 104)), frontDoorLeedsTo, kunskapsSpel, false);
        }

        public override void SetDoorLocations()
        {
            frontDoorLocation = frontDoor.hitBox.Location + new Point(0, 40);
        }
    }
}
