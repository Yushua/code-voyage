using System;

namespace SpaceGameRoom
{
    public class Room
    {
        public string RoomName { get; private set; }
        public int Size { get; private set; }
        public int RoomNumber { get; private set; }
        public bool IsOccupied { get; set; }

        public Room(string name, int size, int number)
        {
            RoomName = name;
            Size = size;
            RoomNumber = number;
            IsOccupied = false;
        }
    }
}
