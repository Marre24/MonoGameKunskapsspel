using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameKunskapsspel
{
    public class RoomManager
    {
        public List<Room> rooms = new List<Room>();
        public int activeRoomId;
        public Room GetActiveRoom() { return rooms[activeRoomId];}


        public void Update(GameTime gameTime, KunskapsSpel kunskapsSpel)
        {
            foreach (Room room in rooms)
                if (room.ID == activeRoomId)
                    room.Update(gameTime, kunskapsSpel);
        }

        public void Draw(KunskapsSpel kunskapsSpel, GameTime gameTime)
        {
            foreach (Room room in rooms)
                if (room.ID == activeRoomId)
                    room.Draw(kunskapsSpel, gameTime);
        }


        public void Add(Room room, KunskapsSpel kunskapsSpel)
        {
            rooms.Add(room);
            room.Initialize(kunskapsSpel);
        }

        public virtual void SetActiveRoom(int id) { activeRoomId = id; }
        public virtual void SetActiveRoom(Room room) { activeRoomId = room.ID; }

    }
}
