using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameKunskapsspel
{
    public class BossFightRoom : Room
    {
        public BossFightRoom(int RoomID, KunskapsSpel kunskapsSpel) : base(RoomID, kunskapsSpel)
        {
        }

        public override void CreateDoors()
        {
            backDoor = new Door(new(floorSegments[1].hitBox.Left, floorSegments[1].hitBox.Bottom - kunskapsSpel.player.hitBox.Height - 20, floorSegments[1].hitBox.Width, kunskapsSpel.player.hitBox.Height),
                back, kunskapsSpel);
            SetDoorLocations();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (FloorSegment floorSegment in floorSegments)
                floorSegment.Draw(gameTime, spriteBatch);
            foreach (Wall wall in walls)
                wall.Draw(gameTime, spriteBatch);
            foreach (SideWall sidewall in sideWalls)
                sidewall.Draw(gameTime, spriteBatch);
            foreach (Enemy enemy in enemies)
                enemy.Draw(gameTime, spriteBatch);
        }

        public override void Initialize()
        {
            //Create floorsegments
            floorSegments = new()
            {
                new FloorSegment(new(16, 8),new(0, 0),kunskapsSpel, "Dungeon"),
                new FloorSegment(new(2, 8),new(7, 8),kunskapsSpel, "Dungeon"),
            };

            //Walls
            walls = new()
            {
                new Wall(16,new(0,0),kunskapsSpel)
            };

            sideWalls = new()
            {
                new SideWall(8, new(0,0), kunskapsSpel, "LeftWall"),
                new SideWall(8, new(16,0), kunskapsSpel, "RightWall"),
            };




            enemies = new()
            {
                new Enemy(kunskapsSpel, floorSegments[0].hitBox.Center + new Point(-200, -300), null, 1),
                new Enemy(kunskapsSpel, floorSegments[0].hitBox.Center + new Point(0,-300), this, 2),
                new Enemy(kunskapsSpel, floorSegments[0].hitBox.Center + new Point(200, -300), null, 3),
            };

            CreateDoors();
            foreach (Enemy enemy in enemies)
                components.Add(enemy);
        }

        public override void SetDoorLocations()
        {
            backSpawnLocation = backDoor.hitBox.Location - new Point(0, 100);
        }

        public override void Update(GameTime gameTime)
        {
            if (kunskapsSpel.player.hitBox.Top <= floorSegments[1].hitBox.Top - 200)
                enemies[1].hasInteracted = false;
            
            foreach (Enemy enemy in enemies)
                enemy.Update(gameTime);
            backDoor.Update(gameTime);
        }
    }
}
