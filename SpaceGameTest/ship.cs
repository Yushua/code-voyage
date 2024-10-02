using System;
using System.Collections.Generic;
using SpaceGameRoom;

namespace SpaceGameShip
{
    public class Ship
    {
        public string Name { get; private set; }
        private List<Room> rooms;

        public Room Canteen { get; private set; }
        public Room Kitchen { get; private set; }
        public Room HeadRoom { get; private set; }

        private int positionX;
        private int positionY;

        public Ship(string name, int crewSize)
        {
            Name = name;
            rooms = new List<Room>();

            positionX = 30;
            positionY = 30;

            HeadRoom = new Room("Head Room", 1, 1);
            Kitchen = new Room("Kitchen", 2, 2);
            Canteen = new Room("Canteen", 3, 3);

            rooms.Add(HeadRoom);
            rooms.Add(Kitchen);
            rooms.Add(Canteen);

            for (int i = 3; i < crewSize + 3; i++)
            {
                rooms.Add(new Room($"Crew Room {i - 2}", i, i));
            }
        }

        public Room GetRoom(int index)
        {
            if (index >= 0 && index < rooms.Count)
            {
                return rooms[index];
            }
            throw new ArgumentOutOfRangeException(nameof(index), "Invalid room index");
        }

        public List<Room> GetAllRooms()
        {
            return new List<Room>(rooms);
        }

        public void PrintRoomLocations()
        {
            Console.WriteLine("Room Locations:");
            Console.WriteLine($"{"Canteen",-20}: {Canteen.RoomName} (Room Number: {Canteen.RoomNumber})");
            Console.WriteLine($"{"Head Room",-20}: {HeadRoom.RoomName} (Room Number: {HeadRoom.RoomNumber})");
            Console.WriteLine($"{"Kitchen",-20}: {Kitchen.RoomName} (Room Number: {Kitchen.RoomNumber})");
            Console.WriteLine();
        }

        public int GetPositionX()
        {
            return positionX;
        }

        public int GetPositionY()
        {
            return positionY;
        }

        public void SetPosition(int x, int y)
        {
            positionX = x;
            positionY = y;
        }
    }
}
