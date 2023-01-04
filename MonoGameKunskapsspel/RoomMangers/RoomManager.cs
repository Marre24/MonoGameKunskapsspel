using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MonoGameKunskapsspel
{
    public class RoomManager
    {
        public List<Room> rooms = new List<Room>();
        public int activeRoomId;

        public void Update(GameTime gameTime)
        {
            foreach (Room room in rooms)
                if (room.RoomID == activeRoomId)
                    room.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Room room in rooms)
                if (room.RoomID == activeRoomId)
                    room.Draw(gameTime, spriteBatch);
        }

        public void Add(Room room)
        {
            rooms.Add(room);
            room.Initialize();
        }

        public Room GetActiveRoom() { return rooms[activeRoomId]; }
        public virtual void SetActiveRoom(int id) { activeRoomId = id; }
        public virtual void SetActiveRoom(Room room) { activeRoomId = room.RoomID; }

    }
}
