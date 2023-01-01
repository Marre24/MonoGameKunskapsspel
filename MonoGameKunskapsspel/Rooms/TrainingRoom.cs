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
            foreach (Chest chest in chests)
                chest.Draw(gameTime, spriteBatch);

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
            npc = new NPC(floorSegments[0].hitBox.Center - new Point(25, 150), kunskapsSpel, new()
            {
                "Problemet som jag har påverkar inte bara mig utan hela riket.",
                "Det har börjat krylla av vättar som bankar på invånarnas huvud vilket gör dom dummare och dummare för varje dag som går",
                "Detta måste sättas stopp för, vilket jag har försökt men min fragila kropp klarar inte av besegra vättarnaerna. Det gör att du får vara min muskelkraft.",
                "Det du behöver göra är att gå ner i deras lya och ta itu med ledaren av dessa vättar",
                "Men du kommer inte att kunna göra det i ditt nuvarande tillstånd. Dessa vättar har gömt undan nycklarna i kistor liknande den ovanför mig.",
                "För att kunna öppna den så behöver du låsa upp kodlåset. Koden till kodlåset är svaret på en mattematisk flervals fråga, testa på min kista",
                "Du byter kodlåsnummer med hjälp av Vänster och Höger pil, när du är nöjd med svaret så trycker du ENTER för att testa svaret",
            }, kunskapsSpel.animations);

            //Create chest
            chests = new()
            {
                new Chest(floorSegments[0].hitBox.Center - new Point(32, 300), kunskapsSpel, 0),
            };

            foreach (FloorSegment floorSegment in floorSegments)
                components.Add(floorSegment);
            foreach (Chest chest in chests)
                components.Add(chest);
            components.Add(npc);
        }

        public override void SetDoorLocations()
        {
            frontSpawnLocation = frontDoor.hitBox.Location + new Point(48, - 100);
        }

        public override void Update(GameTime gameTime)
        {
            if (chests[0].open)
                npc.dialogue = new()
                {
                    "Bra jobbat",
                    "Men vättarna kommer att använda sig av mycket svårare frågor",
                    "Dessa frågor kommer att vara om derivatan av de trigonometriska funkitionerna sin x och cos x",
                    "När man löser dessa problem så är det väldigt viktigt att vinkeln x är skriven i radianer",
                    "Om vinkeln x är skriven i radianer så ger det två stycken samband  f(x) = sin x ger f'(x) = cos x  och  f(x) = cos x ger f'(x) = - sin x",
                    "",
                    "Jag kan gå igenom hur jag löser en exempeluppgifft",
                    "\"Bestäm f'(π/2) då f(x) = 3 sinx - 2 cosx\",  tänk lite själv innan jag går igenom lösningen",
                    "sinx => cosx och cosx => -sinx  vilket gör att f'(x) = 3 (cosx) - 2 (-sinx)  = 3 cosx + 2 sinx vilket ger  f'(π/2) = 3 cos(π/2) + 2 sin(π/2)  = 3 * 0 + 2 * 1 = 2   Svar: f'(π/2) = 2",
                    "Om det var oklart eller du behöver repetera så är det bara att prata med mig igen så förklarar jag allt detta igen",
                    "Annars så kan du följa sigen i öster till grottan där du får använda dina kunskaper",
                };

            npc.Update(gameTime);
            frontDoor.Update(gameTime);
            foreach (Chest chest in chests)
                chest.Update(gameTime);
        }
    }
}
