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
        public FirstRoom(int ID, KunskapsSpel kunskapsSpel) : base(ID, kunskapsSpel) {   }


        public override void Initialize()
        {
            //Create Floors
            floorSegments = new List<FloorSegment> { new FloorSegment(new(10, 5), new(0, 0), kunskapsSpel) };

            ////Create NPC
            //generalGoofy = new NPC(new(500, 200, 200, 200), kunskapsSpel, new()
            //{
            //    "Hej det här rummet är det första",
            //    "Ooga booga"
            //});

            ////Create signs
            //signs.Add(new Sign(new(500, 500, 100, 100), kunskapsSpel, new()
            //{
            //    "Det här är en skylt",
            //    "Den gör inte så mycket men är glad att vara med i spelet"
            //}));

            ////Create Walls
            //Point size = new(floorSegments[0].hitBox.Width, 300);
            //walls = new List<Rectangle>()
            //{
            //    new(floorSegments[0].hitBox.Location - new Point(0, size.Y), size)
            //};

            ////Create Enemies
            //mathias = new Mathias(kunskapsSpel, new Point(1300, 300), new MathiasIntroduction(kunskapsSpel.player, kunskapsSpel, this, new List<string>(){
            //    "Hej jag heter Mathias, FEAR ME"
            //}));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (FloorSegment floorSegment in floorSegments)
                floorSegment.Draw(gameTime, spriteBatch);

            //foreach (Sign sign in signs)
            //    sign.Draw(gameTime, spriteBatch);

            //frontDoor.Draw(gameTime, spriteBatch);
            //generalGoofy.Draw(gameTime, spriteBatch);
            //mathias.Draw(gameTime, spriteBatch);


            //foreach (Rectangle wall in walls)
            //    kunskapsSpel.spriteBatch.Draw(kunskapsSpel.Content.Load<Texture2D>("WallTiles"), wall, wall, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            //generalGoofy.Update(gameTime);

            //foreach (Sign sign in signs)
            //    sign.Update(gameTime);

            //frontDoor.Update(gameTime);

            //mathias.Update(gameTime);
        }

        public override void CreateDoors()
        {
            frontDoor = new Door(new(new(1000, -104), new(128, 104)), front, kunskapsSpel);
        }

        public override void SetDoorLocations()
        {
            frontSpawnLocation = frontDoor.hitBox.Location + new Point(0, 40);
        }
    }
}
